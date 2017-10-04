using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1;

namespace Diplom
{
    //интерфейс описывающий функции, которые реализованы на сервере
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        Goods ShowGoods(Transfer a);
        [OperationContract]
        Goods Show();
        [OperationContract]
        Good get_good(int id);
        [OperationContract]
        void getorder(Order ord);
        [OperationContract]
        void add_good(Good x);
        [OperationContract]
        void edit_good(Good good);
        [OperationContract]
        void del_good(int id);
        [OperationContract]
        Groups get_group();
        [OperationContract]
        Metalls get_metall();
        [OperationContract]
        void add_group(string name, string c_k);
        [OperationContract]
        void del_group(int id);
        [OperationContract]
        void edit_group(int id, string name, string c_k);
        [OperationContract]
        void add_metall(string name, string m_k);
        [OperationContract]
        void del_metall(int id);
        [OperationContract]
        void edit_metall(int id, string name, string m_k);
        [OperationContract]
        void add_order(string FN, string SN, string TN, long ph, int id_buy);
        [OperationContract]
        void remove_order(int id);
        [OperationContract]
        Sales get_sale(bool check);
        [OperationContract]
        void add_sale(Order ord);
        [OperationContract]
        void done_sale(int s);
        [OperationContract]
        Orders get_order();
        [OperationContract]
        void remove_sale(int id);
        [OperationContract]
        void add_user(string FN, string SN, string TN, long phone, string data, string login, string password);
        [OperationContract]
        bool checklogin(string login, string password);
        [OperationContract]
        customer get_user(string login, string password);
        [OperationContract]
        void remove_user(customer cus);
        //[OperationContract]
        //string get_kg(int id);
        //[OperationContract]
        //string get_km(int id);
        [OperationContract]
        probas get_proba();
        [OperationContract]
        void add_proba(proba x);
        [OperationContract]
        Stones get_stone();
        [OperationContract]
        void edit_stone(int id, string name, string c_k);
        [OperationContract]
        void del_stone(int id);
        [OperationContract]
        void add_Stone(Stone x);
        [OperationContract]
        void del_proba(int id);
        [OperationContract]
        void edit_proba(int id, string number);
    }
}
