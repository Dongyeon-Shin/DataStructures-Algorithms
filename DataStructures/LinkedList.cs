using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    // 양방향 링크드 리스트 구현해보기
    public class LinkedListNode<T>
    {
        internal LinkedList<T> list;
        internal LinkedListNode<T> prev;
        internal LinkedListNode<T> next;
        private T item;
        public LinkedList<T> List { get { return list; } }
        public LinkedListNode<T> Prev {  get { return prev;} }
        public LinkedListNode<T> Next { get {  return next;} }
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
    public class LinkedList<T>
    {
        private LinkedListNode<T> head;
        private LinkedListNode<T> tail;
        private int count;
        public LinkedListNode<T> First { get { return head; } }
        public LinkedListNode<T> Last { get {  return tail; } }
        public int Count { get { return count; } }
        public LinkedList()
        {
            this.head = null;
            this.tail = null;
            this.count = 0;
        }
        public LinkedListNode<T> AddFirst(T value)
        {
            // 1. 새로운 노드 생성
            LinkedListNode<T> newNode = new LinkedListNode<T>(this, value);
            // 2. 연결구조 바꾸기
            if (head != null) // 2-1. Head 노드가 있었을 때
            {
                newNode.next = head;
                head.prev = newNode;
            }
            else // 2-2. Head 노드가 없었을 때
            {
                head = newNode;
                tail = newNode;
            }
            // 3. 갯수 늘리기
            count++;
            return newNode;
        }
    }
}
