using System.Collections.Generic;

namespace System.ComponentModel
{
    public static class BindingListExtensions
    {
        public static void AddRange<TItemType>(this BindingList<TItemType> list, IList<TItemType> listItems)
        {
            AddToList(list, listItems, false);
        }
        public static void DistinctAddRange<TItemType>(this BindingList<TItemType> list, IList<TItemType> listItems)
        {
            AddToList<TItemType>(list, listItems, true);
        }

        private static void AddToList<TItemType>(BindingList<TItemType> list, IList<TItemType> listItems, bool checkContains)
        {
            list.RaiseListChangedEvents = false;
            for (int i = 0; i < listItems.Count; i++)
            {
                if (checkContains)
                {
                    if (list.Contains(listItems[i]))
                    {
                        continue;
                    }
                }
                list.Add(listItems[i]);
            }
            list.RaiseListChangedEvents = true;
            list.ResetBindings();
        }
    }
}