# EID.Wrapper
A COM Wrapper for the Belgian EID to allow legacy VBA to access the EID card data.

## Requirements
This DLL depends on Framework 4.0 and will obviously need the EID Middleware installed on the system.

## Installation
The files necessary for the wrapper can be found in the Release.zip-file. Download this file from the repository and perform the installation as outlined below to make the wrapper available on your system.

The version of the DLL that needs to be installed depends on the version of Office used, NOT the OS. For users of a 32-bit Office, you need the x86 folder. If you are using a 64-bit Office, use the x64 folder. The wrapper provides a simple Register-utility that can be run as Administrator and performs the steps needed to get the wrapper DLL itself registered and recognized, after which it can be referenced from the Visual Basic For Applications-editor.

After registering the DLL you may remove the downloaded files as the Register-utility will copy the necessary files to the system-directories.

Remember that if you are using a 64-bit OS with a 32-bit version of Office you will STILL need the 32-bit DLL. 

## Updating
If you want to register a new version, simply download it and call the Register-utility again. After that, remove and re-add the reference to the wrapper from the Visual Basic For Applications-editor by unchecking the reference 
from the Tools/References... screen, closing the screen and then re-adding it. If you distribute a new version of the wrapper to clients you must also distribute a new version of the application that depends on the wrapper where
the reference has been re-added.

## Usage

### Add the reference
After installation a reference to EID_Wrapper is available from the Tools/References... dialog in the Visual Basic For Applications editor. You will then have access to the two main classes in the EID_Wrapper namespace: Wrapper and CardData. The wrapper contains a method GetCardData that will attempt to read data from the EID card and return a CardData-object with the result of the read operation.

### Checking for the presence of a card in the cardreader
The CardData-class contains a CardStatus field that will contain a status indication after the call to GetCardData. This can be used to determine if a card was found.
```
Dim wrapper As New EID_Wrapper.Wrapper
Dim data As EID_Wrapper.CardData

Set data = wrapper.GetCardData()
' The CardStatus field contains the information needed
If data.CardStatus = CardStatus_NoCardFound Then
    MsgBox ("No Card found!")
End If
```

### Reading data
If the CardStatus field indicates the card was read, the values read from the card will be available in the CardData-class.
```
Dim wrapper As New EID_Wrapper.Wrapper
Dim data As EID_Wrapper.CardData

Set data = wrapper.GetCardData()
' Check that card data was read correctly
If data.CardStatus = CardStatus_Read Then
	' Display the surname
	MsgBox (data.Surname)
End If
```

### Using the photograph on the EID
The CardData-class contains both the raw data (as a byte array) for the photograph on the EID, as well as a utility method that can save the photo to a location on disk. The photo can then be retrieved from disk and displayed or used from your VBA code.
```
Dim wrapper As New EID_Wrapper.Wrapper
Dim data As EID_Wrapper.CardData

Set data = wrapper.GetCardData()
' Check that card data was read correctly
If data.CardStatus = CardStatus_Read Then
	' Save the photo to the disk. Make sure this is a user-accessible directory!
	data.SavePhoto ("C:\photo.jpg")
	' Now display the picture in an imagebox
	ImageBox.Picture = "C:\photo.jpg"
End If
```

### Checking for exceptions
If an exception occurs when reading a card, the CardStatus field will have its value set to Error to indicate an exception occurred. An Error-property will also be available containing the exception to help a developer debug the issue.
```
Dim wrapper As New EID_Wrapper.Wrapper
Dim data As EID_Wrapper.CardData

Set data = wrapper.GetCardData()
' Check for exceptions
If data.CardStatus = CardStatus_Error Then
	' Notify the user
	MsgBox ("An unexpected exception has occured: " + data.Error.Message)
End If
```

## Help

### I can't read any data
The wrapper provides an EID.Wrapper.Console-utility that can be used to verify if the DLL works. This works independently from the COM-registration. If the console utility returns your data the problem can be narrowed down to the COM-registration. Please make sure your cardreader is connected and and EID-card placed into the device before attempting to use the utility.

### The data I need is not exposed!
This is a quick-and-dirty project that only exposes the data I need to get an old application going with the new cards being issued. If you need additional data, open an issue or fork this project and send me a pull request and I'll see if I can get you the data you need.

### I get exceptions when saving the photo to disk (for example, GDI+ exceptions)
If you try to save the photo to an invalid location (directory does not exist, not enough permissions) you might see some odd exceptions pop up. Check this first to make sure you are not just having issues writing the file.

### I keep getting weird exceptions!
A lot of weird exceptions can pop up when the version of the DLL and the version of Office (32-bit versus 64-bit) do not match. If you encounter any errors, make sure that you have installed the correct DLL (x86 for 32-bit Office or x64 for 64-bit Office). If you have recently updated the wrapper, make sure to follow the instructions outlined in the 'Updating'-section.

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

## Building
In order to create a release, simply call the Build\Release.bat-script from a Visual Studio Command Prompt. 

## A note for developers
If you want to develop and test the wrapper, you might need to re-check the "Register for COM interop" in the Build-section of the EID.Wrapper project properties if you are doing Release builds. The regasm tool will generate the .tlb file needed on the client systems so a Release build from Visual Studio does not create and register the DLL.
