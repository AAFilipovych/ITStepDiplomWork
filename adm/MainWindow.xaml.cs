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

namespace adm
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //конструктор класса
        public MainWindow()
        {
            //инициализация главного окна
            InitializeComponent();
        }       
        //фукнция класса для обработки кнопки корректировки групп
        private void group_button_Click(object sender, RoutedEventArgs e)
        {
            //создание нового окна для корректировки групп
            Groups_corr a = new Groups_corr();
            //отображение нового окна для корректировки групп
            //a.Show();
            a.ShowDialog();
        }
        //фукнция класса для обработки кнопки корректировки металла
        private void metall_button_Click(object sender, RoutedEventArgs e)
        {
            //создание нового окна для корректировки металла
            Metall_cor a = new Metall_cor();
            //отображение нового окна для корректировки металла
            //a.Show();
            a.ShowDialog();
        }
        //фукнция класса для обработки кнопки корректировки товаров
        private void goods_button_Click(object sender, RoutedEventArgs e)
        {
            //создание нового окна для корректировки товаров
            Goods_corr a = new Goods_corr();
            //отображение нового окна для корректировки товаров
            //a.Show();
            a.ShowDialog();
        }
        //кнопка обработки заказов
        private void oder_button_Click(object sender, RoutedEventArgs e)
        {
            Order_window a = new Order_window();
            //a.Show();
            a.ShowDialog();
           
        }

        private void history_button_Click(object sender, RoutedEventArgs e)
        {
            History_window a = new History_window();
            //a.Show();
            a.ShowDialog();
        }

        private void proba_button_Click(object sender, RoutedEventArgs e)
        {
            Proba_corr a = new Proba_corr();
            a.ShowDialog();
        }

        private void stone_button_Click(object sender, RoutedEventArgs e)
        {
            Stone_corr a = new Stone_corr();
            a.ShowDialog();
        }
    }
}
