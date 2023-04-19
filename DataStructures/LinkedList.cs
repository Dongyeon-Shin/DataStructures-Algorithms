using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    internal class LinkedList<T>
    {
        // 가장 앞에 있는 노드
        LinkedListNode<T> head;
        // 가장 뒤에 있는 노드
        LinkedListNode<T> tail;
        // 노드의 개수
        private int count = 0;

        public LinkedListNode<T> AddAfter(LinkedListNode<T> node, T value)
        {

        }
        public void AddAfter(LinkedListNode<T> node, LinkedListNode<T> newNode)
        {

        }

        public LinkedListNode<T> AddBefore(LinkedListNode<T> node, T value)
        {

        }
        public void AddBefore(LinkedListNode<T> node, LinkedListNode<T> newNode)
        {

        }

        public LinkedListNode<T> AddFirst(T value)
        {

        }
        public void AddFirst(LinkedListNode<T> node)
        {

        }

        public LinkedListNode<T> AddLast(T value)
        {

        }
        public void AddLast(LinkedListNode<T> node)
        {

        }

        public bool Remove(T value)
        {

        }
        public void Remove(LinkedListNode<T> node)
        {

        }

        public LinkedListNode<T>? Find(T value)
        {
            
        }
    }
    public sealed class LinkedListNode<T>
    {
        internal LinkedList<T>? list; // 링크드 리스트
        internal LinkedListNode<T>? next; // 다음 노드
        internal LinkedListNode<T>? prev; // 이전 노드
        internal T item; // 데이터
    }
}
