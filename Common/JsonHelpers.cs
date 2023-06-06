namespace Filuet.Hrbl.Ordering.Common
{
    public static class HrblResponseHelpers
    {
        public static string ResolveHrblMess(this string response)
            => response.Replace("{\"@nil\":\"true\"}", "null")
            .Replace("\"\"", "null"); // Deserialize empty string as null
    }
}