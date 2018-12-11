using SQLite;
using Xamarin.Forms;
using XrnCourse.BucketList.Domain.Models;
using XrnCourse.BucketList.Domain.Services.Abstract;

namespace XrnCourse.BucketList.Domain.Services.SQLite
{
    /// <summary>
    /// Base class for ALL "SQLite" implementations of a service
    /// </summary>
    public abstract class SQLiteServiceBase
    {
        protected readonly SQLiteConnection connection;

        public SQLiteServiceBase()
        {
            //get the platform-specific SQLiteConnection
            var connectionFactory = DependencyService.Get<ISQLiteConnectionFactory>();
            connection = connectionFactory.CreateConnection("bucketdata.db3");

            connection.DropTable<User>();
            connection.DropTable<AppSettings>();
            connection.DropTable<Bucket>();
            connection.DropTable<BucketItem>();

            //create tables (if not existing)
            connection.CreateTable<User>();
            connection.CreateTable<AppSettings>();
            connection.CreateTable<Bucket>();
            connection.CreateTable<BucketItem>();
        }
    }
}
