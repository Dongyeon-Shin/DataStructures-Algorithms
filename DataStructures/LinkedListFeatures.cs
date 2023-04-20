using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    internal class LinkedListFeatures
    {
         //Linked List는 배열 기반 자료구조들과 다르게 데이터가 연속적으로 저장되어 있지 않아서 index를 사용해서 저장해둔 데이터(노드)에
         //접근하는 것이 불가능한 자료구조이다.
         //그 대신 Linked List는 배열에서 요소의 역할을 하는 노드라는 클래스를 통해 데이터를 저장하고
         //접근 할 수 있도록 구현해 놨다.
         //이 노드라는 클래스에는 저장하고 싶은 데이터와 다른 두 노드의 주소값이 저장되어 있어서
         //그 주소들을 통해 원하는 노드에 접근하는 것이다.
         //이러한 저장방식 때문에 LinkedList는 데이터의 삽입 삭제에 용이하다는 장점이 있지만
         //노드를 삭제할때마다 해당 클래스를 버려버리는것이기 떄문에 가비지 컬랙터를 자주 발생시킬 수 있다.
         //따라서 Linked List는 가비지 컬랙터를 사용하는 c#보단 쓰지 않는 다른 프로그래밍 언어에게 더 유용한 자료구조라고 할 수 있다.
    }
}
