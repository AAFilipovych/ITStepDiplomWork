using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Data.Entity;
using ClassLibrary1;

namespace Diplom
{
    class Service1 : IService1
    {
        //функция класса позволяющая записать новый заказ в базу данных
        public void getorder(Order order)
        {
            //использование GoodContext для работы с базой данных
            using (OrderContext db = new OrderContext())
            {
                //добавление заказа в базу данных с параметрами переданными в функцию
                db.orders.Add(new Order { FirstName = order.FirstName, SecondName = order.SecondName, ThirdName = order.ThirdName, Phone = order.Phone, when_buy = order.when_buy });
                //сохранение изменений в базе данных
                db.SaveChanges();
            }
        }
        //функция позволяющая выбрать данные товаров из базы, по выбраным пользователем параметрам.
        public Goods ShowGoods(Transfer s)
        {
            //создание нового объекта класса Goods 
            Goods a = new Goods();
            //использование GoodContext для работы с базой данных
            using (GoodContext db = new GoodContext())
            {
                //выборка данных из базы с записью в промежуточную пеменную
                //var goods = db.goods.Where(p => p.Metall == s.Metall && p.Category == s.Category && p.Price >= s.PriceLOW && p.Price <= s.PriceUP && p.Weight >= s.WeightLOW && p.Weight <= s.WeightUP);
                string query = String.Format("SELECT * FROM dbo.goods WHERE m_k LIKE '{0}' AND c_k LIKE '{1}' AND proba LIKE '{2}' AND s_k LIKE '{3}' AND Price BETWEEN '{4}' AND '{5}' AND Weight BETWEEN '{6}' AND '{7}'", s.Metall, s.Category, s.Proba, s.Stone, s.PriceLOW, s.PriceUP, s.WeightLOW, s.WeightUP);

                var goods = db.goods.SqlQuery(query).ToList();
                //перебор каждого элемента в промежуточной переменной
                foreach (Good u in goods)
                {
                    //запись каждого элемента в переменную нового объекта класса Goods, созданного выше
                    a.list.Add(u);
                }
            }
            //возврат переменной Goods с заполенными элементами
            return a;
        }
       
        //выборка всех товаров из базы данных
        public Goods Show()
        {
            //создание нового объекта класса Goods
            Goods a = new Goods();
            //использование GoodContext для работы с базой данных
            using (GoodContext db = new GoodContext())
            {
                //выборка данных из базы с записью в промежуточную пеменную
                var goods = db.goods;
                //перебор каждого элемента в промежуточной переменной
                foreach (Good u in goods)
                {
                    //запись каждого элемента в переменную нового объекта класса Goods, созданного выше
                    a.list.Add(u);
                }
            }
            //возврат переменной Goods с заполенными элементами
            return a;
        }
        public Good get_good(int id)
        {
            Good g = new Good();
            using (GoodContext db = new GoodContext())
            {
                g = db.goods.Find(id);
            }
            return g;
        }
        //функция позволяющая выбрать группы из базы данных
        public Groups get_group()
        {
            //создание нового объекта класса Groups
            Groups gro = new Groups();
            //использование GoodContext для работы с базой данных
            using (GoodContext db = new GoodContext())
            {
                try
                {
                    //выборка данных из базы с записью в промежуточную пеменную
                    var group = db.groups;
                    //перебор каждого элемента в промежуточной переменной

                    foreach (Group gr in group)
                    {
                        //запись каждого элемента в переменную нового объекта класса Groups, созданного выше
                        gro.list.Add(gr);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException);
                }
            }
            //возврат переменной Groups с заполенными элементами
            return gro;
        }

