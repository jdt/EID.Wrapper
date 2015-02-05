
using System;
using Net.Sf.Pkcs11.Wrapper;

namespace Net.Sf.Pkcs11.Delegates
{
	internal delegate CKR C_GetSessionInfo(
		uint hSession,
		ref CK_SESSION_INFO pInfo
	);
}
