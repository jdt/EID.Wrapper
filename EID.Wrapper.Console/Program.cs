using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EID.Wrapper.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var wrapper = new Wrapper();
            ICardData data = wrapper.GetCardData();

            foreach (var prop in data.GetType().GetProperties())
            {
                System.Console.WriteLine("{0}={1}", prop.Name, prop.GetValue(data, null));
            }

            System.Console.WriteLine("Done... Press any key to exit");
            System.Console.ReadKey();
        }
    }
}
