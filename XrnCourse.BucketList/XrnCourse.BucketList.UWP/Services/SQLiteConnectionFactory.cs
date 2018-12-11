using System.IO;
using Windows.Storage;
using SQLite;
using Xamarin.Forms;
using XrnCourse.BucketList.Domain.Services.Abstract;

[assembly: Dependency(typeof(XrnCourse.BucketList.UWP.Services.SQLiteConnectionFactory))]

namespace XrnCourse.BucketList.UWP.Services
{
    internal class SQLiteConnectionFactory : ISQLiteConnectionFactory
    {
        public SQLiteConnection CreateConnection(string databaseFileName)
        {
            string path = ApplicationData.Current.LocalFolder.Path;
            path = Path.Combine(path, databaseFileName);

            return new SQLiteConnection(
                path,
                SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite,
                storeDateTimeAsTicks: false
            );
        }
    }
}