using VideoStore.Interfaces;

namespace VideoStore
{
    public class DateTime : IDateTime
    {
        public System.DateTime Now()
        {
            return System.DateTime.Now;
        }
    }
}
