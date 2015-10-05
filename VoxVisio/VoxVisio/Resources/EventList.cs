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
    public class EventList<T> : List<T>
    {
        public delegate void ListChangeEventHandler(object sender, eListEvent e);
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
