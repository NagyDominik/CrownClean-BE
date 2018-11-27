using System;
using System.Collections.Generic;
using System.Text;
using CrownCleanApp.Core.DomainService;
using CrownCleanApp.Core.Entity;

namespace CrownCleanApp.Core.ApplicationService.Services
{
    public class UserService : IUserService
    {
        readonly IUserRepository _repo;

        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }

        public User AddUser(User user)
        {
            throw new NotImplementedException();
        }

        public User ApproveUser(User user)
        {
            throw new NotImplementedException();
        }

        public User DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public User GetUserByID(int id)
        {
            throw new NotImplementedException();
        }

        public User UpdateUser(User user)
        {
            throw new NotImplementedException();
        }

        public User UpdateUserPassword(User user)
        {
            throw new NotImplementedException();
        }
    }
}
