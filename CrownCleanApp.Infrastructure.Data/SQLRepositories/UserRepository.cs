using CrownCleanApp.Core.DomainService;
using CrownCleanApp.Core.DomainService.Filtering;
using CrownCleanApp.Core.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CrownCleanApp.Infrastructure.Data.SQLRepositories
{
    public class UserRepository : IUserRepository
    {
        readonly CrownCleanAppContext _ctx;

        public UserRepository(CrownCleanAppContext ctx)
        {
            _ctx = ctx;
        }

        public User Create(User user)
        {
            User addedUser = _ctx.Users.Add(user).Entity;
            _ctx.SaveChanges();
            return addedUser;
        }

        public User Delete(int id)
        {
            User deletedUser = _ctx.Users.Remove(new User { ID = id }).Entity;
            _ctx.SaveChanges();
            return deletedUser;
        }

        public FilteredList<User> ReadAll(UserFilter filter)
        {
            FilteredList<User> filteredList = new FilteredList<User>();

            // If there is a filter, use it to return the correct number of users
            if (filter != null)
            {
                #region Filtering

                filteredList.List = _ctx.Users;

                if (!string.IsNullOrEmpty(filter.Name))
                {
                    filteredList.List = filteredList.List.Where(u => u.FirstName.Contains(filter.Name) || u.LastName.Contains(filter.Name) || (u.FirstName + " " + u.LastName).Contains(filter.Name));
                }

                if (!string.IsNullOrEmpty(filter.Email))
                {
                    filteredList.List = filteredList.List.Where(u => u.Email.Contains(filter.Email));
                }

                if (filter.FilterToCustomerType)
                {
                    if (filter.IsCompany)
                    {
                        filteredList.List = filteredList.List.Where(u => u.IsCompany);
                    }
                    else
                    {
                        filteredList.List = filteredList.List.Where(u => !u.IsCompany);
                    }
                }

                #endregion

                #region Pagination

                if (filter.CurrentPage > 0 && filter.ItemsPerPage > 0)
                {
                    filteredList.List = filteredList.List
                    .Skip((filter.CurrentPage - 1) * filter.ItemsPerPage)
                    .Take(filter.ItemsPerPage);
                }

                filteredList.Count = _ctx.Users.Count();

                #endregion
            }
            else
            {
                filteredList.List = _ctx.Users;
                filteredList.Count = filteredList.List.Count();
            }

            return filteredList;
        }

        public User ReadByID(int id)
        {
            return _ctx.Users.Include(u => u.Orders).Include(u => u.Vehicles).AsNoTracking().FirstOrDefault(u => u.ID == id);
        }

        public User Update(User user)
        {
            _ctx.Attach(user).State = EntityState.Modified;
            //_ctx.Entry(user).Reference(u => u.Orders).IsModified = true;
            _ctx.SaveChanges();
            return user;
        }

    }
}
