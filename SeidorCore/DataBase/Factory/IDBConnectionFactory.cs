using System.Data.Common;

namespace BaseCore.DataBase.Factory
{
    public interface IDBConnectionFactory
    {
        DbConnection GetConnection();
    }
}