using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace PantryProtector
{
    public partial class MainPage : PhoneApplicationPage, INotifyPropertyChanged
    {
        private ItemController itemController;
        private ObservableCollection<Item> _items;
        public ObservableCollection<Item> Items
        {
            get
            {
                return _items;
            }
            set
            {
                if (_items != value)
                {
                    _items = value;
                    NotifyPropertyChanged("Items");
                }
            }
        }

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            itemController = new ItemController();

            // Data context and observable collection are children of the main page.
            this.DataContext = this;
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            // Collect all items in the database.
            Items = itemController.CollectAllItemsInDB();

            // Call the base method
            base.OnNavigatedTo(e);
        }

        private void newItemTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            // Clear the text box when it gets focus.
            newItemTextBox.Text = String.Empty;
        }

        private void newItemAddButton_Click(object sender, RoutedEventArgs e)
        {
            // Create a new item based on the text box.
            Item newItem = new Item { ItemName = newItemTextBox.Text };

            // Add an item to the observable collection.
            Items.Add(newItem);

            // Add an item to the local database.
            itemController.InsertItem(newItem);
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            // Call the base method.
            base.OnNavigatedFrom(e);

            // Save changes to the database.
            itemController.SubmitChanges();
        }

        private void deleteTaskButton_Click(object sender, RoutedEventArgs e)
        {
            // Cast parameter as a button.
            var button = sender as Button;

            if (button != null)
            {
                // Get a handle for the item bound to the button.
                Item itemForDelete = button.DataContext as Item;

                // Remove the item from the observable collection.
                Items.Remove(itemForDelete);

                // Remove the item from the local database.
                itemController.DeleteItem(itemForDelete);
                
                // Put the focus back to the main page.
                this.Focus();
            }
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
}