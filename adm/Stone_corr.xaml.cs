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
using ClassLibrary1;
using System.ServiceModel;

namespace adm
{
    /// <summary>
    /// Логика взаимодействия для Stone_corr.xaml
    /// </summary>
    public partial class Stone_corr : Window
    {
        //создание массива для хранения данных полученных с сервера
        Stones st;
        //конструктор класса
        public Stone_corr()
        {
            //инициализация главного окна
            InitializeComponent();
            //инициализация массива для хранения данных полученных с сервера
            st = new Stones();
            //инициализация полей класса
            init();
        }
        public void init()
        {
            try
            {
                //инициализация переменной для соединения с сервером
                Service1Client proxy = new Service1Client();
                init2(proxy);
                proxy.Close();
            }
            //перехват ошибок соединения с сервером
            catch (CommunicationException)
            {
                MessageBox.Show("Соединение с сервером временно недоступно, попробуйте повторить попытку немного позже");
            }
        }
        //функция класса инициализирующая поля класса
        public void init2(Service1Client proxy)
        {
            //очистка listbox для последующего заполнения новыми данными
            this.listBox.Items.Clear();
            //получение во внутренний массив выборки из базы данных сервера
            st = proxy.get_stone();
            //постепенный перебор всех элементов внутреннего массива
            foreach (Stone stone in st.list)
            {
                //заполнение listbox элементами  из внутреннего массива
                this.listBox.Items.Add(stone.Name.ToString());
            }
        }
        //функция класса проверяющая дубликаты названий групп
        bool check()
        {
            //промежуточная переменная, по умолчанию присвоено значение true, что означает возможность применения имени введенного пользователем в поле имя
            bool check = true;
            //цикл по всем значениям listbox 
            for (int i = 0; i < this.listBox.Items.Count; i++)
            {
                //проверка соответствия каждого значения listbox на похожесть введенного пользователем в поле имя
                if (this.textBox.Text.ToString() == listBox.Items[i].ToString())
                {
                    //в случае соответствия промежуточной переменной присваивается значение false, что означает запрет на имя введенное пользователем в связи с тем, что такое уже существует
                    check = false;
                    //прерывание цикла в случае нахождения хотя бы одного первого совпадения
                    break;
                }
            }
            //возврат промежуточной переменной
            return check;
        }
        //функция обработки события нажатия на кнопку добавления
        private void add_button_Click(object sender, RoutedEventArgs e)
        {
            //проверка исключения нулевого аргумента
            try
            {
                //проверка на корректность ввода названия камня пользователем
                if (this.textBox.Text.ToString() != "" && check())
                {
                    try
                    {
                        Service1Client proxy = new Service1Client();
                        //вызов функции сервера добавляющей введенноый пользователем камень в базу данных
                        Transliter a = new Transliter();
                        string convert = a.converter(this.textBox.Text.ToString());
                        proxy.add_Stone(new Stone { Name= this.textBox.Text.ToString() ,s_k=convert});
                        //вызов функции класса инициализирующей поля класса  
                        init2(proxy);
                        proxy.Close();
                    }
                    //перехват ошибок соединения с сервером
                    catch (CommunicationException)
                    {
                        MessageBox.Show("Соединение с сервером временно недоступно, попробуйте повторить попытку немного позже");
                    }
                }
            }
            //перехват исключения нулевого аргумента
            catch (ArgumentNullException)
            {
                //вызов вспомагательного окна с сообщением об ошибке в случае ее перехвата
                MessageBox.Show("Введите название группы");
            }
            //перехват ошибок соединения с сервером
            catch (CommunicationException)
            {
                MessageBox.Show("Соединение с сервером временно недоступно, попробуйте повторить попытку немного позже");
            }
        }
        //функция класса обрабатывающая кнопку редактирования группы
        private void edit_button_Click(object sender, RoutedEventArgs e)
        {
            //проверка на корректность ввода названия группы пользователем
            if (this.textBox.Text.ToString() != "" && check())
            {
                try
                {
                    Service1Client proxy = new Service1Client();
                    //вызов функции сервера редактирующей выбранную пользователем группу в базу данных
                    Transliter a = new Transliter();
                    string convert = a.converter(this.textBox.Text.ToString());
                    proxy.edit_stone(this.listBox.SelectedIndex + 1, this.textBox.Text.ToString(),convert);
                    //вызов функции класса инициализирующей поля класса  
                    init2(proxy);
                    proxy.Close();
                }
                //перехват ошибок соединения с сервером
                catch (CommunicationException)
                {
                    MessageBox.Show("Соединение с сервером временно недоступно, попробуйте повторить попытку немного позже");
                }
            }
        }
        //функция класса обрабатывающая кнопку удаления группы
        private void del_button_Click(object sender, RoutedEventArgs e)
        {
            //проверка исключений
            try
            {
                Service1Client proxy = new Service1Client();
                //присвоение промежуточной переменной выбранного индекса
                int i = st.list[this.listBox.SelectedIndex].Id;
                //вызов функции сервера для удаления выбранного пользователем группы
                proxy.del_stone(i);
                //вызов функции класса инициализирующей поля класса              
                init2(proxy);
                proxy.Close();
            }
            //перехват исключения аргумента вне массива
            catch (ArgumentOutOfRangeException)
            {
                //вызов вспомагательного окна с сообщением об ошибке в случае ее перехвата
                MessageBox.Show("Выберите значение из списка");
            }
            //перехват исключения нулевого аргумента
            catch (ArgumentNullException)
            {
                //вызов вспомагательного окна с сообщением об ошибке в случае ее перехвата
                MessageBox.Show("Выберите значение из списка");
            }
            //перехват ошибок соединения с сервером
            catch (CommunicationException)
            {
                MessageBox.Show("Соединение с сервером временно недоступно, попробуйте повторить попытку немного позже");
            }
        }
        //функция класса обрабатывающая выбор пользователем элемента в listbox
        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //проверка на нулевую ссылку в случае удаления элемента listbox
            try
            {
                //отображения в textbox имени выбранного пользователем элемента listbox
                this.textBox.Text = this.listBox.SelectedItem.ToString();
            }
            // перехват ошибок связанных с нулевой ссылкой в случае удаления элемента из listbox
            catch (NullReferenceException)
            {
                //обработка ошибки в случае ее перехвата
                this.textBox.Text = this.textBox.Text;
            }
        }
    }
}