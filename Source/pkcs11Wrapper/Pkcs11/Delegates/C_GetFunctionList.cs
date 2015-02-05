
using System;
using Net.Sf.Pkcs11.Wrapper;
namespace Net.Sf.Pkcs11.Delegates
{
	public delegate CKR C_GetFunctionList(
		out IntPtr ppFunctionList 
	);
}
