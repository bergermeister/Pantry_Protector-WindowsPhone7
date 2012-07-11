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

        // Identifier column that is automatically populated by the database. This column is also the primary key, for which a database index is automatically created.
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

        // Define item name: private field, public property, and database column
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

        // Define item quantity
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

        // Define item description
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

        // Define item location
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

        // Define item expiration date
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

        // Define completion value: private field, public property, and database column.
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

        // Version column aids update performance.
        [Column(IsVersion = true)]
        private Binary _version;

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
