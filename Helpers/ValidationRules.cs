using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace PCConfigurator
{
    public class UsernameValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var username = value as string;
            if (string.IsNullOrWhiteSpace(username))
                return new ValidationResult(false, "Имя пользователя не должно быть пустым.");

            if (username.Length < 3 || username.Length > 50)
                return new ValidationResult(false, "Имя пользователя должно содержать от 3 до 50 символов.");

            return ValidationResult.ValidResult;
        }
    }

    public class PasswordValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var password = value as string;
            if (string.IsNullOrWhiteSpace(password))
                return new ValidationResult(false, "Пароль не должен быть пустым.");

            if (password.Length < 6)
                return new ValidationResult(false, "Пароль должен содержать минимум 6 символов.");

            if (!Regex.IsMatch(password, @"[A-Z]"))
                return new ValidationResult(false, "Пароль должен содержать хотя бы одну заглавную букву.");

            if (!Regex.IsMatch(password, @"[0-9]"))
                return new ValidationResult(false, "Пароль должен содержать хотя бы одну цифру.");

            return ValidationResult.ValidResult;
        }
    }

    public class EmailValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var email = value as string;
            if (string.IsNullOrWhiteSpace(email))
                return new ValidationResult(false, "Email не должен быть пустым.");

            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                return new ValidationResult(false, "Введите корректный Email.");

            return ValidationResult.ValidResult;
        }
    }
}
