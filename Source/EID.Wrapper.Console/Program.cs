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

            ListProperties(data);

            for (int i = 0; i < (data.Cards as ICard[]).Count(); i++ )
            {
                var card = (data.Cards as ICard[]).ElementAt(i);
                System.Console.WriteLine("--------Card " + i + "--------");
                ListProperties(card);

                if(card.CardStatus == CardStatus.Available)
                {
                    System.Console.Write("Save photo to temp folder and display? [y]: ");

                    var input = System.Console.ReadLine();
                    if (string.IsNullOrEmpty(input) || input == "y")
                    {
                        var photoPath = Path.GetTempFileName() + ".jpg";
                        card.SavePhoto(photoPath);
                        System.Console.WriteLine("Photo saved to " + photoPath);
                        System.Diagnostics.Process.Start(photoPath);
                    }
                }

                System.Console.WriteLine("-----------------------");
            }

            System.Console.WriteLine("Done... Press any key to exit");
            System.Console.ReadKey();
        }

        private static void ListProperties(object obj)
        {
            foreach (var prop in obj.GetType().GetProperties())
            {
                var dataAsList = prop.GetValue(obj, null) as string[];
                if(dataAsList != null)
                {
                    System.Console.WriteLine("{0}=", prop.Name);
                    foreach(var line in dataAsList)
                    {
                        System.Console.WriteLine("  - {0}", line);
                    }
                }
                else 
                {
                    System.Console.WriteLine("{0}={1}", prop.Name, prop.GetValue(obj, null));
                }
            }
        }
    }
}
