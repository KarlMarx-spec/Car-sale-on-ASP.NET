using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Lifetime;
using InRemote;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using RemoteHello;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace CarSale
{
    public partial class Catalog : System.Web.UI.Page
    {
        static public Iremote helloObj;//объект доступа к первому удаленному объекту через интерфейс
        static public Iremote1 goodByObj;
        static public ILease leaseHi, leaseBy;
        public string price;
        protected void Page_Load(object sender, EventArgs e)
        {
            // OleDbDataReader reader = command.ExecuteReader();
            // открываем соединение с БД
            String connectString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\79520\Desktop\курсачи\вафин курсовая\Автосалон\Проект Remoting\BD.accdb";
            // создаем объект OleDbCommand для выполнения запроса к БД MS Access
            OleDbConnection con = new OleDbConnection(connectString);
            string query = "SELECT * FROM Cars ORDER BY Population DESC";
            OleDbCommand cmd = new OleDbCommand(query, con);
            try
            {
                con.Open();
                OleDbDataAdapter sda = new OleDbDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                rptTable.DataSource = ds;
                rptTable.DataBind();
            }
            catch (Exception ex)
            {
                //...
            }
            finally
            {
                con.Close();
            }
        }

        protected void rptTable_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            
        }
        
        protected void Button1_Click(object sender, EventArgs e)
        {
            //Label1 = button1.Text;
            foreach (RepeaterItem item in rptTable.Items)
            {
                Label1.Text = ((DataBoundLiteralControl)item.Controls[0]).Text + "<br />";
            }
            string pr = Label1.Text;
            char[] price2 = new char[13];
            bool flag = false;
            int j = 0;
            for (int i = 1; i < pr.Length; i++)
            {
                if (pr[i] >= '0' && pr[i] <= '9' && pr[i - 1] == ';')
                    flag = true;
                if (flag == true && pr[i] >= '0' && pr[i] <= '9')
                {
                    price2[j] = pr[i];
                    j++;
                }    
            }
            string pr2 = new string(price2);
            price = pr2.ToString();
            Server.Transfer("Buy.aspx");
        }
        void Check(int x)
        {
            foreach (RepeaterItem item in rptTable.Items)
            {
                    Label1.Text = item.ItemIndex.ToString() + " - " +
                               ((DataBoundLiteralControl)item.Controls[0]).Text +
                               "<br />";
            }
            Label1.ForeColor = Color.Red;
            Label1.Visible = true;
        }
    }
}