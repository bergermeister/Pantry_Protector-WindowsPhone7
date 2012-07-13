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
        private ItemController itemController;          // Database adapter

        /***********************************************************************
         *                           Constructor
         ***********************************************************************/
        public MainPage()
        {
            // Standard Silverlight initialization function
            InitializeComponent();

            // Create a controller to handle database transactions
            itemController = new ItemController();

            // Data context and observable collection are children of the main page.
            this.DataContext = this;
        }

        /***********************************************************************
         *                      Shopping List Functionality
         ***********************************************************************/

        /* Collection of items in the Shopping List */
        private ObservableCollection<Item> _itemsNeeded;
        public ObservableCollection<Item> ItemsNeeded
        {
            get
            {
                return _itemsNeeded;
            }
            set
            {
                if (_itemsNeeded != value)
                {
                    _itemsNeeded = value;
                    NotifyPropertyChanged("ItemsNeeded");
                }
            }
        }

        /* Add New Item to the Shopping List */
        private void newShoppingListItemAddPageButton_Click(object sender, RoutedEventArgs e)
        {
            string parameter = "Shopping List";
            NavigationService.Navigate(new Uri(string.Format("/views/AddItem.xaml?parameter={0}", parameter), UriKind.Relative));
        }

        private void transferItemFromShoppingListHelpButton_Click(object sender, RoutedEventArgs e)
        {
        }

        /* Transfer item from Shopping List to Inventory */
        private void transferItemFromShoppingListButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            if (button != null)
            {
                // Get a handle for the item bound to the button.
                Item itemForTransfer = button.DataContext as Item;

                // Remove the item from the observable collection.
                ItemsNeeded.Remove(itemForTransfer);

                // Transfer the item to inventory
                itemController.Transfer(itemForTransfer, "Inventory");

                ItemsNotNeeded.Clear();
                ItemsNotNeeded = itemController.CollectAllUnneededItemsInDB();
            }
        }

        /***********************************************************************
         *                      Inventory Functionality
         ***********************************************************************/

        /* Collection of items in the inventory */
        private ObservableCollection<Item> _itemsNotNeeded;
        public ObservableCollection<Item> ItemsNotNeeded
        {
            get
            {
                return _itemsNotNeeded;
            }
            set
            {
                if (_itemsNotNeeded != value)
                {
                    _itemsNotNeeded = value;
                    NotifyPropertyChanged("ItemsNotNeeded");
                }
            }
        }

        /* Add New Item to the inventory */
        private void newInventoryItemAddPageButton_Click(object sender, RoutedEventArgs e)
        {
            string parameter = "Inventory";
            NavigationService.Navigate(new Uri(string.Format("/views/AddItem.xaml?parameter={0}", parameter), UriKind.Relative));
        }

        /* Delete inventory item from the inventory*/
        private void deleteInventoryItemButton_Click(object sender, RoutedEventArgs e)
        {
            // Cast parameter as a button.
            var button = sender as Button;

            if (button != null)
            {
                // Get a handle for the item bound to the button.
                Item itemForDelete = button.DataContext as Item;

                // Remove the item from the observable collection.
                ItemsNotNeeded.Remove(itemForDelete);

                // Remove the item from the local database.
                itemController.DeleteItem(itemForDelete);

                // Put the focus back to the main page.
                this.Focus();
            }
        }

        /* Transfer item from Inventory to Shopping List */
        private void transferItemFromInventoryButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            if (button != null)
            {
                // Get a handle for the item bound to the button.
                Item itemForTransfer = button.DataContext as Item;

                // Remove the item from the observable collection.
                ItemsNeeded.Remove(itemForTransfer);

                // Transfer the item to inventory
                itemController.Transfer(itemForTransfer, "ShoppingList");

                ItemsNeeded.Clear();
                ItemsNeeded = itemController.CollectAllNeededItemsInDB();
            }
        }

        /***********************************************************************
         *              On Navigating to this page
         ***********************************************************************/
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            // Collect all items in the database.
            // Items = itemController.CollectAllItemsInDB();    // OBSOLETE

            // Collect all items in the inventory
            ItemsNotNeeded = itemController.CollectAllUnneededItemsInDB();

            // Collect all items in the shopping list.
            ItemsNeeded = itemController.CollectAllNeededItemsInDB();

            // Call the base method
            base.OnNavigatedTo(e);
        }

        /***********************************************************************
         *                      Navigating away
         ***********************************************************************/
        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            // Call the base method.
            base.OnNavigatedFrom(e);

            // Save changes to the database.
            itemController.SubmitChanges();
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

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {

        }

        /***********************************************************************
         *                      Appbar Functions
         ***********************************************************************/
        private void appbarAddButton_Click(object sender, EventArgs e)
        {
            string parameter;
            switch (PantryProtectorPivot.SelectedIndex)
            {
                case 0:
                    parameter = "Shopping List";
                    NavigationService.Navigate(new Uri(string.Format("/views/AddItem.xaml?parameter={0}", parameter), UriKind.Relative));
                    break;
                case 1:
                    parameter = "Inventory";
                    NavigationService.Navigate(new Uri(string.Format("/views/AddItem.xaml?parameter={0}", parameter), UriKind.Relative));
                    break;
                case 2:
                    // Map
                    break;
                default:
                    break;
            }
        }

        private void ShoppingListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        /***********************************************************************
         *                              Gestures
         ***********************************************************************/
        private void ItemGestureListener_Hold(object sender, Microsoft.Phone.Controls.GestureEventArgs e)
        {
            
        }

        private void ItemEditButton_Click(object sender, RoutedEventArgs e)
        {
            string parameter;
            switch (PantryProtectorPivot.SelectedIndex)
            {
                case 0:
                    parameter = "Shopping List";
                    NavigationService.Navigate(new Uri(string.Format("/views/EditItem.xaml?parameter={0}", parameter), UriKind.Relative));
                    break;
                case 1:
                    parameter = "Inventory";
                    NavigationService.Navigate(new Uri(string.Format("/views/EditItem.xaml?parameter={0}", parameter), UriKind.Relative));
                    break;
                default:
                    break;
            }
        }

        private void ItemDeleteMenu_Click(object sender, RoutedEventArgs e)
        {
            
            MenuItem menuItem = (MenuItem) sender;

            if (menuItem != null)
            {
                // Get a handle for the item bound to the button.
                Item itemForDelete = menuItem.DataContext as Item;

                // Remove the item from the observable collection.
                ItemsNotNeeded.Remove(itemForDelete);

                // Remove the item from the local database.
                itemController.DeleteItem(itemForDelete);

                // Refresh the list
                switch (PantryProtectorPivot.SelectedIndex)
                {
                    case 0:
                        ItemsNeeded = itemController.CollectAllNeededItemsInDB();
                        break;
                    case 1:
                        ItemsNotNeeded = itemController.CollectAllUnneededItemsInDB();
                        break;
                    default:
                        break;
                }
            }
        }
    }       
}