using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.GenericRepository.Helpers
{
    public interface IDbServiceUtility<T> where T:class
    {

        bool Connect();
        bool Close();
        bool IUD(string sql, T o);
        List<T> Query(string sql, Object param = null);
        string tableName { get; set; }
        string GetTableName();



    }
}
