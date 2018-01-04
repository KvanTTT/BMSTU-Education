using System;
using System.Data;
using System.Data.SqlClient;

namespace Project13_Exp10_WriteXML
{
    class Program
    {
        static void Main(string[] args)
        {
            // Создаем строку соединения
            string connString = @"server = .\sqlexpress;integrated security = true;database = DistribComp";
            // Создаем строку запроса
            string qry = @"select UserName, Team, Project, RegisterDate, AllScore from tblUsers";
            // Создаем соединение
            SqlConnection conn = new SqlConnection(connString);
            try
            {
                // Создаем адаптер данных
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand(qry, conn);
                // Открываем соединение
                conn.Open();
                // Создаем и наполняем таблицу 'products' набора данных
                DataSet ds = new DataSet();
                da.Fill(ds, "tblUsers");
                // Извлекаем данные из таблицы 'products' набора данных и пишем в XML-файл
                ds.WriteXml(@"..\..\..\Data\User13_10.xml");
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