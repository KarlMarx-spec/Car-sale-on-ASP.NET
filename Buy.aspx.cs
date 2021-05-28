using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Lifetime;
using InRemote;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarSale
{
    public partial class Buy : System.Web.UI.Page
    {
        static public Iremote helloObj; //объект доступа к первому удаленному объекту через интерфейс
        static public Iremote1 goodByObj;
        static public ILease leaseHi, leaseBy;
        string price;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Page Catalog = Page.PreviousPage;
            //string PreviousPage = Request.UrlReferrer.ToString();

            //Catalog cat = PreviousPage as Catalog;
            if (PreviousPage != null)
            {
                price = PreviousPage.price;
                pr.Text = price;
            }
                
            CreateChannel(); //запуск создания каналов
            
        }
        private void CreateChannel()
        {
            try
            {
                //Регистрация tcp канала через код
                Dictionary<string, string> properties_1 = new Dictionary<string, string>();

                properties_1["port"] = "0";//создание криптографической защиты (пару ключ-значение)
                SoapServerFormatterSinkProvider srvPrvd_1 = new SoapServerFormatterSinkProvider();
                srvPrvd_1.TypeFilterLevel = TypeFilterLevel.Full;//прописание кода для правильной сериализации
                SoapClientFormatterSinkProvider clntPrvd_1 = new SoapClientFormatterSinkProvider();
                //то же для клиента
                TcpChannel tcpChannel = new TcpChannel(properties_1, clntPrvd_1, srvPrvd_1);//создание канала 
                ChannelServices.RegisterChannel(tcpChannel, false);//его регистрация

                helloObj = (Iremote)RemotingServices.Connect(typeof(Iremote), "tcp://localhost:8086/Hi");
                //соедение удаленного обьекта
                leaseHi = (ILease)helloObj.GetLifetimeService();//создание спонсора для обьекта

                //Регистрация http канала через конфиг файл
                RemotingConfiguration.Configure("C:\\Users\\79520\\Desktop\\курсачи\\вафин курсовая\\Автосалон\\Проект Remoting\\TP_Client\\Client.config", false);
                goodByObj = (Iremote1)RemotingServices.Connect(typeof(Iremote1), "http://localhost:8087/By");//соедение удаленного обьекта
                leaseBy = (ILease)goodByObj.GetLifetimeService();//создание спонсора для обьекта

                //Привязка удаленных объектов к спонсору
                MyClientSponsor sponsorHi = new MyClientSponsor("Hi");
                MyClientSponsor sponsorBy = new MyClientSponsor("By");
                //регистрирую спонсоры
                leaseHi.Register(sponsorHi);
                leaseBy.Register(sponsorBy);
            }
            catch (Exception e)
            {
                if (e.Message != "Канал \"tcp\" уже зарегистрирован.")
                {
                    error.Text = e.Message;
                    error.ForeColor = Color.Red;
                    error.Visible = true;
                }
                
            }
        }
        
        public class MyClientSponsor : MarshalByRefObject, ISponsor
        {
            private string Name;
            private DateTime lastRenewal;//время последнего вызова
            int count = 0;
            public MyClientSponsor(string name)
            {
                Name = name;
                Console.WriteLine("\nСпонсор " + name + " создан ");
                lastRenewal = DateTime.Now;//время последнего вызова делаю текущим временем
            }
            public TimeSpan Renewal(ILease lease)//обновление времени аренды удаленного обьекта
            {
                count++;
                Console.WriteLine("Вызван метод Renewal спонсора " + Name + " {0}-ый раз", count);
                Console.WriteLine("Время с момента последнего вызова:" + (DateTime.Now - lastRenewal).ToString());
                lastRenewal = DateTime.Now;
                return TimeSpan.FromSeconds(10);
            }
        }
        protected void Button4_Click(object sender, EventArgs e)
        { 
            Random rand = new Random();
            int count = rand.Next(10000);
            // count = helloObj.Check();
            helloObj.Zapis(count, TextBox1.Text, TextBox2.Text, TextBox3.Text, TextBox4.Text, TextBox5.Text, pr.Text);
            error2.Text = "Благодарим за покупку!\nВаш номер заказа " + count + " его стоимость " + pr.Text;
            error2.ForeColor = Color.Blue;
            error2.Visible = true;
            Fill();
        }

        private void Fill()
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox5.Text = "";
        }
    }
}