        public Orders get_order()
        {
            //создание нового объекта класса Orders
            Orders ord = new Orders();
            //использование GoodContext для работы с базой данных
            //using (GoodContext db = new GoodContext())
            using (OrderContext db = new OrderContext())
            {
                //выборка данных из базы с записью в промежуточную пеменную
                var orders = db.orders;
                if (orders != null)
                {
                    //перебор каждого элемента в промежуточной переменной
                    foreach (Order o in orders)
                    {
                        //запись каждого элемента в переменную нового объекта класса Groups, созданного выше
                        ord.list.Add(o);
                    }
                }
            }
            //возврат переменной Groups с заполенными элементами
            return ord;
        }
        //функция позволяющая выбрать металлы из базы данных
        public Metalls get_metall()
        {
            //создание нового объекта класса Metalls
            Metalls met = new Metalls();
            //использование GoodContext для работы с базой данных
            using (GoodContext db = new GoodContext())
            {
                //выборка данных из базы с записью в промежуточную пеменную
                var metall = db.metalls;
                //перебор каждого элемента в промежуточной переменной
                foreach (Metall m in metall)
                {
                    //запись каждого элемента в переменную нового объекта класса, созданного выше
                    met.list.Add(m);
                }
            }
            //возврат переменной Metalls с заполенными элементами
            return met;
        }
        public probas get_proba()
        {
            //создание нового объекта класса 
            probas pr = new probas();
            //использование GoodContext для работы с базой данных
            using (ParamContext db = new ParamContext())
            {
                //выборка данных из базы с записью в промежуточную пеменную
                var prob = db.probas;
                //перебор каждого элемента в промежуточной переменной
                foreach (proba p in prob)
                {
                    //запись каждого элемента в переменную нового объекта класса, созданного выше
                    pr.list.Add(p);
                }
            }
            //возврат переменной с заполенными элементами
            return pr;
        }
        public Stones get_stone()
        {
            //создание нового объекта класса 
            Stones ston = new Stones();
            //использование GoodContext для работы с базой данных
            using (ParamContext db = new ParamContext())
            {
                //выборка данных из базы с записью в промежуточную пеменную
                var s = db.stones;
                //перебор каждого элемента в промежуточной переменной
                foreach (Stone st in s)
                {
                    //запись каждого элемента в переменную нового объекта класса, созданного выше
                    ston.list.Add(st);
                }
            }
            //возврат переменной с заполенными элементами
            return ston;
        }
        //функция добавления товаров в базу данных
        public void add_proba(proba x)
        {
            //использование GoodContext для работы с базой данных
            using (ParamContext db = new ParamContext())
            {
                //добавление товара в базу данных с параметрами передаными в функцию
                db.probas.Add(x);
                //сохранение изменений в базе данных
                db.SaveChanges();
            }
        }
        public void add_Stone(Stone x)
        {
            //использование GoodContext для работы с базой данных
            using (ParamContext db = new ParamContext())
            {
                //добавление товара в базу данных с параметрами передаными в функцию
                db.stones.Add(x);
                //сохранение изменений в базе данных
                db.SaveChanges();
            }
        }

