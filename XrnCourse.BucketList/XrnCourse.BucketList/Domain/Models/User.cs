using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;

namespace XrnCourse.BucketList.Domain.Models
{
    public class User
    {
        [PrimaryKey]
        public Guid Id { get; set; }

        public string UserName { get; set; }
        public string Email { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Bucket> OwnedBuckets { get; set; }
    }
}
