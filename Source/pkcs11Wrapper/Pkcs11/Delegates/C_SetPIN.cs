
using System;
using Net.Sf.Pkcs11.Wrapper;

namespace Net.Sf.Pkcs11.Delegates
{
	internal delegate CKR C_SetPIN(
		uint hSession, 
		byte[] pOldPin, 
		uint ulOldLen,
		byte[] pNewPin, 
		uint ulNewLen
	);
}
