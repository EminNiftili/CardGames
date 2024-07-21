namespace Card.Core.Extensions
{
    public static class LinqExtension
    {
        public static T GetLastAndRemove<T>(this List<T> sender)
        {
            var lastItem = sender.LastOrDefault();
            if(lastItem == null)
            {
                return default;
            }
            sender.Remove(lastItem);
            return lastItem;
        }
        public static List<T> GetLastAndRemove<T>(this List<T> sender, int count)
        {
            List<T> removedValues = new();
            for(int i = 1; i <= count; i++)
            {
                var removedValue = GetLastAndRemove(sender);
                if(removedValue != null)
                {
                    removedValues.Add(removedValue);
                }
            }
            return removedValues;
        }
    }
}
