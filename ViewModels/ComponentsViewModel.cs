﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using PCConfigurator.Data;
using Specification = PCConfigurator.Data.Specification;

namespace PCConfigurator
{
    public class ComponentsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private List<ComponentEntity> _allComponents = new List<ComponentEntity>();
        private ComponentEntity _selectedComponent;
        private string _searchText;
        private string _selectedCategory;
        private decimal? _minPrice;
        private decimal? _maxPrice;
        private string _selectedPriceSort;

        public ObservableCollection<ComponentEntity> FilteredComponents { get; }
            = new ObservableCollection<ComponentEntity>();
        public ObservableCollection<string> Categories { get; }
            = new ObservableCollection<string>();
        public ObservableCollection<string> PriceSortOptions { get; }
            = new ObservableCollection<string> { "Без сортировки", "По возрастанию", "По убыванию" };

        public ComponentEntity SelectedComponent
        {
            get => _selectedComponent;
            set { _selectedComponent = value; OnPropertyChanged(); }
        }

        public string SearchText
        {
            get => _searchText;
            set { _searchText = value; OnPropertyChanged(); ApplyFilters(); }
        }

        public string SelectedCategory
        {
            get => _selectedCategory;
            set { _selectedCategory = value; OnPropertyChanged(); ApplyFilters(); }
        }

        public string SelectedPriceSort
        {
            get => _selectedPriceSort;
            set { _selectedPriceSort = value; OnPropertyChanged(); ApplyFilters(); }
        }

        public decimal? MinPrice
        {
            get => _minPrice;
            set { _minPrice = value; OnPropertyChanged(); ApplyFilters(); }
        }

        public decimal? MaxPrice
        {
            get => _maxPrice;
            set { _maxPrice = value; OnPropertyChanged(); ApplyFilters(); }
        }

        public ICommand ApplyFiltersCommand { get; }
        public ICommand ResetFiltersCommand { get; }
        public ICommand AddComponentCommand { get; }
        public ICommand EditComponentCommand { get; }
        public ICommand DeleteComponentCommand { get; }

        public ComponentsViewModel()
        {
            LoadComponents();
            InitializeCategories();
            SelectedPriceSort = "Без сортировки";

            ApplyFiltersCommand = new RelayCommand(ApplyFilters);
            ResetFiltersCommand = new RelayCommand(ResetFilters);
            AddComponentCommand = new RelayCommand(AddComponent);
            EditComponentCommand = new RelayCommand(EditComponent, CanEditDelete);
            DeleteComponentCommand = new RelayCommand(DeleteComponent, CanEditDelete);
        }

        private bool CanEditDelete() => SelectedComponent != null;

