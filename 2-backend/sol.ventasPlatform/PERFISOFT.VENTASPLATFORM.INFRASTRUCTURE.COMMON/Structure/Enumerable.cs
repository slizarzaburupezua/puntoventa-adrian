namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Structure
{
    public static class Enumerable
    {
        /// <summary>
        /// Devuelve true si la lista es diferente de null y tiene al menos un item, de lo contrario devuelve false.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool HasItems<T>(this IEnumerable<T> enumerable)
        {
            return enumerable != null && enumerable.Any();
        }

    }
}
