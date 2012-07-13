using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace PantryProtector.views
{
    public partial class AddItem : PhoneApplicationPage, INotifyPropertyChanged
    {

        private ItemController itemController;          // Database adapter
        private string param;
        /***********************************************************************
         *                              Constructor
         ***********************************************************************/
        public AddItem()
        {
            // Standard Silverlight initialization function
            InitializeComponent();

            // Initialize database adapter (controller)
            itemController = new ItemController();
        }

        /***********************************************************************
         *                              Navigation
         ***********************************************************************/
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            param = this.NavigationContext.QueryString["parameter"];
            PageTitle.Text = "Add " + param + " Item";
            if (param == "Shopping List")
            {
                ExpirationDatePicker.Visibility = Visibility.Collapsed;
                ExpirationDate.Visibility = Visibility.Collapsed;
            }
            else
            {
                ExpirationDatePicker.Visibility = Visibility.Visible;
                ExpirationDate.Visibility = Visibility.Visible;
            }
        }
        /***********************************************************************
         *                      Clear Text Boxes on Focus
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
         *                          Add New Item
         ***********************************************************************/
        private void SaveNewItem_Click(object sender, EventArgs e)
        {
            // Create a new item based on the information
            bool inSL = false;
            if (param == "Shopping List") inSL = true;

            Item newItem = new Item
            {
                ItemName = ItemNameTextBox.Text,
                ItemDescription = ItemDescriptionTextBox.Text,
                ItemLocation = ItemLocationTextBox.Text,
                ItemQuantity = ItemQuantitySelector.SelectedItem,
                ItemExpiration = ExpirationDatePicker.ValueString,
                ItemInShoppingList = inSL
            };

            if (newItem.ItemName != null && newItem.ItemName != "Name of your item")
            {
                // Add an item to the local database.
                itemController.InsertItem(newItem);

                // Navigate back to the main page
                NavigationService.GoBack();
            }
            else
            {
                MessageBox.Show("Please enter a name for your Item.");
            }
        }

        /***********************************************************************
         *                         Notify Region
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
}