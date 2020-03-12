using Dapper.GenericRepository.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace Dapper.GenericRepository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {


        private IDbServiceUtility<T> _dbServiceUtility;
        public GenericRepository()
        {
            _dbServiceUtility = new DbServiceUtility<T>();
        }
        public bool Delete(int id)
        {
            _dbServiceUtility.Connect();
            var emp = GetById(id);
            var sql = $"delete from {_dbServiceUtility.tableName} where id = @{id}";
            bool flag = _dbServiceUtility.IUD(sql, emp);
            return flag;

        }

        public List<T> GetAll()
        {
            _dbServiceUtility.Connect();
            var sql = $"Select * from {_dbServiceUtility.GetTableName()}";
            var data = _dbServiceUtility.Query(sql);
            _dbServiceUtility.Close();
            return data;
        }

        public T GetById(int id)
        {
            _dbServiceUtility.Connect();
            var sql = $"Select * from {_dbServiceUtility.GetTableName()} Where id = @id";
            var data = _dbServiceUtility.Query(sql, new { id }).FirstOrDefault();
            _dbServiceUtility.Close();
            return data;
        }

        public bool Insert(T o)
        {
            var sb = new StringBuilder($"INSERT INTO {_dbServiceUtility.GetTableName()} ");
            sb.Append("(");

            var properties = GetParameterName(o);
       
       
            properties.ForEach(prop => { sb.Append($"{prop},"); });

            sb
                .Remove(sb.Length - 1, 1)
                .Append(") VALUES (");

            properties.ForEach(prop => { sb.Append($"@{prop},"); });

            sb
                .Remove(sb.Length - 1, 1)
                .Append(")");

            var insertQuery = sb.ToString();
            _dbServiceUtility.Connect();
            bool flag = _dbServiceUtility.IUD(insertQuery, o);
            _dbServiceUtility.Close();
            return flag;

        }

        public bool Update(T o, int id)
        {


            var sb = new StringBuilder($"Update {_dbServiceUtility.GetTableName()} SET ");

            var properties = GetParameterName(o);
            properties.ForEach(prop =>
            {
                if (!prop.Equals("id"))
                {
                    sb.Append($"{prop}=@{prop},");
                }
            });
            sb.Remove(sb.Length - 1, 1); //remove last comma
            sb.Append($" WHERE id ={id}");
            var updateQuery = sb.ToString();
            _dbServiceUtility.Connect();
            bool flag = _dbServiceUtility.IUD(updateQuery, o);
            _dbServiceUtility.Close();
            return flag;

        }
        private List<string> GetParameterName(T o)
        {
            List<string> lst = new List<string>();
            foreach (PropertyInfo prop in o.GetType().GetProperties())
            {
                if (!(prop.Name == "id" || prop.Name.StartsWith("id")))
                {
                    lst.Add(prop.Name);

                }

            }
            return lst;


        }

    }
}


