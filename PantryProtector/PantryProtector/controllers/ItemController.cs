using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;


namespace PantryProtector
{
    public class ItemController : INotifyPropertyChanged
    {
        // Data context for the lcoal database
        private ItemDataContext itemDB;

        /***********************************************************************
         *                          Constructor
         ***********************************************************************/
        public ItemController()
        {
            // Connect to the database and instantiate data context
            itemDB = new ItemDataContext(ItemDataContext.DBConnectionString);
        }

        /***********************************************************************
         *                  Collection of All Items in DB
         ***********************************************************************/
        public ObservableCollection<Item> CollectAllItemsInDB()
        {
            // Select statement to grab all items.
            var itemsInDB = from Item item in itemDB.Items select item;

            // Query the select and return the collection.
            return new ObservableCollection<Item>(itemsInDB);
        }

        /***********************************************************************
         *              Collection of All Items in Shopping List
         ***********************************************************************/
        public ObservableCollection<Item> CollectAllNeededItemsInDB()
        {
            var itemsInDB = from Item item in itemDB.Items
                            where item.ItemInShoppingList == true
                            select item;

            return new ObservableCollection<Item>(itemsInDB);
        }

        /***********************************************************************
         *                Collection of All Items in Inventory
         ***********************************************************************/
        public ObservableCollection<Item> CollectAllUnneededItemsInDB()
        {
            var itemsInDB = from Item item in itemDB.Items
                            where item.ItemInShoppingList == false
                            select item;

            return new ObservableCollection<Item>(itemsInDB);
        }

        /***********************************************************************
         *                     Insert New Item into DB
         ***********************************************************************/
        public Boolean InsertItem(Item newItem)
        {
            itemDB.Items.InsertOnSubmit(newItem);

            // Save the changes
            SubmitChanges();

            return true;
        }

        /***********************************************************************
         *                          Transfer Item
         ***********************************************************************/
        public Boolean Transfer(Item newItem, string table)
        {
            bool inSL = false;                          // initial state is false, item is in inventory or will be transferred here
            if (table == "ShoppingList") inSL = true;   // set to true if ShoppingList is pased as table, item transfered to Shopping List
            
            // Query for the item.
            IQueryable<Item> itemQuery = from Item item in itemDB.Items
                                         where item.ItemName == newItem.ItemName
                                         select item;
            
            // Perform update on entry
            Item itemToUpdate = itemQuery.FirstOrDefault();
            itemToUpdate.ItemInShoppingList = inSL;

            SubmitChanges();

            return true;
        }

        /***********************************************************************
         *                       Remove Item from DB
         ***********************************************************************/
        public void DeleteItem(Item itemForDelete)
        {
            // Remove the item from the database
            itemDB.Items.DeleteOnSubmit(itemForDelete);

            // Save the changes to the database.
            itemDB.SubmitChanges();
        }

        /***********************************************************************
         *                       Save Changes to DB
         ***********************************************************************/
        public void SubmitChanges()
        {
            itemDB.SubmitChanges();
        }

        /***********************************************************************
         *                        Region: Notify
         ***********************************************************************/
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify Silverlight that a property has changed.
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
    
    public class ItemDataContext : DataContext
    {
        // Specify the connection string as a static, used in main page and app.xaml.
        public static string DBConnectionString = "Data Source=isostore:/Item.sdf";

        // Pass the connection string to the base class.
        public ItemDataContext(string connectionString) : base(connectionString) { }

        // Specify a single table for the to-do items.
        public Table<Item> Items;
    }
}
