using System;
using Net.Sf.Pkcs11.Wrapper;

namespace Net.Sf.Pkcs11.Delegates
{
	internal delegate CKR  C_GenerateKeyPair(
		uint hSession,
		ref CK_MECHANISM pMechanism,
		CK_ATTRIBUTE[] pPublicKeyTemplate,
		uint ulPublicKeyAttributeCount,
		CK_ATTRIBUTE[] pPrivateKeyTemplate,
		uint ulPrivateKeyAttributeCount,
		ref uint phPublicKey,
		ref uint phPrivateKey
	);
}