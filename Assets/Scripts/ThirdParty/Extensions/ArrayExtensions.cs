using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace sdk_feed.Common.Extensions
{
    public static class ArrayExtensions
    {
        private static readonly Random _random = new Random();
        
        
        public static void Shuffle<T>(this IList<T> list)
        {
            var provider = new RNGCryptoServiceProvider();
            var n = list.Count;
            while (n > 1)
            {
                var box = new byte[1];
                do provider.GetBytes(box);
                while (!(box[0] < n * (byte.MaxValue / n)));
                var k = box[0] % n;
                n--;
                var value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static T GetRandomElement<T>(this T[] array) => array[UnityEngine.Random.Range(0, array.Length)];

        public static void Shuffle<T>(this LinkedList<T> list)
        {
            for (var i = list.Count; i > 1; i--)
            {
                var node = NodeAt(list, _random.Next(i));

                if (list.Last == node) continue;
                list.Remove(node);
                list.AddLast(node);
            }
        }

        private static LinkedListNode<T> NodeAt<T>(LinkedList<T> source, int index)
        {
            var current = source.First;
            while (index-- > 0)
            {
                current = current?.Next;
            }

            return current;
        }

        public static void MoveFirstElementToLast<T>(this LinkedList<T> list)
        {
            list.AddLast(list.First.Value);
            list.RemoveFirst();
        }
    }
}