
using System;
using Net.Sf.Pkcs11.Wrapper;

namespace Net.Sf.Pkcs11.Delegates
{
	internal delegate CKR C_GetMechanismList(
		uint slotID, 
		CKM[] pMechanismList,
		ref uint pulCount
	);
}
