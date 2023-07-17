using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore
{
    class Book
    {
        private string iSBN;
        private string title;
        private string description;
        private int price;

        public string ISBN { get => iSBN; set => iSBN = value; }
        public string Title { get => title; set => title = value; }
        public string Description { get => description; set => description = value; }
        public int Price { get => price; set => price = value; }
    }
}
