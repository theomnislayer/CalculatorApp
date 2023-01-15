namespace CalculatorLib
{
    public static class Constants
    {
        //So it won't go past 8 digits.
        public const int LongestLength = 8;
        public static List<string> OperationString = new List<string>()
        {
            string.Empty,
            "/",
            "*",
            "-",
            "+"
        };
    }
}
