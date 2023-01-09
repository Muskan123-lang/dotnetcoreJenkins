using System;
using System.Linq;
using System.Text;

namespace DataStructures02
{
    class Program02
    {
        class Stack1<T>
        {
            private T[] items;
            private int top;
            private int capacity;


            /// <summary>
            /// Constructor
            /// </summary>
            public Stack1()
            {
                this.capacity = 10000;
                this.items = new T[this.capacity];
                this.top = -1;
            }



            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="capacity"></param>
            public Stack1(int capacity)
            {
                this.capacity = capacity;
                this.items = new T[capacity];
                this.top = -1;
            }



            /// <summary>
            /// Add item to the stack
            /// </summary>
            /// <param name="item"></param>
            public void Push(T item)
            {
                if (!IsFull())
                {
                    items[++top] = item;
                }
                else
                {
                    throw new Exception("Stack is full");
                }
            }




            /// <summary>
            /// Returns the top item with deleting
            /// </summary>
            /// <returns></returns>
            public T Pop()
            {
                if (!IsEmpty())
                {
                    return items[top--];
                }
                else
                {
                    throw new Exception("Stack is empty");
                }
            }



            
            
            /// <summary>
            ///  Views the first element in the Stack but does not remove it.
            /// </summary>
            /// <returns></returns>
            public T Peek()
            {
                return items[top];
            }






            /// <summary>
            /// Contains value in stack
            /// </summary>
            /// <param name="item"></param>
            /// <returns></returns>
            public bool Contains(T item)
            {
                for (int i = 0; i < top; i++)
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
                return top + 1;
            }







            /// <summary>
            /// Checks if stack is empty
            /// </summary>
            /// <returns></returns>
            public bool IsEmpty()
            {
                return top == -1;
            }







            /// <summary>
            /// Checks if stack is full
            /// </summary>
            /// <returns></returns>
            public bool IsFull()
            {
                return top == capacity - 1;
            }




           



            /// <summary>
            /// Reverse stack
            /// </summary>
            public void Reverse()
            {
                T[] itemsTemp = new T[top + 1];
                int counter = top;
                for (int i = 0; i <= top; i++)
                {
                    itemsTemp[counter] = items[i];
                    counter--;
                }
                items = itemsTemp;

            }






            ///<summary>
            ///     Middle element of stack
            /// </summary>
            public T findMiddle()
            {
                int i=0;
                while (i < (Size() / 2))
                {
                    i++;
                }
                return items[i];
            }




            /// <summary>
            /// Print stack
            /// </summary>
            public void Print()
            {
                if (IsEmpty())
                {
                    throw new Exception("Stack is empty");
                }
                else
                {
                    Console.WriteLine("Items in the stack are:");
                    for (int i = top; i >= 0; i--)
                    {
                        Console.WriteLine(items[i]);
                    }
                }
            }


        }




        class StackIterator<T>
        {
            private Stack1<T> currentStack;


            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="currentStack"></param>
            public StackIterator(Stack1<T> currentStack)
            {
                this.currentStack = currentStack;
            }


            public bool IsEmpty()
            {
                return this.currentStack.IsEmpty();
            }



            public T Pop()
            {
                return this.currentStack.Pop();
            }
        }



        static void Main(string[] args)
        {


            try
            {
                Stack1<int> stack = new Stack1<int>();
                stack.Push(8);
                stack.Push(5);
                stack.Push(10);
                stack.Push(2);
                stack.Push(99);
                stack.Print();
                Console.WriteLine("\nStack size is: {0}", stack.Size());

                
            
                
                // Reverse
                stack.Reverse();
                Console.WriteLine("\nReversed Stack: ");
                stack.Print();
                
                
                // Center
                Console.WriteLine("\nCenter of stack: " + stack.findMiddle());


                // Contains
                if (stack.Contains(10))
                {
                    Console.WriteLine("\nStack contains item 10");
                }
                else
                {
                    Console.WriteLine("\nStack does not contain item 10");
                }
                if (stack.Contains(546))
                {
                    Console.WriteLine("\nStack contains item 546");
                }
                else
                {
                    Console.WriteLine("\nStack does not contain item 546");
                }

                
                
                
                // Peek element
                Console.WriteLine("\nThe top item is {0}", stack.Peek());
                
                
                // delete Peek element
                Console.WriteLine("\nDelete the top item: {0}", stack.Pop());
               
                // Size of stack
                Console.WriteLine("\nStack size is: {0}", stack.Size());


                // Sort Stack
                Console.WriteLine("\nSorted Stack");
                Stack1<int> tmpStack = new Stack1<int>();
                while (stack.Size() > 0)
                {
                    // pop out the first element
                    int tmp = stack.Pop();
                    
                    // while temporary stack is not empty and
                    // top of stack is greater than temp
                    while (tmpStack.Size() > 0 && tmpStack.Peek() > tmp)
                    {
                        // pop from temporary stack and
                        // push it to the input stack
                        stack.Push(tmpStack.Pop());
                        
                    }

                    // push temp in temporary of stack
                    tmpStack.Push(tmp);
                    
                }
                tmpStack.Print();


                // Iterator in Stack
                StackIterator<int> stackIterator = new StackIterator<int>(tmpStack);
                Console.WriteLine("\nDisplay items using StackIterator");
                while (!stackIterator.IsEmpty())
                {
                    Console.WriteLine(stackIterator.Pop());
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
