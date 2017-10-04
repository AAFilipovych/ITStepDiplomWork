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
using User.ServiceReference1;
using System.ServiceModel;
using ClassLibrary1;

namespace User
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void login_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string login = this.login_textBox.Text.ToString();
                string password = this.pass_textBox.Text.ToString();
                MainWindow a = (MainWindow)this.Owner;
                //a.do_login(this.login_textBox.Text.ToString(), this.pass_textBox.Text.ToString());
                a.do_login(login, password);
                this.Close();
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

        private void reg_button_Click(object sender, RoutedEventArgs e)
        {
            Registration a = new Registration();
            a.Owner = this;
            a.ShowDialog();
        }
    }
}
