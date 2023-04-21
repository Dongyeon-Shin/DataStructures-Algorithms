namespace Iterator
{
    internal class Program
    {
	    public class List <T> : IEnumerable<T>
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
                        items[index] = value;
                    }
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
            // 반복될 수 있는 데이터를 받아 foreach를 사용해 모든 내용물을 Add로 붙여주는 함수
            public void AddRange(IEnumerable<T> items)
            {
                foreach (T item in items)
                {
                    Add(item);
                }
            }
            // Add와 RemoveAt에서 쓴 기능을 응용
            public void Insert(int index, T item)
            {
                // 허용량 체크
                if (size == items.Length)
                {
                    Grow();
                }
                // RemoveAt에서 한것처럼 하지만 덮어씌우는 주체는 반대로
                // 해당 인덱스 부터의 값들을 한칸씩 뒤로 민다.
                Array.Copy(items, index, items, index + 1, size - index);
                // 해당 인덱스의 값을 원하는 값으로 바꿔준다.
                items[index] = item;
                size++;
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
            // 배열의 뒤에서부터 조건과 일치하는지 확인하는 함수
            public T? FindLast(Predicate<T> match)
            {
                if (match == null)
                {
                    throw new ArgumentNullException("match");
                }
                // 단순히 Find의 검색 순서만 바꿔주면 된다.
                for (int i = size - 1; i >= 0; i--)
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
            // 마찬가지로 FindIndex와 동일하지만 순서만 거꾸로인 함수
            public int FindLastIndex(Predicate<T> match)
            {
                for (int i = size - 1; i >= 0; i--)
                {
                    if (match(items[i]))
                    {
                        return i;
                    }
                }
                return -1;
            }
            // 복사 대상인 배열과 그 배열의 어느 인덱스 부터 붙여넣을지 매개변수로 받아 그 조건대로 복사한다.
            public void CopyTo(T[] array, int arrayIndex)
            {
                Array.Copy(items, 0, array, arrayIndex, size);
            }
            // 클래스의 배열 중 사용하고 있는 크기만큼의 배열을 생성해 값을 복사한다.
            public T[] ToArray()
            {
                T[] array = new T[size];
                CopyTo(array, 0);
                return array;
            }
            public void Clear()
            {
                size = 0;
                // 어차피 size 바깥의 값은 신경 쓸 필요 없다.
                Array.Clear(items, 0, 1);
            }
            private void Grow()
            {
                int newCapacity = items.Length * 2; // 실제 C#의 List는 단순히 몇배 해주는 형태가 아니라 남은 메모리를 고려하는 등 굉장히 복잡하게 구현되어 있다.
                                                    // 그래서 실제 C#의 리스트를 생성해서 Capacity를 확인하면 0으로 나온다. 위의 defaultCapacity처럼 10이 아니다.
                T[] newItems = new T[newCapacity];
                Array.Copy(items, 0, newItems, 0, size);
                items = newItems;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return new Enumerator(this);
            }
            public IEnumerator<T> GetEnumerator()
            {
                return new Enumerator(this);
            }


            public struct Enumerator : IEnumerator<T>
            {
                private int position;
                private List<T> items;
                private T current;
                private int size;
                internal Enumerator(List<T> list)
                {
                    this.items = list;
                    this.position = -1;
                    this.current = default(T);
                    size = list.Count;
                }
                object IEnumerator.Current { get { return current; } }
                public T Current { get { return current; } }
                public bool MoveNext()
                {
                    if (position == size - 1)
                    {
                        Reset();
                        return false;
                    }
                    current = items[position++];
                    return true;
                }
                public void Reset()
                {
                    position = -1;
                    current = default(T);
                }
                public void Dispose()
                {

                }
            }     
    }
        public class LinkedList<T> : IEnumerable<T>
        {
            // 가장 앞에 있는 노드
            LinkedListNode<T>? head;
            // 가장 뒤에 있는 노드
            LinkedListNode<T>? tail;
            private int count = 0;
            public LinkedListNode<T> First { get { return head; } }
            public LinkedListNode<T> Last { get { return tail; } }
            public int Count { get { return count; } }
            public LinkedList()
            {
                this.head = null;
                this.tail = null;
                this.count = 0;
            }
            public LinkedListNode<T> AddAfter(LinkedListNode<T> node, T value)
            {
                if (node.list == this)
                {
                    throw new InvalidOperationException();
                }
                if (node == null)
                {
                    throw new ArgumentException(nameof(node));
                }
                LinkedListNode<T> newNode = new LinkedListNode<T>(this, value);
                // 기존 노드와 새 노드를 연결하는 작업
                newNode.prev = node;
                node.next = newNode;
                // 기존 노드의 앞 노드와 새 노드를 연결하는 작업
                if (node.next == null)
                {
                    tail = newNode;
                }
                else
                {
                    newNode.next = node.next;
                    node.next.prev = newNode;
                }
                count++;
                return newNode;
            }
            public void AddAfter(LinkedListNode<T> node, LinkedListNode<T> newNode)
            {
                if (node.list != this)
                {
                    throw new InvalidOperationException();
                }
                if (node == null)
                {
                    throw new ArgumentException(nameof(node));
                }
                newNode.list = this;
                newNode.prev = node;
                node.next = newNode;
                if (node.next == null)
                {
                    newNode.next = null;
                    tail = newNode;
                }
                else
                {
                    newNode.next = node.next;
                    node.next.prev = newNode;
                }
                count++;
            }

            public LinkedListNode<T> AddBefore(LinkedListNode<T> node, T value)
            {
                if (node.list != this)
                {
                    throw new InvalidOperationException();
                }
                if (node == null)
                {
                    throw new ArgumentException(nameof(node));
                }
                LinkedListNode<T> newNode = new LinkedListNode<T>(this, value);
                // 기존 노드와 새 노드를 연결하는 작업
                newNode.next = node;
                node.prev = newNode;
                // 기존 노드의 전 노드와 새노드를 연결하는 작업
                if (node.prev == null)
                {
                    head = newNode;
                }
                else
                {
                    newNode.prev = node.prev;
                    node.prev.next = newNode;
                }
                count++;
                return newNode;
            }
            public void AddBefore(LinkedListNode<T> node, LinkedListNode<T> newNode)
            {
                if (node.list != this)
                {
                    throw new InvalidOperationException();
                }
                if (node == null)
                {
                    throw new ArgumentException(nameof(node));
                }
                newNode.list = this;
                newNode.next = node;
                node.prev = newNode;
                if (node.prev == null)
                {
                    newNode.prev = null;
                    head = newNode;
                }
                else
                {
                    newNode.prev = node.prev;
                    node.prev.next = newNode;
                }
                count++;
            }
            public LinkedListNode<T> AddFirst(T value)
            {
                LinkedListNode<T> newNode = new LinkedListNode<T>(this, value);
                if (head == null)
                {
                    tail = newNode;
                    // 가장 앞의 노드가 null 일 경우
                    // 즉 이 노드가 현재 유일하게 생성된 노드일 경우
                    // LinkedList의 tail 노드를 이 노드로 설정한다.
                }
                else
                {
                    newNode.next = head;
                    head.prev = newNode;
                    // 기존의 노드와 새로 생성한 노드가 서로의 주소를 보유하도록 한다.
                }
                head = newNode;
                count++;
                return newNode;
            }
            public void AddFirst(LinkedListNode<T> node)
            {
                if (node == null)
                {
                    throw new ArgumentException(nameof(node));
                }
                // 이하동문
                if (head == null)
                {
                    tail = node;
                }
                else
                {
                    node.next = head;
                    head.prev = node;
                }
                head = node;
                count++;
            }

            public LinkedListNode<T> AddLast(T value)
            {
                // AddLast는 간단하게 AddFirst를 뒤집어서 구현하면 된다.
                LinkedListNode<T> newNode = new LinkedListNode<T>(this, value);
                if (tail == null)
                {
                    head = newNode;
                }
                else
                {
                    newNode.prev = head;
                    tail.next = newNode;
                }
                tail = newNode;
                count++;
                return newNode;
            }
            public void AddLast(LinkedListNode<T> node)
            {
                if (node == null)
                {
                    throw new ArgumentException(nameof(node));
                }
                if (tail == null)
                {
                    head = node;
                }
                else
                {
                    node.prev = head;
                    tail.next = node;
                }
                tail = node;
                count++;
            }

            public bool Remove(T value)
            {
                LinkedListNode<T> correspondNode = Find(value);
                if (correspondNode == null)
                {
                    return false;
                }
                else
                {
                    Remove(correspondNode);
                    return true;
                }
            }
            public void Remove(LinkedListNode<T> node)
            {
                if (node.List != this)
                {
                    throw new InvalidOperationException();
                }
                if (node == null)
                {
                    throw new ArgumentException(nameof(node));
                }
                // 지우려는 노드가 head나 tail일 경우 head나 tail 노드를 변경
                if (node == head)
                {
                    head = node.next;
                }
                if (node == tail)
                {
                    tail = node.prev;
                }
                // 노드의 전후 노드를 서로 이어주는 작업
                if (node.prev != null)
                {
                    node.prev.next = node.next;
                }
                if (node.next != null)
                {
                    node.next.prev = node.prev;
                }
                count--;
            }

            public LinkedListNode<T>? Find(T value)
            {
                LinkedListNode<T> target = head;
                EqualityComparer<T> comparer = EqualityComparer<T>.Default;
                // LinkedList의 노드들을 head 노드부터 시작해서 tail 노드까지
                // 한번씩 찾는 데이터와 일치하는지 확인하고 그 값을 반환
                while (target != null)
                {
                    if (comparer.Equals(value, target.Value))
                    {
                        return target;
                    }
                    else
                    {
                        target = target.Next;
                    }
                }
                return null;
            }
            IEnumerator IEnumerable.GetEnumerator()
            {
                return new Enumerator(this);
            }
            public IEnumerator<T> GetEnumerator()
            {
                return new Enumerator(this);
            }
            public struct Enumerator : IEnumerator<T>
            {
                private LinkedList<T> list;
                private LinkedListNode<T> node;
                private T current;
                internal Enumerator(LinkedList<T> list)
                {
                    this.list = list;
                    this.node = list.head;
                    this.current = default(T);
                }
                object IEnumerator.Current { get { return current; } }
                public T Current { get { return current; } }

                public bool MoveNext()
                {
                    if(node != null)
                    {
                        current = node.item;
                        node = node.Next;
                        return true;
                    }
                    else
                    {
                        current = default(T);
                        return false;
                    }
                }
                public void Reset()
                {
                    this.node = list.head;
                    current = default(T);
                }
                public void Dispose()
                {

                }
            }
        }
        public sealed class LinkedListNode<T>
        {
            internal LinkedList<T>? list; // 링크드 리스트
            internal LinkedListNode<T>? next; // 다음 노드
            internal LinkedListNode<T>? prev; // 이전 노드
                                              // 지금 구현하는 LinkedList는 양방향 형식이기 때문에 가장 처음의 노드나 마지막의 노드는 각각 이전 노드나 다음 노드를 가지지 않는다.
                                              // 그래서 next나 prev가 null값을 가질 수 있도록 nullable을 이용한다.
            internal T item; // 데이터
            public LinkedList<T> List { get { return list; } }
            public LinkedListNode<T> Prev { get { return prev; } }
            public LinkedListNode<T> Next { get { return next; } }
            public T Value { get { return item; } set { item = value; } }
            public LinkedListNode(T value)
            {
                this.list = null;
                this.prev = null;
                this.next = null;
                this.item = value;
            }
            public LinkedListNode(LinkedList<T> list, T value)
            {
                this.list = list;
                this.prev = null;
                this.next = null;
                this.item = value;
            }
            public LinkedListNode(LinkedList<T> list, LinkedListNode<T> prev, LinkedListNode<T> next, T value)
            {
                this.list = list;
                this.prev = prev;
                this.next = next;
                this.item = value;
            }
        }
        // 반복기 Enumerator(Iterator)
        // 자료구조에 저장되어 있는 요소들을 순회하는 인터페이스
        static void Main(string[] args)
        {
            // 대부분의 자료구조가 반복기를 지원함
            // 반복기를 이용한 기능을 구현할 경우, 그 기능은 대부분의 자료구조를 호환할 수 있음
            List<int> list = new List<int>();
            LinkedList<int> linkedList = new LinkedList<int>();
            Stack<int> stack = new Stack<int>();
            Queue<int> queue = new Queue<int>();
            SortedList<int, int> sList = new SortedList<int, int>();
            SortedSet<int> set = new SortedSet<int>();
            SortedDictionary<int, int> map = new SortedDictionary<int, int>();
            Dictionary<int, int> dic = new Dictionary<int, int>();
            for (int i = 1; i <= 5; i++)
            {
                list.Add(i);
                linkedList.AddLast(i);
            }
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine(list[i]);
            }
            LinkedListNode<int> node = linkedList.First;
            while (node != null)
            {
                Console.WriteLine(node.Value);
                node = node.Next;
            }
            foreach (int i in list) { }
            foreach (int i in linkedList) { }
            foreach (int i in stack) { }
            foreach (int i in queue) { }
            foreach (KeyValuePair<int, int> i in sList) { }
            foreach (int i in set) { }
            foreach (KeyValuePair<int, int> i in map) { }
            foreach (KeyValuePair<int, int> i in dic) { }
            // 반복기 직접조작
            List<string> strings = new List<string>();
            for (int i = 0; i < 5; i++)
            {
                strings.Add($"{i}데이터");
            }
            IEnumerator<string> iter = strings.GetEnumerator();
            iter.MoveNext();
            Console.WriteLine(iter.Current);
            iter.MoveNext();
            Console.WriteLine(iter.Current);
            // foreach문과 동일한 결과
            iter.Reset();
			while (iter.MoveNext())
			{
				Console.WriteLine(iter.Current);
			}
            iter.Dispose();
        }
    }
}
