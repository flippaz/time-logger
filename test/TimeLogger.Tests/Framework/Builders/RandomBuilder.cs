using System;

namespace TimeLogger.Tests.Framework.Builders
{
    public static class RandomBuilder
    {
        private static readonly Random _random = new Random();

        public static bool NextBool()
        {
            return NextNumber(2) == 0;
        }

        public static DateTime NextDateTime()
        {
            return NextDateTimeToMillisecond();
        }

        public static DateTime NextDateTimeToMillisecond()
        {
            var startTime = new DateTime(2000, 01, 01, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan maxOffset = DateTime.UtcNow - startTime;
            int totalSeconds = NextNumber((int)maxOffset.TotalSeconds);
            int milliseconds = NextNumber(1000);

            return startTime + TimeSpan.FromSeconds(totalSeconds) + TimeSpan.FromMilliseconds(milliseconds);
        }

        public static decimal NextDecimal()
        {
            return (decimal)NextDouble();
        }

        public static double NextDouble(int fractionalPlaces = 2)
        {
            lock (_random)
            {
                return Math.Round(_random.NextDouble(), fractionalPlaces);
            }
        }

        public static T NextEnum<T>()
        {
            Array values = Enum.GetValues(typeof(T));

            return (T)values.GetValue(NextNumber(values.Length));
        }

        public static long NextLong()
        {
            return (long)int.MaxValue + NextNumber();
        }

        public static int NextNumber()
        {
            lock (_random)
            {
                return _random.Next();
            }
        }

        public static int NextNumber(int maxValue)
        {
            lock (_random)
            {
                return _random.Next(maxValue);
            }
        }

        public static int NextNumber(int minValue, int maxValue)
        {
            lock (_random)
            {
                return _random.Next(minValue, maxValue);
            }
        }

        public static short NextShort(short maxValue = 9999)
        {
            lock (_random)
            {
                return (short)_random.Next(maxValue);
            }
        }

        public static string NextString()
        {
            return Guid.NewGuid().ToString();
        }
    }
}