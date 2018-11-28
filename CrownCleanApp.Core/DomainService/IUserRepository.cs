using CrownCleanApp.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrownCleanApp.Core.DomainService
{
    public interface IUserRepository
    {
        /// <summary>
        /// Add a user to the database.
        /// </summary>
        /// <param name="user">The user that will be saved to the database.</param>
        /// <returns>The saved User object.</returns>
        User Create(User user);

        /// <summary>
        /// Retrieve the list of users stored in the database.
        /// </summary>
        /// <returns>The list of users stored in the database.</returns>
        IEnumerable<User> ReadAll();

        /// <summary>
        /// Return an individual user based on the ID.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <returns>The User with the supplied ID if exists, null otherwise.</returns>
        User ReadByID(int id);

        /// <summary>
        /// Update an existing user in the database.
        /// </summary>
        /// <param name="user">The user that will be updated.</param>
        /// <returns>The updated user.</returns>
        User Update(User user);

        /// <summary>
        /// Delete a user from the database based on ID.
        /// </summary>
        /// <param name="id">The ID of the user that will be deleted.</param>
        /// <returns>The deleted user if existed, null otherwise.</returns>
        User Delete(int id);
    }
}
