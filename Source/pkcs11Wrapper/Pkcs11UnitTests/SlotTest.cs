using System;
using NUnit.Framework;
using Net.Sf.Pkcs11;

namespace Net.Sf.Pkcs11.Test
{
	[TestFixture]
	class SlotTest
	{
		[Test]
		public void getSlotInfoTest()
		{
			var module = Module.GetInstance("gclib.dll");
            
            using (module)
            {
                var slots = module.GetSlotList(true);

                foreach (var slot in slots)
                {
                    Console.WriteLine(slot.SlotId);

                    SlotInfo si = slot.SlotInfo;

                    Console.WriteLine(si.FirmwareVersion);
                    Console.WriteLine(si.HardwareVersion);
                    Console.WriteLine(si.ManufacturerID);
                    Console.WriteLine(si.SlotDescription);
                } 
            }
		}
	}
}
