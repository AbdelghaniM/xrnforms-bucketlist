using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using XrnCourse.BucketList.Domain.Models;

namespace XrnCourse.BucketList.Domain.Services
{
    public class BucketsInMemoryService
    {
        private static List<Bucket> bucketLists;
        private static List<Bucket> BucketLists
        {
            get
            {
                if(bucketLists == null)
                    bucketLists = InitializeBucketList();
                return bucketLists;
            }
        }

        private static List<Bucket> InitializeBucketList() {
            var buckets = new List<Bucket>
            {
                new Bucket{
                    Id = Guid.NewGuid(),
                    OwnerId = Guid.Empty, //the first user
                    Title = "Siegfried's first bucket list",
                    Description = "A simple bucket list",
                    ImageUrl = null, IsFavorite = true,
                    
                }
            };

            //items for first bucket
            buckets[0].Items = new List<BucketItem>
            {
                new BucketItem{
                    Id = Guid.NewGuid(), ItemDescription="Make a world trip",
                    Order = 1,
                    Bucket = buckets[0], BucketId = buckets[0].Id
                },
                new BucketItem{
                    Id = Guid.NewGuid(), ItemDescription="Learn Xamarin",
                    Order = 2, CompletionDate = DateTime.Now,
                    Bucket = buckets[0], BucketId = buckets[0].Id
                },
                new BucketItem{
                    Id = Guid.NewGuid(), ItemDescription="Publish my first mobile app",
                    Order = 3,
                    Bucket = buckets[0], BucketId = buckets[0].Id
                }
            };
            return buckets;
        }

        public async Task<IEnumerable<Bucket>> GetBucketListsForUser(Guid userid)
        {
            await Task.Delay(Constants.Mocking.FakeDelay);
            return BucketLists.Where(b => b.OwnerId == userid);
        }

        public async Task<Bucket> GetBucketList(Guid bucketId)
        {
            await Task.Delay(Constants.Mocking.FakeDelay);
            return BucketLists.FirstOrDefault(b => b.Id == bucketId);
        }

        public async Task SaveBucketList(Bucket bucket)
        {
            await Task.Delay(Constants.Mocking.FakeDelay);
            var savedbucket = BucketLists.FirstOrDefault(b => b.Id == bucket.Id);
            if (savedbucket == null) //this is a new bucket
            {
                savedbucket = bucket;
                savedbucket.Id = Guid.NewGuid();
                BucketLists.Add(savedbucket);
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
            var bucket = BucketLists.FirstOrDefault(b => b.Id == bucketId);
            BucketLists.Remove(bucket);
        }

    }
}
