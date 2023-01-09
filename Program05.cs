using System;
using System.Collections;
using System.Collections.Generic;
namespace DataStructures05
{
    class Program05
    {
        static void Main(string[] args)
        {
            var ht = new HashTable<int, string>();
            Console.WriteLine($"\nSize of hashmap: {ht.Size}");
            Console.WriteLine("\nInserting mapping 0 -> zero, 1 -> one");
            ht.Insert(0, "zero");
            ht.Insert(1, "one");
            Console.WriteLine($"\nSize of hashmap: {ht.Size}");
            Console.WriteLine($"\nGetting value by key 0: {ht[0]}");
            Console.WriteLine($"\nGetting value by key 1: {ht[1]}");


            Console.WriteLine("\nIterating through key-value pairs:");
            foreach (var pair in ht)
            {
                Console.WriteLine($"{pair.Key}->{pair.Value}");
            }


            Console.WriteLine("\nRemoving value by 0");
            ht.Remove(0);


            Console.WriteLine("\nPrinting hashtable:");
            ht.Print();


            Console.WriteLine($"\nSize of hashmap: {ht.Size}");


            Console.WriteLine($"\nhashmap contains value by key 1: {ht.ContainsKey(1)}");
            Console.WriteLine($"\nhashmap contains value by key 2: {ht.ContainsKey(2)}");
        }


        public class HashTable<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
        {
            public static readonly int[] primes = {
            3, 7, 11, 17, 23, 29, 37, 47, 59, 71, 89, 107, 131, 163, 197, 239, 293, 353, 431, 521, 631, 761, 919,
            1103, 1327, 1597, 1931, 2333, 2801, 3371, 4049, 4861, 5839, 7013, 8419, 10103, 12143, 14591,
            17519, 21023, 25229, 30293, 36353, 43627, 52361, 62851, 75431, 90523, 108631, 130363, 156437,
            187751, 225307, 270371, 324449, 389357, 467237, 560689, 672827, 807403, 968897, 1162687, 1395263,
            1674319, 2009191, 2411033, 2893249, 3471899, 4166287, 4999559, 5999471, 7199369};


            private struct Entry
            {
                public int hashCode;
                public int next;
                public TKey key;
                public TValue value;
            }


            private int[] buckets;
            private Entry[] entries;
            private int count;
            private int version;
            private int freeList;
            private int freeCount;
            private IEqualityComparer<TKey> comparer;

            /// <summary>
            /// constructor
            /// </summary>
            /// <returns></returns>
            public HashTable()
            {
                Initialize(0);
                this.comparer = EqualityComparer<TKey>.Default;
            }

            /// <summary>
            ///  display size
            /// </summary>
            /// <returns></returns>
            public int Size
            {
                get { return count - freeCount; }
            }

            /// <summary>
            ///     Check if element present or not
            /// </summary>
            /// <returns></returns>
            public bool ContainsKey(TKey key)
            {
                return FindEntry(key) >= 0;
            }

            /// <summary>
            ///   getting and setting values for hashtable
            /// </summary>
            /// <returns></returns>
            public TValue this[TKey key]
            {
                get
                {
                    int i = FindEntry(key);
                    if (i >= 0) return entries[i].value;
                    return default(TValue);
                }
                set
                {
                    Insert(key, value);
                }
            }

            /// <summary>
            ///     display elements of hashtable
            /// </summary>
            /// <returns></returns>
            public void Print()
            {
                foreach (var pair in this)
                {
                    Console.WriteLine($"{pair.Key}->{pair.Value}");
                }
            }

            /// <summary>
            ///  Iterator
            /// </summary>
            /// <returns></returns>
            public Enumerator GetEnumerator()
            {
                return new Enumerator(this);
            }


            IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
            {
                return new Enumerator(this);
            }

            /// <summary>
            ///  finds entry
            /// </summary>
            /// <returns></returns>
            private int FindEntry(TKey key)
            {
                if (buckets != null)
                {
                    int hashCode = comparer.GetHashCode(key) & 0x7FFFFFFF;
                    for (int i = buckets[hashCode % buckets.Length]; i >= 0; i = entries[i].next)
                    {
                        if (entries[i].hashCode == hashCode && comparer.Equals(entries[i].key, key)) return i;
                    }
                }
                return -1;
            }

            /// <summary>
            ///  To initialize capacity
            /// </summary>
            /// <returns></returns>
            private void Initialize(int capacity)
            {
                int size = GetPrime(capacity);
                buckets = new int[size];
                for (int i = 0; i < buckets.Length; i++) buckets[i] = -1;
                entries = new Entry[size];
                freeList = -1;
            }


