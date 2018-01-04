using System;
using System.Data;
using System.Data.SqlClient;

namespace Project13_Exp5_CallSP
{
    class Program
    {
        static void Main(string[] args)
        {
            // Создаем соединение
            SqlConnection conn = new SqlConnection(@"Data Source=(local)\SQLEXPRESS; Integrated Security = true; database = DistribComp");
            try
            {
                // Открываем соединение
                conn.Open();
                // Создаем команду
                SqlCommand cmd = conn.CreateCommand();
                // Определяем хранимую процедуру для исполнения
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_select_active_users";
                // Выполняем команду
                SqlDataReader rdr = cmd.ExecuteReader();
                // Обрабатываем результирующий набор данных
                while (rdr.Read())
                {
                    Console.WriteLine(
                       "{0}"
                     , rdr[0].ToString()
                     //, rdr[1].ToString()
                    );
                }
                // Закрываем читателя
                rdr.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                // Закрываем соединение
                conn.Close();
            }
            Console.ReadLine();
        }
    }
}
