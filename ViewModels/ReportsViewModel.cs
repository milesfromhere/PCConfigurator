using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using PCConfigurator.Data;
using Microsoft.Win32;
using System.IO;
using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;

namespace PCConfigurator.ViewModels
{
    public class ReportsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        public ObservableCollection<ReportType> ReportTypes { get; set; } = new ObservableCollection<ReportType>();
        public ObservableCollection<ReportDataItem> ReportData { get; set; } = new ObservableCollection<ReportDataItem>();

        private ReportType _selectedReportType;
        public ReportType SelectedReportType
        {
            get => _selectedReportType;
            set
            {
                _selectedReportType = value;
                OnPropertyChanged(nameof(SelectedReportType));
            }
        }

        private DateTime? _startDate;
        public DateTime? StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }

        private DateTime? _endDate;
        public DateTime? EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }

        public ICommand GenerateCommand { get; }
        public ICommand ExportCommand { get; }
        public ICommand PrintCommand { get; }

        public ReportsViewModel()
        {
            ReportTypes.Add(new ReportType { Id = 1, Name = "Отчет по продажам" });
            ReportTypes.Add(new ReportType { Id = 2, Name = "Отчет по компонентам" });
            ReportTypes.Add(new ReportType { Id = 3, Name = "Отчет по заказам" });

            SelectedReportType = ReportTypes[0];
            StartDate = DateTime.Today.AddMonths(-1);
            EndDate = DateTime.Today;

            GenerateCommand = new RelayCommand(GenerateReport);
            ExportCommand = new RelayCommand(ExportToExcel);
            PrintCommand = new RelayCommand(PrintReport);
        }

        private void GenerateReport()
        {
            if (!StartDate.HasValue || !EndDate.HasValue)
            {
                MessageBox.Show("Выберите период для отчета", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            ReportData.Clear();
            try
            {
                using (var context = new PCComponentsContext())
                {
                    switch (SelectedReportType.Id)
                    {
                        case 1: // Отчет по продажам
                            var salesData = context.OrderItems
                                .Include(oi => oi.Order)
                                .Include(oi => oi.Component)
                                .Where(oi => oi.Order.CreatedDate >= StartDate.Value && oi.Order.CreatedDate <= EndDate.Value)
                                .GroupBy(oi => new { oi.Order.CreatedDate.Date, oi.Component.Name })
                                .Select(g => new ReportDataItem
                                {
                                    Date = g.Key.Date,
                                    ComponentName = g.Key.Name,
                                    Quantity = g.Sum(oi => oi.Quantity),
                                    Amount = g.Sum(oi => oi.Price * oi.Quantity),
                                    Status = "Продано"
                                })
                                .OrderBy(r => r.Date)
                                .ToList();

                            foreach (var item in salesData)
                                ReportData.Add(item);
                            break;

                        case 2: // Отчет по компонентам
                            var componentsData = context.Components
                                .Select(c => new ReportDataItem
                                {
                                    Date = DateTime.Today,
                                    ComponentName = c.Name,
                                    Quantity = c.Stock,
                                    Amount = c.Price * c.Stock,
                                    Status = c.Stock > 0 ? "В наличии" : "Нет в наличии"
                                })
                                .OrderBy(r => r.ComponentName)
                                .ToList();

                            foreach (var item in componentsData)
                                ReportData.Add(item);
                            break;

                        case 3: // Отчет по заказам
                            var ordersData = context.Orders
                                .Where(o => o.CreatedDate >= StartDate.Value && o.CreatedDate <= EndDate.Value)
                                .Select(o => new ReportDataItem
                                {
                                    Date = o.CreatedDate,
                                    ComponentName = "Заказ #" + o.OrderId,
                                    Quantity = 1,
                                    Amount = o.TotalPrice,
                                    Status = o.Status.ToString()
                                })
                                .OrderByDescending(r => r.Date)
                                .ToList();

                            foreach (var item in ordersData)
                                ReportData.Add(item);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка генерации отчета: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExportToExcel()
        {
            if (!ReportData.Any())
            {
                MessageBox.Show("Нет данных для экспорта", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx",
                FileName = $"Отчет_{SelectedReportType.Name}_{DateTime.Now:yyyy-MM-dd}"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Отчет");

                        // Заголовки
                        worksheet.Cell(1, 1).Value = "Дата";
                        worksheet.Cell(1, 2).Value = "Компонент";
                        worksheet.Cell(1, 3).Value = "Количество";
                        worksheet.Cell(1, 4).Value = "Сумма";
                        worksheet.Cell(1, 5).Value = "Статус";

                        // Данные
                        int row = 2;
                        foreach (var item in ReportData)
                        {
                            worksheet.Cell(row, 1).Value = item.Date;
                            worksheet.Cell(row, 2).Value = item.ComponentName;
                            worksheet.Cell(row, 3).Value = item.Quantity;
                            worksheet.Cell(row, 4).Value = item.Amount;
                            worksheet.Cell(row, 5).Value = item.Status;
                            row++;
                        }

                        // Форматирование
                        var range = worksheet.Range(1, 1, row - 1, 5);
                        range.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        range.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Columns().AdjustToContents();

                        workbook.SaveAs(saveFileDialog.FileName);
                    }

                    MessageBox.Show("Отчет успешно экспортирован!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при экспорте: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void PrintReport()
        {
            if (!ReportData.Any())
            {
                MessageBox.Show("Нет данных для печати", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // TODO: Реализовать печать отчета
            MessageBox.Show("Функция печати будет реализована в следующем обновлении", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

    public class ReportType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ReportDataItem
    {
        public DateTime Date { get; set; }
        public string ComponentName { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
    }
}
