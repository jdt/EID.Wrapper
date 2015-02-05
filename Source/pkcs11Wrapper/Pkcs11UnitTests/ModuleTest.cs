using System;
using NUnit.Framework;
using Net.Sf.Pkcs11.Objects;
using Net.Sf.Pkcs11.Wrapper;
using Net.Sf.Pkcs11;

namespace Net.Sf.Pkcs11.Test
{
	[TestFixture]
	class ModuleTest
	{
		[Test]
		public void GetInfoTest()
		{
			Module module=Module.GetInstance("gclib.dll");

            using (module)
            {
                Info info = module.GetInfo();
                Console.WriteLine(info);
            }
		}
	}
}
