using System;

namespace Numbers
{
    public static class IntegerExtensions
    {
        /// <summary>
        /// Obtains formalized information in the form of an enum <see cref="ComparisonSigns"/>
        /// about the relationship of the order of two adjacent digits for all digits of a given number.
        /// </summary>
        /// <param name="number">Source number.</param>
        /// <returns>Information in the form of an enum <see cref="ComparisonSigns"/>
        /// about the relationship of the order of two adjacent digits for all digits of a given number
        /// or null if the information is not defined.</returns>
        public static ComparisonSigns? GetTypeComparisonSigns(this long number)
        {
            if (number / 10 == 0)
            {
                return null;
            }

            if (number == long.MinValue)
            {
                return ComparisonSigns.LessThan | ComparisonSigns.Equals | ComparisonSigns.MoreThan;
            }

            long[] array = new long[64];
            int n = 63;
            while (number > 9)
            {
                array[n] = number % 10;
                n--;
                number /= 10;
            }

            array[n] = number;
            bool less = false, eq = false, more = false;

            for (; n < 63; n++)
            {
                if (array[n] < array[n + 1])
                {
                    less = true;
                }
                else if (array[n] > array[n + 1])
                {
                    more = true;
                }
                else
                {
                    eq = true;
                }
            }

            ComparisonSigns result = 0;
            if (less)
            {
                result = result | ComparisonSigns.LessThan;
            }

            if (more)
            {
                result = result | ComparisonSigns.MoreThan;
            }

            if (eq)
            {
                result = result | ComparisonSigns.Equals;
            }

            return result;
        }

        /// <summary>
        /// Gets information in the form of a string about the type of sequence that the digit of a given number represents.
        /// </summary>
        /// <param name="number">Source number.</param>
        /// <returns>The information in the form of a string about the type of sequence that the digit of a given number represents.</returns>
        public static string GetTypeOfDigitsSequence(this long number)
        {
            ComparisonSigns? result = GetTypeComparisonSigns(number);

            if (result == null)
            {
                return "One digit number.";
            }

            if ((int)result == 1)
            {
                return "Strictly Increasing.";
            }

            if ((int)result == 2)
            {
                return "Strictly Decreasing.";
            }

            if ((int)result == 4)
            {
                return "Monotonous.";
            }

            if ((int)result == 5)
            {
                return "Increasing.";
            }

            if ((int)result == 6)
            {
                return "Decreasing.";
            }

            return "Unordered.";
        }
    }
}
