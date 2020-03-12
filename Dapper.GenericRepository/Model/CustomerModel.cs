using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.DynamicData;

namespace Dapper.GenericRepository.Model
{
    [TableName("Customer")]
    public class CustomerModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public int contact_number { get; set; }
    }
}