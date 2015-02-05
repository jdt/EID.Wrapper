using System;
using Net.Sf.Pkcs11.Wrapper;


namespace Net.Sf.Pkcs11.Delegates
{
	internal delegate CKR C_DecryptUpdate(
		uint hSession,
		byte[] pEncryptedPart,
		uint ulEncryptedPartLen,
		byte[] pPart,
		ref uint pulPartLen
	);
}
