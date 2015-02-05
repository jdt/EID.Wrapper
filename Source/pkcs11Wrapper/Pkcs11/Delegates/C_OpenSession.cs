
using System;
using Net.Sf.Pkcs11.Wrapper;

namespace Net.Sf.Pkcs11.Delegates
{
	internal delegate CKR C_OpenSession(
		uint slotID,
		uint flags,
		ref uint pApplication,
		IntPtr Notify,
		ref uint phSession
	);
}
