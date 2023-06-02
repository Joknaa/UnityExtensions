using System;
using System.Collections.Generic;

namespace OknaaEXTENSIONS {
    public static class CollectionsExtensions {
        /// <summary> 
        /// Takes a random element of a list
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static T Random<T>(this IList<T> list) {
            T randomElement;
            try {
                randomElement = list[UnityEngine.Random.Range(0, list.Count)];
            }
            catch (Exception e) {
                Console.WriteLine("You cant take a random element out of an empty/null list. " + e);
                throw;
            }

            return randomElement;
        }
        

        /// <summary>
        /// Shuffles the elements of a list
        /// </summary>
        /// <param name="list"></param>
        /// <typeparam name="T"></typeparam>
        public static void Shuffle<T>(this List<T> list) {
            var count = list.Count;
            for (int i = 0; i < count; i++) {
                T temp = list[i];
                int randomIndex = UnityEngine.Random.Range(i, list.Count);
                list[i] = list[randomIndex];
                list[randomIndex] = temp;
            }
        }

        /// <summary>
        /// Converts an IEnumerable to a List
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static List<T> ToList<T>(this IEnumerable<T> source) {
            return source != null ? new List<T>(source) : new List<T>();
        }


        /// <summary>
        /// Fills the list with int values from start to end
        /// </summary>
        /// <param name="list"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static IList<int> AddRange<T>(this IList<int> list, int start, int end) {
            for (int i = start; i < end; i++) {
                list.Add(i);
            }

            return list;
        }

        /// <summary>
        /// Removes the elements of one list from another list
        /// </summary>
        /// <param name="list">this list we are gonna remove from</param>
        /// <param name="subtractedList">the list we are gonna remove</param>
        /// <returns></returns>
        public static IList<T> Subtract<T>(this IList<T> list, IList<T> subtractedList) {
            List<T> newList = new List<T>();
            foreach (var element in list) {
                if (!subtractedList.Contains(element)) newList.Add(element);
            }

            return newList;
        }

        /// <summary> 
        /// Gets the next element in a list, if the CURRENT element is NULL, or is the last one, the first one is returned
        /// </summary>
        /// <param name="list"></param>
        /// <param name="currentElement">the current element of the list</param>
        /// <returns></returns>
        public static T Next<T>(this IList<T> list, T currentElement = default) {
            var currentIndex = currentElement == null ? 0 : list.IndexOf(currentElement);
            var nextIndex = currentIndex + 1;
            if (nextIndex >= list.Count) nextIndex = 0;
            return list[nextIndex];
        }


        /// <summary> 
        /// Checks if a number is between two other numbers, the order of "a" and "b" is irrelevant.
        /// </summary>
        /// <param name="number">Number to check</param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool IsBetween<T>(this T number, T a, T b) where T : IComparable<T> {
            var min = a.CompareTo(b) < 0 ? a : b;
            var max = a.CompareTo(b) < 0 ? b : a;
            return number.CompareTo(min) >= 0 && number.CompareTo(max) <= 0;
        }
    }
}