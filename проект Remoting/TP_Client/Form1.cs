using System;
using System.Drawing;
using System.Windows.Forms;

namespace TP_Client
{
    public partial class Form1 : Form
    {
        //Конструктор формы
        public Form1()
        {
            InitializeComponent();
        }

        //Кнопка выхода из аккаунта пользователя
        private void button1_Click(object sender, EventArgs e)
        {
            StartForm f = new StartForm();
            f.Show();
            Hide();
        }

        //Кнопка Забронировать стол
        private void button2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }

        //Кнопка режим работы и контакты
        private void button3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 2;
        }

        //Кнопка Виртуальная экскурсия
        private void button4_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 3;
        }

        bool bron = false; // проверка, выбрана ли дата
        int price = 0; // стоимость за человека на выбранном столике (считывается из БД)
        int sum = 0; // депозит за столик
        string desk_number = "1"; // номер стола
        string Dat = ""; // выбранная дата
        int NumbPers = 1; // количество человек за столиком

        //Метод реакции на выбор даты
        private void comboBox1_ValueMemberChanged(object sender, EventArgs e)
        {
            bron = true;
            Button[] b = new Button[] { desk1, desk2, desk3, desk4, desk5, desk6, desk7, desk8};

            //проверяем бронь всех столиков
            bool[] desk_free = Program.goodByObj.DeskDateRead(comboBox1.Text);

            //свободные столики - зеленый цвет (Lime), занятые - красный
            for (int i = 0; i < b.Length; i++)
            {
                if (desk_free[i] == true) b[i].BackColor = Color.Lime;
                else b[i].BackColor = Color.Red;
            }
        }

        //Метод обработки нажатия на столики
        private void buttons(object sender, EventArgs e)
        {
            Button[] b = new Button[] { desk1, desk2, desk3, desk4, desk5, desk6, desk7, desk8};

            //Запоминаем выбранную дату
            Dat = comboBox1.Text;

            if (bron) //Если дата выбрана, то позволяем выбрать столик
            {
                //Определяем номер нажатой кнопки
                desk_number = sender.ToString();
                desk_number = desk_number.Substring(desk_number.Length - 1); //выделяем последний символ (номер кнопки)

                if (b[Convert.ToInt32(desk_number) - 1].BackColor == Color.Lime)
                {
                    //Считываем из БД цену за столик
                    price = Convert.ToInt32(Program.goodByObj.DeskPriceRead(desk_number));

                    //Сразу вводим номер столика в заказ
                    label13.Text = desk_number;

                    //переходим на вкладку оформления брони
                    tabControl1.SelectedIndex = 1;
                }
                else
                {
                    MessageBox.Show("Данный столик уже занят, выберите другой или другую дату!");
                }
            }
            else //Если дата не выбрана, то сначала просим выбрать столик
            {
                MessageBox.Show("Сначала выберите дату!");
            }
        }

        //Метод обработки кнопок + и -
        private void PlusMinus(object sender, EventArgs e)
        {
            //Определяем номер нажатой кнопки
            string pm = sender.ToString();
            pm = pm.Substring(pm.Length - 1); //выделяем последний символ

            int numb = Convert.ToInt32(desk_number);
            if (pm == "+") //обрабатываем нажатие +
            {
                if (numb < 4 && NumbPers < 8) NumbPers++;
                if (numb >= 4 && numb <= 7 && NumbPers < 6) NumbPers++;
                if (numb == 8 && NumbPers < 15) NumbPers++;
            }
            if(pm == "-") //обрабатываем нажатие -
            {
                if (NumbPers > 1) NumbPers--;
            }

            //Выводим новое количество человек
            label16.Text = NumbPers.ToString();

            //Пересчитываем депозит
            DepositCalculation();
        }

        //Метод подсчёта депозита
        private void DepositCalculation()
        {
            if(desk_number == "8") //условия для 8 (VIP) столика
            {
                if(NumbPers < 8) sum = price;
                else sum = price + (NumbPers - 7) * 1000;
            }
            else //условия для всех остальных
            {
                sum = NumbPers * price;
            }

            //отображаем стоимость депозита за столик
            label20.Text = sum.ToString() + " руб.";
        }

        //Кнопка Забронировать
        private void buttonToBook(object sender, EventArgs e)
        {
            if(textBox1.Text != "" && textBox2.Text != "" && comboBox2.Text != "")
            {
                //записываем данные в БД
               // Program.helloObj.Zapis(Dat, desk_number, Program.Name, NumbPers.ToString(), comboBox2.Text, Program.Mob, sum.ToString());
                //выдаем сообщение клиенту
                MessageBox.Show("Столик успешно забронирован!\nНаш администратор свяжется с вами в ближайшее время...");
                //возвращаемся обратно к брони столиков
                tabControl1.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Кажется, что одно из полей не заполнено...");
            }
        }

        //Метод обновления данных клиентского интерфейса
        private void DataUpdate()
        {
            //Обнуляем переменные
            bron = false;

            //Обновляем даты
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(new string[] { "8 мая (пятница)", "9 мая (суббота)", "15 мая (пятница)", "16 мая (суббота)", "22 мая (пятница)", "23 мая (суббота)", "29 мая (пятница)", "30 мая (суббота)"});
            comboBox1.Text = "Выбери тут свободную дату";

            //Обновляем цвет кнопок
            Button[] b = new Button[] { desk1, desk2, desk3, desk4, desk5, desk6, desk7, desk8 };
            for(int i = 0; i < b.Length; i++)
            {
                b[i].BackColor = Color.Lime;
            }

            //Обновляев вкладку брони
            NumbPers = 1;
            label16.Text = NumbPers.ToString();
            textBox1.Text = Program.Name;
            textBox2.Text = Program.Mob;
            comboBox2.Text = "";
            DepositCalculation();
        }

        //Событие перехода на другую вкладку
        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            //обновляем данные при каждом переходе с вкладки на вкладку
            DataUpdate();
        }

        //Метод загрузки формы
        private void Form1_Load(object sender, EventArgs e)
        {
            DataUpdate(); //обновляем данные
            tabControl1.SelectedIndex = 2; //открываем вкладку контакты
        }

        //Событие нажатия на ссылку посещения экскурсии
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //переходим на сайт виртуальной экскурсии
            System.Diagnostics.Process.Start("https://vk.com/app6313802_-123731155");
        }
    }
}
