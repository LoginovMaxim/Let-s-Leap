namespace Gameplay.Extensions
{
    public static class StringExtensions
    {
        public static string GetShortTypeName(this string typeFullName)
        {
            return typeFullName.Split('.')[^1];
        }
    }
}