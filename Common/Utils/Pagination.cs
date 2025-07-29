namespace webecommerce.Common.Utils
{
    public class Pagination
    {
        public int Limit { get; set; }
        public int Offset { get; set; }

        public Pagination(int limit = 10, int offset = 0)
        {
            Limit = limit;
            Offset = offset;
        }
    }
} 