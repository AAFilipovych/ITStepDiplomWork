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
using ClassLibrary1;
using System.IO;
using User.ServiceReference1;
using System.ServiceModel;
using System.Net.Mail;
using System.Net;

namespace User
{
    /// <summary>
    /// Логика взаимодействия для Order.xaml
    /// </summary>
    public partial class Order : Window
    {
        //Список заказов
        Goods order;
        //конструктор класса с параметром связки с родительским окном
        public Order(MainWindow f1)
        {
            //инициализация окна заказа
            InitializeComponent();            
            //инициализация полей класса оформления заказов
            init(f1);
        }       
        //функция класса инициализирующая поля класса
        void init(MainWindow f1)
        {
            init2(f1);
            //создание объекта класса List<Good>
            order = new Goods();
            //перебор всех элементов корзины 
            for (int i = 0; i < f1.Cart.Count; i++)
            {
                //добавление нового товара в корзину
                order.list.Add(f1.geter.list[f1.Cart[i]]);
               //добавление записи в OrderBox
               this.OrderBox.Items.Add(order.list[i].Category + " " + order.list[i].Name + " " + order.list[i].Price + " гривен");
            }
        }
        void init2(MainWindow f1)
        {
            if(f1.cust!=null)
            { 
            this.FNtextBox.Text = f1.cust.FName;
            this.SNtextBox.Text = f1.cust.SName;
            this.TNtextBox.Text = f1.cust.TName;
            this.phtextBox.Text = f1.cust.phone.ToString();
            }
        }
        //функция, которая обрабатывает кнопку отправки заказа
        private void Order_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //проверка телефона на ввод цифр
               long phone= Convert.ToInt64(phtextBox.Text);
                //открытие соединения с сервером
                Service1Client proxy = new Service1Client();
                //вызов функции сервера, которая вносит заказ в базу данных
               for(int i=0;i<order.list.Count;++i)
                {
                    proxy.add_order(FNtextBox.Text, SNtextBox.Text, TNtextBox.Text, phone, order.list[i].Id);
                }
               //закрытие соединения               
                proxy.Close();
                string message = String.Format("Уважаемый {0} {1} {2}! Благодарим Вас за покупку!Ваш заказ успешно оформлен, менеджер свяжется с Вами в ближайшее время. Это окно будет автоматически закрыто через 5 секунд.", FNtextBox.Text, SNtextBox.Text, TNtextBox.Text);
                AutoClosingMessageBox.Show(message, "ЗАКАЗ ОФОРМЛЕН", 5000);
                try
                {
                    //создание сообщения
                    string em = "diplom.step.2017@gmail.com";
                    string login = "diplom.step.2017@gmail.com";
                    string pas = "Lbgkjv2017";                    
                    MailMessage mail = new MailMessage();
                    //от кого
                    mail.From = new MailAddress(em);
                    //кому 
                    mail.To.Add(new MailAddress(this.email_textBox.Text));
                    //тема
                    mail.Subject = "Заказ ювелирных товаров";
                    //текст письма
                    string body = "";
                    foreach (Good good in order.list)
                    {
                        string stone;
                        if (good.stone == "")
                        {
                            stone = "";
                        }
                        else
                        {
                            stone = " Камень:" + good.stone;
                        }
                        body += "Арктикул:" + good.Id + " Название:" + good.Name + " Металл:" + good.Metall + " Проба:" + good.proba + " Группа товаров:" + good.Category + " Вес:" + good.Weight + " Цена:" + good.Price + stone;
                    }
                    mail.Body = "Уважаемый " + FNtextBox.Text + " " + SNtextBox.Text + " " + TNtextBox.Text + "! Вы заказали: " + body;
                    //создаем клиента для отправки сообщения
                    SmtpClient client = new SmtpClient();
                    //хостинг клиента
                    client.Host = "smtp.gmail.com";                   
                    //порт клиента
                    client.Port = 587;                   
                    //поддержка шифрования
                    client.EnableSsl = true;
                    //параметры доступа
                    client.Credentials = new NetworkCredential(login, pas);
                    //доставка должна иметь параметр по сети
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    //отправка сообщения
                    client.Send(mail);
                    mail.Dispose();
                }
                catch (System.Security.Authentication.AuthenticationException)
                { }               
                this.Close();
            }
            //перехват ошибок ввода формата
            catch (FormatException)
            {
                //сообщение пользователю относительно ошибочного ввода телефона
                MessageBox.Show("некорректный ввод контактного телефона");
            }
            //перехват ошибок соединения с сервером
            catch (CommunicationException)
            {
                MessageBox.Show("Соединение с сервером временно недоступно, попробуйте повторить попытку немного позже");
            }
        }      
        private void del_button_Click(object sender, RoutedEventArgs e)
        {
            //удаление товара 
            this.order.list.RemoveAt(this.OrderBox.SelectedIndex);
            //очистка OrderBox
            this.OrderBox.Items.Clear();
            //перебор всех элементов корзины
            foreach (var order in order.list)
            {
                //добавление заказа в OrderBox
                this.OrderBox.Items.Add(order.Category + " " + order.Name + " " + order.Price + " гривен");
            }
            //обнуление картинки в image.Source
            this.image.Source = null;
        }
        //функция класса позволяющая конвертировать картинки из базы данных в формате byte[] с последующим размещением их в соответствующих элементах формы
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
        //обработка выбора пользователем элемента в OrderBox
        private void OrderBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //проверка на вылет за границы массива
            try
            {
                //присвоение полю image.Source картинки из OrderBox
                this.image.Source = LoadImage(order.list[this.OrderBox.SelectedIndex].Photo1);
            }
            //перехват ошибки
            catch (ArgumentOutOfRangeException)
            {
                //присвоение image.Source значения по умолчанию
                this.image.Source = null;
            }
        }
    }
}
