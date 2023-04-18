using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    internal class List<T>
    {
        private const int DefaultCapacity = 10;
        private T[] items;
        private int size;
        public void Add(T item)
        {
            if (size == items.Length)
            {
                Grow();
            }
            items[size++] = item;
        }
        private void Grow()
        {
            int newCapacity = items.Length * 2;
            T[] newItems = new T[newCapacity];
            Array.Copy(items, 0, newItems, 0, size);
            items = newItems;
        }
    }
}
