# EID.Wrapper
A COM Wrapper for the Belgian EID to allow legacy VBA to access the EID card data

pkcs11Wrapper
The sources for the pkcs11 wrapper are based on rev76, downloaded from http://sourceforge.net/projects/pkcs11net/ 
Changes were made according to the C# example readme on https://code.google.com/p/eid-mw/source/browse/trunk/sdk/Examples/CS/readme.txt?r=1188
Aside from setting references to NUnit and BouncyCastle.Crypto, the following changes were made in code in accordance with the readme:

4.1) A change is needed to pkcs11net as our pkcs11 library uses the CDECL calling convention
add "[System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(System.Runtime.InteropServices.CallingConvention.Cdecl)]"
before each delegate function. (all delegate functions are listed in a 'delegate' folder)
e.g.
[System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(System.Runtime.InteropServices.CallingConvention.Cdecl)]
internal delegate CKR C_CloseAllSessions(
uint slotID
);
4.2) A second change might also be needed to pkcs11net, as some of its pkcs11 structs do not have their alignment set to 1.
We (beidpkcs11.dll) package the pkcs11 structs with 1-byte alignment, but the pkcs11net wrapper uses the default.
How to change the alignment of the pkcs11net wrapper structs:
e.g. for the CK_ATTRIBUTE struct:
in CK_ATTRIBUTE.cs change [StructLayout(LayoutKind.Sequential,Charset.Unicode)] to
[StructLayout(LayoutKind.Sequential,Charset.Unicode,Pack=1)]
