using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab
{

    // 이진탐색구조는 분명 탐색에 있어서 매우 효율적인 방식이지만 단점이 존재한다.
    // 그것은 저장하는 값의 크기가 계속 커지거나 계속 작아지면 어느 한쪽으로 굉장히 치우친 형태로
    // 트리가 길어지는 불균형 문제가 발생할 수 있다는 것이다.
    // 따라서 사람들은 해당 문제로 인한 탐색 속도의 저하를 예방하기 위해 
    // red-black tree나 AVL트리 같은 자가균형기능을 사용한다.
    // 자가균형기능을 사용하면 값을 균등하게 분배해서 최악의 상황에 탐색이 O(n)의
    // 시간 복잡도를 가지는 일을 막아줄 수 있다.
    public class BinarySearchTree<T> where T : IComparable<T>
    {
        private Node root;
        private Comparer<T> comparer;
        public BinarySearchTree()
        {
            this.root = null;
        }
        public class Node
        {
            T item;
            public T Item { get { return item; } set { item = value; } }
            Node parent;
            public Node Parent { get { return parent; } set { parent = value; } }
            Node leftChild;
            public Node LeftChild { get { return leftChild; } set { leftChild = value; } }
            Node rightChild;
            public Node RightChild { get { return rightChild; } set { rightChild = value; } }
            public Node(T item)
            {
                this.item = item;
                this.parent = null;
                this.leftChild = null;
                this.rightChild = null;
            }
        }
        public void Add(T item)
        {
            Node newNode = new Node(item);
            Node current = root;
            if (root == null)
            {
                root = newNode;
                return;
            }
            while (current != null)
            {
                newNode.Parent = current;
                if (comparer.Compare(current.Item, item) < 1)
                {
                    if (current.LeftChild == null)
                    {
                        current.LeftChild = newNode;
                    }
                    else
                    {
                        current = current.LeftChild;
                    } 
                }
                else
                {
                    if (current.RightChild == null)
                    {
                        current.RightChild = newNode;
                    }
                    else
                    {
                        current = current.RightChild;
                    }
                }
            }

            //맨 꼭대기에서 차례차례 해당 줄기가 끝일때까지
            // nullable?
        }
        public bool Remove(T item)
        {
            if (root == null)
            {
                return false;
            }
            Node findNode = Find(item);
            if (findNode ==  null)
            {
                return false;
            }
            else
            {
                EraseNode(findNode);
                return true;
            }
        }
        private void EraseNode(Node node)
        {
            if (node.LeftChild == null && node.RightChild == null)
            {
                if (node.LeftChild != null)
                {
                    node.Parent.LeftChild = null;
                }
                else if (node.RightChild != null)
                {
                    node.Parent.RightChild = null;
                }
                else
                    root = null;
            }
            else if (node.LeftChild != null || node.RightChild != null)
            {
                Node parent = node.Parent;
                Node child = node.RightChild != null ? node.LeftChild : node.RightChild;

                if (node.LeftChild != null)
                {
                    parent.LeftChild = child;
                    child.Parent = parent;
                }
                else if (node.RightChild != null)
                {
                    parent.RightChild = child;
                    child.Parent = parent;
                }
                else
                {
                    root = child;
                    child.Parent = null;
                }
            }
            else
            {
                Node nextNode = node.RightChild;
                while (nextNode.LeftChild != null)
                {
                    nextNode = nextNode.LeftChild;
                }
                node.Item = nextNode.Item;
                EraseNode(nextNode);
            }
        }
        public Node Find(T item)
        {
            if (root == null)
            {
                return null;
            }
            Node current = root;
            while (current != null)
            {
                if (comparer.Compare(item, current.Item) < 0)
                {
                    current = current.LeftChild;
                }
                else if (comparer.Compare(item, current.Item) > 0)
                {
                    current = current.RightChild;
                }
                else
                {
                    return current;
                }
            }
            return null;
        }
        private void CheckRedBlackConditions()
        {
            // redblack은 bool로
            // 조건에따라
            ReStructing();
            ReColoring();
        }
        private void ReStructing()
        {
            // 색바꾸기
            CheckRedBlackConditions();
        }
        private void ReColoring()
        {
            // 색바꾸기
            CheckRedBlackConditions();
        }
        //TODO: 셋합치기 null일시 기본값 leaf 블랙을 사용
        private void GetGrandParent()
        {

        }
        private void GetParent()
        {

        }
        private void GetUncle()
        {

        }
    }
}
