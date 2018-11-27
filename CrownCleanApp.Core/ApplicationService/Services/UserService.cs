using System;
using System.Collections.Generic;
using System.IO;
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
            if (user.ID != 0)
            {
                throw new InvalidDataException("Cannot add user with existing ID!");
            }
            if (string.IsNullOrEmpty(user.FirstName))
            {
                throw new InvalidDataException("Cannot add a user without first name!");
            }
            if (string.IsNullOrEmpty(user.LastName))
            {
                throw new InvalidDataException("Cannot add a user without last name!");
            }
            if (string.IsNullOrEmpty(user.PhoneNumber))
            {
                throw new InvalidDataException("Cannot add a user without a phone number!");
            }
            if (string.IsNullOrEmpty(user.Email))
            {
                throw new InvalidDataException("Cannot add a user without an email address!");
            }
            if (user.IsCompany && !string.IsNullOrEmpty(user.TaxNumber))
            {
                throw new InvalidDataException("Cannot add a user with a tax number! Did you mean to add a company instead?");
            }
            if (!user.IsCompany && string.IsNullOrEmpty(user.TaxNumber))
            {
                throw new InvalidDataException("Cannot add a company without tax number! Did you mean to add an individual customer instead?");
            }

            return _repo.Create(user);

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
