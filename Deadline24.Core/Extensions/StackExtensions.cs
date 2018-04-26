using System.Collections.Generic;

namespace Deadline24.Core.Extensions
{
    public static class StackExtensions
    {
        public static void PushMany<TElement>(this Stack<TElement> stack, IEnumerable<TElement> items)
        {
            foreach (var item in items)
            {
                stack.Push(item);
            }
        }
    }
}
