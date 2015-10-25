using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoxVisio.Resources
{
    public enum eListEvent
    {
        ItemAdded,
        ItemRemoved,
    }
    /// <summary>
    /// The EventList behaves the same as a standard list, with the addition that it will fire an event whenever an item is added or removed from it's dataset.
    /// </summary>
    /// <typeparam name="T">The type of data that the list holds.</typeparam>
    public class EventList<T> : List<T>
    {
        public delegate void ListChangeEventHandler(object sender, eListEvent e);
        /// <summary>
        /// The OnChange event fires every time an item is added or removed from the list's dataset.
        /// </summary>
        public event ListChangeEventHandler OnChange;

        public new void Add(T item)
        {
            base.Add(item);
            if (null != OnChange)
            {
                OnChange(this, eListEvent.ItemAdded);
            }
            
        }

        public new void Remove(T item)
        {
            base.Remove(item);
            if (null != OnChange)
            {
                OnChange(this, eListEvent.ItemRemoved);
            }
            
        }

        public new void RemoveAt(int index)
        {
            base.RemoveAt(index);
            if (null != OnChange)
            {
                OnChange(this, eListEvent.ItemRemoved);
            }
           
        }
        

    }
}
