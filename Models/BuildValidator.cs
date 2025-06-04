using System;
using System.Linq;
using PCConfigurator.Data;

namespace PCConfigurator.Helpers
{
    public static class BuildValidator
    {
        /// <summary>
        /// Проверяет, совместимы ли процессор и материнская плата по сокету.
        /// Если хотя бы у одного из компонентов отсутствует строка, начинающаяся с "Сокет:",
        /// функция считает их несовместимыми.
        /// </summary>
        public static bool IsCompatible(ComponentEntity cpu, ComponentEntity motherboard)
        {
            if (cpu == null || motherboard == null)
                return true; // Если хотя бы один компонент не выбран, пропускаем проверку

            // Ищем строку, начинающуюся с "Socket" или "Сокет" (с/без двоеточия)
            string cpuSpec = cpu.SpecificationsText.FirstOrDefault(s =>
                s.TrimStart().StartsWith("Socket", StringComparison.OrdinalIgnoreCase) ||
                s.TrimStart().StartsWith("Сокет", StringComparison.OrdinalIgnoreCase));
            string mbSpec = motherboard.SpecificationsText.FirstOrDefault(s =>
                s.TrimStart().StartsWith("Socket", StringComparison.OrdinalIgnoreCase) ||
                s.TrimStart().StartsWith("Сокет", StringComparison.OrdinalIgnoreCase));

            // Если информация отсутствует хотя бы у одного из компонентов – считаем их совместимыми (не блокируем сохранение)
            if (string.IsNullOrWhiteSpace(cpuSpec) || string.IsNullOrWhiteSpace(mbSpec))
                return true;

            // Извлекаем значение сокета (после пробела или двоеточия)
            string cpuSocket = ExtractSocketValue(cpuSpec);
            string mbSocket = ExtractSocketValue(mbSpec);

            return !string.IsNullOrEmpty(cpuSocket) && cpuSocket.Equals(mbSocket, StringComparison.OrdinalIgnoreCase);
        }

        private static string ExtractSocketValue(string spec)
        {
            if (string.IsNullOrWhiteSpace(spec)) return null;
            var parts = spec.Split(new[] { ':', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length < 2) return null;
            // Если есть двоеточие, значение после него, иначе после первого слова
            return parts[1].Trim();
        }
    }
}
