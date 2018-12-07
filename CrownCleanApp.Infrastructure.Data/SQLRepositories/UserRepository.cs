﻿using CrownCleanApp.Core.DomainService;
using CrownCleanApp.Core.DomainService.Filtering;
using CrownCleanApp.Core.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public FilteredList<User> ReadAll(Filter filter)
        {
            var filteredList = new FilteredList<User>();

            // If there is a filter, use it to return the correct number of users
            if (filter != null && filter.ItemsPerPage > 0 && filter.CurrentPage > 0)
            {
                filteredList.List = _ctx.Users
                    .Skip((filter.CurrentPage - 1) * filter.ItemsPerPage)
                    .Take(filter.ItemsPerPage);
                filteredList.Count = _ctx.Users.Count();
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
