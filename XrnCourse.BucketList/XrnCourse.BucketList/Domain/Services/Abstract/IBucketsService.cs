using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XrnCourse.BucketList.Domain.Models;

namespace XrnCourse.BucketList.Domain.Services.Abstract
{
    public interface IBucketsService
    {
        Task DeleteBucketList(Guid bucketId);
        Task<Bucket> GetBucketList(Guid bucketId);
        Task<IEnumerable<Bucket>> GetBucketListsForUser(Guid userid);
        Task SaveBucketList(Bucket bucket);
    }
}