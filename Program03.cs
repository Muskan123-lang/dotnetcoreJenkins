using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace DataStructure03
{
    class Program03
    {
        public static int INT_MAX { get; private set; }

        class Queue<T>
        {
            private T[] items;
            private int front;
            private int rear;
            private int capacity;
            /// <summary>
            /// Constructor
            /// </summary>
            public Queue()
            {
                this.capacity = 1000;
                this.items = new T[this.capacity];
                this.front = 0;
                this.rear = 0;
            }
            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="capacity"></param>
            public Queue(int capacity)
            {
                this.capacity = capacity;
                this.items = new T[capacity];
                this.front = 0;
                this.rear = 0;
            }
            /// <summary>
            /// Add item to the Queue
            /// </summary>
            /// <param name="item"></param>
            public void Push(T item)
            {


                if (IsFull())
                {
                    throw new Exception("Queue is full");
                }


                // insert element at the rear 
                items[rear] = item;
                rear++;


            }
            /// <summary>
            /// Returns the first item with deleting
            /// </summary>
            /// <returns></returns>
            public T Pop()
            {

                // check if queue is empty 
                if (IsEmpty())
                {
                    throw new Exception("Queue is empty");
                }
                T frontItem = items[this.front];
                // shift elements to the right
                for (int i = 0; i < rear - 1; i++)
                {
                    items[i] = items[i + 1];
                }
                rear--;
                return frontItem;
            }







            /// <summary>
            ///  Views the first element in the Queue but does not remove it.
            /// </summary>
            /// <returns></returns>
            public T Peek()
            {
                return items[this.front];
            }







            /// <summary>
            /// Contains value in Queue
            /// </summary>
            /// <param name="item"></param>
            /// <returns></returns>
            public bool Contains(T item)
            {
                for (int i = front; i < rear; i++)
                {
                    if (item.Equals(items[i]))
                    {
                        return true;
                    }
                }
                return false;
            }





            /// <summary>
            /// Returns size
            /// </summary>
            /// <returns></returns>
            public int Size()
            {
                return rear;
            }





            /// <summary>
            /// Checks if Queue is empty
            /// </summary>
            /// <returns></returns>
            public bool IsEmpty()
            {
                return front == rear;
            }




            /// <summary>
            /// Checks if Queue is full
            /// </summary>
            /// <returns></returns>
            public bool IsFull()
            {
                return capacity == rear;
            }




            /// <summary>
            /// Reverse Queue
            /// </summary>
            public void Reverse()
            {
                T[] itemsTemp = new T[rear];
                int counter = rear - 1;
                for (int i = front; i < rear; i++)
                {
                    itemsTemp[counter] = items[i];
                    counter--;
                }


                items = itemsTemp;
            }


            ///<summary>
            ///     Middle of Queue
            /// </summary>
            public T findMiddle()
            {
                int i = 0;
                while (i < (Size()/2))
                {
                    i++;
                }
                return items[i];
            }





            /// <summary>
            /// Print Queue
            /// </summary>
            public void Print()
            {
                if (IsEmpty())
                {
                    throw new Exception("Queue is empty");
                }


                Console.WriteLine("Items in the Queue are:");
                // traverse front to rear and print elements 
                for (int i = front; i < rear; i++)
                {
                    Console.WriteLine(items[i]);
                }
            }

            internal object ToList()
            {
                throw new NotImplementedException();
            }
        }


        class QueueIterator<T>
        {
            private Queue<T> currentQueue;


            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="currentQueue"></param>
            public QueueIterator(Queue<T> currentQueue)
            {
                this.currentQueue = currentQueue;
            }




            /// <summary>
            ///     CHECK EMPTY OR NOT
            /// </summary>
            /// <returns></returns>
            public bool IsEmpty()
            {
                return this.currentQueue.IsEmpty();
            }



            /// <summary>
            /// DELETE ELEMENT
            /// </summary>
            /// <returns></returns>
            public T Pop()
            {
                return this.currentQueue.Pop();
            }
        }
        static void Main(string[] args)
        {


            try
            {
                Queue<int> myQueue = new Queue<int>(7);

                //INSERT ELEMENTS
                myQueue.Push(88);
                myQueue.Push(78);
                myQueue.Push(89);
                myQueue.Push(23);
                myQueue.Push(15);
                myQueue.Push(24);
                myQueue.Push(35);
                myQueue.Print();
                Console.WriteLine("Queue size is: {0}", myQueue.Size());
                myQueue.Reverse();
                Console.WriteLine("\nAfter Queue is Reversed");
                myQueue.Print();


                // MIDDLE ELEMENT
                Console.WriteLine("\nMiddle element of Queue is : " + myQueue.findMiddle());



                // CONTAINS ELEMENT OR NOT
                if (myQueue.Contains(25))
                {
                    Console.WriteLine("Queue contains item 25");
                }
                else
                {
                    Console.WriteLine("Queue does not contain item 25");
                }
                if (myQueue.Contains(15))
                {
                    Console.WriteLine("Queue contains item 15");
                }
                else
                {
                    Console.WriteLine("Queue does not contain item 15");
                }
                


                // DISPLAY PEEK ELEMENT
                Console.WriteLine("The front item is {0}", myQueue.Peek());
                
                
                // DELETE ELEMENTS
                Console.WriteLine("Delete the front item: {0}", myQueue.Pop());
                
                
                // SIZE
                Console.WriteLine("Queue size is: {0}", myQueue.Size());




                //  ITERATOR

                QueueIterator<int> QueueIterator = new QueueIterator<int>(myQueue);
                Console.WriteLine("\nDisplay items using QueueIterator");
                while (!QueueIterator.IsEmpty())
                {
                    Console.WriteLine(QueueIterator.Pop());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
    }
}
