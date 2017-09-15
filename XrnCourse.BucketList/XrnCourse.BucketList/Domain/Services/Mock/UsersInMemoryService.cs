using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XrnCourse.BucketList.Domain.Models;
using XrnCourse.BucketList.Domain.Services.Abstract;

namespace XrnCourse.BucketList.Domain.Services.Mock
{
    public class UsersInMemoryService : IUsersService
    {
        private static List<User> users = new List<User>
        {
            new User{
                Id = Guid.Empty,
                UserName = "Siegfried",
                Email="siegfried@bucketlists.test",
            }
        };

        public async Task<User> GetUserById(Guid id)
        {
            await Task.Delay(Constants.Mocking.FakeDelay);
            return users.FirstOrDefault(u => u.Id == id);
        }

        public async Task SaveUser(User user)
        {
            var oldUser = await GetUserById(user.Id);
            oldUser = user;
        }
    }
}
