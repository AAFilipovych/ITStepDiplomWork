using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using adm.ServiceReference1;
using System.ServiceModel;
using ClassLibrary1;
using System.IO;

namespace adm
{
    /// <summary>
    /// Логика взаимодействия для Order_window.xaml
    /// </summary>
    public partial class Order_window : Window
    {
        Orders orders;
        List<Good> goods;

        public Order_window()
        {
            InitializeComponent();
            init();
        }
        private void init()
        {
            try
            {
                //создание соединения с сервером
                Service1Client proxy = new Service1Client();
                //создание промежуточной переменной для приема перечня возможных заказов из базы данных
                orders = new Orders();
                //создание промежуточной переменной для приема перечня возможных товаров в заказе
                goods = new List<Good>();

                //вызов функции производящей основную инициализацию полей класса.
                init2(proxy);
                //хакрытие соединения с сервером
                proxy.Close();
            }
            //перехват ошибок соединения с сервером
            catch (CommunicationException)
            {
                MessageBox.Show("Соединение с сервером временно недоступно, попробуйте повторить попытку немного позже");
            }
        }

        //функция класса, производящая основную инициализацию полей класса, включена в init().
        private void init2(Service1Client proxy)
        {
            try
            {
                // получение перечня возможных металлов из базы данных
                orders = proxy.get_order();
                //постепенный перебор всех полученных из базы данных
                foreach (Order o in orders.list)
                {
                    goods.Add(proxy.get_good(o.id_buy));

                }
                foreach (Good g in goods)
                {
                    this.Buyers_listBox.Items.Add(g.Name+" "+g.Category+" "+g.Metall+ " " + g.proba+ " " + g.stone+ " " + g.Weight.ToString()+ " " + g.Price.ToString());
                }
               
            }
            //перехват ошибок соединения с сервером
            catch (CommunicationException)
            {
                MessageBox.Show("Соединение с сервером временно недоступно, попробуйте повторить попытку немного позже");
            }
            catch (NullReferenceException)
            { }
            catch (ArgumentOutOfRangeException)
            { }
        }
        private static BitmapImage LoadImage(byte[] imageData)
        {
            //проверка данных и длины массива byte[]
            if (imageData == null || imageData.Length == 0)
            {
                //возврат null значения в случае null данных
                return null;
            }
            //создание промежуточной переменной
            var image = new BitmapImage();
            //создание потока для конвертации данных из массива byte[]
            using (var mem = new MemoryStream(imageData))
            {
                //стартовая позиция 
                mem.Position = 0;
                //начало работы 
                image.BeginInit();
                //опция создания в пиксель формат
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                //опция кеша при загрузке
                image.CacheOption = BitmapCacheOption.OnLoad;
                //источник пути null
                image.UriSource = null;
                //присвоение потока записи в промежуточную переменную
                image.StreamSource = mem;
                //завершение работы
                image.EndInit();
            }
            //стабилизация картинки
            image.Freeze();
            //возврат конвертированной картинки 
            return image;
        }
        //функция обнуления данных
        private void clear()
        {
            this.Buyers_listBox.Items.Clear();
            this.Buyers_listBox.SelectedIndex = -1;
            this.image.Source = null;
            this.FNtextBox.Text = "";
            this.SNtextBox.Text = "";
            this.TNtextBox.Text = "";
            this.phtextBox.Text = "";
            orders.list.Clear();
            goods.Clear();
        }

        private void Send_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Buyers_listBox.SelectedIndex != -1)
            {
                try
                {
                    //создание соединения с сервером
                    Service1Client proxy = new Service1Client();
                    proxy.add_sale(orders.list[Buyers_listBox.SelectedIndex]);
                    proxy.remove_order(orders.list[Buyers_listBox.SelectedIndex].Id);
                    proxy.Close();
                    Service1Client proxy2 = new Service1Client();
                    clear();
                    init2(proxy2);
                    proxy2.Close();
                }
                //перехват ошибки
                catch (ArgumentOutOfRangeException)
                {
                    MessageBox.Show("Заказ не выбран");
                }
                //перехват ошибок соединения с сервером
                catch (CommunicationException)
                {
                    MessageBox.Show("Соединение с сервером временно недоступно, попробуйте повторить попытку немного позже");
                }
            }
        }

        private void Buyers_listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Buyers_listBox.SelectedIndex != -1)
            {
                try
                {
                    //присвоение полю image.Source картинки из OrderBox
                    this.image.Source = LoadImage(goods[Buyers_listBox.SelectedIndex].Photo1);
                    this.FNtextBox.Text = orders.list[Buyers_listBox.SelectedIndex].FirstName;
                    this.SNtextBox.Text = orders.list[Buyers_listBox.SelectedIndex].SecondName;
                    this.TNtextBox.Text = orders.list[Buyers_listBox.SelectedIndex].ThirdName;
                    this.phtextBox.Text = orders.list[Buyers_listBox.SelectedIndex].Phone.ToString();
                }
                //перехват ошибки
                catch (ArgumentOutOfRangeException)
                {
                    //присвоение значения по умолчанию
                    this.image.Source = null;
                    this.FNtextBox.Text = "";
                    this.SNtextBox.Text = "";
                    this.TNtextBox.Text = "";
                    this.phtextBox.Text = "";
                }
            }
        }

        private void del_button_Click(object sender, RoutedEventArgs e)
        {
            if (Buyers_listBox.SelectedIndex != -1)
            {
                try
                {
                    //создание соединения с сервером
                    Service1Client proxy = new Service1Client();

                    proxy.remove_order(orders.list[Buyers_listBox.SelectedIndex].Id);
                    clear();
                    init2(proxy);
                    proxy.Close();
                }
                //перехват ошибки
                catch (ArgumentOutOfRangeException)
                {
                    MessageBox.Show("Заказ не выбран");
                }
                //перехват ошибок соединения с сервером
                catch (CommunicationException)
                {
                    MessageBox.Show("Соединение с сервером временно недоступно, попробуйте повторить попытку немного позже");
                }
            }
        }
    }
}