        private void LoadComponents()
        {
            try
            {
                using (var context = new PCComponentsContext())
                {
                    var comps = context.Components
                        .Include(c => c.Category)
                        .Include(c => c.Specifications)
                        .ToList();
                    _allComponents.Clear();
                    _allComponents.AddRange(comps);

                    FilteredComponents.Clear();
                    foreach (var component in _allComponents)
                    {
                        FilteredComponents.Add(component);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки компонентов: {ex.Message}",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void InitializeCategories()
        {
            Categories.Clear();
            Categories.Add("Все");
            Categories.Add("Процессор");
            Categories.Add("Видеокарта");
            Categories.Add("Оперативная память");
            Categories.Add("Материнская плата");
            Categories.Add("Блок питания");
            Categories.Add("Накопитель");
            Categories.Add("Кулер");
            Categories.Add("Корпус");

            SelectedCategory = "Все";
        }

        private void ApplyFilters()
        {
            FilteredComponents.Clear();
            var query = _allComponents.AsEnumerable();

            if (!string.IsNullOrEmpty(SearchText))
                query = query.Where(c => c.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrEmpty(SelectedCategory) && SelectedCategory != "Все")
                query = query.Where(c => c.Category != null && c.Category.CategoryName == SelectedCategory);
            if (MinPrice.HasValue)
                query = query.Where(c => c.Price >= MinPrice.Value);
            if (MaxPrice.HasValue)
                query = query.Where(c => c.Price <= MaxPrice.Value);

            // Apply price sorting
            switch (SelectedPriceSort)
            {
                case "По возрастанию":
                    query = query.OrderBy(c => c.Price);
                    break;
                case "По убыванию":
                    query = query.OrderByDescending(c => c.Price);
                    break;
                default:
                    query = query.OrderBy(c => c.Name);
                    break;
            }

            foreach (var component in query)
                FilteredComponents.Add(component);
        }

        private void ResetFilters()
        {
            SearchText = string.Empty;
            SelectedCategory = "Все";
            MinPrice = null;
            MaxPrice = null;
            SelectedPriceSort = "Без сортировки";
            ApplyFilters();
        }

        private void AddComponent()
        {
            var dialog = new ComponentEditWindow();
            if (dialog.ShowDialog() == true)
            {
                using (var context = new PCComponentsContext())
                {
                    var categoryEntity = context.Categories
                        .FirstOrDefault(c => c.CategoryName == dialog.Component.Category)
                        ?? new PCConfigurator.Data.Category { CategoryName = dialog.Component.Category };

                    if (categoryEntity.CategoryID == 0)
                    {
                        context.Categories.Add(categoryEntity);
                        context.SaveChanges();
                    }

                    var newComponent = new ComponentEntity
                    {
                        Name = dialog.Component.Name,
                        Price = dialog.Component.Price,
                        ImagePath = dialog.Component.ImagePath,
                        CategoryID = categoryEntity.CategoryID,
                        Stock = dialog.Component.Stock
                    };

                    foreach (var spec in dialog.Component.Specifications)
                    {
                        newComponent.Specifications.Add(new PCConfigurator.Data.Specification { SpecText = spec });
                    }

                    context.Components.Add(newComponent);
                    context.SaveChanges();
                    _allComponents.Add(newComponent);
                }
                ApplyFilters();
            }
        }

        private void EditComponent()
        {
            if (SelectedComponent == null) return;

            var dialog = new ComponentEditWindow(SelectedComponent);
            if (dialog.ShowDialog() == true)
            {
                try
                {
                    using (var context = new PCComponentsContext())
                    {
                        var compToUpdate = context.Components
                            .Include(c => c.Specifications)
                            .FirstOrDefault(c => c.ComponentID == SelectedComponent.ComponentID);
                        if (compToUpdate != null)
                        {
                            compToUpdate.Name = dialog.Component.Name;
                            compToUpdate.Price = dialog.Component.Price;
                            compToUpdate.ImagePath = dialog.Component.ImagePath;
                            compToUpdate.Stock = dialog.Component.Stock;

                            var categoryEntity = context.Categories
                                .FirstOrDefault(c => c.CategoryName == dialog.Component.Category)
                                ?? new PCConfigurator.Data.Category { CategoryName = dialog.Component.Category };

                            if (categoryEntity.CategoryID == 0)
                            {
                                context.Categories.Add(categoryEntity);
                                context.SaveChanges();
                            }
                            compToUpdate.CategoryID = categoryEntity.CategoryID;

                            compToUpdate.Specifications.Clear();
                            foreach (var spec in dialog.Component.Specifications)
                            {
                                compToUpdate.Specifications.Add(new PCConfigurator.Data.Specification { SpecText = spec });
                            }
                            context.SaveChanges();

                            int index = _allComponents.FindIndex(c => c.ComponentID == SelectedComponent.ComponentID);
                            if (index >= 0)
                                _allComponents[index] = compToUpdate;
                        }
                    }
                    ApplyFilters();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка обновления компонента: {ex.Message}",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void DeleteComponent()
        {
            if (SelectedComponent == null) return;

            if (MessageBox.Show($"Удалить компонент: {SelectedComponent.Name}?",
                "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    using (var context = new PCComponentsContext())
                    {
                        var compToDelete = context.Components
                            .Include(c => c.Specifications)
                            .FirstOrDefault(c => c.ComponentID == SelectedComponent.ComponentID);

                        if (compToDelete != null)
                        {
                            context.Specifications.RemoveRange(compToDelete.Specifications);
                            context.Components.Remove(compToDelete);
                            context.SaveChanges();
                        }
                    }

                    _allComponents.Remove(SelectedComponent);
                    SelectedComponent = null;
                    ApplyFilters();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка удаления компонента: {ex.Message}",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}