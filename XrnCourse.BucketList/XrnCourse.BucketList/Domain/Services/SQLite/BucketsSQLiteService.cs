using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using XrnCourse.BucketList.Domain.Models;
using XrnCourse.BucketList.Domain.Services.Abstract;

namespace XrnCourse.BucketList.Domain.Services.SQLite
{
    public class BucketsSQLiteService : SQLiteServiceBase, IBucketsService
    {
        public async Task DeleteBucketList(Guid bucketId)
        {
            await Task.Run(() =>
            {
                try
                {
                    connection.Delete<Bucket>(bucketId);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
            });
        }

        public async Task<Bucket> GetBucketList(Guid bucketId)
        {
            return await Task.Run<Bucket>(() =>
            {
                try
                {
                    Bucket bucket = connection.Table<Bucket>().Where(e => e.Id == bucketId).FirstOrDefault();
                    if (bucket != null)
                        connection.GetChildren<Bucket>(bucket, true);
                    return bucket;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
            });
        }

        public async Task<IEnumerable<Bucket>> GetBucketListsForUser(Guid userid)
        {
            return await Task.Run<IEnumerable<Bucket>>(() =>
            {
                try
                {
                    var buckets = connection.GetAllWithChildren<Bucket>(b => b.OwnerId == userid, false).ToList();
                    return buckets;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
            });
        }

        public async Task SaveBucketList(Bucket bucket)
        {
            await Task.Run(() =>
            {
                try
                {
                    connection.InsertOrReplaceWithChildren(bucket);
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
