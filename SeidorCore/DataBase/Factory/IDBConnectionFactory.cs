using System.Data.Common;

namespace SeidorCore.DataBase.Factory
{
    public interface IDBConnectionFactory
    {
        DbConnection GetConnection();
    }
}