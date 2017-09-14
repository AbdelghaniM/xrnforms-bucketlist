using System;
using System.Collections.Generic;
using System.Linq;

namespace XrnCourse.BucketList.Domain.Models
{
    public class Bucket
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public User Owner { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public bool IsFavorite { get; set; }
        public List<BucketItem> Items { get; set; }
        public bool IsCompleted => Items.All(i => i.CompletionDate.HasValue);
        public float PercentCompleted
        {
            get
            {
                if (Items?.Count > 0)
                    return (float)Items.Count(i => i.CompletionDate.HasValue) / (float)Items.Count;
                else
                    return 0f;
            }
        }
    }
}
