using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace DataStructures04
{
    class Program04
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Creating priority queue");
            var pq = new PriorityQueue<Student>();


            var s1 = new Student("Aiden", 1.0);
            var s2 = new Student("Baker", 2.0);
            var s3 = new Student("Chung", 3.0);
            var s4 = new Student("Dunne", 4.0);
            var s5 = new Student("Eason", 5.0);
            var s6 = new Student("Flynn", 6.0);


            Console.WriteLine($"Adding {s5} to priority queue");
            pq.Enqueue(s5);
            Console.WriteLine($"Adding {s3} to priority queue");
            pq.Enqueue(s3);
            Console.WriteLine($"Adding {s6} to priority queue");
            pq.Enqueue(s6);
            Console.WriteLine($"Adding {s4} to priority queue");
            pq.Enqueue(s4);
            Console.WriteLine($"Adding {s1} to priority queue");
            pq.Enqueue(s1);
            Console.WriteLine($"Adding {s2} to priority queue");
            pq.Enqueue(s2);


            Console.WriteLine("\nPriority queue elements are:");
            Console.WriteLine(pq.ToString());
            Console.WriteLine($"\nPriority queue size is: {pq.Size()}");
            Console.WriteLine();

            pq.SortPriorityQueue();
            Console.WriteLine("\nSorted Priority Queue: "+ pq.ToString());


            Console.WriteLine($"\nPeeking element of priority queue: {pq.Peek()}");
            Console.WriteLine($"Checking if priority queue contains peeked element: {pq.Contains(s1)}");


            Console.WriteLine("\nRemoving element from priority queue");
            var s = pq.Dequeue();
            Console.WriteLine($"Removed element is {s}");
            Console.WriteLine($"\nChecking if priority queue contains peeked element: {pq.Contains(s1)}");
            Console.WriteLine("\nPriority queue is now:");
            Console.WriteLine(pq.ToString());
            Console.WriteLine();


            Console.WriteLine("\nRemoving another element from priority queue");
            s = pq.Dequeue();
            Console.WriteLine($"Removed student is {s}");
            Console.WriteLine("\nPriority queue is now:");
            Console.WriteLine(pq.ToString());
            Console.WriteLine();


            Console.WriteLine("Printing the reversed priority queue:");
            var reversed = pq.Reverse();
            reversed.Print();
            Console.WriteLine();


            Console.WriteLine("Iterating through the reversed priority queue:");
            foreach (var elem in reversed)
            {
                Console.WriteLine(elem);
            }
            Console.WriteLine();

            Console.WriteLine("Middle element of Priority Queue : " + reversed.findMiddle());

            Console.WriteLine("\nDequeueing elements from the reversed priority queue:");
            Console.WriteLine(reversed.Dequeue());
            Console.WriteLine(reversed.Dequeue());
            Console.WriteLine(reversed.Dequeue());
            Console.WriteLine(reversed.Dequeue());

            Console.ReadLine();
        }
    }


    public class Student : IComparable<Student>
    {
        public string LastName { get; }
        public double Priority { get; set; } // smaller values are higher priority


        public Student(string lastName, double priority)
        {
            LastName = lastName;
            Priority = priority;
        }


        public override string ToString() => "(" + LastName + ", " + Priority.ToString("F1") + ")";


        public int CompareTo(Student other)
        {
            if (Priority < other.Priority) return -1;
            if (Priority > other.Priority) return 1;
            return 0;
        }
    }


    public class PriorityQueue<T> : IEnumerable<T> where T : IComparable<T>
    {
        private readonly List<T> data;
        private readonly bool reversed;


        public PriorityQueue(bool reversed = false)
        {
            data = new List<T>();
            this.reversed = reversed;
        }



        /// <summary>
        ///  insert elements in Priority Queue
        /// </summary>
        /// <returns></returns>
        public void Enqueue(T item)
        {
            data.Add(item);
            var ci = data.Count - 1; // child index; start at end
            while (ci > 0)
            {
                var pi = (ci - 1) / 2; // parent index
                var compared = data[ci].CompareTo(data[pi]);
                if (!reversed && compared >= 0 || reversed && compared < 0)
                    break; // child item is larger than (or equal) parent so we're done

                Swap(ci, pi);
                ci = pi;
            }

        }



        /// <summary>
        ///  delete element of Priority Queue
        /// </summary>
        /// <returns></returns>
        public T Dequeue()
        {

            // assumes pq is not empty; up to calling code
            var li = data.Count - 1; // last index (before removal)
            var frontItem = data[0]; // fetch the front
            data[0] = data[li];
            data.RemoveAt(li);

            li--; // last index (after removal)
            var pi = 0; // parent index. start at front of pq
            while (true)
            {
                var ci = pi * 2 + 1; // left child index of parent
                if (ci > li) break; // no children so done


                var rc = ci + 1; // right child                
                if (rc <= li &&
                    (!reversed && data[rc].CompareTo(data[ci]) < 0 || reversed && data[rc].CompareTo(data[ci]) >= 0)) // if there is a rc (ci + 1), and it is smaller than left child, use the rc instead
                    ci = rc;

                var comparedPi = data[pi].CompareTo(data[ci]);
                if (!reversed && comparedPi <= 0 || reversed && comparedPi > 0)
                    break; // parent is smaller than (or equal to) smallest child so done

                Swap(pi, ci); // swap parent and child
                pi = ci;
            }
            return frontItem;
        }



        /// <summary>
        ///  display top most element of Priority Queue
        /// </summary>
        /// <returns></returns>
        public T Peek() => data[0];





        /// <summary>
        ///     sort Priority Queue
        /// </summary>
        public void SortPriorityQueue() => data.Sort();



        /// <summary>
        ///  check if an element presents in Priority Queue or not
        /// </summary>
        /// <returns></returns>
        public bool Contains(T elem)
        {
            return data.Contains(elem);
        }



        /// <summary>
        ///  Reverse Priority Queue
        /// </summary>
        /// <returns></returns>
        public PriorityQueue<T> Reverse()
        {
            var reversedQueue = new PriorityQueue<T>(true);
            foreach (var elem in data)
            {
                reversedQueue.Enqueue(elem);
            }
            return reversedQueue;
        }



        /// <summary>
        ///     middle element of a Priority Queue
        /// </summary>
        /// <returns></returns>

        public T findMiddle()
        {
            int i = 0;
            while (i < (Size()/2))
            {
                i++;
            }
            return data[i];
        }



        /// <summary>
        ///  display size of Priority Queue
        /// </summary>
        /// <returns></returns>
        public int Size() => data.Count;




        /// <summary>
        ///  display elements through Iterator of Priority Queue
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator() => data.GetEnumerator();


        IEnumerator IEnumerable.GetEnumerator() => data.GetEnumerator();




        /// <summary>
        ///  display elements of Priority Queue
        /// </summary>
        /// <returns></returns>
        public void Print() => Console.WriteLine(ToString());


        public override string ToString()
        {
            var s = default(string);


            foreach (var elem in data)
                s += elem + " ";


            return s;
        }


        private void Swap(int firstIndex, int secondIndex)
        {
            var tmp = data[firstIndex];
            data[firstIndex] = data[secondIndex];
            data[secondIndex] = tmp;
        }
    }
}
