
using System;
using Net.Sf.Pkcs11.Wrapper;
namespace Net.Sf.Pkcs11.Delegates
{
	internal delegate CKR C_Digest(
		uint hSession,
		byte[] pData,
		uint ulDataLen,
		byte[] pDigest,
		ref uint pulDigestLen
	);
}
