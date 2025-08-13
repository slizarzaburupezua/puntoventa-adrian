namespace PERFISOFT.VENTASPLATFORM.SEEDWORK.DOMAIN
{
    public class Filter<T>
    {
        public Pager Pagination { get; set; }
        public List<Func<T, bool>> Conditions { get; set; }
    }
}
