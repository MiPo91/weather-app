using System;
using eKoodi.Utilities.Test;

namespace weather_app
{
    class Program
    {

        static void Main()
        {
            Console.WriteLine("Select Option: 1. Weather Now, 2. Weather next 7 days.");
            string type = Console.ReadLine();
            Console.WriteLine("Give city name, ex. london");
            string city = Console.ReadLine();
            Console.WriteLine("Give country code of the city, ex. uk");
            string country = Console.ReadLine();

            if (type == "1")
            {
                eKoodi.Utilities.Test.TestUtility.GetAndSave(type, city, country);
                eKoodi.Utilities.Test.TestUtility.ForecastNow();
            }
            else if( type == "2")
            {
                eKoodi.Utilities.Test.TestUtility.GetAndSave(type, city, country);
                eKoodi.Utilities.Test.TestUtility.ForecastFiveDays();
            }

            Console.ReadKey();
        }


    }
}
