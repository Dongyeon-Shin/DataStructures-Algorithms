namespace Iterator
{
    internal class Program
    {
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