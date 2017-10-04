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
    /// Логика взаимодействия для Proba_corr.xaml
    /// </summary>
    public partial class Proba_corr : Window
    {
        Metalls met;
        probas pr;
        public Proba_corr()
        {
            InitializeComponent();
            met = new Metalls();
            pr = new probas();
            init();
        }

        void init()
        {
            //инициализация переменной для соединения с сервером
            Service1Client proxy = new Service1Client();

            //получение во внутренний массив выборки из базы данных сервера
            met = proxy.get_metall();
            foreach (Metall m in met.list)
            {
                this.metall_comboBox.Items.Add(m.Name);
            }
           

            init2(proxy);
            proxy.Close();
        }
        void init2(Service1Client proxy)
        {
            try
            {
                //очистка listbox для последующего заполнения новыми данными
                this.listBox.Items.Clear();
               
                pr = proxy.get_proba();
                this.metall_comboBox.SelectedIndex = 0;

            }
            //перехват ошибок соединения с сервером
            catch (CommunicationException)
            {
                MessageBox.Show("Соединение с сервером временно недоступно, попробуйте повторить попытку немного позже");
            }
        }
        private void add_button_Click(object sender, RoutedEventArgs e)
        {
            if (this.textBox.Text.ToString() != "" && check())
            {
                try
                {
                    int i = metall_comboBox.SelectedIndex;
                    int id_met = this.met.list[metall_comboBox.SelectedIndex].Id;
                    string number = this.textBox.Text;
                    Service1Client proxy = new Service1Client();
                    proxy.add_proba(new proba { number = number, metall = id_met });
                    init2(proxy);
                    proxy.Close();
                    this.metall_comboBox.SelectedIndex = i;
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

        }

        private void edit_button_Click(object sender, RoutedEventArgs e)
        {
            //проверка на корректность ввода названия металла пользователем
            if (this.textBox.Text.ToString() != "" && check())
            {
                try
                {
                    //инициализация переменной для соединения с сервером
                    Service1Client proxy = new Service1Client();
                    //вызов функции сервера редактирующей выбранный пользователем металл в базу данных
                    int id=0;
                    foreach (proba p in pr.list)
                    {
                        if (p.metall==met.list[metall_comboBox.SelectedIndex].Id&&p.number==listBox.SelectedItem.ToString())
                        {
                             id=p.id;
                        }                        
                    }                    
                    proxy.edit_proba(id, this.textBox.Text.ToString());
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

        private void del_button_Click(object sender, RoutedEventArgs e)
        {
            //проверка исключений
            try
            {
                Service1Client proxy = new Service1Client();                
                //вызов функции сервера для удаления выбранного пользователем группы
                proxy.del_proba(pr.list[this.listBox.SelectedIndex].id);
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

        private void metall_comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.listBox.Items.Clear();

            for (int i = 0; i < pr.list.Count; i++)
            {
                if (pr.list[i].metall == this.metall_comboBox.SelectedIndex + 1)
                    this.listBox.Items.Add(pr.list[i].number.ToString());
            }
        }
    }
}
