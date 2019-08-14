using System;

namespace ODataBpmOnlineCRUD
{
    class TryParseProperty
    {
        public static string TryParseStringProperty(string propertyName, string entityName)
        {
            Console.WriteLine(String.Format(Localize.Input, propertyName, entityName, String.Format(Localize.TypeOnly, Localize.String)));
            string nameParseResult = Console.ReadLine();
            return nameParseResult;
        }
        public static int TryParseIntProperty(string propertyName, string entityName)
        {
            int propertyParseResult = 0;
            bool propertyParseResultIsSuccess = false;
            int attemptCount = 0;
            string propertyInputString = String.Format(Localize.Input, propertyName, entityName, String.Format(Localize.TypeOnly, Localize.Integer));
            while (propertyParseResultIsSuccess == false)
            {
                if (attemptCount == 0)
                    Console.WriteLine(propertyInputString);
                else if (attemptCount < 5)
                    Console.WriteLine(String.Format(Localize.TypeMismatch, Localize.Integer) + " " + propertyInputString);
                else
                    Environment.Exit(0);
                string completeness = Console.ReadLine();
                propertyParseResultIsSuccess = int.TryParse(completeness, out propertyParseResult);
                attemptCount += 1;
            }
            return propertyParseResult;
        }
        public static DateTime TryParseDateTimeProperty(string propertyName, string entityName)
        {
            DateTime propertyParseResult = new DateTime();
            bool propertyParseResultIsSuccess = false;
            int attemptCount = 0;
            string propertyInputString = String.Format(Localize.Input, propertyName, entityName, String.Format(Localize.TypeOnly, Localize.DateTime));
            while (propertyParseResultIsSuccess == false)
            {
                if (attemptCount == 0)
                    Console.WriteLine(propertyInputString);
                else if (attemptCount < 5)
                    Console.WriteLine(String.Format(Localize.TypeMismatch, Localize.DateTime) + " " + propertyInputString);
                else
                    Environment.Exit(0);
                string completeness = Console.ReadLine();
                propertyParseResultIsSuccess = DateTime.TryParse(completeness, out propertyParseResult);
                attemptCount += 1;
            }
            return propertyParseResult;
        }
        public static Guid TryParseGuidProperty(string propertyName, string entityName)
        {
            Guid propertyParseResult = Guid.Empty;
            bool propertyParseResultIsSuccess = false;
            int attemptCount = 0;
            string propertyInputString = String.Format(Localize.Input, propertyName, entityName, String.Format(Localize.TypeOnly, Localize.Guid));
            while (propertyParseResultIsSuccess == false)
            {
                if (attemptCount == 0)
                    Console.WriteLine(propertyInputString);
                else if (attemptCount < 5)
                    Console.WriteLine(String.Format(Localize.TypeMismatch, Localize.Guid) + " " + propertyInputString);
                else
                    Environment.Exit(0);
                string completeness = Console.ReadLine();
                propertyParseResultIsSuccess = Guid.TryParse(completeness, out propertyParseResult);
                attemptCount += 1;
            }
            return propertyParseResult;
        }
    }
}
