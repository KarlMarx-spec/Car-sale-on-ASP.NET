using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Runtime.Remoting.Lifetime;
using InRemote;


namespace RemoteHello
{
    public class GoodBy : MarshalByRefObject, Iremote1
    {
        public GoodBy()
        {
            Console.WriteLine("Объект GoodBy создан");
        }
        ~GoodBy()
        {
            Console.WriteLine("Объект GoodBy уничтожен");
        }

        //Прописываем путь к БД
        String connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\79520\Desktop\курсачи\вафин курсовая\Автосалон\Проект Remoting\BD.accdb";

        //Проверка наличия пользователя в БД
        public bool LogIn(string Mob, string Password)
        {
            OleDbConnection con = new OleDbConnection(connectionString);
            List<String> mob = new List<String>();
            List<String> pas = new List<String>();

            OleDbCommand cmd;
            OleDbDataReader reader;

            bool abc1 = false;
            bool abc2 = false;
            bool abc = false;

            using (con)
            {
                con.Open();

                cmd = new OleDbCommand("SELECT Mob FROM Users", con);
                reader = cmd.ExecuteReader();
                while (reader.Read()) mob.Add(reader.GetString(0));
                foreach (String m in mob) if (Mob == m) abc1 = true;

                cmd = new OleDbCommand("SELECT Password FROM Users", con);
                reader = cmd.ExecuteReader();
                while (reader.Read()) pas.Add(reader.GetString(0));
                foreach (String p in pas) if (Password == p) abc2 = true;

                if (abc1 == true && abc2 == true) abc = true;

                con.Close();
            }

            return abc;
        }

        //Считывание имени пользователя
        public string UserNameRead(string Mob)
        {
            string zapros = "SELECT FIO FROM Users WHERE Mob = '" + Mob + "'";
            string U = "";

            OleDbConnection con = new OleDbConnection(connectionString);
            List<String> str = new List<String>();

            OleDbCommand cmd;
            OleDbDataReader reader;

            using (con)
            {
                con.Open();

                cmd = new OleDbCommand(zapros, con);
                reader = cmd.ExecuteReader();

                while (reader.Read()) str.Add(reader.GetString(0));
                foreach (String s in str)
                {
                    U = s;
                }

                con.Close();
            }

            return U;
        }

        //Заполнение цен на столики
        public string DeskPriceRead(string Num)
        {
            string zapros = "SELECT Price FROM Price WHERE Num = '" + Num + "'";
            string U = "";

            OleDbConnection con = new OleDbConnection(connectionString);
            List<String> str = new List<String>();

            OleDbCommand cmd;
            OleDbDataReader reader;

            using (con)
            {
                con.Open();

                cmd = new OleDbCommand(zapros, con);
                reader = cmd.ExecuteReader();

                while (reader.Read()) str.Add(reader.GetString(0));
                foreach (String s in str)
                {
                    U = s;
                }

                con.Close();
            }

            return U;
        }

        //Проверка на занятость столика в выбранную дату
        public bool[] DeskDateRead(string Date)
        {
            bool[] U = new bool[] { true, true, true, true, true, true, true, true, true, true, true };
            string c = "";

            OleDbConnection con = new OleDbConnection(connectionString);
            List<string> str = new List<string>();
            OleDbCommand cmd;

            using (con)
            {
                con.Open();

                for (int i = 0; i < U.Length; i++)
                {
                    c = "SELECT COUNT(*) FROM Rasp WHERE Dat = '" + Date + "' AND Num = '" + (i + 1) + "'";
                    cmd = new OleDbCommand(c, con);
                    int rowcount = (int)cmd.ExecuteScalar();
                    if (rowcount > 0) U[i] = false;
                }

                con.Close();
            }

            return U;
        }

        

        public override Object InitializeLifetimeService()
        {
            ILease lease = (ILease)base.InitializeLifetimeService();
            if (lease.CurrentState == LeaseState.Initial)
            {
                lease.InitialLeaseTime = TimeSpan.FromSeconds(3);
                lease.SponsorshipTimeout = TimeSpan.FromSeconds(10);
                lease.RenewOnCallTime = TimeSpan.FromSeconds(2);
            }
            return lease;
        }
    }
}