        //функция добавления товаров в базу данных
        public void add_good(Good x)
        {
            //использование GoodContext для работы с базой данных
            using (GoodContext db = new GoodContext())
            {
                //добавление товара в базу данных с параметрами передаными в функцию
                db.goods.Add(x);
                //сохранение изменений в базе данных
                db.SaveChanges();
            }
        }
        //функция добавления групп в базу данных
        public void add_group(string name, string c_k)
        {
            //использование GoodContext для работы с базой данных
            using (GoodContext db = new GoodContext())
            {
                //добавление группы в базу данных с параметрами передаными в функцию
                db.groups.Add(new Group { Name = name, c_k = c_k });
                //сохранение изменений в базе данных
                db.SaveChanges();
            }
        }
        //функция добавления металлов в базу данных
        public void add_metall(string name, string m_k)
        {
            //использование GoodContext для работы с базой данных
            using (GoodContext db = new GoodContext())
            {
                // добавление металла в базу данных с параметрами передаными в функцию
                db.metalls.Add(new Metall { Name = name, m_k = m_k });
                //сохранение изменений в базе данных
                db.SaveChanges();
            }
        }
        //функция удаления товаров из базы данных
        public void del_good(int id)
        {
            //использование GoodContext для работы с базой данных
            using (GoodContext db = new GoodContext())
            {
                //создание нового объекта класса Good
                Good good = new Good();
                //проверка на ошибки нулевого аргумента
                try
                {
                    //получение первого элемента из базы данных
                    good = db.goods.Find(id);
                    //проверка перемнной на null значение
                    if (good != null)
                    {
                        //удаление товара из базы данных
                        db.goods.Remove(good);
                        //сохранение изменений в базе данных
                        db.SaveChanges();
                    }

                }
                //перехват ошибок нулевого аргумента
                catch (ArgumentNullException)
                {}
            }
        }
        public void del_proba(int id)
        {
            //использование GoodContext для работы с базой данных
            using (ParamContext db = new ParamContext())
            {
                //создание нового объекта класса Good
                proba prob = new proba();
                //проверка на ошибки нулевого аргумента
                try
                {
                    //получение первого элемента из базы данных
                    prob = db.probas.Find(id);
                    //проверка перемнной на null значение
                    if (prob != null)
                    {
                        //удаление товара из базы данных
                        db.probas.Remove(prob);
                        //сохранение изменений в базе данных
                        db.SaveChanges();
                    }

                }
                //перехват ошибок нулевого аргумента
                catch (ArgumentNullException)
                {}
            }
        }
        public void edit_proba(int id, string number)
        {
            //использование GoodContext для работы с базой данных
            using (ParamContext db = new ParamContext())
            {
                //создание нового объекта класса Good
                proba prob = new proba();
                //проверка на ошибки нулевого аргумента
                try
                {
                    //получение первого элемента из базы данных
                    prob = db.probas.Find(id);
                    using (GoodContext db2 = new GoodContext())
                    {
                        //проверка на ошибки нулевой ссылки
                        try
                        {
                            //выборка всех товаров металла, который будет отредактирован
                            var goods = db2.goods.Where(o => o.proba == prob.number);
                            //проверка результата на null
                            if (goods != null)
                            {
                                //перебор всех элементов в результате выборки goods
                                foreach (Good p in goods)
                                {
                                    p.proba = number;             
                                }
                            }
                            db2.SaveChanges();
                        }
                        //перехват ошибок нулевого аргумента
                        catch (ArgumentNullException)
                        { }
                        //перехват ошибок нулевой ссылки
                        catch (NullReferenceException)
                        { }

                    }
                    //проверка перемнной на null значение
                    if (prob != null)
                    {
                        prob.number = number;
                       
                        db.SaveChanges();
                    }

                }
                //перехват ошибок нулевого аргумента
                catch (ArgumentNullException)
                {}
            }
        }

