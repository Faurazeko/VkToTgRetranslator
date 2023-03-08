using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;

namespace VkToTgRetranslator
{
    class Utils
    {
        public static string GetNameOfCallingClass(int skipFrames = 2)
        {
            string fullName;
            Type declaringType;
            do
            {
                MethodBase method = new StackFrame(skipFrames, false).GetMethod();
                declaringType = method.DeclaringType;
                if (declaringType == null)
                {
                    return method.Name;
                }
                skipFrames++;
                fullName = declaringType.FullName;
            }
            while (declaringType.Module.Name.Equals("mscorlib.dll", StringComparison.OrdinalIgnoreCase));

            return fullName;
        }

        public static string GetLastNameOfCallingClassWithSpaces()
        {
            var lastName = GetNameOfCallingClass(3).Split('.').Last();

            lastName = Regex.Replace(lastName, "([a-z])([A-Z])", "$1 $2");

            return lastName;
        }

        public static DateTime UnixTimeToLocal(double unixTime)
		{
			var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

			return dateTime.AddSeconds(unixTime).ToLocalTime();
		}
    }
}
