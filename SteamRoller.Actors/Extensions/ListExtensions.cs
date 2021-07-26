using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteamRoller.Actors.Extensions
{
    public static class ListExtensions
    {
        public static List<T> FindCommon<T>(IEnumerable<List<T>> lists)
        {
            Dictionary<T, int> map = new Dictionary<T, int>();
            int listCount = 0; // number of lists

            foreach (IEnumerable<T> list in lists)
            {
                listCount++;
                foreach (T item in list)
                {
                    // Item encountered, increment count
                    int currCount;
                    if (!map.TryGetValue(item, out currCount))
                        currCount = 0;

                    currCount++;
                    map[item] = currCount;
                }
            }

            List<T> result = new List<T>();
            foreach (KeyValuePair<T, int> kvp in map)
            {
                // Items whose occurrence count is equal to the number of lists are common to all the lists
                if (kvp.Value == listCount)
                    result.Add(kvp.Key);
            }

            return result;
        }

        public static IEnumerable<TResult> IntersectMany<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, IEnumerable<TResult>> selector)
        {
            using (var enumerator = source.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                    return new TResult[0];

                var ret = selector(enumerator.Current);

                while (enumerator.MoveNext())
                {
                    ret = ret.Intersect(selector(enumerator.Current));
                }

                return ret;
            }
        }
    }
}
