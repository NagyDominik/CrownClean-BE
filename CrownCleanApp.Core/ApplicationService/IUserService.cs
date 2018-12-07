using CrownCleanApp.Core.DomainService.Filtering;
using CrownCleanApp.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrownCleanApp.Core.ApplicationService
{
    public interface IUserService
    {
        /// <summary>
        /// Add a user to the database.
        /// </summary>
        /// <param name="user">The user object that will be added to the database.</param>
        /// <returns>The user object that was added to the database.</returns>
        User AddUser(User user);

        /// <summary>
        /// Return all the user objects located in the database.
        /// </summary>
        /// <returns>All the user objects located in the database.</returns>
        FilteredList<User> GetAllUsers(Filter filter);

        /// <summary>
        /// Fetch a user based on the ID.
        /// </summary>
        /// <param name="id">The ID of the user that will be returned.</param>
        /// <returns>The user with the specified ID if exists, null otherwise.</returns>
        User GetUserByID(int id);

        /// <summary>
        /// Update an existing user in the database.
        /// </summary>
        /// <param name="user">The user that will be updated.</param>
        /// <returns>The updated user.</returns>
        User UpdateUser(User user);

        /// <summary>
        /// Update the password of the user.
        /// </summary>
        /// <param name="user">The user whose password will be updated.</param>
        /// <returns>The updated user.</returns>
        User UpdateUserPassword(User user);

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <returns></returns>
        User ApproveUser(int id);

        /// <summary>
        /// Delete a user from the database based on ID.
        /// </summary>
        /// <param name="id">The ID of the user that will be deleted.</param>
        /// <returns>The deleted user.</returns>
        User DeleteUser(int id);
    }
}
