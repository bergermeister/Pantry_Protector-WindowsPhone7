using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls.Primitives;

namespace PantryProtector.helpers
{
    public class DigitDataSource : ILoopingSelectorDataSource
    {    
        public DigitDataSource(int minValue, int maxValue, int step, int defaultValue, string stringFormat)    
        {        
            MinValue = minValue;        
            MaxValue = maxValue;        
            Step = step;        
            SelectedItem = defaultValue;        
            StringFormat = stringFormat;    
        }     
        
        public int MinValue    
        {        
            get;        
            set;    
        }     
        public int MaxValue    
        {        
            get;        
            set;    
        }     
        
        public int Step    
        {        
            get;        
            set;    
        }     

        private string ApplyFormat(int digit)    
        {        
            return digit.ToString(StringFormat);    
        }     
        
        public string StringFormat   
        {      
            get;       
            private set;    
        }     
        
        #region ILoopingSelectorDataSource Members     
        
        public object GetNext(object relativeTo)   
        {        
            return Convert.ToInt32(relativeTo) + Step > MaxValue ? ApplyFormat(MinValue) : ApplyFormat(Convert.ToInt32(relativeTo) + Step);   
        }    
 
        public object GetPrevious(object relativeTo)   
        {        
            return Convert.ToInt32(relativeTo) - Step < MinValue ? ApplyFormat(MaxValue) : ApplyFormat(Convert.ToInt32(relativeTo) - Step);    
        }     
        
        public int selectedItem;   
        public object SelectedItem    
        {        
            get        
            {            
                return ApplyFormat(selectedItem);       
            }        
            set        
            {            
                int newValue = Convert.ToInt32(value);           
                if (selectedItem != newValue)           
                {                    
                    int previousSelectedItem = selectedItem;                    
                    selectedItem = newValue;                    
                    EventHandler<SelectionChangedEventArgs> handler = SelectionChanged;                    
                    if (handler != null)                        
                        handler(this, new SelectionChangedEventArgs(new object[] { ApplyFormat(previousSelectedItem) }, new object[] { ApplyFormat(selectedItem) }));           
                }        
            }    
        }     
        
        public event EventHandler<SelectionChangedEventArgs> SelectionChanged;    

        #endregion
    }
}
