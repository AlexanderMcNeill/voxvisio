using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoxVisio.Resources
{
    public enum eDictEvent
    {
        ItemAdded,
        ItemRemoved,
    }
    class EventDictionary<TKey, TValue> : Dictionary<TKey, TValue>
    {
        public delegate void DicitonaryChangeEventHandler(object sender, eDictEvent e);
        /// <summary>
        /// The OnChange event fires every time an item is added or removed from the dict's dataset.
        /// </summary>
        public event DicitonaryChangeEventHandler OnChange;

        public new void Add(TKey key , TValue value )
        {
            base.Add(key,value);
            
            if (null != OnChange)
            {
                OnChange(this, eDictEvent.ItemAdded);
            }
        }

        public void Remove(TKey key)
        {
            base.Remove(key);
            if (null != OnChange)
            {
                OnChange(this, eDictEvent.ItemRemoved);
            }
        }
        
    }
}
