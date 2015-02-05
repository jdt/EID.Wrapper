
using System;
using Net.Sf.Pkcs11.Wrapper;

namespace Net.Sf.Pkcs11.Delegates
{
	internal delegate CKR C_SignFinal(
		uint hSession,
		byte[] pSignature,
		ref uint pulSignatureLen
	);
}
