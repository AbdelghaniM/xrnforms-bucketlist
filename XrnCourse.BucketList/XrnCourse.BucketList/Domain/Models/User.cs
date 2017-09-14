using System;
using System.Collections.Generic;

namespace XrnCourse.BucketList.Domain.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<Bucket> OwnedBuckets { get; set; }
    }
}
