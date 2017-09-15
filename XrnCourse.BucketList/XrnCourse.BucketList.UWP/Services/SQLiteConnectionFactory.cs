using SQLite.Net;
using SQLite.Net.Interop;
using SQLite.Net.Platform.WinRT;
using System.IO;
using Windows.Storage;
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
                new SQLitePlatformWinRT(),
                path,
                SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite,
                storeDateTimeAsTicks: false
            );
        }
    }
}