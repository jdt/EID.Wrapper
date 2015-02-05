
using System;
using Net.Sf.Pkcs11.Wrapper;

namespace Net.Sf.Pkcs11.Delegates
{
	internal delegate CKR C_Login(
		uint hSession,
		CKU userType,
		byte[] pPin,
		uint ulPinLen
	);
}
