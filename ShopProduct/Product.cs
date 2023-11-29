using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProduct
{
    public class Product
    {
        public Product(int id, string name, decimal price, int stock, string fotourl)
        {
            _productid = id;
            _name = name;
            _price = price;
            _stock = stock;
            _fotourl = fotourl;
        }

        private int _productid;
        public int Productid
        {
            get { return _productid; }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
        }

        private decimal _price;

        public decimal Price
        {
            get { return _price; }
            set { _price = value; }
        }

        private int _stock;
        public int Stock
        {
            get { return _stock; }
        }

        private string _fotourl;
        public string FotoUrl
        {
            get { return _fotourl; }
        }

        public void SetPrice(decimal price)
        {
            _price = price * 0.8m;
        }
    }
}
