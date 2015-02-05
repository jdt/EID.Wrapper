
using System;
using Net.Sf.Pkcs11.Wrapper;
namespace Net.Sf.Pkcs11.Delegates
{
	internal delegate CKR C_DigestFinal(
		uint hSession,
		byte[] pDigest,
		ref uint pulDigestLen
	);
}
