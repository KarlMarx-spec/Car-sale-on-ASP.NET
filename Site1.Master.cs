using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InRemote;
using RemoteHello;
using System.Data;
using System.Data.OleDb;

namespace CarSale
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
           Server.Transfer("Catalog.aspx", true);
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Server.Transfer("Contacts.aspx", true);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Server.Transfer("Service.aspx", true);
        }
    }
}