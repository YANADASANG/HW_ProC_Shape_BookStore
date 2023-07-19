using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore
{
    class Order
    {
        public string iSBN;
        public int customerId;
        public int quatity;
        public int price;
        public string ISBN { get => iSBN; set => iSBN = value; }
        public int CustomerId { get => customerId; set => customerId = value; }
        public int Quatity { get => quatity; set => quatity = value; }
        public int Price { get => price; set => price = value; }
    }
}
