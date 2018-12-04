using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            if (user == null)
            {
                throw new InvalidDataException("Input is null!");
            }
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
            if (user.Addresses == null || user.Addresses.Count < 1)
            {
                throw new InvalidDataException("Cannot add a user without at least one address!");
            }
            if (!user.IsCompany && !string.IsNullOrEmpty(user.TaxNumber))
            {
                throw new InvalidDataException("Cannot add a user with a tax number! Did you mean to add a company instead?");
            }
            if (user.IsCompany && string.IsNullOrEmpty(user.TaxNumber))
            {
                throw new InvalidDataException("Cannot add a company without tax number! Did you mean to add an individual customer instead?");
            }
            return _repo.Create(user);
        }

        public User ApproveUser(int id)
        {
            if (id == 0)
            {
                throw new InvalidDataException("Cannot approve user without ID!");
            }
            if (id < 0)
            {
                throw new InvalidDataException("No User with negative ID exists!");
            }

            User approveUser = _repo.ReadByID(id);

            if (approveUser != null)
            {
                if (approveUser.IsApproved)
                {
                    throw new InvalidDataException("User is already approved!");
                }

                else
                {
                    approveUser.IsApproved = true;
                }
            }
            else
            {
                throw new InvalidDataException("There is no user with the ID of " + id + " in the database!");
            }
            return _repo.Update(approveUser);
        }

        public User DeleteUser(int id)
        {
            if (id < 0)
            {
                throw new InvalidDataException("No User with negative ID exists!");
            }

            return _repo.Delete(id);
        }

        public List<User> GetAllUsers()
        {
            return _repo.ReadAll().ToList();
        }

        public User GetUserByID(int id)
        {
            if (id < 0)
            {
                throw new InvalidDataException("No User with negative ID exists!");
            }

            return _repo.ReadByID(id);

        }

        public User UpdateUser(User user)
        {
            if (user == null)
            {
                throw new InvalidDataException("Input is null!");
            }

            return _repo.Update(user);
        }

        public User UpdateUserPassword(User user)
        {
            throw new NotImplementedException();
        }
    }
}
