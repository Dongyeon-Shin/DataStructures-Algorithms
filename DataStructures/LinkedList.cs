using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    public class LinkedList<T>
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
            if(node.list != this)
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
}
