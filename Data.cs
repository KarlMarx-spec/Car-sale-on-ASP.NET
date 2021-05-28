using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace ASP_KURSACH
{
    public class Data
    {
        //Прописываем путь к БД
        static String connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\79520\Desktop\курсачи\вафин курсовая\9. Аня (Бронь столиков в клубе)\Проект Remoting\BD.accdb";

        //Метод записи брони в расписание
        public static void Zapis(string Name, string Fam, string Otch, string Phone, string Email)
        {
            try
            {
                List<String> number = new List<String>();
                OleDbCommand cmd;
                string abc;

                OleDbConnection con = new OleDbConnection(connectionString);

                using (con)
                {
                    con.Open();

                    abc = "INSERT INTO People VALUES('" + Name + "','" + Fam + "','" + Otch + "','" + Phone + "','" + Email + "')";

                    cmd = new OleDbCommand(abc, con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Упс, что-то пошло не так:\n " + e.Message);
            }
        }
    }
}