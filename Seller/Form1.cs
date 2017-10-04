using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Seller.ServiceReference1;
using ClassLibrary1;
using System.ServiceModel;

namespace Seller
{
    public partial class SellerForm : Form
    {
        //переменная класса Sales, которая принимает выборку из базы данных
        Sales s;
        //конструктор класса
        public SellerForm()
        {    
            //инициализация формы        
            InitializeComponent();
            //инициализация нового объекта класса Sales
            s = new Sales();
            //запуск функции, которая проводит первичную инициализацию полей класса
            init();
        }
        //функция, которая проводит первичную инициализацию полей класса
        void init()
        {
            try
            {
                //создание соединения с сервером
                Service1Client proxy = new Service1Client();
                //запуск функции, которая проводит основную инициализацию полей класса
                init2(proxy);
            }
            //перехват ошибок соединения с сервером
            catch (CommunicationException)
            {
                MessageBox.Show("Соединение с сервером временно недоступно, попробуйте повторить попытку немного позже");
            }
        }
        //функция, которая проводит основную инициализацию полей класса
        void init2(Service1Client proxy)
        {
            try
            {
                //получение выборки актуальных продаж        
                s = proxy.get_sale(false);
                //создание промежуточной переменной класса Good
                Good g = new Good();

                //перебор всех элементов в Sales
                foreach (Sale sale in s.list)
                {
                    //получение выборки с сервера
                    g = proxy.get_good(sale.id_buy);
                    //добавление элемента в checkedListBox
                    this.checkedListBox1.Items.Add(sale.FirstName + " " + sale.SecondName + " " + sale.ThirdName + " " + sale.Phone + " " + sale.when_buy + " " + g.Name + " " + g.Metall + " " + g.Category + " " + g.Price.ToString());
                }
            }
            //перехват ошибок соединения с сервером
            catch (CommunicationException)
            {
                MessageBox.Show("Соединение с сервером временно недоступно, попробуйте повторить попытку немного позже");
            }
        }
        //Кнопка, которая обрабатывает актуальные продажи, переводя их в статус выполненных
        private void Go_button_Click(object sender, EventArgs e)
        {
            try
            {
                //создание соединения с сервером
                Service1Client proxy = new Service1Client();
                //перебор всех элементов среди выбранных пользователем в checkedListBox
                for (int i = 0; i < this.checkedListBox1.CheckedIndices.Count; i++)
                {
                    //перевод продажи в статус выполненной
                    proxy.done_sale(s.list[checkedListBox1.CheckedIndices[i]].Id);
                }
                //удаление из checkedListBox записей о выполненных продажах
                foreach (var item in checkedListBox1.CheckedItems.OfType<string>().ToList())
                {
                    checkedListBox1.Items.Remove(item);
                }
                proxy.Close();
            }
            //перехват ошибок соединения с сервером
            catch (CommunicationException)
            {
                MessageBox.Show("Соединение с сервером временно недоступно, попробуйте повторить попытку немного позже");
            }
        }
        //Кнопка, которая обрабатывает обновление checkedListBox
        private void button_refresh_Click(object sender, EventArgs e)
        {
            //удаление из checkedListBox записей о прежних продажах
            foreach (var item in checkedListBox1.Items.OfType<string>().ToList())
            {
                checkedListBox1.Items.Remove(item);
            }
            //запуск функции, которая проводит основную инициализацию полей класса
            init();
        }
    }
}
