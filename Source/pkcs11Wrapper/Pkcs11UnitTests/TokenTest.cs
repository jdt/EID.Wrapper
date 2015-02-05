

using System;
using Net.Sf.Pkcs11;
using Net.Sf.Pkcs11.Wrapper;
using NUnit.Framework;

namespace Net.Sf.Pkcs11.Test
{
    [TestFixture]
    class TokenTest
    {
        [Test]
        public void TokenInfoTest()
        {
            Module module = Module.GetInstance("siecap11.dll");
            using (module)
            {
                Slot slot = module.GetSlotList(true)[0];
                TokenInfo ti = slot.Token.TokenInfo;
                Console.WriteLine(ti);
            }
        }

        [Test]
        public void GetCkmList()
        {
            Module module = Module.GetInstance("gclib.dll");
            using (module)
            {
                Slot slot = module.GetSlotList(true)[0];

                CKM[] ckms = slot.Token.MechanismList;

                foreach (CKM ckm in ckms) Console.WriteLine(ckm);

            }
        }
    }
}
