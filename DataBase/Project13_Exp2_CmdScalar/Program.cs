using System;
using System.Data;
using System.Data.SqlClient;

namespace Project13_Exp2_CmdScalar
{
    class Program
    {
        static void Main()
        {
            // Создаем соединение
            SqlConnection conn = new SqlConnection(@"Data Source=(local)\SQLEXPRESS; Integrated Security = true; database = DistribComp");
            // Создаем команду (как с текстом, так и с соединением)
            string sql = @"Select count(*) from tblUsers";
            SqlCommand cmd = new SqlCommand(sql, conn);
            Console.WriteLine("Command created.");
            try
            {
                // ОТкрываем соединение
                conn.Open();
                // Выполняем запрос (замечаем, что запрос скалярный)
                Console.WriteLine("Number of Users: {0}", cmd.ExecuteScalar());
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                conn.Close();
                Console.WriteLine("Connection closed.");
            }
            Console.ReadLine();
        }
    }
}