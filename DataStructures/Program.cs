namespace DataStructures
{
    internal class Program
    {
        // 배열 Array
        // 연속적인 메모리상에 동일한 타입의 요소를 일렬로 저장하는 자료구조
        // 초기화떄 정한 크기가 소멸까지 유지됨
        // 배열의 요소는 인덱스를 사용하여 직접적으로 엑세스 가능

        // 배열은 데이터를 일렬, 연속적으로 저장 따라서 가장 처음 인덱스 0의 위치만 안다면
        // 두번째 세번째의 위치도 알 수있다.
        // 원하는 인덱스 곱하기 자료형의 크기를 인덱스 0 위치에 더하면

        // 배열의 사용
        void Array()
        {
            int[] intArray = new int[100];
            // 인덱스를 통한 접근
            intArray[0] = 10;
            int value = intArray[0];
        }
        // 배열의 시간복잡도
        //접근    탐색
        //O(1)    O(n)
        // int 배열 20번째 자료 접근: 20번째 자료의 주소 = 배열의 주소 + int의 자료형의 크기 * 20
        // 데이터가 n개 있을 때 탐색
        public int FindIndex(int[] intArray, int data)
        {
            // 배열은 정렬되있는 상태가 아니기 떄문에 이분법이 불가능
            for (int i = 0; i < intArray.Length; i++)
            {
                if (intArray[i] == data)
                { return i; }
            }
            return -1;
        }

        // 선형리스트 (동적배열) Dynamic Array)
        // 런타임 도중 크기를 확장할 수있는 배열기반의 자료구조
        // 배열요소의 갯수를 특정할 수 없는 경우 사용

        // 배열과 같은 방식 단지 처음에 커다란배열을 해놓고 추가하고 뺀만큼 count해서 사용한다.
        // list.Count : 몇개를 사용하고 있는지.
        // list.Capacity : 허용량 처음에 생성해논 배열의 크기

        // List의 사용
        void List()
        {
            List<string> list = new List<string>();
            // 배열 요소 삽입
            list.Add("0번 데이터");
            list.Add("1번 데이터");
            list.Add("2번 데이터");

            // 배열 요소 삭제
            list.Remove("1번 데이터");

            // 배열 요소 접근
            list[0] = "데이터 0";
            string value = list[0];

            // 배열 요소 탐색
            string? findValue = list.Find(x => x.Contains('2'));
            int findIndex = list.FindIndex(x => x.Contains('0'));
        }
        // list의 시간복잡도
        // 접근   탐색  삽입  삭제
        // O(1)   O(n)  O(n) O(n)


        // 연결리스트 Linked List
        // 데이터를 포함하는 노드들을연결식으로 만든 자료구조
        // 노드는 데이터와 이전/다음 노드 객체를 참조하고 있음
        // 노드가 메모리에 연속적으로 배치되지 않고 이전/다음노드의 위치를 확인

        // 링크드리스트의 사용
        void LinkedList()
        {
            LinkedList<string> linkedList = new LinkedList<string>();
            // 링크드리스트 요소 삽입
            linkedList.AddFirst("0번 앞데이터");
            linkedList.AddFirst("1번 앞데이터");
            linkedList.AddLast("0번 뒤데이터");
            linkedList.AddLast("1번 뒤데이터");

            // 링크드리스트 요소 삭제
            linkedList.Remove("1번 앞데이터");

            // 링크드리스트 요소 탐색
            // 노드란 이전주소, 데이터, 다음주소를 담고있는 클래스이다.
            // 그리고 첫번째이냐 마지막이냐에 따라 이전주소나 다음주소는 없을 수도 있기떄문에
            // nullable을 이용한다.
            LinkedListNode<string> findNode = linkedList.Find("0번 뒤데이터");

            // 링크드리스트 노드를 통한 노드 참조
            LinkedListNode<string> prevNode = findNode.Previous;
            LinkedListNode<string> nextNode = findNode.Next;

            // 링크드리스트 노드를 통한 노드 삽입
            linkedList.AddBefore(findNode, "찾은노드 앞데이터");
            linkedList.AddAfter(findNode, "찾은노드 뒤데이터");

            // 링크드리스트 노드를 통한 삭제
            linkedList.Remove(findNode);
        }

        // LinkedList의 시간복잡도
        // 접근   탐색   삽입   삭제
        // O(n)   O(n)　 O(1)  O(1)
        // 인덱스
        // 사용
        // 불가능

        // LinkedList의 종류
        // 단방향 링크드 리스트: 노드에 다음 노드의 주소만 가지고 있는 링크드 리스트
        // 양방향 링크드 리스트: 노드가 전과 다음 노드의 주소를 모두 가지고 있는 타입의 링크드 리스트
        // 환(원)형 링크드 리스트: 주소를 가지고 있는 방식으로 연결되어 있다가 마지막 노드에서 다시 첫번째 노드로 연결되는 형태의 링크드 리스트
        // c#의 경우 환형 링크드 리스트 방식이다.

        static void Main(string[] args)
        {
            
        }
    }
}