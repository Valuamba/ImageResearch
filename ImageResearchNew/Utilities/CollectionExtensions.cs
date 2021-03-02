using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ImageResearchNew.Utilities
{
    public static class CollectionExtensions
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> enumerable)
        {
            return new ObservableCollection<T>(enumerable);
        }
    }
}
