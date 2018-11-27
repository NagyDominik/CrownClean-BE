using CrownCleanApp.Core.DomainService;
using CrownCleanApp.Core.Entity;
using System;
using System.Collections.Generic;
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

        public User Create()
        {
            throw new NotImplementedException();
        }

        public User Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> ReadAll()
        {
            throw new NotImplementedException();
        }

        public User ReadByID(int id)
        {
            throw new NotImplementedException();
        }

        public User Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}
