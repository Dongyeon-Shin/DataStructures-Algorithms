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
        public int Count { get { return size; } }
        public int Capacity { get { return items.Length; } }
        public List()
        {
            this.items = new T[DefaultCapacity];
            this.size = 0;
        }
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= size)
                {
                    throw new IndexOutOfRangeException();
                }
                return items[index];
            }
            set
            {
                if (index < 0 || index >= size)
                {
                    throw new IndexOutOfRangeException();
                }
                items[index] = value;
            }
        }
        public void Add(T item)
        {
            if (size == items.Length)
            {
                Grow();
            }
            items[size++] = item;
        }
        public bool Remove(T item)
        {
            int index = IndexOf(item);
            if (index >= 0)
            {
                RemoveAt(index);
                return true;
            }
            else
            {
                return false;
            }
        }
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= size)
            {
                throw new IndexOutOfRangeException();
            }
            size--;
            Array.Copy(items, index + 1, items, index, size - index);
        }
        public int IndexOf(T item)
        {
            return Array.IndexOf(items, item, 0, size);
        }
        public T? Find(Predicate<T> match)
        {
            if (match == null)
            {
                throw new ArgumentNullException("match");
            }
            for (int i = 0; i < size; i++)
            {
                if (match(items[i]))
                {
                    return items[i];
                }
            }
            return default(T?);
        }
        public int FindIndex(Predicate<T> match)
        {
            for (int i = 0; i < size; i++)
            {
                if (match(items[i]))
                {
                    return i;
                }
            }
            return -1;
        }
        private void Grow()
        {
            int newCapacity = items.Length * 2; // 실제 C#의 List는 단순히 몇배 해주는 형태가 아니라 남은 메모리를 고려하는 등 굉장히 복잡하게 구현되어 있다.
            // 그래서 실제 C#의 리스트를 생성해서 Capacity를 확인하면 0으로 나온다. 위의 defaultCapacity처럼 10이 아니다.
            T[] newItems = new T[newCapacity];
            Array.Copy(items, 0, newItems, 0, size);
            items = newItems;
        }
    }
}
