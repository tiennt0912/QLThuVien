using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Microsoft.Data.SqlClient;

namespace AmateurProject.Db
{
    public static class DBHelper
    {
        private static readonly string sqlConnectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=QLThuVien;Integrated Security=True";
        public static SqlConnection GetSqlConnection() {
            return new SqlConnection(sqlConnectionString);
        }
    }
}
