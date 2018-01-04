using System;
using System.Data;
using System.Data.SqlClient;

namespace Project13_Exp8_FilterSort
{
    class Program
    {
        static void Main(string[] args)
        {
            // connection string
            string connString = @"server = .\sqlexpress;integrated security = true;database = DistribComp";
            // query 1
            string sql1 = @"select * from tblUsers ";
            // query 2
            string sql2 = @"select * from tblPayments where PaymentCredit > 290";
            // combine queries
            string sql = sql1 + sql2;
            // create connection
            SqlConnection conn = new SqlConnection(connString);
            try
            {
                // create data adapter
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand(sql, conn);
                // create and fill data set
                DataSet ds = new DataSet();
                da.Fill(ds, "tblUsers");
                // get the data tables collection
                DataTableCollection dtc = ds.Tables;
                // display data from first data table
                //
                // display output header
                Console.WriteLine("Результаты из таблицы 'tblUsers':");
                Console.WriteLine("UserName".PadRight(23) + "AvgScorePerDay".PadLeft(23) + "\n");
                // Устанавливаем фильтр
                string fl = "Country = 'Germany'";
                // Определяем порядок сортировки
                string srt = "AvgScorePerDay asc";
                // display filtered and sorted data
                foreach (DataRow row in dtc["tblUsers"].Select(fl, srt))
                {
                    Console.WriteLine("{0}\t{1}", row["UserName"].ToString().PadRight(25), row["AvgScorePerDay"]);
                }
                // Распечатываем содержимое второй таблицы
                //
                // Печатаем заголовок
                Console.WriteLine("\n----------------------------");
                Console.WriteLine("Результаты из таблицы 'tblPayments':");
                Console.WriteLine("PaymentDate".PadRight(20) + "ЦPaymentCredit".PadLeft(28) + "\n");
                // display data
                foreach (DataRow row in dtc[1].Rows)
                {
                    Console.WriteLine("{0}\t{1}", row["PaymentDate"].ToString().PadRight(25), row["PaymentCredit"]);
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