using Net.Sf.Pkcs11.Objects;
using Net.Sf.Pkcs11.Params;
using Net.Sf.Pkcs11.Wrapper;

using System;
using Net.Sf.Pkcs11;
using NUnit.Framework;

namespace Net.Sf.Pkcs11.Test
{
	[TestFixture]
	public class KeyPairTest
	{
		
		
		[Test]
		public void GenerateRsaKeyPair(){
			
			char[] lbl= "my-rsa-pair".ToCharArray();
			byte[] id= new byte[]{123};
			byte[] pubExp=new byte[]{3};
			uint modulusBits=1024;
			
			RSAPublicKey pubTemplate=new RSAPublicKey();
			pubTemplate.Token.Value=true;
			pubTemplate.Encrypt.Value=true;
			pubTemplate.Verify.Value=true;
			pubTemplate.Label.Value=lbl;
			pubTemplate.Id.Value=id;
			pubTemplate.PublicExponent.Value= pubExp;
			pubTemplate.ModulusBits.Value=modulusBits;
			
			RSAPrivateKey privTemplate= new RSAPrivateKey();
			privTemplate.Token.Value=true;
			privTemplate.Sign.Value=true;
			privTemplate.Decrypt.Value=true;
			privTemplate.Label.Value=lbl;
			privTemplate.Id.Value=id;
			privTemplate.Sensitive.Value=true;
			
			KeyPair kp = session.GenerateKeyPair(new Mechanism(CKM.RSA_PKCS_KEY_PAIR_GEN),pubTemplate,privTemplate);
			Console.WriteLine(kp);
		}
		
		
		
		
		#region session olusturulup kapatılıyor.
		
		Session session;
		
		
		[TestFixtureSetUp]
		public void Init()
		{
			Module m=Module.GetInstance("siecap11.dll");
			
			session= m.GetSlotList(true)[0].Token.OpenSession(false);
			
			session.Login(UserType.USER,"1234");
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
