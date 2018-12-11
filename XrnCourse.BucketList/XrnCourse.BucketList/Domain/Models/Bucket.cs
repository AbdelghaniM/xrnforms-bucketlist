using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using SQLiteNetExtensions.Attributes;

namespace XrnCourse.BucketList.Domain.Models
{
    public class Bucket
    {
        [PrimaryKey]
        public Guid Id { get; set; }

        [ForeignKey(typeof(User))]
        public Guid OwnerId { get; set; }

        [ManyToOne(nameof(OwnerId), CascadeOperations = CascadeOperation.CascadeRead)]
        public User Owner { get; set; }

        [NotNull, MaxLength(50)]
        public string Title { get; set; }

        [NotNull, MaxLength(50)]
        public string Description { get; set; }

        public string ImageUrl { get; set; }
        public bool IsFavorite { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<BucketItem> Items { get; set; }

        [Ignore]
        public bool IsCompleted => Items.All(i => i.CompletionDate.HasValue);

        [Ignore]
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
