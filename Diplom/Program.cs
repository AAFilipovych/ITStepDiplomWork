using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Drawing;
using ClassLibrary1;
using System.Data.Entity;

namespace Diplom
{
    class Program
    {
        static void Main(string[] args)
        {
            //using (OrderContext db = new OrderContext())
            //{
            //    db.Database.Delete();
            //}

            //инициализация базы данных           
            Database.SetInitializer(new GoodsInitializer());
            Database.SetInitializer(new ParamsInitializer());
            //создание сервисхоста
            ServiceHost sh = new ServiceHost(typeof(Service1));
            //открытие соединения
            sh.Open();
            //сообщение о старте сервера
            Console.WriteLine("Сервер запущен");
            //сообщение о варианте остановки сервера
            Console.Write("Для остановки сервера нажмите enter");
            //пауза в коде, которая снимается нажатием кнопки enter
            Console.ReadLine();
            //закрытие соединения
            sh.Close();
            //сообщение об остановке сервера
            Console.WriteLine("Сервер остановлен");
        }
    }
}

