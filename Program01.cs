using System;
using System.Linq;
using System.Text;

namespace DataStructures
{
    class Program01
    {

        class Node<T>
        {
            public T item;
            public Node<T> next;
            public Node()
            {
                this.next = null;
            }
        }
        class LinkList<T>
        {
            public Node<T> head { get; set; }
            public LinkList()
            {
                this.head = null;
            }




            /// <summary>
            /// inserts item
            /// </summary>
            /// <param name="item"></param>
            public void insert(T item)
            {
                Node<T> newNode = new Node<T>();
                newNode.item = item;
                if (this.head == null)
                {
                    this.head = newNode;
                }
                else
                {
                    Node<T> tempNode = this.head;
                    while (tempNode.next != null)
                    {
                        tempNode = tempNode.next;
                    }
                    tempNode.next = newNode;
                }
            }



            /// <summary>
            /// Returns node by Position
            /// </summary>
            /// <param name="position"></param>
            /// <returns></returns>
            private Node<T> getNodeByPosition(int position)
            {
                if (position < 0)
                {
                    throw new IndexOutOfRangeException("Wrong index. Index: " + position);
                }
                Node<T> currentNode = head;
                int i = 0;
                try
                {
                    for (; i < position; ++i)
                    {
                        currentNode = currentNode.next;
                    }
                }
                catch (NullReferenceException)
                {
                    throw new IndexOutOfRangeException("Wrong index. Index: " + position + ", Size: " + i);
                }
                return currentNode;
            }



            /// <summary>
            /// insert at position
            /// </summary>
            /// <param name="position"></param>
            /// <param name="item"></param>
            public void insertAt(int position, T item)
            {
                Node<T> newNode = new Node<T>();
                newNode.item = item;
                newNode.next = null;
                if (position == 0)
                {
                    newNode.next = head;
                    head = newNode;
                }
                else if (position == size())
                {
                    insert(item);
                }
                else
                {
                    Node<T> prevNode = getNodeByPosition(position - 1);
                    Node<T> nextNode = getNodeByPosition(position);
                    prevNode.next = newNode;
                    newNode.next = nextNode;
                }
            }



            /// <summary>
            /// Checks if LinkList is empty
            /// </summary>
            /// <returns></returns>
            public bool isEmpty()
            {
                return head == null;
            }




            /// <summary>
            /// Display the middle element  of the link list
            /// </summary>
            /// <returns></returns>
            public T center()
            {
                Node<T> node1 = head;
                Node<T> node2 = head;


                if (head != null)
                {
                    while (node2 != null && node2.next != null)
                    {
                        node2 = node2.next.next;
                        node1 = node1.next;
                    }
                }
                return node1.item;
            }




            /// <summary>
            /// Delete item
            /// </summary>
            /// <param name="item"></param>
            public void delete(T item)
            {
                if (this.head.item.Equals(item))
                {
                    head = head.next;
                }
                else
                {
                    Node<T> tempNode = head;
                    Node<T> tempPrevious = head;
                    bool isFound = false;
                    while (!(isFound = tempNode.item.Equals(item)) && tempNode.next != null)
                    {
                        tempPrevious = tempNode;
                        tempNode = tempNode.next;
                    }
                    if (isFound)
                    {
                        tempPrevious.next = tempNode.next;
                    }
                    else
                    {
                        Console.WriteLine("Item not found!");
                    }
                }
            }




            /// <summary>
            /// Delete item at position
            /// </summary>
            /// <param name="position"></param>
            public void deleteAt(int position)
            {
                if (head == null)
                    return;


                Node<T> tempNode = head;
                if (position == 0)
                {
                    head = tempNode.next;
                    return;
                }
                // Find previous node 
                for (int i = 0; tempNode != null && i < position - 1; i++)
                {
                    tempNode = tempNode.next;
                }
                if (tempNode == null || tempNode.next == null)
                {
                    return;
                }
                Node<T> next = tempNode.next.next;
                tempNode.next = next;
            }





            /// <summary>
            /// Returns size of linklist
            /// </summary>
            /// <returns></returns>
            public int size()
            {
                Node<T> tempNode = head;
                int numberNodes = 0;
                while (tempNode != null)
                {
                    numberNodes++;
                    tempNode = tempNode.next;
                }
                return numberNodes;
            }






            /// <summary>
            /// reverse
            /// </summary>
            public void reverse()
            {
                Node<T> previousNode = null;
                Node<T> currentNode = head;
                Node<T> tempNode = null;
                while (currentNode != null)
                {
                    tempNode = currentNode.next;
                    currentNode.next = previousNode;
                    previousNode = currentNode;
                    currentNode = tempNode;
                }
                head = previousNode;
            }





            /// <summary>
            /// Display items of link list
            /// </summary>
            public void print()
            {
                Console.WriteLine("Link List Items:");
                Node<T> tempNode = this.head;
                while (tempNode != null)
                {
                    Console.WriteLine(tempNode.item);
                    tempNode = tempNode.next;
                }
            }
        }


        class LinkListIterator<T>
        {
            private Node<T> currentNode;



            public LinkListIterator(LinkList<T> linkList)
            {
                this.currentNode = linkList.head;
            }
            // returns false if next node does not exist
            public bool hasNext()
            {
                return this.currentNode != null;
            }


            // return current item
            public T next()
            {
                T data = this.currentNode.item;
                this.currentNode = this.currentNode.next;
                return data;
            }
        }

        static void Main(string[] args)
        {


            try
            {
                Console.WriteLine("Input items");
                LinkList<int> linkList = new LinkList<int>();
                linkList.insert(5);
                linkList.insert(10);
                linkList.insert(7);
                linkList.insert(2);
                linkList.insertAt(2, 11);
                linkList.print();
                Console.WriteLine("The middle element is: " + linkList.center() + "\n");
                Console.WriteLine("Reverse: ");
                linkList.reverse();
                linkList.print();
                Console.WriteLine("Size = {0}", linkList.size());


                Console.WriteLine("\nDisplay items using LinkListIterator");
                LinkListIterator<int> linkListIterator = new LinkListIterator<int>(linkList);
                while (linkListIterator.hasNext())
                {
                    Console.WriteLine(linkListIterator.next());
                }


                Console.WriteLine("\nDelete element 2 from the link list");
                linkList.delete(2);
                Console.WriteLine("\nDelete at index 1 from the link list");
                linkList.deleteAt(1);
                linkList.print();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
    }
}
