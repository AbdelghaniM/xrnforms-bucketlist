using SQLite;
using SQLiteNetExtensions.Attributes;
using System;

namespace XrnCourse.BucketList.Domain.Models
{
    public class BucketItem
    {
        [PrimaryKey]
        public Guid Id { get; set; }

        [NotNull, MaxLength(50)]
        public string ItemDescription { get; set; }

        public int Order { get; set; }
        public DateTime? CompletionDate { get; set; }

        [ForeignKey(typeof(Bucket))]
        public Guid BucketId { get; set; }

        [ManyToOne(nameof(BucketId))]
        public Bucket Bucket { get; set; }
    }
}
