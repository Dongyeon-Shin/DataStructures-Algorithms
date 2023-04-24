using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab
{
    internal class StackAndQueue
    {
        /// <summary>
        /// 기존 List가 가지고 있던 기능을 통해 Adapter Pattern으로 Stack의 기능을 구현한 클래스
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public class StackAdapterByList<T>
        {
            private List<T> container;
            public StackAdapterByList()
            {
                container = new List<T>();
            }
            /// <summary>
            /// 저장공간에 데이터를 추가하는 함수
            /// </summary>
            /// <param name="item"></param>
            public void Push(T item)
            {
                container.Add(item);
            }
            /// <summary>
            /// 저장공간에 뒤에서부터 데이터를 제거하는 함수
            /// </summary>
            /// <returns></returns>
            public T Pop()
            {
                T removedElement = Peek();
                container.RemoveAt(container.Count - 1);
                return removedElement;
            }
            /// <summary>
            /// 가장 마지막의 데이터를 반환하는 함수
            /// </summary>
            /// <returns></returns>
            public T Peek()
            {
                return container[container.Count - 1];
            }
        }
        /// <summary>
        /// 배열을 통해 Queue의 기능을 구현한 클래스
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public class Queue<T>
        {
            private const int DefaultCapacity = 4;
            private T[] array;
            // 저장된 데이터 중 가장 먼저 저장한 데이터
            private int head;
            // 저장된 데이터 중 가장 늦게 저장한 데이터
            private int tail;
            // 저장중인 데이터의 수
            private int size;
            public Queue()
            {
                array = new T[DefaultCapacity];
                head = 0;
                tail = 0;
                size = 0;
            }
            public virtual int Count
            {
                get { return size; }
            }
            public void Enqueue(T item)
            {
                // 사용량이 배열의 크기보다 클시 더 큰 배열로 교체
                if (size == array.Length)
                {
                    Grow();
                }
                array[tail] = item;
                // 마지막 데이터를 가장 최근에 저장한 데이터로 바꿔주는 코드
                MoveNext(ref tail);
                size++;
            }
            private void MoveNext(ref int index)
            {
                // 원형 배열을 구현하기 위해 인덱스가 1씩 증가하다가 끝에 도달하면 0으로 돌아가도록 한 코드
                index = (index == array.Length - 1) ? 0 : index + 1;
            }
            private void Grow()
            {
                if (head < tail)
                {
                    Array.Resize(ref array, array.Length * 2);
                }
                else
                {
                    // 데이터의 삽입 삭제를 반복하다가 가장 마지막 데이터가 첫번째 데이터보다 앞에 있을 경우
                    // 배열의 크기를 바꾸는 김에 정렬까지 시키는 코드
                    T[] newArray = new T[array.Length * 2];
                    Array.Copy(array, head, newArray, 0, array.Length - head);
                    Array.Copy(array, 0, newArray, array.Length - head, tail);
                    array = newArray;
                    tail = size;
                    head = 0;
                }
            }
            public T Dequeue()
            {
                if (size == 0)
                {
                    throw new InvalidOperationException();
                }
                // 가장 처음의 데이터를 더이상 사용하지 않는걸로 표시하고 그 다음 저장했던 데이터로 바꿔주는 코드
                T removed = array[head];
                MoveNext(ref head);
                size--;
                return array[head];
            }
            public T Peek()
            {
                if (size == 0)
                {
                    throw new InvalidOperationException();
                }
                return array[head];
                // 저장한 데이터 중 가장 오래된 값을 리턴하는 코드
            }
        }
    }
}
