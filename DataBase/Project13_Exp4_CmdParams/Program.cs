using System;
using System.Data;
using System.Data.SqlClient;

namespace Project13_Exp4_CmdParams
{
    class Program
    {
        static void Main(string[] args)
        {
            // Устанавливаем значения параметрического запроса
            string Name = "KvanTTT";
            string Project = "rosetta@home";
            DateTime RegisterDate = DateTime.Now;
            // Создаем соединение
            SqlConnection conn = new SqlConnection(@"Data Source=(local)\SQLEXPRESS; Integrated Security = true; database = DistribComp");
            // Определяем скалярный запрос на выборку
            string sqlqry = @"select count(*) from tblUsers";
            // Определяем параметрический запрос на вставку
            string sqlins = @"
                DECLARE @max_id INT
	            SELECT @max_id = Max(UserID)
	            FROM tblUsers
	            INSERT tblUsers (UserID, UserName, Team, Country, Project, RegisterDate, AvgScorePerDay, AllScore)
	            VALUES(@max_id + 1, @Name, NULL, NULL, @Project, @RegisterDate, 0, 0)";
            // Определяем параметрический запрос на удаление
            string sqldel = @"delete from tblUsers where UserName = @Name and Project = @Project and RegisterDate = @RegisterDate";
            // Создаем две команды
            SqlCommand cmdqry = new SqlCommand(sqlqry, conn);
            SqlCommand cmdnon = new SqlCommand(sqlins, conn);
            // Добавляем параметры к команде на вставку
            cmdnon.Parameters.Add("@Name", SqlDbType.NVarChar, 60);
            cmdnon.Parameters.Add("@Project", SqlDbType.NVarChar, 30);
            cmdnon.Parameters.Add("@RegisterDate", SqlDbType.DateTime);
            try
            {
                // Открываем соединение
                conn.Open();
                // Выполняем запрос на выборку, чтобы получить количество сотрудников
                Console.WriteLine("Before INSERT: Number of users {0}\n", cmdqry.ExecuteScalar());
                // Выполняем запрос на вставку
                cmdnon.Parameters["@Name"].Value = Name;
                cmdnon.Parameters["@Project"].Value = Project;
                cmdnon.Parameters["@RegisterDate"].Value = RegisterDate;
                Console.WriteLine("Query execute {0}", cmdnon.CommandText);
                cmdnon.ExecuteNonQuery();
                Console.WriteLine("Afetr INSERT: Users count {0}\n", cmdqry.ExecuteScalar());
                // Выполняем запорос на удаление
                cmdnon.CommandText = sqldel;
                Console.WriteLine("Выполняется запрос {0}", cmdnon.CommandText);
                cmdnon.ExecuteNonQuery();
                Console.WriteLine("After DELETE: Users count {0}\n", cmdqry.ExecuteScalar());
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                // Закрываем соединение
                conn.Close();
                Console.WriteLine("Connection Closed.");
            }
            Console.ReadLine();
        }
    }
}
