using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Dapper.GenericRepository.Helpers
{
    public class DbServiceUtility<T> : IDbServiceUtility<T> where T : class
    {
        private string _conString = ConfigurationManager.ConnectionStrings["conString"].ToString();
        private NpgsqlConnection _con;
        public string tableName { get => this.GetTableName(); set => throw new NotImplementedException(); }

        public bool Close()
        {
            _con.Close();
            return true;
        }

        public bool Connect()
        {
            _con = new NpgsqlConnection(_conString);
            _con.Open();
            if (_con.State==System.Data.ConnectionState.Open)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public string GetTableName()
        {
            CustomAttributeData customAttribute = typeof(T).CustomAttributes.FirstOrDefault();
            var args = customAttribute.ConstructorArguments.FirstOrDefault().Value.ToString();
            return args;
        }

        public bool IUD(string sql, T o)
        {
            bool flag = false;
            int result = _con.Execute(sql, o);
            if (result>0)
            {
                flag = true;

            }
            else
            {
                return false;
            }
            return flag;
        }

        public List<T> Query(string sql, object param = null)
        {
            return _con.Query<T>(sql, param).ToList();
        }
    }
}