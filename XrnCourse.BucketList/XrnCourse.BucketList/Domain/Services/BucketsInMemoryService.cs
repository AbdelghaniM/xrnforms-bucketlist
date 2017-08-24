using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using XrnCourse.BucketList.Domain.Models;

namespace XrnCourse.BucketList.Domain.Services
{
    public class BucketsInMemoryService
    {
        private static List<Bucket> bucketLists = new List<Bucket>
        {
            new Bucket{
                Id = Guid.NewGuid(),
                OwnerId = Guid.Empty, //the first user
                Title = "Siegfried's first bucket list",
                Description = "A simple bucket list",
                ImageUrl = null, IsFavorite = true,
                Items = new List<BucketItem>
                {
                    new BucketItem{
                        Id = Guid.NewGuid(), ItemDescription="Make a world trip",
                        Order = 1
                    },
                    new BucketItem{
                        Id = Guid.NewGuid(), ItemDescription="Learn Xamarin",
                        CompletionDate = DateTime.Now,
                        Order = 2
                    },
                    new BucketItem{
                        Id = Guid.NewGuid(), ItemDescription="Publish my first mobile app",
                        Order = 3
                    }
                }
            }
        };

        public async Task<IEnumerable<Bucket>> GetBucketListsForUser(Guid userid)
        {
            await Task.Delay(Constants.Mocking.FakeDelay);
            return bucketLists.Where(b => b.OwnerId == userid);
        }

        public async Task<Bucket> GetBucketList(Guid bucketId)
        {
            await Task.Delay(Constants.Mocking.FakeDelay);
            return bucketLists.FirstOrDefault(b => b.Id == bucketId);
        }

        public async Task SaveBucketList(Bucket bucket)
        {
            await Task.Delay(Constants.Mocking.FakeDelay);
            var savedbucket = bucketLists.FirstOrDefault(b => b.Id == bucket.Id);
            if (savedbucket == null) //this is a new bucket
            {
                savedbucket = bucket;
                savedbucket.Id = Guid.NewGuid();
                bucketLists.Add(savedbucket);
            }
            savedbucket.Title = bucket.Title;
            savedbucket.Description = bucket.Description;
            savedbucket.ImageUrl = bucket.ImageUrl;
            savedbucket.IsFavorite = bucket.IsFavorite;
            savedbucket.OwnerId = bucket.OwnerId;
            savedbucket.Items = bucket.Items;
        }

        public async Task DeleteBucketList(Guid bucketId)
        {
            await Task.Delay(Constants.Mocking.FakeDelay);
            var bucket = bucketLists.FirstOrDefault(b => b.Id == bucketId);
            bucketLists.Remove(bucket);
        }
    }
}
