using System;
using System.Collections.Generic;
using System.Text;

namespace TeamBuilders.Utilities
{
    public static class Check
    {
        public static void CheckLenght(int expectedLenght, string[] array)
        {
            if (expectedLenght != array.Length)
            {
                throw new FormatException(Constants.ErrorMessages.InvalidArgumentsCount);
            }
        }
    }
}
