
using System;
using Net.Sf.Pkcs11.Wrapper;

namespace Net.Sf.Pkcs11.Delegates
{
	internal delegate CKR C_CopyObject(
		uint hSession,
		uint hObject,
		CK_ATTRIBUTE[] hTemplate,
		uint ulCount,
		ref uint phNewObject
	);
}
