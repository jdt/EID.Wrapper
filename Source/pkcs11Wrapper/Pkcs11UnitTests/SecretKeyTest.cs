using System;
using Net.Sf.Pkcs11;
using Net.Sf.Pkcs11.Objects;
using Net.Sf.Pkcs11.Wrapper;
using NUnit.Framework;

namespace Net.Sf.Pkcs11.Test
{
	[TestFixture]
	public class SecretKeyTest
	{
		[Test]
		public void GenerateEncryptDecryptDestroyTest()
		{
			
			Mechanism mech= new Mechanism(CKM.DES3_KEY_GEN);
			Des3SecretKey template=new Des3SecretKey();
			template.Token.Value=true;
			template.Sensitive.Value=true;
			template.Label.Value="my-test-des3-key".ToCharArray();
			
			template.Modifiable.Value=true;
			template.Value.Value=new byte[]{
				1,2,3,4,5,6,7,8,9,0,
				1,2,3,4,5,6,7,8,9,0,
				1,2,3,4				
			};
			
			Des3SecretKey sc2 =(Des3SecretKey )session.CreateObject(template);
						
		}
		
		#region session olusturulup kapatılıyor.
		
		Session session;
		
		
		[TestFixtureSetUp]
		public void Init()
		{
			Module m=Module.GetInstance("siecap11.dll");
			
			session= m.GetSlotList(true)[0].Token.OpenSession(false);
			
			session.Login(UserType.USER, "1234");
		}
		
		[TestFixtureTearDown]
		public void Dispose()
		{
			session.Logout();
			session.Module.Dispose();
			
			Console.WriteLine();
		}
		#endregion
	}
}
