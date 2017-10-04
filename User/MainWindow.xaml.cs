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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClassLibrary1;
using System.IO;
using User.ServiceReference1;
using System.ServiceModel;


namespace User
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        //поле класса, которое принимает выборку базы данных с сервера
        public Goods geter;
        //поле класса, которое хранит идентификационные номера, выбранных пользователем товаров
        public List<int> Cart;
        //поле класса, которое хранит сумму заказов пользователя
        decimal summ = 0;
        //поле класса, которое хранит данные логина пользователя
        public customer cust;
        Metalls met;
        Groups gr;
        probas pr;
        Stones st;
        public MainWindow()
        {
            //инициализация главного окна
            InitializeComponent();
            //создание объекта класса Goods
            geter = new Goods();
            //создание объекта класса Groups
            gr = new Groups();
            //создание объекта класса Metalls
            met = new Metalls();
            //создание объекта класса 
            pr = new probas();
            //создание объекта класса 
            st = new Stones();
            //создание объекта класса List<int>
            Cart = new List<int>();
            //создание объект класса customer
            cust = new customer();
            //вызов функции инициализирующей переменные класса
            init();
        }
        //фукнция которая обрабатывает отправку запроса
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //вызов функции очистки данных
                clear();
                //создание нового соединения с сервером
                Service1Client proxy = new Service1Client();
                //создание нового объекта класса transfer, который будет содержать параметры запроса пользователя, необходимые для корректной выборки
                Transfer a = new Transfer();
                //инициализация группы, которая выбрана пользователем для выборки
                if(this.checkBox_group.IsChecked==true)
                { 
                a.Category = gr.list[this.Category_CB.SelectedIndex].c_k;
                }
                else
                {
                    a.Category = "%";
                }
                //инициализация металла, который выбран пользователем для выборки 
                if(this.checkBox.IsChecked==true)
                { 
                a.Metall = met.list[this.Metal_CB.SelectedIndex].m_k;
                }
                else
                {
                    a.Metall = "%";
                }
                if(this.checkBox_proba.IsEnabled==true&&this.checkBox_proba.IsChecked==true)
                {
                    foreach(proba p in pr.list)
                    {
                        if(p.metall==Metal_CB.SelectedIndex+1&&p.number==pr_cb.SelectedItem.ToString())
                        {
                            a.Proba = p.number;
                        }
                    }
                }
                else
                {
                    a.Proba = "%";
                }
               
                if(this.checkBox_stone.IsChecked==true)
                { 
                a.Stone = st.list[this.st_comboBox.SelectedIndex].s_k;
                }
                else
                {
                    a.Stone = "%";
                }
                //проверка корректности введенного пользователем формата и пределов числа
                try
                {
                    //инициализация нижней стоимости, которая выбрана пользователем для выборки
                    a.PriceLOW = Convert.ToDecimal(this.bprice_tb.Text);
                }
                //обработка ошибки ввода формата
                catch (FormatException)
                {
                    //присвоение 0 по умолчанию, в случае ошибки ввода формата
                    a.PriceLOW = 0;
                }
                //обработка ошибки пределов числа
                catch (OverflowException)
                {
                    //вывод сообщения об ошибке
                    MessageBox.Show("ошибка при вводе нижней цены, введите адекватную стоимость");
                }
                //проверка корректности введенного пользователем формата и пределов числа
                try
                {
                    //инициализация верхней стоимости, которая выбрана пользователем для выборки
                    a.PriceUP = Convert.ToDecimal(this.price_tb.Text);
                }
                //обработка ошибки ввода формата
                catch (FormatException)
                {
                    //присвоение 1000000 по умолчанию, в случае ошибки ввода формата
                    a.PriceUP = 1000000;
                }
                //обработка ошибки пределов числа
                catch (OverflowException)
                {
                    //вывод сообщения об ошибке
                    MessageBox.Show("ошибка при вводе верхней цены, введите адекватную стоимость");
                }
                //проверка корректности введенного пользователем формата и пределов числа
                try
                {
                    //инициализация нижней величины веса, который выбран пользователем для выборки
                    a.WeightLOW = Convert.ToDecimal(this.bweight_tb.Text);
                }
                //обработка ошибки ввода формата
                catch (FormatException)
                {
                    //присвоение 0 по умолчанию, в случае ошибки ввода формата
                    a.WeightLOW = 0;
                }
                //обработка ошибки пределов числа
                catch (OverflowException)
                {
                    //вывод сообщения об ошибке
                    MessageBox.Show("ошибка при вводе нижнего веса, введите адекватный вес");
                }
                //проверка корректности введенного пользователем формата и пределов числа
                try
                {
                    //инициализация верхней величины веса, который выбран пользователем для выборки
                    a.WeightUP = Convert.ToDecimal(this.weight_tb.Text);
                }
                //обработка ошибки ввода формата
                catch (FormatException)
                {
                    //присвоение 1000000 по умолчанию, в случае ошибки ввода формата
                    a.WeightUP = 1000000;
                }
                //обработка ошибки пределов числа
                catch (OverflowException)
                {
                    //вывод сообщения об ошибке
                    MessageBox.Show("ошибка при вводе верхнего веса, введите адекватный вес");
                }
                //вызов функции сервера ShowGoods с присвоением выборки в geter
                geter = proxy.ShowGoods(a);
                //зактрытие соединения
                proxy.Close();
                //перебор всех элементов в geter
                foreach (Good c in geter.list)
                {
                    //внесение в listbox имени выбранных товаров
                    this.listBox.Items.Add(c.Name);
                }
            }
            //перехват ошибок соединения с сервером
            catch (CommunicationException)
            {
                MessageBox.Show("Соединение с сервером временно недоступно, попробуйте повторить попытку немного позже");
            }
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
        //функция класса, которая создает соединение с всервером и инициализирует поля класса
        private void init()
        {            
            try
            {
                this.Metal_CB.Items.Clear();
                this.Category_CB.Items.Clear();
                this.pr_cb.Items.Clear();
                this.st_comboBox.Items.Clear();
                met.list.Clear();
                pr.list.Clear();
                gr.list.Clear();
                st.list.Clear();
                //создание соединения с сервером
                Service1Client proxy = new Service1Client();

                //получение выборки металлов из базы данных сервера
                met = proxy.get_metall();
                //получение выборки проб из базы данных сервера
                pr = proxy.get_proba();                
                //получение выборки групп из базы данных сервера
                gr = proxy.get_group();
                //получение выборки камней из базы данных сервера
                st = proxy.get_stone();
                //закрытие соединения с сервером
                proxy.Close();
                //перебор элементов в met.list
                foreach (Metall m in met.list)
                {
                    //внесение металлов в комбобокс
                    this.Metal_CB.Items.Add(m.Name.ToString());
                }
                //присвоение индекса первого элемента по умолчанию  
                this.Metal_CB.SelectedIndex = 0;
                this.Metal_CB.Visibility = Visibility.Collapsed;
                this.pr_cb.Visibility = Visibility.Collapsed;
                this.checkBox_proba.Visibility = Visibility.Collapsed;


                //перебор элементов в gr.list
                foreach (Group group in gr.list)
                {
                    //внесение групп в комбобокс
                    this.Category_CB.Items.Add(group.Name.ToString());
                }
                //присвоение индекса первого элемента по умолчанию  
                this.Category_CB.SelectedIndex = 0;
                this.Category_CB.Visibility = Visibility.Collapsed;
                //перебор элементов в st.list
                foreach (Stone s in st.list)
                {
                    //внесение групп в комбобокс
                    this.st_comboBox.Items.Add(s.Name.ToString());
                }
                //присвоение индекса первого элемента по умолчанию  
                this.st_comboBox.SelectedIndex = 0;
                this.st_comboBox.Visibility = Visibility.Collapsed;
            }
            //перехват ошибок соединения с сервером
            catch (CommunicationException)
            {
                MessageBox.Show("Соединение с сервером временно недоступно, попробуйте повторить попытку немного позже");
                init();
            }
        }
        //функция обработки изменения индекса 
        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //проверка корректности выбранного индекса
                if (this.listBox.SelectedIndex != -1)
                {
                    //инициализация поля фотографии 1 выбранным пользователем товаром
                    this.Photo1.Source = LoadImage(geter.list[this.listBox.SelectedIndex].Photo1);
                    //инициализация поля фотографии 2 выбранным пользователем товаром
                    this.Photo2.Source = LoadImage(geter.list[this.listBox.SelectedIndex].Photo2);
                    //инициализация поля фотографии 3 выбранным пользователем товаром
                    this.Photo3.Source = LoadImage(geter.list[this.listBox.SelectedIndex].Photo3);
                    //инициализация поля фотографии 4 выбранным пользователем товаром
                    this.Photo4.Source = LoadImage(geter.list[this.listBox.SelectedIndex].Photo4);
                    //установка в комбобокс группы к которой принадлежит товар выбранный пользователем
                    this.Category_CB.SelectedItem = geter.list[this.listBox.SelectedIndex].Category;
                    //установка в комбобокс металла к которому принадлежит товар выбранный пользователем
                    this.Metal_CB.SelectedItem = geter.list[this.listBox.SelectedIndex].Metall;
                    //установка актуальной цені в метку
                    this.label_price.Content = "Цена " + geter.list[this.listBox.SelectedIndex].Price.ToString() + " грн";
                    //установка актуального веса в метку
                    this.label_weight.Content = "Вес " + geter.list[this.listBox.SelectedIndex].Weight.ToString() + " грамм";
                    this.pr_cb.SelectedItem = geter.list[this.listBox.SelectedIndex].proba;
                    this.st_comboBox.SelectedItem = geter.list[this.listBox.SelectedIndex].stone;
                    this.richTextBox.Document.Blocks.Clear();
                    this.richTextBox.Document.Blocks.Add(new Paragraph(new Run(geter.list[this.listBox.SelectedIndex].Describe)));
                }
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Не выбрано ни одного товара");
            }
        }
        //функция класса, очищающая корзину от старых товаров
        private void cart_clear()
        {
            //обнуление поля количества стоимости товаров
            this.count_tb.Text = "0";
            //обнуление поля накопление стоимости заказа
            this.amount_tb.Text = "0";
            //обнуление корзины
            this.Cart.Clear();
            //обнуление промежуточной переменной, накапливающей сумму заказа
            summ = 0;
        }
        //функция класса, очищающая значения формы и полей класса.
        private void clear()
        {
            //установка индекса в позицию -1, которая невозможна к выбору пользователем
            this.listBox.SelectedIndex = -1;
            //очистка listbox от значений
            this.listBox.Items.Clear();
            //очистка geter.list от значений
            this.geter.list.Clear();
            //обнуление значения поля фотография 1
            this.Photo1.Source = null;
            //обнуление значения поля фотография 2
            this.Photo2.Source = null;
            //обнуление значения поля фотография 3
            this.Photo3.Source = null;
            //обнуление значения поля фотография 4
            this.Photo4.Source = null;

        }
        //функция обрабатывающая нажатие кнопки добавления в корзину
        private void add_in_cart_button_Click(object sender, RoutedEventArgs e)
        {
            //добавление в корзину индекса выбранного пользователем товара
            this.Cart.Add(this.listBox.SelectedIndex);
            //Добавление в поле заказов, количества выбранных пользователем товаров
            this.count_tb.Text = this.Cart.Count.ToString();
            //промежуточная переменная, которая накапливает сумму заказа
            summ += geter.list[this.listBox.SelectedIndex].Price;
            //добавление в поле корзины сумарной стоимости заказа
            this.amount_tb.Text = summ.ToString();
        }
        //функция, которая обрабатывает нажатие кнопки очистки
        private void cl_button_Click(object sender, RoutedEventArgs e)
        {
            //функция очистки корзины
            cart_clear();
        }
        //обработка кнопки оформления заказов
        private void order_button_Click(object sender, RoutedEventArgs e)
        {
            //Создание новой формы для оформления заказа
            Order newForm = new Order(this);
            //отображение новой формы для оформления заказа
            newForm.Show();
            //очистка корзины от старого содержимого, которое перешло в форму оформления заказов
            cart_clear();
        }

        private void autorization_button_Click(object sender, RoutedEventArgs e)
        {
            Login a = new Login();
            a.Owner = this;
            a.ShowDialog();
        }
        //функция обрабатывающая кнопку выход из логина
        private void exit_button_Click(object sender, RoutedEventArgs e)
        {
            this.login_label.Content = "Гость";
            this.cust = null;
        }
        public void do_login(string login, string password)
        {
            try
            {
                //создание соединения с сервером
                Service1Client proxy = new Service1Client();
                this.cust = proxy.get_user(login, password);
                this.login_label.Content = cust.FName;
            }
            catch (CommunicationException)
            {
                MessageBox.Show("Сервер временно недоступен");
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Пользователь не зарегистрирован в системе");
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Пользователь не зарегистрирован в системе");
            }
        }

        private void Metal_CB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            this.pr_cb.Items.Clear();

            int sum = pr.list.Count;
            for (int i = 0; i < sum; i++)
            {
                if (pr.list[i].metall == this.Metal_CB.SelectedIndex+1)
                    this.pr_cb.Items.Add(pr.list[i].number.ToString());
            }

            this.pr_cb.SelectedIndex = 0;
            
        }

        private void refresh_button_Click(object sender, RoutedEventArgs e)
        {
            init();
        }

        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {
            checkBox.Content = "";
            checkBox.Width = 19;
            this.Metal_CB.Visibility = Visibility.Visible;
            this.label_proba.Visibility = Visibility.Collapsed;
            checkBox_proba.Visibility=Visibility.Visible;
        }
        private void checkBox_Unchecked(object sender, RoutedEventArgs e)
        {
            checkBox.Content = "Активация фильтра";
            checkBox.Width = 136;
            this.Metal_CB.Visibility = Visibility.Collapsed;
            checkBox_proba.IsChecked = false;
            checkBox_proba.Visibility = Visibility.Collapsed;
            this.label_proba.Visibility = Visibility.Visible;
        }

        private void checkBox_proba_Checked(object sender, RoutedEventArgs e)
        {
            checkBox_proba.Content = "";
            checkBox_proba.Width = 19;
            this.pr_cb.Visibility = Visibility.Visible;
        }
        private void checkBox_proba_Unchecked(object sender, RoutedEventArgs e)
        {
            checkBox_proba.Content = "Активация фильтра";
            checkBox_proba.Width = 136;
            this.pr_cb.Visibility = Visibility.Collapsed;
        }
        private void checkBox_group_Checked(object sender, RoutedEventArgs e)
        {
            checkBox_group.Content = "";
            checkBox_group.Width = 19;
            this.Category_CB.Visibility = Visibility.Visible;
        }
        private void checkBox_group_Unchecked(object sender, RoutedEventArgs e)
        {
            checkBox_group.Content = "Активация фильтра";
            checkBox_group.Width = 136;
            this.Category_CB.Visibility = Visibility.Collapsed;
        }

        private void checkBox_stone_Checked(object sender, RoutedEventArgs e)
        {
            checkBox_stone.Content = "";
            checkBox_stone.Width = 19;
            this.st_comboBox.Visibility = Visibility.Visible;
        }
        private void checkBox_stone_Unchecked(object sender, RoutedEventArgs e)
        {
            checkBox_stone.Content = "Активация фильтра";
            checkBox_stone.Width = 136;
            this.st_comboBox.Visibility = Visibility.Collapsed;
        }
    }
}