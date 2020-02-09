namespace PandaApp.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using Models;
    using PandaApp;

    public class UsersService : IUsersService
    {
        private readonly ApplicationDbContext db;

        public UsersService(ApplicationDbContext db)
        {
            this.db = db;
        }
        
        public void CreateUser(string username, string email, string password)
        {
            var user = new User
            {
                Email = email,
                Username = username,
                Password = this.Hash(password)
            };

            this.db.Users.Add(user);
            this.db.SaveChanges();
        }

        public string GetUserId(string username, string password)
        {
            var passwordHash = this.Hash(password);
            return this.db.Users
                .Where(x => x.Username == username && x.Password == passwordHash)
                .Select(x => x.Id)
                .FirstOrDefault();
        }

        public string GetUsername(string userId)
        {
            return this.db.Users
                .Where(x => x.Id == userId)
                .Select(x => x.Username)
                .FirstOrDefault();
        }

        public IEnumerable<string> GetAllUsernames()
        {
            return this.db.Users.Select(u => u.Username).ToList();
        }


        public bool IsEmailUsed(string email)
        {
            return this.db.Users.Any(x => x.Email == email);
        }

        public bool IsUsernameUsed(string username)
        {
            return this.db.Users.Any(x => x.Username == username);
        }

        private string Hash(string input)
        {
            var crypt = new SHA256Managed();
            var hash = new StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(input));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }

            return hash.ToString();
        }
    }
}
