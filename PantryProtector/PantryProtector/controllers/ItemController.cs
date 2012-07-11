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

        public ItemController()
        {
            // Connect to the database and instantiate data context
            itemDB = new ItemDataContext(ItemDataContext.DBConnectionString);

            // Data context and observable collection are children of the controller
            //this.DataContext = this;
        }

        // Return a copy of all items in the database.
        public ObservableCollection<Item> CollectAllItemsInDB()
        {
            var itemsInDB = from Item item in itemDB.Items select item;

            return new ObservableCollection<Item>(itemsInDB);
        }

        // Insert a new item into the database
        public Boolean InsertItem(Item newItem)
        {
            var itemsInDB = from Item item in itemDB.Items
                            where item.ItemName.Equals(newItem.ItemName)
                            select item;
            // If no duplicate entires exist, create a new item
            if (itemsInDB.Count() == 0)
            {
                // Insert the new item.
                itemDB.Items.InsertOnSubmit(newItem);

                // Save the changes
                SubmitChanges();

                return true;
            }
            // Else we already have an entry, so add the quantities.
            else
            {
                // Add the quantities together
                Item item = itemsInDB.First();
                item.ItemQuantity += newItem.ItemQuantity;

                // Save the changes
                SubmitChanges();

                return true;
            }
            // Add item to the database

            // We should never reach this.
            return false;
        }

        // Remove an item from the database
        public void DeleteItem(Item itemForDelete)
        {
            // Remove the item from the database
            itemDB.Items.DeleteOnSubmit(itemForDelete);

            // Save the changes to the database.
            itemDB.SubmitChanges();
        }

        // Save changes to the database
        public void SubmitChanges()
        {
            itemDB.SubmitChanges();
        }

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
