using System;
using Net.Sf.Pkcs11.Wrapper;

namespace Net.Sf.Pkcs11.Delegates
{
	internal delegate CKR C_Verify(
		uint hSession,
		byte[] pData,
		uint ulDataLen,
		byte[] pSignature,
		uint pulSignatureLen
	);
}
