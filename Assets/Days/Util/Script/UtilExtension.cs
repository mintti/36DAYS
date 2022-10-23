namespace Days.Util.Script
{
    public static class UtilExtension
    {
        
        public static bool IsValidArray<T>(this T[,] arr, float x, float y)
        {
            return arr.IsValidArray((int) x, (int) y);
        }
        public static bool IsValidArray<T>(this T[,] arr, int x, int y)
        {
            return (x >= 0 && arr.GetLength(0) < x &&
                    y >= 0 && arr.GetLength(1) < y);
        }
    }
}