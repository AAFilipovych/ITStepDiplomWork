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
using System.IO;
using System.Drawing;
using Microsoft.Win32;
using ClassLibrary1;
using System.ServiceModel;


namespace adm
{
    /// <summary>
    /// Логика взаимодействия для Goods_corr.xaml
    /// </summary>
    public partial class Goods_corr : Window
    {
        //класс принимающий с сервера запрошенную выборку из базы данных
        public Goods geter;
        public Groups gr;
        public Metalls met;
        public probas pr;
        public Stones st;

        public Goods_corr()
        {
            //инициализация главного окна
            InitializeComponent();
            //создание объекта класса принимающего с сервера запрошенную выборку из базы данных
            geter = new Goods();
            gr = new Groups();
            met = new Metalls();
            pr = new probas();
            st = new Stones();

            //вызов функции класса, которая создает соединение с всервером и вызывает init2().
            init();
        }
        //функция класса, которая создает соединение с всервером и вызывает init2().
        private void init()
        {
            try
            {
                //создание соединения с сервером
                Service1Client proxy = new Service1Client();
                //получение выборки с базы данных
                pr = proxy.get_proba();
                // получение перечня возможных металлов из базы данных
                met = proxy.get_metall();
                
                foreach (Metall met in met.list)
                {
                    //инициализация комбобокса отвечащего за перечень категорий(групп)
                    this.Metal_CB.Items.Add(met.Name.ToString());
                }
                //установка индекса по умолчанию в комбобоксе, отвечающему за металлы на первый, элемент
                this.Metal_CB.SelectedIndex = 0;
                // получение перечня возможных категорий(групп) из базы данных
                gr = proxy.get_group();
                
                foreach (Group group in gr.list)
                {
                    //инициализация комбобокса отвечащего за перечень категорий(групп)
                    this.Group_CB.Items.Add(group.Name.ToString());
                }
                //установка индекса по умолчанию в комбобоксе, отвечающему за категории(группы) на первый, элемент
                this.Group_CB.SelectedIndex = 0;          
                
                //получение выборки с базы данных
                st = proxy.get_stone();
                //перебор элементов в st.list
                foreach (Stone stone in st.list)
                {
                    //инициализация комбобокса отвечащего за перечень категорий(групп)
                    this.st_comboBox.Items.Add(stone.Name.ToString());
                }
                //for (int i = 0; i < st.list.Count; i++)
                //{
                //    this.st_comboBox.Items.Add(st.list[i].Name);
                //}
                this.st_comboBox.SelectedIndex = 0;
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
               
                //получение во внутренний массив всех данных из базы данных сервера
                geter = proxy.Show();
                //постепенный перебор всех полученных из базы данных во внутренний массив товаров
                foreach (Good c in geter.list)
                {
                    //инициализация listbox значениеями из базы данных
                    this.listBox.Items.Add(c.Name + " " + c.Price.ToString() + " грн " + c.Weight.ToString() + " грамм ");
                }
                //установка индекса listbox с перечнем товаров на первый элемент
                this.listBox.SelectedIndex = 0;
            }
            //перехват ошибок соединения с сервером
            catch (CommunicationException)
            {
                MessageBox.Show("Соединение с сервером временно недоступно, попробуйте повторить попытку немного позже");
            }

        }
        //фнукция класса позволяющая конвертировать картинки из базы данных в формате byte[] с последующим размещением их в соответствующих элементах формы
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

