namespace iPlanner.Infrastructure.Common
{
    public static class IdGenerator
    {
        private static readonly object _lock = new object();

        public static string GenerateUUID()
        {
            lock (_lock)
            {
                return Guid.NewGuid().ToString();
            }
        }

        public static string GenerateUUIDWithoutHyphens()
        {
            lock (_lock)
            {
                return Guid.NewGuid().ToString("N");
            }
        }

        public static bool IsValidUUID(string uuid)
        {
            return Guid.TryParse(uuid, out _);
        }
    }
}
