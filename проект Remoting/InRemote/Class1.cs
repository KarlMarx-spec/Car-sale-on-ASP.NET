using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Runtime.Remoting.Lifetime;
using InRemote;

namespace InRemote
{
    public interface Iremote
    {
        //void RegUsers(string FIO, string Mob, string Password);
        void Zapis(int Num, string Name, string Fam, string Otch, string Phone, string Email, string price);
        Object GetLifetimeService();
        Object InitializeLifetimeService();
    }
}