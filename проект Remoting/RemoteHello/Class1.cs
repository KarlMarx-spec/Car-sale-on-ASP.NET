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
    public class Hello : MarshalByRefObject, Iremote
    {
        public Hello()
        {
            Console.WriteLine("Объект Hello создан");
        }
        ~Hello()
        {
            Console.WriteLine("Объект Hello уничтожен");
        }

        //Прописываем путь к БД
        String connectString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\79520\Desktop\курсачи\вафин курсовая\Автосалон\Проект Remoting\BD.accdb";
        public void conn()
        {
            OleDbConnection myConnection = new OleDbConnection(connectString);
            string query = "SELECT * FROM Cars ORDER BY Population DESC";

            myConnection.Open();

            // создаем объект OleDbCommand для выполнения запроса к БД MS Access
            OleDbCommand command = new OleDbCommand(query, myConnection);
            // открываем соединение с БД

            // получаем объект OleDbDataReader для чтения табличного результата запроса SELECT
            OleDbDataReader reader = command.ExecuteReader();
            List<string> mylist = new List<string>();
            while (reader.Read())
            {
                mylist.Add((string)reader[0]);
            }
            // закрываем OleDbDataReader
            reader.Close();
        }
        
        public void Zapis(int Num, string Name, string Fam, string Otch, string Phone, string Email, string price)
        {
            try
            {
                List<String> number = new List<String>();
                OleDbCommand cmd;
                string abc;

                OleDbConnection con = new OleDbConnection(connectString);

                using (con)
                {
                    con.Open();
                    int pr1 = int.Parse(price);
                    price = pr1.ToString();
                    abc = "SELECT Model FROM Cars WHERE Price='" + price + "'";
                    cmd = new OleDbCommand(abc, con);
                    OleDbDataReader oread = cmd.ExecuteReader();
                    oread.Read();
                    string res = oread[0].ToString();
                    abc = "INSERT INTO People VALUES('" + Num.ToString() + "','" + Fam + "','" + Name + "','" + Otch + "','" + Phone + "','" + Email + "','" + res + "')";
                    
                    cmd = new OleDbCommand(abc, con);
                    cmd.ExecuteNonQuery();
                    abc = "UPDATE Cars SET Population = Population + 1 WHERE Price='" + price + "'";
                    cmd = new OleDbCommand(abc, con);
                    cmd.ExecuteNonQuery();
                    //abc = "UPDATE Cars SET Population = +1 WHERE Price='" + price + "'";
                    //cmd = new OleDbCommand(abc, con);
                    
                }
                con.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Упс, что-то пошло не так:\n " + e.Message);
            }
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