        public void del_stone(int id)
        {
            //использование GoodContext для работы с базой данных
            using (ParamContext db = new ParamContext())
            {
                //создание нового объекта класса Good
                Stone s = new Stone();
                //проверка на ошибки нулевого аргумента
                try
                {
                    //получение первого элемента из базы данных
                    s = db.stones.Find(id);
                    //использование GoodContext для работы с базой данных
                    using (GoodContext db2 = new GoodContext())
                    {                        
                            //проверка на ошибки нулевой ссылки
                            try
                            {
                                //выборка всех товаров металла, который будет отредактирован
                                var goods = db2.goods.Where(o => o.stone == s.Name);
                                //проверка результата на null
                                if (goods != null)
                                {
                                    //перебор всех элементов в результате выборки goods
                                    foreach (Good p in goods)
                                    {
                                        p.stone = "";
                                        p.s_k = "";
                                    }
                                }
                                db2.SaveChanges();
                            }
                            //перехват ошибок нулевого аргумента
                            catch (ArgumentNullException)
                            { }
                            //перехват ошибок нулевой ссылки
                            catch (NullReferenceException)
                            { }
                        
                    }
                    //проверка перемнной на null значение
                    if (s != null)
                    {
                        //удаление товара из базы данных
                        db.stones.Remove(s);
                        //сохранение изменений в базе данных
                        db.SaveChanges();
                    }

                }
                //перехват ошибок нулевого аргумента
                catch (ArgumentNullException)
                {}
            }
        }
        //функция удаления групп из базы данных
        public void del_group(int id)
        {
            //использование GoodContext для работы с базой данных
            using (GoodContext db = new GoodContext())
            {
                //создание нового объекта класса Group
                Group group = new Group();
                //проверка на ошибки нулевого аргумента
                try
                {
                    //получение элемента группы из базы по идентификационному номеру
                    group = db.groups.Find(id);
                    //проверка полученного результата на null
                    if (group != null)
                    {
                        //проверка на ошибки нулевой ссылки
                        try
                        {
                            //выборка всех товаров группы, которая будет удалена
                            var goods = db.goods.Where(o => o.Category == group.Name.ToString());
                            //проверка результата на null
                            if (goods != null)
                            {
                                //перебор всех элементов в результате выборки goods
                                foreach (Good p in goods)
                                {
                                    //удаление каждого товара, который находится в группе, полежащей удалению
                                    db.goods.Remove(p);
                                }
                            }
                        }
                        //перехват ошибки нулевой ссылки
                        catch (NullReferenceException)
                        {

                        }
                    }
                    //удаление выборанной пользователем группы
                    db.groups.Remove(group);
                    //сохранение изменений в базе данных
                    db.SaveChanges();
                }
                //перехват ошибок нулевого аргумента
                catch (ArgumentNullException)
                {}
                //перехват ошибок нулевой ссылки
                catch (NullReferenceException)
                {}
            }
        }
        //функция удаления металлов из базы данных
        public void del_metall(int id)
        {
            //использование GoodContext для работы с базой данных
            using (GoodContext db = new GoodContext())
            {
                //создание нового объекта класса Metall 
                Metall met = new Metall();
                //проверка на ошибки нулевой ссылки
                try
                {
                    //получение элемента группы из базы по идентификационному номеру
                    met = db.metalls.Find(id);
                    //проверка полученного результата на null
                    if (met != null)
                    {
                        //выборка всех товаров из этого металла, которая будет удалена
                        var goods = db.goods.Where(o => o.Metall == met.Name.ToString());
                        //проверка результата на null
                        if (goods != null)
                        {
                            //перебор всех элементов в результате выборки goods
                            foreach (Good p in goods)
                            {
                                //удаление каждого товара, который находится в группе, полежащей удалению
                                db.goods.Remove(p);
                            }
                        }
                        //удаление выборанного пользователем металла
                        db.metalls.Remove(met);
                        //сохранение изменений в базе данных
                        db.SaveChanges();
                    }
                    using (ParamContext db2 = new ParamContext())
                    {
                        //проверка на ошибки нулевого аргумента
                        try
                        {
                            var probas = db2.probas.Where(p => p.metall == id);
                            //получение первого элемента из базы данных

                            //проверка перемнной на null значение
                            if (probas != null)
                            {
                                foreach (proba p in probas)
                                {
                                    //удаление товара из базы данных
                                    db2.probas.Remove(p);
                                }
                                //сохранение изменений в базе данных
                                db2.SaveChanges();
                            }

                        }
                        //перехват ошибок нулевого аргумента
                        catch (ArgumentNullException)
                        {}
                    }
                }
                //перехват ошибок нулевого аргумента
                catch (ArgumentNullException)
                {}
                //перехват ошибок нулевой ссылки
                catch (NullReferenceException)
                {}
            }
        }
        //функция редактирования товаров
        public void edit_good(Good good)
        {
            //использование GoodContext для работы с базой данных
            using (GoodContext db = new GoodContext())
            {
                //проверка на ошибки нулевой ссылки
                try
                {
                    //получение элемента по идентификационному номеру
                    var goods = db.goods.FirstOrDefault(o => o.Id == good.Id);
                    //проверка на null значение
                    if (goods != null)
                    {
                        //присвоение товару в базе данных параметра переданного через аргумент функции, который выбран пользователем в форме
                        goods.Metall = good.Metall;
                        //присвоение товару в базе данных параметра переданного через аргумент функции, который выбран пользователем в форме
                        goods.m_k = good.m_k;
                        //присвоение товару в базе данных параметра переданного через аргумент функции, который выбран пользователем в форме
                        goods.Category = good.Category;
                        //присвоение товару в базе данных параметра переданного через аргумент функции, который выбран пользователем в форме
                        goods.c_k = good.c_k;
                        //присвоение товару в базе данных параметра переданного через аргумент функции, который выбран пользователем в форме
                        goods.proba = good.proba;
                        //присвоение товару в базе данных параметра переданного через аргумент функции, который выбран пользователем в форме
                        goods.stone = good.stone;
                        //присвоение товару в базе данных параметра переданного через аргумент функции, который выбран пользователем в форме
                        goods.s_k = good.s_k;
                        //присвоение товару в базе данных параметра переданного через аргумент функции, который выбран пользователем в форме
                        goods.Name = good.Name;
                        //присвоение товару в базе данных параметра переданного через аргумент функции, который выбран пользователем в форме
                        goods.Photo1 = good.Photo1;
                        //присвоение товару в базе данных параметра переданного через аргумент функции, который выбран пользователем в форме
                        goods.Photo2 = good.Photo2;
                        //присвоение товару в базе данных параметра переданного через аргумент функции, который выбран пользователем в форме
                        goods.Photo3 = good.Photo3;
                        //присвоение товару в базе данных параметра переданного через аргумент функции, который выбран пользователем в форме
                        goods.Photo4 = good.Photo4;
                        //присвоение товару в базе данных параметра переданного через аргумент функции, который выбран пользователем в форме
                        goods.Price = good.Price;
                        //присвоение товару в базе данных параметра переданного через аргумент функции, который выбран пользователем в форме
                        goods.Weight = good.Weight;
                        //сохранение изменений в базе данных
                        db.SaveChanges();
                    }
                }
                //перехват ошибок нулевого аргумента
                catch (ArgumentNullException)
                {}
                //перехват ошибок нулевой ссылки
                catch (NullReferenceException)
                {}
            }
        }
        //функция редактирования групп из базы данных
        public void edit_group(int id, string name, string c_k)
        {
            //создание объекта класса Group
            Group group = new Group();
            //использование GoodContext для работы с базой данных
            using (GoodContext db = new GoodContext())
            {
                //получение первого элемента из базы данных
                group = db.groups.Find(id);
                //проверка на ошибки нулевой ссылки
                try
                {
                    //выборка всех товаров группы, которая будет отредактирована
                    var goods = db.goods.Where(o => o.Category == group.Name.ToString());
                    //проверка результата на null
                    if (goods != null)
                    {
                        //перебор всех элементов в результате выборки goods
                        foreach (Good p in goods)
                        {
                            //присвоение всем товарам, которые относились к отредактированной группе, нового значения
                            p.Category = name;
                            p.c_k = c_k;
                        }
                    }
                }
                //перехват ошибок нулевого аргумента
                catch (ArgumentNullException)
                {}
                //перехват ошибок нулевой ссылки
                catch (NullReferenceException)
                {}
                //присвоение нового значения группе
                group.Name = name;
                group.c_k = c_k;
                //сохранение изменений
                db.SaveChanges();
            }
        }
        public void edit_stone(int id, string name, string s_k)
        {
            //создание объекта класса Stone
            Stone stone = new Stone();          
            //проверка на ошибки нулевой ссылки
            try
            {
                using (ParamContext db2 = new ParamContext())
                {
                    //получение первого элемента из базы данных
                    stone = db2.stones.Find(id);
                    using (GoodContext db = new GoodContext())
                    {
                        //проверка на ошибки нулевой ссылки
                        try
                        {
                            //выборка всех товаров металла, который будет отредактирован
                            var goods = db.goods.Where(o => o.stone == stone.Name);
                            //проверка результата на null
                            if (goods != null)
                            {
                                //перебор всех элементов в результате выборки goods
                                foreach (Good p in goods)
                                {
                                    p.stone = name;
                                    p.s_k = s_k;
                                }
                            }
                            db.SaveChanges();
                        }
                        //перехват ошибок нулевого аргумента
                        catch (ArgumentNullException)
                        { }
                        //перехват ошибок нулевой ссылки
                        catch (NullReferenceException)
                        { }
                    }
                    //присвоение нового значения группе
                    stone.Name = name;
                    stone.s_k = s_k;
                    //сохранение изменений
                    db2.SaveChanges();
                }
                //использование GoodContext для работы с базой данных
                
            }
            //перехват ошибок нулевого аргумента
            catch (ArgumentNullException)
            { }
            //перехват ошибок нулевой ссылки
            catch (NullReferenceException)
            { }
        }


