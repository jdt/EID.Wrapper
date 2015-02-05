
using System;
using Net.Sf.Pkcs11.Wrapper;

namespace Net.Sf.Pkcs11.Delegates
{
	internal delegate CKR C_SetAttributeValue(
		uint hSession,
		uint hObject,
		ref CK_ATTRIBUTE pTemplate,
		uint ulCount
	);
}
