using System;
using System.Linq;
using PCConfigurator.Data;

namespace PCConfigurator.Services
{
    public class AuthService
    {
        private readonly PCComponentsContext _context;

        public AuthService(PCComponentsContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public User Authenticate(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return null;

            try
            {
                var user = _context.Users
                    .FirstOrDefault(u => u.Username == username && u.Password == password);

                // Если поле аватарки не заполнено, задаём значение по умолчанию.
                if (user != null && string.IsNullOrEmpty(user.AvatarPath))
                {
                    user.AvatarPath = "default-avatar.png";
                }

                return user;
            }
            catch
            {
                return null;
            }
        }

        public bool Register(string username, string password, string email)
        {
            if (_context.Users.Any(u => u.Username == username))
                return false;

            var newUser = new User
            {
                Username = username,
                Password = password, // Не забывайте хешировать в реальном приложении!
                Email = email,
                IsAdmin = false
                // AvatarPath не устанавливаем – благодаря значению по умолчанию в модели он будет равен "default-avatar.png"
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();
            return true;
        }
    }
}
