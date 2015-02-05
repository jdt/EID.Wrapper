
using System;
using Net.Sf.Pkcs11.Wrapper;

namespace Net.Sf.Pkcs11.Delegates
{
	internal delegate CKR C_GetSlotInfo(
		uint slotID, 
		ref CK_SLOT_INFO pInfo
	);
}
