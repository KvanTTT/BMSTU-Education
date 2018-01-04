using System;
using System.Data;
using System.Data.SqlClient;

namespace Project13_Exp6_CallSP2
{
    class Program
    {
        static void Main(string[] args)
        {
            // Создаем соединение
            SqlConnection conn = new SqlConnection(@"server = .\sqlexpress;integrated security = true;database = DistribComp");
            try
            {
                // Открываем соединение
                conn.Open();
                // Создаем команду
                SqlCommand cmd = conn.CreateCommand();
                // Определяем хранимую процедуру для исполнения
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_add_user";
                // Создаем входной параметр
                SqlParameter inparm = cmd.Parameters.Add("@Name ", SqlDbType.VarChar, 60);
                inparm.Direction = ParameterDirection.Input;
                inparm.Value = "Asdf";
                SqlParameter ouparm = cmd.Parameters.Add("@Project", SqlDbType.VarChar, 30);
                ouparm.Direction = ParameterDirection.Input;
                ouparm.Value = "rosetta@home";
                SqlParameter retval = cmd.Parameters.Add("@RegisterDate", SqlDbType.DateTime);
                retval.Direction = ParameterDirection.Input;
                retval.Value = "2009-12-13";
                SqlDataReader rdr = cmd.ExecuteReader();
                /*// Обрабатываем результирующий набор данных
                while (rdr.Read())
                {
                    Console.WriteLine(
                       "{0} {1}"
                     , rdr[0].ToString().PadRight(5)
                     , rdr[1].ToString()
                    );
                }
                rdr.Close();
                // Печатаем значение выходного параметра
                Console.WriteLine("Значение выходного параметра равно {0}", cmd.Parameters["@ordercount"].Value);
                // Печатаем возвращаемое значение
                Console.WriteLine("Возвращаемое значение равно {0}", cmd.Parameters["return_value"].Value);*/
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
