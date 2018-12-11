using SQLite;

namespace XrnCourse.BucketList.Domain.Services.Abstract
{
    public interface ISQLiteConnectionFactory
    {
        SQLiteConnection CreateConnection(string databaseFileName);
    }
}
