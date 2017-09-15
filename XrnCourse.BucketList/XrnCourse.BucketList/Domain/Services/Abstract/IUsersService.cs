using System;
using System.Threading.Tasks;
using XrnCourse.BucketList.Domain.Models;

namespace XrnCourse.BucketList.Domain.Services.Abstract
{
    public interface IUsersService
    {
        Task<User> GetUserById(Guid id);
        Task SaveUser(User user);
    }
}