        //функция класса позволяющая конвертировать картинки из полей формы в масив byte[] для последующего внесения их в базу данных с помощью entityframework
        public byte[] ImageSourceToBytes(ImageSource imageSource)
        {
            //создание переменной BitmapEncoder с применением декодера Png
            BitmapEncoder encoder = new PngBitmapEncoder();
            //создание byte массива
            byte[] bytes = null;
            //создание промежуточной переменной для декодирования imageSource
            var bitmapSource = imageSource as BitmapSource;
            //проверка на null значение для избежания ошибок
            if (bitmapSource != null)
            {
                //передача промежуточной переменной в encoder
                encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
                //запуск потока для записи в массив byte[]
                using (var stream = new MemoryStream())
                {
                    //сохранение 
                    encoder.Save(stream);
                    //конвертирование в byte[]
                    bytes = stream.ToArray();
                }
            }
            //возврат данных в формате byte[]
            return bytes;
        }
        //функция класса, очищающая значения формы и полей класса.
        private void clear()
        {
            //сброс выбранного индекса listbox для устранения возможных ошибок в коде
            this.listBox.SelectedIndex = -1;
            //очистка listbox от всех значений
            this.listBox.Items.Clear();
            //обнуления класса geter который принимает базу товаров с сервера
            //this.geter = null;
            this.geter.list.Clear();
            //обнуление поля для ввода/вывода имени
            this.tb_name.Text = "";
            //обнуление поля для ввода/вывода цены
            this.tb_price.Text = "0";
            //обнуление поля для ввода/вывода веса
            this.tb_weight.Text = "0";
            //обнуление поля для ввода/вывода фотографии 1
            this.Photo1.Source = null;
            //обнуление поля для ввода/вывода фотографии 2
            this.Photo2.Source = null;
            //обнуление поля для ввода/вывода фотографии 3
            this.Photo3.Source = null;
            //обнуление поля для ввода/вывода фотографии 4
            this.Photo4.Source = null;
        }
        //функция класса позволяющая получить путь к файлу, который пользователь выберет в качестве картинки одного из полей формы
        private string getpath()
        {
            //создание переменной в которую будет записан путь к файлу выбранному пользователем
            string path = "";
            //создание диалогового окна в котором пользователь выберет нудный ему файл фотографии товара
            OpenFileDialog myDialog = new OpenFileDialog();
            //установка фильтра для отсеивания файлов с ненужными расширениями
            myDialog.Filter = "Картинки(*.JPEG;*.JPG;*.GIF)|*.JPEG;*.JPG;*.GIF";
            //установка проверки на существование файлов
            myDialog.CheckFileExists = true;
            //описание обработки положительного выбора пользователя в диалоговом окне
            if (myDialog.ShowDialog() == true)
            {
                //присвоение значения пути к выбранному файлу переменной path
                path = myDialog.FileName;
            }
            //возврат переменной path
            return path;
        }

        //функция класса описывающая работу кнопки добавления товара
        private void add_button_Click(object sender, RoutedEventArgs e)
        {
            //проверка заполненности всех полей, необходимых для добавления нового товара
            if (tb_name.Text != "" && tb_price.Text != "" && tb_weight.Text != "" && Photo1.Source != null && Photo2.Source != null && Photo3.Source != null && Photo4.Source != null)
            {
                //проверка нулевой ссылки и вылета за пределы массива
                try
                {
                    //создание переменной для соединения с сервером
                    Service1Client proxy = new Service1Client();
                    //проверка корректности введенных пользоветелем данных на возможность их конвертирования в формат децимал  
                    try
                    {

                        //присвоение переменной price конвертированного из поля цены значения, если пользователей введено не корректное значение,  отработает соответствующий блок catch ниже по коду
                        decimal price = Convert.ToDecimal(this.tb_price.Text);
                        //присвоение переменной weight конвертированного из поля вес значения, если пользователей введено не корректное значение,  отработает соответствующий блок catch ниже по коду
                        decimal weight = Convert.ToDecimal(this.tb_weight.Text);
                        //вызов функции на сервере добавляющей товар в базу данных
                        proxy.add_good(new Good
                        {
                            Name = tb_name.Text.ToString(),
                            Category = gr.list[Group_CB.SelectedIndex].Name,
                            c_k = gr.list[Group_CB.SelectedIndex].c_k,
                            Metall = met.list[Metal_CB.SelectedIndex].Name,
                            m_k =met.list[Metal_CB.SelectedIndex].m_k,
                            proba =pr.list[pr_comboBox.SelectedIndex].number,
                            stone =st.list[st_comboBox.SelectedIndex].Name,
                            s_k =st.list[st_comboBox.SelectedIndex].s_k,
                            Describe = new TextRange(richTextBox.Document.ContentStart,
                            richTextBox.Document.ContentEnd).Text,
                            Price = price,
                            Weight = weight,
                            Photo1 = ImageSourceToBytes(Photo1.Source),
                            Photo2 = ImageSourceToBytes(Photo2.Source),
                            Photo3 = ImageSourceToBytes(Photo3.Source),
                            Photo4 = ImageSourceToBytes(Photo4.Source)
                        });
                        //вызов функции класса, очищающей значения перед их обновленным заполнением
                        clear();
                        //вызов функции класса, которяа проводит инициализацию данных класса
                        init2(proxy);

                    }
                    //поимка исключения связанного с ошибкой формата ввода данных
                    catch (FormatException)
                    {
                        //вызов вспомагательного окна с сообщением об ошибке в случае ее поимки
                        MessageBox.Show("Некорректно введенные данные");
                    }
                    //поимка исключения связанного с ошибкой допустимой границы ввода данных
                    catch (OverflowException)
                    {
                        //вызов вспомагательного окна с сообщением об ошибке в случае ее поимки
                        MessageBox.Show("Слишком большое значение");
                    }
                    //закрытие соединения с сервером
                    proxy.Close();

                }
                //поимка исключения связанного с вылетом за пределы массива
                catch (ArgumentOutOfRangeException)
                {
                    //вызов вспомагательного окна с сообщением об ошибке в случае ее поимки
                    MessageBox.Show("Выберите значение из списка");
                }
                //поимка исключения связанного с нулевой ссылкой
                catch (ArgumentNullException)
                {
                    //вызов вспомагательного окна с сообщением об ошибке в случае ее поимки
                    MessageBox.Show("Выберите значение из списка");
                }
                //перехват ошибок соединения с сервером
                catch (CommunicationException)
                {
                    MessageBox.Show("Соединение с сервером временно недоступно, попробуйте повторить попытку немного позже");
                }
            }
            //альтернативная ветка кода, которая отработает в случае, если не заполнены все необходимые поля на форме
            else
            {
                //вызов вспомагательного окна с сообщением об ошибке в случае ее поимки
                MessageBox.Show("Заполните все данные перед добавлением товара");
            }
        }

