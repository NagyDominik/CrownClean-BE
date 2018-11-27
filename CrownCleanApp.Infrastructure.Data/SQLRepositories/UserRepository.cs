using CrownCleanApp.Core.DomainService;
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

        public IEnumerable<User> ReadAll()
        {
            return _ctx.Users;
        }

        public User ReadByID(int id)
        {
            return _ctx.Users.FirstOrDefault(u => u.ID == id);
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
