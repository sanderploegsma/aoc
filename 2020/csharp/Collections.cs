﻿using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
    public static class Collections
    {
        public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> o) where T : class =>
            o.Where(x => x != null)!;

        public static IEnumerable<(T, T)> Pairwise<T>(this IEnumerable<T> items)
        {
            var list = items.ToList();
            return list.SkipLast(1).Zip(list.Skip(1));
        }

        public static IEnumerable<(T Value, int Index)> Indexed<T>(this IEnumerable<T> items) =>
            items.Select((item, i) => (item, i));

        public static ISet<T> UnionAll<T>(this IEnumerable<ISet<T>> sets)
        {
            var list = sets.ToList();
            return list
                .Skip(1)
                .Aggregate(list.First(),
                    (acc, cur) =>
                    {
                        acc.UnionWith(cur);
                        return acc;
                    });
        }

        public static ISet<T> IntersectAll<T>(this IEnumerable<ISet<T>> sets)
        {
            var list = sets.ToList();
            return list
                .Skip(1)
                .Aggregate(list.First(),
                    (acc, cur) =>
                    {
                        acc.IntersectWith(cur);
                        return acc;
                    });
        }

        public static IEnumerable<IEnumerable<T>> SubSets<T>(this IEnumerable<T> items)
        {
            var set = items.ToArray();

            bool[] state = new bool[set.Length + 1];
            for (int x; !state[set.Length]; state[x] = true)
            {
                yield return Enumerable.Range(0, state.Length)
                    .Where(i => state[i])
                    .Select(i => set[i]);

                for (x = 0; state[x]; state[x++] = false)
                {
                }
            }
        }
    }
}