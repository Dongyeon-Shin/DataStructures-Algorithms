using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    internal class List<T> : IEnumerable<T>, IEnumerator<T>
    {
        private const int DefaultCapacity = 10;
        private T[] items;
        private int size;
        private int position = -1; // foreach문을 사용하기 위해 현재 읽고있는 인덱스를 나타내는 값
        // 0이 아니라 -1인 이유는 Reset 함수를 살펴보면 Sets the enumerator to its initial position,
        // which is before the first element in the collection. 라고 요구되어 있기 때문이다.
        // -1 로 시작하는 이유는 그 어떤 인덱스도 가리키지 않고 있다는 상황을 표현하기 위해서다.
        // ex) 무언가를 판정하는 함수에서 존재하지않을때 -1을 리턴하는 것과 같은 이치
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
                    // 배열의 크기를 바꾸는 코드
                    Array.Resize<T>(ref items, index + 1);
                    // TODO: 처음엔 -1인 포지션의 인덱스를 읽을때 오류를 방지해주는 코드인 줄 알았지만 아니었다.
                    // 일반화 없이 IEnumerable과 IEnumerator을 상속받고 foreach를 사용했을 때 이 코드가 없었으면
                    // 오류가 발생했지만 둘다 일반화를 적용시키고 테스트 해보니 이 코드가 없어도 정삭적으로 작동한다.
                    // 따라서 추후 foreach 정확한 작동원리와 position이 -1부터 시작해야 하는 이유를 반드시 조사할 것.
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
        // IEnumerator 인터페이스의 함수들을 정의한 것
        // 요약:
        //     Gets the element in the collection at the current position of the enumerator.
        //     자료구조 클래스의 요소(인덱스의 값) 중 현재 위치의 요소를 읽어오는 함수
        // 반환 값:
        //     The element in the collection at the current position of the enumerator.
        //     자료구조 클래스의 현재 위치 인덱스의 요소를 반환
        object IEnumerator.Current { get { return items[position]; } }
        public T Current { get { return items[position]; } }
        // 요약:
        //     Advances the enumerator to the next element of the collection.
        //     enumerator를 자료구조 클래스의 다음 요소로 진행시키는 함수
        // 반환 값:
        //     true if the enumerator was successfully advanced to the next element; false if
        //     the enumerator has passed the end of the collection.
        //     enumerator가 성공적으로 다음 요소로 넘어갔으면 true를 자료구조 클래스의 마지막 요소를 지나갔으면 false를 반환
        // 예외:
        //   T:System.InvalidOperationException:
        //     The collection was modified after the enumerator was created.
        public bool MoveNext()
        {
            // enumerator가 자료구조 클래스의 마지막 요소를 지나갔는지 확인하는 코드
            // 비교 값이 size - 1인 이유는 position이 0이 아니라 -1이기 때문이다.
            if (position == size - 1)
            {
                Reset();
                return false;
            }
            // 다음 요소로 진행시키는 코드
            position++;
            return true;
        }
        // 요약:
        //     Sets the enumerator to its initial position, which is before the first element
        //     in the collection.
        //     enumerator를 자료구조 클래스의 첫번째 요소보다 전에 있는 처음의 포지션 으로 초기화하는 함수
        // 예외:
        //   T:System.InvalidOperationException:
        //     The collection was modified after the enumerator was created.
        public void Reset()
        {
            // 자료구조 클래스의 첫번째 요소(인덱스 0)의 전, 즉 -1로 position변수를 초기화 하는 코드
            position = -1;
        }
        // IEnumerable 인터페이스의 함수를 정의한 것
        // 요약:
        //     Returns an enumerator that iterates through a collection.
        //     자료구조 클래스를 통해 반복되는 특징을 가진 enumerator를 반환하는 함수
        // 반환 값:
        //     An System.Collections.IEnumerator object that can be used to iterate through
        //     the collection.
        //     자료구조 클래스처럼 반복되는 특징을 가진 IEnumerator 오브젝트를 반환
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
            // 클래스의 배열의 요소에 접근할 수 있도록 하는 인덱서를 반환하는 코드
        }
        public IEnumerator<T> GetEnumerator()
        {
            return this;            
        }
        // 요약:
        //     Performs application-defined tasks associated with freeing, releasing, or resetting
        //     unmanaged resources.
        //     관리되지 않은 리소스의 확보, 해제 또는 다시 설정에 관련된 함수
        public void Dispose()
        {

        }
        // TODO: IEnumeranle과 IEnumerator에 일반화를 적용시키자 필요하다고 문법 에러를 띄운 함수
        // IDisposable.Dispose 일단 이름으로 유추해보면 쓸모 없어진 것들을 처리하는 용도
        // MSDN에서는 함수 설명 요약과 동일하게 관리되지 않은 리소스의 확보, 해제 또는 다시 설정에 관련된 함수라고 한다.
        // 하지만 지금 함수를 내용없이 정의하기만 했는데도 정상적으로 작동한다.
        // 이 함수가 이 클래스에서 정확히 어떤 용도로 사용된건지 알아낼 것
    }
}
