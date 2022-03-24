using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ConsoleEventLog
{
    class Program
    {
        static void Main(string[] args)
        {
            Connection();
            Console.ReadKey();

        }


        private static void Connection()
        {
          var  con = new SqlConnection("Server=DESKTOP-PU7I8L5;database=northwnd;trusted_connection=true");
          try
          {
              var cmd = new SqlCommand();
              con.Open();
              cmd.Connection = con;
              cmd.CommandText = "Select count(*) from Orders";
              var count = (int)cmd.ExecuteScalar();

              Console.WriteLine(count);
          }
          catch (Exception e)
          {
              var logger = new Logger();
              logger.Log(e);
          }
          finally
          {
              if (con.State == ConnectionState.Open)
                con.Close();
          }


        }

    }
}
