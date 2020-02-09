namespace PandaApp.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Models;

    public interface IUsersService
    {
        void CreateUser(string username, string email, string password);

        string GetUserId(string username, string password);

        string GetUsername(string userId);

        IEnumerable<string> GetAllUsernames();
        
        bool IsUsernameUsed(string username);

        bool IsEmailUsed(string email);
    }
}