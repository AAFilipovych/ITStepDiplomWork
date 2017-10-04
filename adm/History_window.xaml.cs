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
using System.ServiceModel;
using adm.ServiceReference1;
using System.IO;


namespace adm
{
    /// <summary>
    /// Логика взаимодействия для History_window.xaml
    /// </summary>
    public partial class History_window : Window
    {
        Sales s;
        List<BitmapImage> photo;
        public History_window()
        {
            InitializeComponent();
            init();
        }

        void init()
        {
            try
            {
                Service1Client proxy = new Service1Client();
                photo = new List<BitmapImage>();
                init2(proxy);
                proxy.Close();
            }
            //перехват ошибок соединения с сервером
            catch (CommunicationException)
            {
                MessageBox.Show("Соединение с сервером временно недоступно, попробуйте повторить попытку немного позже");
            }
        }
        void init2(Service1Client proxy)
        {
            try
            {
                s = new Sales();
                s = proxy.get_sale(true);

                Good g = new Good();
                foreach (Sale sale in s.list)
                {

                    g = proxy.get_good(sale.id_buy);
                    this.listBox.Items.Add("Фамилия: " + sale.FirstName + "| Имя: " + sale.SecondName + "| Отчество: " + sale.ThirdName + "| Телефон: " + sale.Phone + "| Дата покупки: " + sale.when_buy + "| Название товара: " + g.Name + "| Тип металла: " + g.Metall + "| Группа товаров: " + g.Category + "| Стоимость товара: " + g.Price.ToString());
                    photo.Add(LoadImage(g.Photo1));
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
        private void delete(bool check)
        {
            try
            {
                Service1Client proxy = new Service1Client();
                if (!check)
                {
                    proxy.remove_sale(-1);
                }
                else
                {
                    proxy.remove_sale(s.list[this.listBox.SelectedIndex].Id);
                }
                this.listBox.Items.Clear();
                this.image.Source = null;
                init2(proxy);
                proxy.Close();
            }
            //перехват ошибок соединения с сервером
            catch (CommunicationException)
            {
                MessageBox.Show("Соединение с сервером временно недоступно, попробуйте повторить попытку немного позже");
            }
        }
        private void del_button_Click(object sender, RoutedEventArgs e)
        {
            delete(true);
        }

        private void clear_button_Click(object sender, RoutedEventArgs e)
        {
            delete(false);
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.listBox.SelectedIndex != -1)
            {
                this.image.Source = photo[this.listBox.SelectedIndex];
            }

        }
    }
}
