
using System;
using Net.Sf.Pkcs11.Wrapper;

namespace Net.Sf.Pkcs11.Delegates
{
	internal delegate CKR C_CreateObject(
		uint hSession,
		CK_ATTRIBUTE[] pTemplate,
		uint ulCount,
		ref uint phObject
	);
}