        //функция класса описывающая работу кнопки редактирования товара
        private void edit_button_Click(object sender, RoutedEventArgs e)
        {
            //проверка заполненности всех полей, необходимых для редактирования товара
            if (tb_name.Text != "" && tb_price.Text != "" && tb_weight.Text != "" && Photo1.Source != null && Photo2.Source != null && Photo3.Source != null && Photo4.Source != null)
            {
                //проверка нулевой ссылки и вылета за пределы массива
                try
                {
                    //создание переменной для соединения с сервером
                    Service1Client proxy = new Service1Client();
                    //захват индекса, который пользователь выбрал в listbox
                    int i = this.listBox.SelectedIndex;
                    //проверка корректности введенных пользоветелем данных на возможность их конвертирования в формат децимал
                    try
                    {
                        //присвоение переменной price конвертированного из поля цены значения, если пользователей введено не корректное значение,  отработает соответствующий блок catch ниже по коду
                        decimal price = Convert.ToDecimal(this.tb_price.Text);
                        //присвоение переменной weight конвертированного из поля вес значения, если пользователей введено не корректное значение,  отработает соответствующий блок catch ниже по коду
                        decimal weight = Convert.ToDecimal(this.tb_weight.Text);
                        //вызов функции на сервере редактирующей товар в базе данных  
                        int id = 0;
                        foreach (proba p in pr.list)
                        {
                            if (p.metall == met.list[Metal_CB.SelectedIndex].Id && p.number == pr_comboBox.SelectedItem.ToString())
                            {
                                id = p.id-1;
                            }
                        }
                        proxy.edit_good(new Good
                        {
                            Id = geter.list[i].Id,
                            Name = tb_name.Text.ToString(),
                            Category = gr.list[Group_CB.SelectedIndex].Name,
                            c_k = gr.list[Group_CB.SelectedIndex].c_k,
                            Metall = met.list[Metal_CB.SelectedIndex].Name,
                            m_k = met.list[Metal_CB.SelectedIndex].m_k,
                            proba = pr.list[id].number,
                            stone = st.list[st_comboBox.SelectedIndex].Name,
                            s_k = st.list[st_comboBox.SelectedIndex].s_k,
                            Describe = new TextRange(richTextBox.Document.ContentStart,
                            richTextBox.Document.ContentEnd).Text,
                            Price = price,
                            Weight = weight,
                            Photo1 = ImageSourceToBytes(Photo1.Source),
                            Photo2 = ImageSourceToBytes(Photo2.Source),
                            Photo3 = ImageSourceToBytes(Photo3.Source),
                            Photo4 = ImageSourceToBytes(Photo4.Source)
                        });
                       
                        //вызов функции класса, очищающей значения перед их обновленным заполнением
                        clear();
                        //вызов функции класса, которяа проводит инициализацию данных класса
                        init2(proxy);
                    }
                    //поимка исключения связанного с ошибкой формата ввода данных
                    catch (FormatException)
                    {
                        //вызов вспомагательного окна с сообщением об ошибке в случае ее поимки
                        MessageBox.Show("Некорректно введенные данные");
                    }
                    //поимка исключения связанного с ошибкой допустимой границы ввода данных
                    catch (OverflowException)
                    {
                        //вызов вспомагательного окна с сообщением об ошибке в случае ее поимки
                        MessageBox.Show("Слишком большое значение");
                    }
                    //закрытие соединения с сервером
                    proxy.Close();


                }
                //поимка исключения связанного с вылетом за пределы массива
                catch (ArgumentOutOfRangeException)
                {
                    //вызов вспомагательного окна с сообщением об ошибке в случае ее поимки
                    MessageBox.Show("Выберите значение из списка");
                }
                //поимка исключения связанного с нулевой ссылкой
                catch (ArgumentNullException)
                {
                    //вызов вспомагательного окна с сообщением об ошибке в случае ее поимки
                    MessageBox.Show("Выберите значение из списка");
                }
                //перехват ошибок соединения с сервером
                catch (CommunicationException)
                {
                    MessageBox.Show("Соединение с сервером временно недоступно, попробуйте повторить попытку немного позже");
                }
            }
            //альтернативная ветка кода, которая отработает в случае, если не заполнены все необходимые поля на форме
            else
            {
                //вызов вспомагательного окна с сообщением об ошибке в случае ее поимки
                MessageBox.Show("Заполните все данные перед редактированием товара");
            }
        }

