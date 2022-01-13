namespace WordCounter.Lib.Processing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    internal static class Sorter
    {
        public static IEnumerable<KeyValuePair<string, int>> Sort(Dictionary<string, int> dictionary)
        {
            var items = from pair in dictionary
                              orderby pair.Value descending
                              select pair;

            return items;
        }
    }
}
