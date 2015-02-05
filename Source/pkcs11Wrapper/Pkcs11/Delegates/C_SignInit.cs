
using System;
using Net.Sf.Pkcs11.Wrapper;

namespace Net.Sf.Pkcs11.Delegates
{
	internal delegate CKR C_SignInit(
		uint hSession,
		ref CK_MECHANISM pMechanism,
		uint hKey
	);
}
