using System;
using System.Diagnostics;
using System.Threading.Tasks;
using XrnCourse.BucketList.Domain.Models;
using XrnCourse.BucketList.Domain.Services.Abstract;

namespace XrnCourse.BucketList.Domain.Services.SQLite
{
    public class UsersSQLiteService : SQLiteServiceBase, IUsersService
    {
        private async Task EnsureDefaultUser()
        {
            await Task.Run(async () =>
            {
                try
                {
                    //make sure we always have 1 user with Guid.Empty (= local user)
                    await SaveUser(
                        new User
                        {
                            Id = Guid.Empty,
                            UserName = "Default User",
                            Email = "",
                        });
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
            });
        }

        public async Task<User> GetUserById(Guid id)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    await EnsureDefaultUser();
                    return connection.Find<User>(id);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
            });
        }

        public async Task SaveUser(User user)
        {
            await Task.Run(() =>
            {
                try
                {
                    connection.InsertOrReplace(user);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
            });
        }
    }
}