        //функция редактирования групп из базы данных
        public void edit_metall(int id, string name, string m_k)
        {
            //создание объекта класса Metall
            Metall met = new Metall();
            //использование GoodContext для работы с базой данных
            using (GoodContext db = new GoodContext())
            {
                //получение первого элемента из базы данных
                met = db.metalls.Find(id);
                //проверка на ошибки нулевой ссылки
                try
                {
                    //выборка всех товаров металла, который будет отредактирован
                    var goods = db.goods.Where(o => o.Metall == met.Name.ToString());
                    //проверка результата на null
                    if (goods != null)
                    {
                        //перебор всех элементов в результате выборки goods
                        foreach (Good p in goods)
                        {
                            //присвоение всем товарам, которые относились к отредактированному металлу, нового значения
                            p.Metall = name;
                            p.m_k = m_k;
                        }
                    }
                }
                //перехват ошибок нулевого аргумента
                catch (ArgumentNullException)
                { }
                //перехват ошибок нулевой ссылки
                catch (NullReferenceException)
                { }
                //присвоение нового значения группе
                met.Name = name;
                met.m_k = m_k;
                //сохранение изменений
                db.SaveChanges();
            }
        }
        //функция обработки заказов
        public void add_order(string FN, string SN, string TN, long ph, int good_id)
        {
            //использование OrderContext для работы с базой данных
            
            using (OrderContext db = new OrderContext())
            {
                //добавление заказа в базу данных с параметрами передаными в функцию
                db.orders.Add(new Order { FirstName = FN, SecondName = SN, ThirdName = TN, Phone = ph, when_buy = DateTime.Now, id_buy = good_id });
                //сохранение изменений в базе данных
                db.SaveChanges();
            }
        }
        public void remove_order(int id)
        {
            Order ord = new Order();
            //использование GoodContext для работы с базой данных
            //using (GoodContext db = new GoodContext())
            using (OrderContext db = new OrderContext())
            {
                //получение первого элемента из базы данных
                ord = db.orders.Find(id);
                //проверка на ошибки нулевой ссылки
                try
                {
                    if (ord != null)
                    {
                        //удаление заказа
                        db.orders.Remove(ord);
                        //сохранение изменений в базе данных
                        db.SaveChanges();
                    }
                }
                //перехват ошибок нулевого аргумента
                catch (ArgumentNullException)
                {}
                //перехват ошибок нулевой ссылки
                catch (NullReferenceException)
                {}
            }
        }
        //Функция выборки данных из базы данных продаж
        public Sales get_sale(bool check)
        {
            //создание промежуточной переменной 
            Sales s = new Sales();
            //использование OrderContext для работы с базой данных
            using (OrderContext db = new OrderContext())
            {
                //обработка случая когда нужны только активные продажи
                if (!check)
                {
                    //получение выборки из базы данных
                    var sale = db.sales.Where(o => o.done == false);
                    //перебор всех элементов в переменной sale
                    foreach (Sale sal in sale)
                    {
                        //запись каждого элемента в промежуточную переменную s
                        s.list.Add(sal);
                    }
                }
                //обработка случая, когда нужны все продажи, включая исторические
                else
                {
                    //получение выборки из базы данных
                    var sale = db.sales;
                    //перебор всех элементов в переменной sale 
                    foreach (Sale sal in sale)
                    {
                        //запись каждого элемента в промежуточную переменную s
                        s.list.Add(sal);
                    }
                }
            }
            //передача результатов
            return s;
        }
        public void add_sale(Order ord)
        {
            //использование GoodContext для работы с базой данных
            using (OrderContext db = new OrderContext())
            {
                //добавление заказа в базу данных с параметрами передаными в функцию
                db.sales.Add(new Sale { FirstName = ord.FirstName, SecondName = ord.SecondName, ThirdName = ord.ThirdName, Phone = ord.Phone, when_buy = ord.when_buy, id_buy = ord.id_buy, when_done = ord.when_buy, done = false });
                //сохранение изменений в базе данных
                db.SaveChanges();
            }
        }
        //функция, которая переносит активные продажи в раздел выполненных
        public void done_sale(int id)
        {
            //использование OrderContext для работы с базой данных
            using (OrderContext db = new OrderContext())
            {
                //получение товара по идентификационному номеру
                var sale = db.sales.FirstOrDefault(p => p.Id == id);
                //присваивание этому товару статуса выполненного
                sale.done = true;
                //присваивание этому товару времени в которое была закрыта продажа
                sale.when_done = DateTime.Now;
                //сохранение изменений в базе данных
                db.SaveChanges();
            }
        }
        //функция которая удаляет товар по идентификационному номеру
        public void remove_sale(int id)
        {
            //использование OrderContext для работы с базой данных
            using (OrderContext db = new OrderContext())
            {
                //обработка случая, когда нужно очистить базу данных истории полностью
                if (id == -1)
                {
                    //получение данных 
                    var sale = db.sales;
                    //удаление данных
                    db.sales.RemoveRange(sale);
                    //сохранение изменений
                    db.SaveChanges();
                }
                //обработка случая, когда нужно удалить всего одну запись из базы данных 
                else
                {
                    //получение данных по идентификационному номеру
                    var sale = db.sales.FirstOrDefault(p => p.Id == id);
                    //удаление записи из базы данных
                    db.sales.Remove(sale);
                    //сохранение изменений
                    db.SaveChanges();
                }
            }
        }
        public void add_user(string FN, string SN, string TN, long phone, string data, string log, string pass)
        {
            using (customerContext db = new customerContext())
            {
                db.pass.Add(new PassStorage { login = log, password = pass });
                db.SaveChanges();
                int id = db.pass.FirstOrDefault(p => p.login == log).Id;
                db.users.Add(new customer { FName = FN, SName = SN, TName = TN, phone = phone, data = data, login_id = id });
                db.SaveChanges();
            }
        }
        public bool checklogin(string login, string password)
        {
            using (customerContext db = new customerContext())
            {
                if (db.pass.FirstOrDefault(p => p.login == login) != null)
                {
                    return false;
                }
            }
            return true;
        }
        public customer get_user(string login, string password)
        {
            customer cus = new customer();
            using (customerContext db = new customerContext())
            {
                var check = db.pass.FirstOrDefault(p => p.login == login && p.password == password);
                int id = check.Id;
                cus = db.users.Find(id);
            }
            return cus;
        }
        public void remove_user(customer cus)
        {
            using (customerContext db = new customerContext())
            {
                var pass = db.pass.FirstOrDefault(p => p.Id == cus.login_id);
                db.pass.Remove(pass);
                db.users.Remove(cus);
            }
        }
    }
}
