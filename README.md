# EID.Wrapper
A COM Wrapper for the Belgian EID to allow legacy VBA to access the EID card data.

## Requirements
This DLL depends on Framework 4.0 and will obviously need the EID Middleware installed on the system.

## Installation
The files necessary for the wrapper can be found in the Release.zip-file. Download this file from the repository and perform the installation as outlined below to make the wrapper available on your system. After registering the DLL you may remove the downloaded files as the Register-utility will copy the necessary files to the system-directories.

The version of the DLL that needs to be installed depends on the version of Office used, NOT the OS. For users of a 32-bit Office, you need the x86 folder. If you are using a 64-bit Office, use the x64 folder. The wrapper provides a simple Register-utility that can be run as Administrator and performs the steps needed to get the wrapper DLL itself registered and recognized, after which it can be referenced from the Visual Basic For Applications-editor.

Remember that if you are using a 64-bit OS with a 32-bit version of Office you will STILL need the 32-bit DLL. 

## Usage
After installation a reference to EID_Wrapper is available from the Tools/References... dialog in the Visual Basic For Applications editor. The following code can then be used to fetch data from the EID-card.

```
Dim wrapper As New EID_Wrapper.Wrapper
Dim data As EID_Wrapper.CardData

Set data = wrapper.GetCardData()
' Display the surname
MsgBox (data.Surname)

## Help

### I can't read any data
The wrapper provides an EID.Wrapper.Console-utility that can be used to verify if the DLL works. This works independently from the COM-registration. If the console utility returns your data the problem can be narrowed down to the COM-registration. Please make sure your cardreader is connected and and EID-card placed into the device before attempting to use the utility.

### The data I need is not exposed!
This is a quick-and-dirty project that only exposes the data I need to get an old application going with the new cards being issued. If you need additional data, open an issue or fork this project and send me a pull request and I'll see if I can get you the data you need.

### I keep getting weird exceptions!
A lot of weird exceptions can pop up when the version of the DLL and the version of Office (32-bit versus 64-bit) do not match. If you encounter any errors, make sure that you have installed the correct DLL (x86 for 32-bit Office or x64 for 64-bit Office).

### I'm still stuck!
This DLL has been tested on different combinations of OS and Office and always from clean installations (that is, apart from installing the .NET Framework version 4, the EID Middleware and the Java JRE to test the basic Viewer-applet). Despite my best efforts there can still be some problems. If you are well and truly stuck with a problem you are welcome to open an issue and we can see about working it out, but I can and will not make any guarantees that this DLL will work for you.

### What is the pkcs11 wrapper code doing here?
This code is based on the examples provided by the EID Middleware authors. The pkcs11 code is compiled into this wrapper to reduce the complexities of dealing with assembly references in a COM-wrapper DLL so as to limit this wrapper to the single DLL. The sources for the pkcs11 wrapper are based on rev76, downloaded from http://sourceforge.net/projects/pkcs11net/ Changes were made according to the C# example readme on https://code.google.com/p/eid-mw/source/browse/trunk/sdk/Examples/CS/readme.txt?r=1188

These are the changes made according to the readme:

4.1) A change is needed to pkcs11net as our pkcs11 library uses the CDECL calling convention. Add "[System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(System.Runtime.InteropServices.CallingConvention.Cdecl)]"
before each delegate function. (all delegate functions are listed in a 'delegate' folder) e.g.
[System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(System.Runtime.InteropServices.CallingConvention.Cdecl)]
internal delegate CKR C_CloseAllSessions(
uint slotID
);

4.2) A second change might also be needed to pkcs11net, as some of its pkcs11 structs do not have their alignment set to 1. We (beidpkcs11.dll) package the pkcs11 structs with 1-byte alignment, but the pkcs11net wrapper uses the default. How to change the alignment of the pkcs11net wrapper structs: e.g. for the CK_ATTRIBUTE struct: in CK_ATTRIBUTE.cs change [StructLayout(LayoutKind.Sequential,Charset.Unicode)] to [StructLayout(LayoutKind.Sequential,Charset.Unicode,Pack=1)]

## A note for developers
If you want to develop and test the wrapper, you might need to re-check the "Register for COM interop" in the Build-section of the EID.Wrapper project properties if you are doing Release builds. The regasm tool will generate the .tlb file needed on the client systems so a Release build from Visual Studio does not create and register the DLL.
