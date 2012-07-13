using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;
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
    [Table]
    public class Item : INotifyPropertyChanged, INotifyPropertyChanging
    {
        // Define ID: private field, public property, and database column.
        private int _itemId;

        /***********************************************************************
         *                      Column: PrimaryKey
         ***********************************************************************/
        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int ItemId
        {
            get
            {
                return _itemId;
            }
            set
            {
                if (_itemId != value)
                {
                    NotifyPropertyChanging("ItemId");
                    _itemId = value;
                    NotifyPropertyChanged("ItemId");
                }
            }
        }

        /***********************************************************************
         *                      Column: ItemName
         ***********************************************************************/
        private string _itemName;

        [Column(CanBeNull = false)]
        public string ItemName
        {
            get
            {
                return _itemName;
            }
            set
            {
                if (_itemName != value)
                {
                    NotifyPropertyChanging("ItemName");
                    _itemName = value;
                    NotifyPropertyChanged("ItemName");
                }
            }
        }

        /***********************************************************************
         *                      Column: ItemQuantity
         ***********************************************************************/
        private int _itemQuantity;

        [Column(CanBeNull = false)]
        public int ItemQuantity
        {
            get
            {
                return _itemQuantity;
            }
            set
            {
                if (_itemQuantity != value)
                {
                    NotifyPropertyChanging("ItemQuantity");
                    _itemQuantity = value;
                    NotifyPropertyChanged("ItemQuantity");
                }
            }
        }

        /***********************************************************************
         *                      Column: ItemDescription
         ***********************************************************************/
        private string _itemDescription;

        [Column]
        public string ItemDescription
        {
            get
            {
                return _itemDescription;
            }
            set
            {
                if (_itemDescription != value)
                {
                    NotifyPropertyChanging("ItemDescription");
                    _itemDescription = value;
                    NotifyPropertyChanged("ItemDescription");
                }
            }
        }

        /***********************************************************************
         *                      Column: ItemLocation
         ***********************************************************************/
        private string _itemLocation;

        [Column]
        public string ItemLocation
        {
            get
            {
                return _itemLocation;
            }
            set
            {
                if (_itemLocation != value)
                {
                    NotifyPropertyChanging("ItemLocation");
                    _itemLocation = value;
                    NotifyPropertyChanged("ItemLocation");
                }
            }
        }

        /***********************************************************************
         *                      Column: ItemExpiration
         ***********************************************************************/
        private string _itemExpiration;

        [Column(CanBeNull = false)]
        public string ItemExpiration
        {
            get
            {
                return _itemExpiration;
            }
            set
            {
                if (_itemExpiration != value)
                {
                    NotifyPropertyChanging("ItemExpiration");
                    _itemExpiration = value;
                    NotifyPropertyChanged("ItemExpiration");
                }
            }
        }


        /***********************************************************************
         *                      Column: ItemCategory
         ***********************************************************************/
        private string _itemCategory;

        [Column]
        public string ItemCategroy
        {
            get
            {
                return _itemCategory;
            }
            set
            {
                if (_itemCategory != value)
                {
                    NotifyPropertyChanging("ItemCategory");
                    _itemCategory = value;
                    NotifyPropertyChanged("ItemCategroy");
                }
            }
        }

        /***********************************************************************
         *                      Column: ItemInShoppingList
         ***********************************************************************/
        private bool _itemInShoppingList;

        [Column(CanBeNull = false)]
        public bool ItemInShoppingList
        {
            get
            {
                return _itemInShoppingList;
            }
            set
            {
                if (_itemInShoppingList != value)
                {
                    NotifyPropertyChanging("InGroceryList");
                    _itemInShoppingList = value;
                    NotifyPropertyChanged("InGroceryList");
                }
            }
        }

        /***********************************************************************
         *                      Column: IsComplete
         ***********************************************************************/
        private bool _isComplete;

        [Column]
        public bool IsComplete
        {
            get
            {
                return _isComplete;
            }
            set
            {
                if (_isComplete != value)
                {
                    NotifyPropertyChanging("IsComplete");
                    _isComplete = value;
                    NotifyPropertyChanged("IsComplete");
                }
            }
        }

        /***********************************************************************
         *                      Column: Version
         ***********************************************************************/
        [Column(IsVersion = true)]
        private Binary _version;

        /***********************************************************************
         *                      Region: Notify
         ***********************************************************************/
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify the page that a data context property changed
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region INotifyPropertyChanging Members

        public event PropertyChangingEventHandler PropertyChanging;

        // Used to notify the data context that a data context property is about to change
        private void NotifyPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        #endregion
    }
}
