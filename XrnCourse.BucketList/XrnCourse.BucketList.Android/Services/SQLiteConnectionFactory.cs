using System;
using System.IO;
using SQLite;
using Xamarin.Forms;
using XrnCourse.BucketList.Domain.Services.Abstract;

[assembly: Dependency(typeof(XrnCourse.BucketList.Droid.Services.SQLiteConnectionFactory))]

namespace XrnCourse.BucketList.Droid.Services
{
    internal class SQLiteConnectionFactory : ISQLiteConnectionFactory
    {
        public SQLiteConnection CreateConnection(string databaseFileName)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            path = Path.Combine(path, databaseFileName);

            return new SQLiteConnection(path,
                SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite,
                storeDateTimeAsTicks: false
            );
        }
    }
}