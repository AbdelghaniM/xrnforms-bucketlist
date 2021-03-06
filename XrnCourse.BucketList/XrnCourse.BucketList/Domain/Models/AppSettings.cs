﻿using System;
using SQLite;

namespace XrnCourse.BucketList.Domain.Models
{
    public class AppSettings
    {
        [PrimaryKey]
        public Guid CurrentUserId { get; set; }
        public bool EnableListSharing { get; set; }
        public bool EnableNotifications { get; set; }
    }
}
