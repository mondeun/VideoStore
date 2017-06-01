using VideoStore.Interfaces;

namespace VideoStore
{
    /// <summary>
    /// Wrapper for System.DateTime
    /// </summary>
    public class DateTime : IDateTime
    {
        /// <summary>
        /// Get current time
        /// </summary>
        /// <returns>Current time</returns>
        public System.DateTime Now()
        {
            return System.DateTime.Now;
        }
    }
}
