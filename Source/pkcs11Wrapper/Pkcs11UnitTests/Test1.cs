using System;
using NUnit.Framework;
using Net.Sf.Pkcs11.Objects;
using Net.Sf.Pkcs11.Wrapper;
using Net.Sf.Pkcs11;

namespace Net.Sf.Pkcs11.Test
{
	[TestFixture]
	public class Test1
	{
		[Test]
		public void InitCard()
		{
			Pkcs11Module m= Pkcs11Module.GetInstance("siecap11.dll");
			m.Initialize();
			uint h=m.GetSlotList(true)[0];
			m.InitToken(h,"1234","my-card");
			uint hSession = m.OpenSession(h,0,false);
			m.Login(hSession, CKU.SO,"1234");
			m.InitPIN( hSession, "1234");
		}

		[Test]
		public void xxxxx()
		{
			Pkcs11Module m= Pkcs11Module.GetInstance("siecap11.dll");
			m.Initialize();
			uint h=m.GetSlotList(true)[0];
			uint hSession = m.OpenSession(h,0,false);
			m.Login(hSession, CKU.USER,"1234");

			CKM[] ckms=m.GetMechanismList(h);
			foreach(CKM ckm in ckms )
			{
				MechanismInfo tmp=new MechanismInfo(m.GetMechanismInfo(h, ckm));
				if(tmp.HW){
					Console.WriteLine(tmp);
				}
				Console.WriteLine(tmp);

			}
		}
		
		
		[Test]
		public void waitForSlotEvent()
		{
			Pkcs11Module m= Pkcs11Module.GetInstance("gclib.dll");
			m.Initialize();
			uint h=m.GetSlotList(true)[0];
			uint h2=m.WaitForSlotEvent(false);
			SlotInfo s= new SlotInfo(m.GetSlotInfo(h2));
			Console.WriteLine(s);
		}
		
	}
}
