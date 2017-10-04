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

namespace User
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (this.FNtextBox.Text != null && this.SNtextBox.Text != null && this.TNtextBox.Text != null && new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd).Text != null && this.textBox_log.Text != null && this.textBox_pass.Text != null)
            {
                try
                {
                    long phone;
                    Service1Client proxy = new Service1Client();
                    phone = Convert.ToInt32(this.phtextBox.Text);
                    proxy.add_user(this.FNtextBox.Text, this.SNtextBox.Text, this.TNtextBox.Text, phone, new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd).Text, this.textBox_log.Text, this.textBox_pass.Text);
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
            else
            {
                MessageBox.Show("Не все поля заполнены");
            }
        }
    }
}