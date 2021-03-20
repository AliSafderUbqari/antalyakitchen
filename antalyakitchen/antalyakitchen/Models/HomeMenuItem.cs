using System;
using System.Collections.Generic;
using System.Text;

namespace antalyakitchen.Models
{
    public enum MenuItemType
    {
        SelectMenu,
        PopulateMenuwithListandCard,
        NewItemPage,
        About
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
