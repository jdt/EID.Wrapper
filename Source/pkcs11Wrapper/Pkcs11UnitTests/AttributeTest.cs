using System;
using NUnit.Framework;
using Net.Sf.Pkcs11.Objects;
using Net.Sf.Pkcs11.Wrapper;

namespace Net.Sf.Pkcs11.Test
{
	[TestFixture]
	class AttributeTest
	{
		[Test]
		public void DateAttributeTest()
		{
			DateAttribute da= new DateAttribute();
			da.Value= DateTime.Today;
			
			DateAttribute da2= new DateAttribute(da.CK_ATTRIBUTE);
			
			Assert.AreEqual( da.Value.Year, da2.Value.Year);
			Assert.AreEqual( da.Value.Month, da2.Value.Month);
			Assert.AreEqual( da.Value.Day, da2.Value.Day);
		}
		
		[Test]
		public void UIntAttributeTest(){
			UIntAttribute attr= new UIntAttribute(3);
			attr.Value = 100;
			UIntAttribute attr2=new UIntAttribute(attr.CK_ATTRIBUTE);

			Assert.AreEqual( attr.Value,attr2.Value );
			Assert.AreEqual(attr.Type, attr2.Type);
			
		}
		
		[Test]
		public void BooleanAttributeTest(){
			BooleanAttribute attr= new BooleanAttribute(3);
			attr.Value = true;
			BooleanAttribute attr2=new BooleanAttribute(attr.CK_ATTRIBUTE);

			Assert.AreEqual( attr.Value,attr2.Value );
			Assert.AreEqual(attr.Type, attr2.Type);
			
			attr.Value=false;
			
			attr2=new BooleanAttribute(attr.CK_ATTRIBUTE);

			Assert.AreEqual( attr.Value,attr2.Value );
			Assert.AreEqual(attr.Type, attr2.Type);
			
		}
	}
}
