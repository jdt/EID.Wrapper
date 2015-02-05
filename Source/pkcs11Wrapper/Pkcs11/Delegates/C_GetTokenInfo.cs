
using System;
using Net.Sf.Pkcs11.Wrapper;

namespace Net.Sf.Pkcs11.Delegates
{
	internal delegate CKR C_GetTokenInfo(
		uint slotID, 
		ref CK_TOKEN_INFO pInfo
	);
}