        //функция класса описывающая работу кнопки удаления товара
        private void del_button_Click(object sender, RoutedEventArgs e)
        {
            //проверка нулевой ссылки и вылета за пределы массива
            try
            {
                //создание переменной для соединения с сервером
                Service1Client proxy = new Service1Client();
                //захват индекса, который пользователь выбрал в listbox
                int id = geter.list[this.listBox.SelectedIndex].Id;
                //вызов функции сервера, которая производит удаление товара
                proxy.del_good(id);
                //вызов функции класса, очищающей значения полей класса
                clear();
                //вызов функции класса , производящей основую инициализацию полей класса
                init2(proxy);
                //закрытие соединения с сервером
                proxy.Close();
            }
            //поимка исключения связанного с вылетом за пределы массива
            catch (ArgumentOutOfRangeException)
            {
                //вызов вспомагательного окна с сообщением об ошибке в случае ее поимки
                MessageBox.Show("Выберите значение из списка");
            }
            //поимка исключения связанного с нулевой ссылкой
            catch (ArgumentNullException)
            {
                //вызов вспомагательного окна с сообщением об ошибке в случае ее поимки
                MessageBox.Show("Выберите значение из списка");
            }
            //перехват ошибок соединения с сервером
            catch (CommunicationException)
            {
                MessageBox.Show("Соединение с сервером временно недоступно, попробуйте повторить попытку немного позже");
            }

        }
        // функция класса описывающая работу полей класса в случае выбора пользователем значения из перечня listbox
        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //проверка, устраняющая ошибки связанные с некорректным индексом
            if (this.listBox.SelectedIndex != -1)
            {
                //присвоение значению поля "имя" имени выбранного в listbox товара
                this.tb_name.Text = geter.list[this.listBox.SelectedIndex].Name;
                //присвоение значению поля "цена" цены выбранного в listbox товара
                this.tb_price.Text = geter.list[this.listBox.SelectedIndex].Price.ToString();
                //присвоение значению поля "вес" веса выбранного в listbox товара
                this.tb_weight.Text = geter.list[this.listBox.SelectedIndex].Weight.ToString();
                //присвоение значению поля "категория" категории выбранного в listbox товара
                this.Group_CB.SelectedItem = geter.list[this.listBox.SelectedIndex].Category;
                //присвоение значению поля "камни" категории выбранного в listbox товара
                this.st_comboBox.SelectedItem = geter.list[this.listBox.SelectedIndex].stone;
                //присвоение значению поля "металл" металла выбранного в listbox товара
                this.Metal_CB.SelectedItem = geter.list[this.listBox.SelectedIndex].Metall;
                //присвоение значению поля "проба" категории выбранного в listbox товара
                this.pr_comboBox.SelectedItem = geter.list[this.listBox.SelectedIndex].proba;
                this.richTextBox.Document.Blocks.Clear();
                this.richTextBox.Document.Blocks.Add(new Paragraph(new Run(this.geter.list[listBox.SelectedIndex].Describe)));
                //присвоение значению поля "фото1" фото1 выбранного в listbox товара
                this.Photo1.Source = LoadImage(geter.list[this.listBox.SelectedIndex].Photo1);
                //присвоение значению поля "фото2" фото2 выбранного в listbox товара
                this.Photo2.Source = LoadImage(geter.list[this.listBox.SelectedIndex].Photo2);
                //присвоение значению поля "фото3" фото3 выбранного в listbox товара
                this.Photo3.Source = LoadImage(geter.list[this.listBox.SelectedIndex].Photo3);
                //присвоение значению поля "фото4" фото4 выбранного в listbox товара
                this.Photo4.Source = LoadImage(geter.list[this.listBox.SelectedIndex].Photo4);
            }
        }
        //функция класса обрабатывающая кнопку, позволяющую загрузить первую фотографию товара 
        private void btn_foto1_Click(object sender, RoutedEventArgs e)
        {
            //получения пути к выбранной пользователем фотографии товара
            string path = getpath();
            //проверка на корректность выбора пути
            if (path != "")
            {
                //присвоение полю фото1 выбранной фотографии с помощью функции класса LoadImage
                this.Photo1.Source = LoadImage(File.ReadAllBytes(path));
            }

        }
        //функция класса обрабатывающая кнопку, позволяющую загрузить вторую фотографию товара 
        private void btn_foto2_Click(object sender, RoutedEventArgs e)
        {
            //получения пути к выбранной пользователем фотографии товара
            string path = getpath();
            //проверка на корректность выбора пути
            if (path != "")
            {
                //присвоение полю фото2 выбранной фотографии с помощью функции класса LoadImage
                this.Photo2.Source = LoadImage(File.ReadAllBytes(path));
            }
        }
        //функция класса обрабатывающая кнопку, позволяющую загрузить третью фотографию товара 
        private void btn_foto3_Click(object sender, RoutedEventArgs e)
        {
            //получения пути к выбранной пользователем фотографии товара
            string path = getpath();
            //проверка на корректность выбора пути
            if (path != "")
            {
                //присвоение полю фото3 выбранной фотографии с помощью функции класса LoadImage
                this.Photo3.Source = LoadImage(File.ReadAllBytes(path));
            }
        }
        //функция класса обрабатывающая кнопку, позволяющую загрузить четвертую фотографию товара 
        private void btn_foto4_Click(object sender, RoutedEventArgs e)
        {
            //получения пути к выбранной пользователем фотографии товара
            string path = getpath();
            //проверка на корректность выбора пути
            if (path != "")
            {
                //присвоение полю фото4 выбранной фотографии с помощью функции класса LoadImage
                this.Photo4.Source = LoadImage(File.ReadAllBytes(path));
            }
        }

        private void Metal_CB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //обновление данных
            this.pr_comboBox.Items.Clear();
            //перебор элементов
            for (int i = 1; i < pr.list.Count; i++)
            {
                if (pr.list[i].metall == this.Metal_CB.SelectedIndex+1)
                    this.pr_comboBox.Items.Add(pr.list[i].number.ToString());
            }
            //установка индекса по умолчанию
            this.pr_comboBox.SelectedIndex = 0;
        }

        private void refresh_button_Click(object sender, RoutedEventArgs e)
        {
            init();
        }
    }
}
