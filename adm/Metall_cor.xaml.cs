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
    /// Логика взаимодействия для Metall_cor.xaml
    /// </summary>
    public partial class Metall_cor : Window
    {
        //создание переменной для соединения с сервером
        
        //создание массива, который будет хранить идентификационные номера металлов
        List<int> id;
        //создание массива для хранения данных полученных с сервера
        Metalls met;
        //конструктор класса
        public Metall_cor()
        {
            //инициализация главного окна
            InitializeComponent();
            //инициализация полей класса
            init();
        }
        //функция класса инициализирующая поля класса
        public void init()
        {
            try {
                Service1Client proxy = new Service1Client();
                
                //инициализация массива для хранения данных полученных с сервера
                id = new List<int>();
                //инициализация переменной для соединения с сервером
                proxy = new Service1Client();
                //инициализация массива для хранения данных полученных с сервера
                met = new Metalls();
                //получение во внутренний массив выборки из базы данных сервера
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
            //очистка listbox для последующего заполнения новыми данными
            this.listBox.Items.Clear();
            met = proxy.get_metall();
            //постепенный перебор всех элементов внутреннего массива
            foreach (Metall m in met.list)
            {
                //заполнение listbox элементами  из внутреннего массива
                this.listBox.Items.Add(m.Name.ToString());
                //заполнение массива идентификационых номеров
                id.Add(m.Id);
            }
        }
        //функция класса проверяющая дубликаты названий металлов.
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
        //функция обработки события нажатия на кнопку добавления металла
        private void add_button_Click(object sender, RoutedEventArgs e)
        {
            //проверка исключений
            try
            {
                //проверка на корректность ввода названия металла пользователем
                if (this.textBox.Text.ToString() != "" && check())
                {
                    Service1Client proxy = new Service1Client();
                    //вызов функции сервера добавляющей введенный пользователем металл в базу данных
                    Transliter a = new Transliter();
                    proxy.add_metall(this.textBox.Text.ToString(),a.converter(this.textBox.Text.ToString()));
                    //вызов функции класса инициализирующей поля класса                 
                    init2(proxy);
                    proxy.Close();
                }

            }
            //перехват исключения нулевого аргумента
            catch (ArgumentNullException)
            {
                //вызов вспомагательного окна с сообщением об ошибке в случае ее перехвата
                MessageBox.Show("Введите название металла");
            }
            //перехват ошибок соединения с сервером
            catch (CommunicationException)
            {
                MessageBox.Show("Соединение с сервером временно недоступно, попробуйте повторить попытку немного позже");
            }
        }
        //функция класса обрабатывающая кнопку редактирования металла
        private void edit_button_Click(object sender, RoutedEventArgs e)
        {
            //проверка на корректность ввода названия металла пользователем
            if (this.textBox.Text.ToString() != "" && check())
            {
                try
                {
                    Service1Client proxy = new Service1Client();
                    //вызов функции сервера редактирующей выбранный пользователем металл в базу данных
                    Transliter a = new Transliter();
                    proxy.edit_metall(met.list[this.listBox.SelectedIndex].Id, this.textBox.Text.ToString(), a.converter(this.textBox.Text.ToString()));
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
        //функция класса обрабатывающая кнопку удаления металла
        private void del_button_Click(object sender, RoutedEventArgs e)
        {
            //проверка исключений на вылет за пределы массива и нулевой ссылки
            try
            {
                Service1Client proxy = new Service1Client();
                //вызов функции сервера для удаления выбранного пользователем металла
                proxy.del_metall(met.list[this.listBox.SelectedIndex].Id);
                //вызов функции класса инициализирующей поля класса 
                init2(proxy);
                proxy.Close();

            }
            //перехват исключения аргумента вне массива
            catch (ArgumentOutOfRangeException)
            {
                //вызов вспомагательного окна с сообщением об ошибке в случае ее перехвата
                MessageBox.Show("Некорректно введенный индекс");
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
            //перехват ошибок связанных с нулевой ссылкой в случае удаления элемента из listbox
            catch (NullReferenceException)
            {
                //обработка ошибки в случае ее перехвата
                this.textBox.Text = this.textBox.Text;
            }
        }
    }
}







