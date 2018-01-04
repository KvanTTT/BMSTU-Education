using System;
using System.Data;
using System.Data.SqlClient;

namespace Project13_Exp3_CmdRead
{
    class Program
    {
        static void Main(string[] args)
        {
            // Создаем соединение
            SqlConnection conn = new SqlConnection(@"Data Source=(local)\SQLEXPRESS; Integrated Security = true; database = DistribComp");
            // Создаем команду
            string sql = @"select UserName, Project from tblUsers";
            SqlCommand cmd = new SqlCommand(sql, conn);
            Console.WriteLine("Command created and connected.");
            try
            {
                // Открываем соединение
                conn.Open();
                // Выполняем запрос
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Console.WriteLine("User Name and Project: {0} {1}",
                       rdr.GetValue(0),
                       rdr.GetValue(1)
                    );
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                // Закрываем соединение
                conn.Close();
                Console.WriteLine("Connection closed.");
            }
            Console.ReadLine();
        }
    }
}