            /// <summary>
            ///  to insert key and value
            /// </summary>
            /// <returns></returns>
            public void Insert(TKey key, TValue value)
            {
                if (buckets == null) Initialize(0);
                int hashCode = comparer.GetHashCode(key) & 0x7FFFFFFF;
                int targetBucket = hashCode % buckets.Length;


                for (int i = buckets[targetBucket]; i >= 0; i = entries[i].next)
                {
                    if (entries[i].hashCode == hashCode && comparer.Equals(entries[i].key, key))
                    {
                        entries[i].value = value;
                        version++;
                        return;
                    }
                }
                int index;
                if (freeCount > 0)
                {
                    index = freeList;
                    freeList = entries[index].next;
                    freeCount--;
                }
                else
                {
                    if (count == entries.Length)
                    {
                        Resize();
                        targetBucket = hashCode % buckets.Length;
                    }
                    index = count;
                    count++;
                }


                entries[index].hashCode = hashCode;
                entries[index].next = buckets[targetBucket];
                entries[index].key = key;
                entries[index].value = value;
                buckets[targetBucket] = index;
                version++;
            }


            /// <summary>
            ///  to resize hashtable
            /// </summary>
            /// <returns></returns>

            private void Resize()
            {
                Resize(ExpandPrime(count), false);
            }



            /// <summary>
            ///  to expand size
            /// </summary>
            /// <returns></returns>
            public static int ExpandPrime(int oldSize)
            {
                int newSize = 2 * oldSize;


                return GetPrime(newSize);
            }


            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            private void Resize(int newSize, bool forceNewHashCodes)
            {
                int[] newBuckets = new int[newSize];
                for (int i = 0; i < newBuckets.Length; i++) newBuckets[i] = -1;
                Entry[] newEntries = new Entry[newSize];
                Array.Copy(entries, 0, newEntries, 0, count);
                if (forceNewHashCodes)
                {
                    for (int i = 0; i < count; i++)
                    {
                        if (newEntries[i].hashCode != -1)
                        {
                            newEntries[i].hashCode = (comparer.GetHashCode(newEntries[i].key) & 0x7FFFFFFF);
                        }
                    }
                }
                for (int i = 0; i < count; i++)
                {
                    if (newEntries[i].hashCode >= 0)
                    {
                        int bucket = newEntries[i].hashCode % newSize;
                        newEntries[i].next = newBuckets[bucket];
                        newBuckets[bucket] = i;
                    }
                }
                buckets = newBuckets;
                entries = newEntries;
            }



            /// <summary>
            ///  to remove value by its key
            /// </summary>
            /// <returns></returns>
            public bool Remove(TKey key)
            {
                if (buckets != null)
                {
                    int hashCode = comparer.GetHashCode(key) & 0x7FFFFFFF;
                    int bucket = hashCode % buckets.Length;
                    int last = -1;
                    for (int i = buckets[bucket]; i >= 0; last = i, i = entries[i].next)
                    {
                        if (entries[i].hashCode == hashCode && comparer.Equals(entries[i].key, key))
                        {
                            if (last < 0)
                            {
                                buckets[bucket] = entries[i].next;
                            }
                            else
                            {
                                entries[last].next = entries[i].next;
                            }
                            entries[i].hashCode = -1;
                            entries[i].next = freeList;
                            entries[i].key = default(TKey);
                            entries[i].value = default(TValue);
                            freeList = i;
                            freeCount++;
                            version++;
                            return true;
                        }
                    }
                }
                return false;
            }


            IEnumerator IEnumerable.GetEnumerator()
            {
                return new Enumerator(this);
            }


            public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>,
                IDictionaryEnumerator
            {
                private HashTable<TKey, TValue> dictionary;
                private int index;
                private KeyValuePair<TKey, TValue> current;


                internal const int DictEntry = 1;
                internal const int KeyValuePair = 2;


                internal Enumerator(HashTable<TKey, TValue> dictionary)
                {
                    this.dictionary = dictionary;
                    index = 0;
                    current = new KeyValuePair<TKey, TValue>();
                }


                public bool MoveNext()
                {
                    while ((uint)index < (uint)dictionary.count)
                    {
                        if (dictionary.entries[index].hashCode >= 0)
                        {
                            current = new KeyValuePair<TKey, TValue>(dictionary.entries[index].key, dictionary.entries[index].value);
                            index++;
                            return true;
                        }
                        index++;
                    }


                    index = dictionary.count + 1;
                    current = new KeyValuePair<TKey, TValue>();
                    return false;
                }


                public KeyValuePair<TKey, TValue> Current
                {
                    get { return current; }
                }


                public void Dispose()
                {
                }


                object IEnumerator.Current
                {
                    get
                    {
                        return new KeyValuePair<TKey, TValue>(current.Key, current.Value);
                    }
                }


                void IEnumerator.Reset()
                {
                    index = 0;
                    current = new KeyValuePair<TKey, TValue>();
                }


                DictionaryEntry IDictionaryEnumerator.Entry
                {
                    get
                    {
                        return new DictionaryEntry(current.Key, current.Value);
                    }
                }


                object IDictionaryEnumerator.Key
                {
                    get
                    {
                        return current.Key;
                    }
                }


                object IDictionaryEnumerator.Value
                {
                    get
                    {
                        return current.Value;
                    }
                }
            }


            public static int GetPrime(int min)
            {
                for (int i = 0; i < primes.Length; i++)
                {
                    int prime = primes[i];
                    if (prime >= min) return prime;
                }


                return min;
            }
        }
    }
}
