using System;
using System.Data;
using System.Data.SqlClient;

namespace Project13_Exp7_PopDataSet
{
    class Program
    {
        static void Main(string[] args)
        {
            // Определяем строку соединения
            string connString = @"server = .\sqlexpress;integrated security = true;database = DistribComp";
            // Определяем запрос
            string sql = @"
            select UserName, Project, AllScore
            from tblUsers
            where Country = 'Russian federation'
            ";
            // Создаем соединение
            SqlConnection conn = new SqlConnection(connString);
            try
            {
                // ОТкрываем соединение
                conn.Open();
                // Создаем адаптер данных
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                // Создаем объект "ds" типа "DataSet"
                DataSet ds = new DataSet();
                // Наполняем объект "ds"
                da.Fill(ds, "tblUsers");
                // Получаем таблицу данных
                DataTable dt = ds.Tables["tblUsers"];
                // Распечатываем содержимое таблтцы
                foreach (DataRow row in dt.Rows)
                {
                    foreach (DataColumn col in dt.Columns)
                        Console.WriteLine(row[col]);
                    Console.WriteLine("".PadLeft(20, '-'));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
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
