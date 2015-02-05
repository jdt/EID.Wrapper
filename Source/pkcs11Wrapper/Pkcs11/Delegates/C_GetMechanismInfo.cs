
using System;
using Net.Sf.Pkcs11.Wrapper;

namespace Net.Sf.Pkcs11.Delegates
{
	internal delegate CKR C_GetMechanismInfo(
		uint slotID, 
		CKM type, 
		ref CK_MECHANISM_INFO pInfo
	);
}
