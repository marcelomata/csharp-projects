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
        private ArrayList menus;

        public Restaurant()
        {
            menus = new ArrayList();
        }

        public void addMenu(Menu menu)
        {
            menus.Add(menu);
        }

        public ArrayList getMenus()
        {
            return menus;
        }

    }


    class Menu
    {
        private String name;
        private ArrayList categories;

        public Menu(String n)
        {
            name = n;
            categories = new ArrayList();
        }

        public ArrayList getCategories() 
        {
            return categories;
        }

        public void addCategories(MenuCategory category)
        {
            categories.Add(category);
        }

    }

    class MenuCategory
    {
        private String name;
        private ArrayList itens;

        public MenuCategory(String n) 
        {
            name = n;
            itens = new ArrayList();
        }

        public void addItem(MenuCategoryItem item)
        {
            itens.Add(item);
        }

        public ArrayList getItens() 
        {
            return itens;
        }

    }


    class MenuCategoryItem
    {
        private String name;
        private double price;
        private String description;

        public MenuCategoryItem(String n, double p, String d)
        {
            name = n;
            price = p;
            description = d;
        }

        public double getPrice() 
        {
            return price;
        }

        public String getName()
        {
            return name;
        }

        public String getDescription()
        {
            return description;
        }
    }
}
