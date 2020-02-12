namespace MusacaApp.Services
{
    using System.Collections.Generic;
    using ViewModels.Order;

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