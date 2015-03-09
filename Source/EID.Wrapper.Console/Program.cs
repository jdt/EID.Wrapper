using System;
using System.Collections.Generic;
using System.IO;
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

            System.Console.Write("Save photo to temp folder and display? [y]: ");

            var input = System.Console.ReadLine();
            if (string.IsNullOrEmpty(input) || input == "y")
            {
                var photoPath = Path.GetTempFileName() + ".jpg";
                data.SavePhoto(photoPath);
                System.Console.WriteLine("Photo saved to " + photoPath);
                System.Diagnostics.Process.Start(photoPath);
            }
            System.Console.WriteLine("Done... Press any key to exit");
            System.Console.ReadKey();
        }
    }
}
