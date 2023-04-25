using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab
{
    internal class PriorityQueue
    {
        // 여기서 힙이란 메모리 영역을 의미하는 것이아닌 데이터가 완전이진트리형태로 모든 부모 데이터의 우선순위가
        // 그 자식 데이터의 우선순위보다는 높은 상태를 의미한다.
        // Node 단위로 저장하지만 힙 메모리 영역에 저장되는 클래스 형식이 아니라
        // 스택 메모리 영역을 사용하는 구조체 형식을 배열로 저장하는 형태이기 때문에
        // LinkedList같은 자료구조보다 가비지 컬랙터의 압박에서 자유롭다.
        // 완전이진트리형태란 한 부모노드가 가질 수 있는 최대 자식노드의 개수가 2개이고
        // 왼쪽부터 순서대로 배열에 저장되어 있는 형태이다.
        // 데이터를 추가하고 삭제할때 저장되어 있는 모든 데이터를 정렬하는 것이 아니라.
        // 한 갈래만 정렬하기 때문에 트리의 높이에 따른
        // O(logN)의 효율적인 시간복잡도를 가진다.

        /// <summary>
        /// 데이터를 저장하고 해당 데이터들 중 우선순위가 가장 높은 데이터를 효율적으로 찾을 수 있는 자료구조
        /// </summary>
        /// <typeparam name="TElement">저장되는 데이터</typeparam>
        /// <typeparam name="TPriority">데이터의 우선순위</typeparam>
        public class PriorityQueue<TElement, TPriority>
        {
            /// <value>데이터와 우선순위를 캡슐화한 형태로 보관중인 구조체</value>
            private struct Node
            {
                /// <value>저장되는 데이터</value>
                public TElement element;
                /// <value>데이터의 우선순위</value>
                public TPriority priority;
                /// <summary>
                /// 구조체의 초기화
                /// </summary>
                /// <param name="element">저장되는 데이터</param>
                /// <param name="priority">데이터의 우선순위</param>
                public Node(TElement element, TPriority priority)
                {
                    this.element = element;
                    this.priority = priority;
                }
            }
            /// <value>Node구조체들을 보관할 리스트</value>
            private List<Node> nodes;
            /// <value>값을 비교해서 -1, 0, 1 중 하나로 결과를 반환하는 인터페이스를 사용 가능한 클래스</value>
            private Comparer<TPriority> comparer;
            /// <summary>
            /// 클래스의 생성자
            /// </summary>
            public PriorityQueue()
            {
                this.nodes = new List<Node>();
                this.comparer = Comparer<TPriority>.Default;
            }
            /// <summary>
            /// 클래스의 생성자
            /// </summary>
            /// <param name="comparer">우선순위 기준을 판정</param>
            public PriorityQueue(Comparer<TPriority> comparer)
            {
                this.nodes = new List<Node>();
                this.comparer = comparer;
            }
            /// <value>저장중인 데이터의 수</value>
            public int Count { get { return nodes.Count; } }
            /// <summary>
            /// 가장 밑에 데이터를 저장하고 부모와 우선순위를 비교해 서로 위치를 바꾸는 방식으로 데이터들의 저장이 힙 형태를 유지하도록 하는 함수
            /// </summary>
            /// <param name="element">저장한 데이터</param>
            /// <param name="priority">데이터의 우선순위</param>
            public void Enqueue(TElement element, TPriority priority)
            {
                // 추가된 노드
                Node newNode = new Node(element, priority);
                nodes.Add(newNode);
                //추가된 노드의 현재 인덱스
                int newNodeIndex = nodes.Count - 1;
                //추가된 노드의 현재 부모노드의 인덱스
                int parentIndex;
                // 현재 인덱스가 가장 우선순위가 높은 데이터가 저장되는 인덱스에 도달할 때까지
                while (newNodeIndex > 0)
                {
                    parentIndex = GetParentIndex(newNodeIndex);
                    // 부모 노드의 우선순위보다 추가된 노드의 우선순위가 높다면
                    // 현재 인덱스에 부모 노드를 옮기고
                    // 현재 인덱스는 부모 인덱스로 변경
                    if (ComparePriority(nodes[newNodeIndex].priority, nodes[parentIndex].priority) == -1)
                    {
                        nodes[newNodeIndex] = nodes[parentIndex];
                        newNodeIndex = parentIndex;
                    }
                    else
                    {
                        break;
                    }
                }
                // 현재 인덱스에 저장한 노드를 추가된 노드로 변경
                nodes[newNodeIndex] = newNode;
            }
            /// <summary>
            /// 부모의 인덱스를 구하는 함수
            /// </summary>
            /// <param name="childIndex">자식 노드의 인덱스</param>
            /// <returns>부모 노드의 인덱스</returns>
            private int GetParentIndex(int childIndex)
            {
                return (childIndex - 1) / 2;
            }
            /// <summary>
            /// 우선순위가 낮은 (맨 아래? 마지막?의) 노드를 가장 우선순위가 높은 노드를 삭제하고 그 위치로 변경
            /// 그리고 위에서 부터 자식 노드들의 우선순위와 비교하면서 힙 형태로 다시 정렬
            /// </summary>
            /// <returns>삭제한 우선순위가 가장 높았던 노드의 데이터</returns>
            /// <exception cref="InvalidOperationException"></exception>
            public TElement Dequeue()
            {
                if (nodes.Count == 0)
                {
                    throw new InvalidOperationException();
                }
                // 가장 우선순위가 높았던 노드의 인덱스이자 현재 정렬중인 인덱스가 들어올 지역변수
                int currentIndex = 0;
                // 삭제될(우선순위가 가장 높았던 노드의 데이터)
                TElement removedElement = nodes[currentIndex].element;
                // 우선순위가 가장 높은 노들의 자리에 우선순위가 낮은 노드를 덮어씌우기
                nodes[currentIndex] = nodes[Count - 1];
                // 비교 대상인 노드
                Node currentNode = nodes[currentIndex];
                // 비교 대상인 노드와 비교할 자식 노드의 인덱스
                int childNodeIndex;
                // 왼쪽 자식노드의 인덱스
                int leftChildIndex;
                // 오른쪽 자식노드의 인덱스
                int rightChildIndex;
                nodes.RemoveAt(Count - 1);
                // 가장 우선순위가 낮은 노드의 인덱스에 도달할때까지
                while (currentIndex < Count)
                {
                    leftChildIndex = GetLeftChildIndex(currentIndex);
                    rightChildIndex = GetRightChildIndex(currentIndex);
                    // 비교대상인 노드와 우선순위를 비교해야하는 자식 노드를 찾는 코드
                    if (rightChildIndex < nodes.Count)
                    {
                        childNodeIndex = ComparePriority(nodes[leftChildIndex].priority, nodes[rightChildIndex].priority) == -1 ? leftChildIndex : rightChildIndex;
                    }
                    else if (leftChildIndex < nodes.Count)
                    {
                        childNodeIndex = leftChildIndex;
                    }
                    else
                    {
                        nodes[currentIndex] = currentNode;
                        break;
                    }
                    // 자식 노드와 우선순위를 비교해서 위치를 바꾸거나 데이터가 힙 형태인지 확인해서 반복문을 끝내는 코드
                    if (ComparePriority(nodes[childNodeIndex].priority, currentNode.priority) != -1)
                    {
                        nodes[currentIndex] = currentNode;
                        break;
                    }
                    else
                    {
                        nodes[currentIndex] = nodes[childNodeIndex];
                        currentIndex = childNodeIndex;
                    }
                }

                return removedElement;
            }
            /// <summary>
            /// 왼쪽의 자식노드의 인덱스를 구하는 함수
            /// </summary>
            /// <param name="parentIndex">부모노드의 인덱스</param>
            /// <returns>자식노드의 인덱스</returns>
            private int GetLeftChildIndex(int parentIndex)
            {
                return parentIndex * 2 + 1;
            }
            /// <summary>
            /// 오른쪽의 자식노드의 인덱스를 구하는 함수
            /// </summary>
            /// <param name="parentIndex">부모노드의 인덱스</param>
            /// <returns>자식노드의 인덱스</returns>
            private int GetRightChildIndex(int parentIndex)
            {
                return parentIndex * 2 + 2;
            }
            /// <summary>
            /// 현재 가장 우선순위가 높은 데이터를 반환하는 함수
            /// </summary>
            /// <returns>가장 우선순위가 높은 데이터</returns>
            public TElement Peek()
            {
                return nodes[0].element;
            }
            /// <summary>
            /// 자식노드의 우선순위와 부모노드의 우선순위를 비교해서 결과를 반환하는 함수
            /// </summary>
            /// <param name="childPriority">자식노드의 우선순위</param>
            /// <param name="parentPriority">부모노드의 우선순위</param>
            /// <returns>비교결과</returns>
            private int ComparePriority(TPriority childPriority, TPriority parentPriority)
            {
                return comparer.Compare(childPriority, parentPriority);
            }
        }
    }
}
