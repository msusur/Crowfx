using System.Collections;

namespace Crow.Library.Common.Helpers
{
    public static class ListUtils
    {
        public static bool IsListNullOrEmpty(IList list)
        {
            return list == null || list.Count == 0;
        }
    }

}