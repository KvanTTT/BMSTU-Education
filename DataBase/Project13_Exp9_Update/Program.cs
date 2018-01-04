using System;
using System.Data;
using System.Data.SqlClient;

namespace Project13_Exp9_Update
{
    class Program
    {
        static void Main(string[] args)
        {
            // Создаем строку соединения
            string connString = @"
            server = .\sqlexpress;
            integrated security = true;
            database = DistribComp
         ";

            // Создаем  строку запроса на выборк
            string qry = @"
            select
               *
            from
               tblUsers
            where
               Country = 'INDONESIA'
         ";

            // Создаем строку запроса на обновление
            string upd = @"
            update tblUsers   
            set
               Team = @Team
            where
               UserID = @UserID
         ";

            // Создаем соединение
            SqlConnection conn = new SqlConnection(connString);

            try
            {
                // Создаем адаптер данных
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand(qry, conn);

                // Создаем и наполняем таблицу 'employees' набора данных
                DataSet ds = new DataSet();
                da.Fill(ds, "tblUsers");

                // Получаем ссылку на таблицу 'employees'
                DataTable dt = ds.Tables["tblUsers"];

                // Изменяем поле 'city' первой записи
                dt.Rows[3]["Team"] = "Elite";

                // Распечатываем строки измененной таблицы 'employees' в наборе данных 
                foreach (DataRow row in dt.Rows)
                {
                    Console.WriteLine(
                       "{0} {1} {2}",
                       row["UserName"].ToString().PadRight(15),
                       row["Country"].ToString().PadLeft(25),
                       row["Team"]);
                }

                // Обновляем таблицу 'Employees' БД
                //
                // Создаем команду обновления для текущего соединения
                SqlCommand cmd = new SqlCommand(upd, conn);
                //
                // Отображаем (мапируем) параметры
                //
                // 'City'
                cmd.Parameters.Add(
                   "@Team",
                   SqlDbType.NVarChar,
                   15,
                   "Team");
                //
                // 'EmployeeID'
                SqlParameter parm =
                   cmd.Parameters.Add(
                      "@UserID",
                      SqlDbType.Int,
                      4,
                      "UserID");
                parm.SourceVersion = DataRowVersion.Original;
                //
                // Адаптер 'da' выполняет команду обновления, обнаруженную в свойстве 'UpdateCommand', 
                // поскольку
                da.UpdateCommand = cmd;
                da.Update(ds, "tblUsers");
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