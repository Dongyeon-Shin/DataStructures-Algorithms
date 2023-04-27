using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab
{
    internal class HashTable
    {
         //해싱이란: 해싱함수를 이용해서 키를 주소로 변환해 주는 작업이다.
         //해시테이블의 층돌은 데이터를 삽입하고 삭제를 하다보면 저장된 데이터들 중에
         //서로 값은 다르지만 주소가 일치하는 데이터들이 생겨나는 오류가 발생한다.
         //이러한 충돌의 해결방안은 대표적으로 체이닝과 개방주소법이 있다.
         //체이닝은 연결 리스트 방식으로 충돌이 생기면 데이터 안에 다른 데이터의 주소를 보관하는 방식이고
         //개방주소법은 충돌이 일어난 인덱스의 뒤에 가장 까가운 비어있는 인덱스에 데이터를 삽입하는 방식이다.
         //둘다 충돌을 해결하는 좋은 방법이지만 c#은 가비지 컬랙터 때문에 연결 리스트를 최대한 지양해야 하기때문에
         //개방 주소법을 사용한다.
    }
    public class Dictionary<TKey, TValue> where TKey : IEquatable<TKey>
    {
        private const int DefaultCapacity = 1000;
        private Entry[] entries;
        private struct Entry
        {
            public enum State
            {
                None,
                Using,
                Deleted
            }
            public State state;
            public TKey key;
            public TValue value;
        }
        public Dictionary()
        {
            entries = new Entry[DefaultCapacity];
        }
        public TValue this[TKey key]
        {
            get
            {
                TValue value;
                if (TryGetValue(key, out value))
                    return value;
                else
                    throw new KeyNotFoundException();
            }
            set
            {
                Add(key, value);
            }
        }
        public void Add(TKey key, TValue value)
        {
            int index = Math.Abs(key.GetHashCode() % entries.Length);
            bool breakWhile = false;
            while (true)
            {
                switch (entries[index].state)
                {
                    case Entry.State.Using:
                        if (key.Equals(entries[index].key))
                        {
                            throw new ArgumentException();
                        }
                        break;
                    case Entry.State.Deleted:
                        break;
                    default:
                        breakWhile = true;
                        break;
                }
                if (breakWhile)
                {
                    break;
                }
                index = ++index % entries.Length;
            }
            entries[index].state = Entry.State.Using;
            entries[index].key = key;
            entries[index].value = value;
        }
        public bool ContainsKey(TKey key)
        {
            return TryGetValue(key, out var value);
        }
        public bool TryGetValue(TKey key, out TValue value)
        {
            int index = FindIndex(key);
            if (index < 0)
            {
                value = default(TValue);
                return false;
            }
            else
            {
                value = entries[index].value;
                return true;
            }
        }
        public void Clear()
        {
            entries = new Entry[DefaultCapacity];
        }
        public bool Remove(TKey key)
        {
            int index = FindIndex(key);
            if (index < 0)
            {
                return false;
            }
            else
            {
                entries[index].state = Entry.State.Deleted;
                return true;
            }
        }
        private int FindIndex(TKey key)
        {
            int index = Math.Abs(key.GetHashCode() % entries.Length);
            while (true)
            {
                if (entries[index].state == Entry.State.None)
                    break;
                else if (entries[index].state == Entry.State.Using)
                {
                    if (key.Equals(entries[index].key))
                    {
                        return index;
                    }
                }
                index = (index + 1) % entries.Length;
            }
            return -1;
        }
    }
}
