using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    class Restaurant
    {
        public String name { get; set; }
        public IList<Menu> menus { get; set; }

        public Restaurant(String n)
        {
            name = n;
        }

        public void addMenu(Menu menu)
        {
            menus.Add(menu);
        }

        public IList<MenuCategory> getCategories()
        {
            return categories;
        }

    }

    class Menu
    {
        public String name { get; set; }
        public IList<MenuCategory> categories { get; set; }

        public Menu(String n)
        {
            name = n;
        }

        public void addCategories(MenuCategory category)
        {
            categories.Add(category);
        }

        public IList<MenuCategory> getCategories()
        {
            return categories;
        }
    }

    class MenuCategory
    {
        public String name { get; set; }
        public IList<MenuCategoryItem> items { get; set; }

        public MenuCategory(String n) 
        {
            name = n;
        }

        public void addItem(MenuCategoryItem item)
        {
            items.Add(item);
        }

        public IList<MenuCategoryItem> getItems() 
        {
            return items;
        }

    }

    class MenuCategoryItem
    {
        public String name { get; set; }
        public String description { get; set; }
        public Double price { get; set; }

        public MenuCategoryItem(String n, double p, String d)
        {
            name = n;
            price = p;
            description = d;
        }
    }
}
