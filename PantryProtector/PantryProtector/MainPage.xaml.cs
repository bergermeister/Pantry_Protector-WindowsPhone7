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
        private IntLoopingDataSource _quantityList = new IntLoopingDataSource() { MinValue = 1, MaxValue = 60, SelectedItem = 1 };
        public IntLoopingDataSource QuantityList
        {
            get
            {
                return _quantityList;
            }
            set
            {
                if (_quantityList != value)
                {
                    _quantityList = value;
                    NotifyPropertyChanged("QuantityList");
                }
            }
        }
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
            // Standard Silverlight initialization function
            InitializeComponent();

            // Populate the Quantity List
            this.QuantityPicker.DataSource = new IntLoopingDataSource() { MinValue = 1, MaxValue = 10, SelectedItem = 1 };
            //for (int i = 0; i < 50; i++) _quantityList.Add(i);
            
            // Create a controller to handle database transactions
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

        /***********************************************************************
         *              Clear Text Boxes on Focus
         ***********************************************************************/
        private void newItemNameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            // Clear the text box when it gets focus.
            ItemNameTextBox.Text = String.Empty;
        }

        private void newItemDescriptionTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            // Clear the text box when it gets focus.
            ItemDescriptionTextBox.Text = String.Empty;
        }
        
        private void newItemLocationTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            // Clear the text box when it gets focus.
            ItemLocationTextBox.Text = String.Empty;
        }

        /***********************************************************************
         *              Add New Item
         ***********************************************************************/
        private void newItemAddButton_Click(object sender, RoutedEventArgs e)
        {
            // Create a new item based on the text box.
            Item newItem = new Item {   ItemName = ItemNameTextBox.Text,
                                        ItemDescription = ItemDescriptionTextBox.Text,
                                        ItemLocation = ItemLocationTextBox.Text,
                                        ItemExpiration = ExpirationDatePicker.ValueString};

            // Add an item to the observable collection.
            Items.Add(newItem);

            // Add an item to the local database.
            itemController.InsertItem(newItem);
        }

        /***********************************************************************
         *              Navigating away
         ***********************************************************************/
        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            // Call the base method.
            base.OnNavigatedFrom(e);

            // Save changes to the database.
            itemController.SubmitChanges();
        }

        /***********************************************************************
         *              Delete Item
         ***********************************************************************/
        private void deleteItemButton_Click(object sender, RoutedEventArgs e)
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

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void GroceryListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }       
}