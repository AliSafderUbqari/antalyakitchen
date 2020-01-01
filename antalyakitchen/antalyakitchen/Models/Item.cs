using System;

namespace antalyakitchen.Models
{
    public class Item
    {
        public string Id { get; set; }
        public string name { get; set; }
        public string count { get; set; }
        public int itemPrice { get; set; }
    }

    public class Category
    {
        public string Id { get; set; }
        public string name { get; set; }
        public string count { get; set; }
    }
}