# EID.Wrapper
A COM Wrapper for the Belgian EID to allow legacy VBA to access the EID card data.

## Requirements
This DLL depends on Framework 4.0 and will obviously need the EID Middleware installed on the system. The version of the DLL that needs to be installed depends on the version of Office used, NOT the OS. For users of a 32-bit Office, you need the x86 folder. If you are using a 64-bit Office, use the x64 folder. 

## Installation
The files necessary for the wrapper can be found in the Release.zip-file.

The wrapper provides a simple Register-utility as part of the x86 or x64-folder that can be run as Administrator and performs the steps needed to get the wrapper DLL itself registered and recognized, after which it can be referenced from the Visual Basic For Applications-editor. The Register-utility will output the calls it makes and the resulting output so the result of the installation can be verified. (It relies on calls to system tools so the utility itself cannot make any guarantees as to the success of the installation)

After registering the DLL you may remove the downloaded files as the Register-utility will copy the necessary files to the system-directories.

Remember that if you are using a 64-bit OS with a 32-bit version of Office you will STILL need the 32-bit DLL. 

### Silent (unattended) installations
For unattended installations, the argument "-s" (without quotes) can be used so the utility closes itself after installation. Note that this prevents anybody from checking the result of the installation (see above).

## Updating
If you want to register a new version, simply download it and call the Register-utility again. After that, remove and re-add the reference to the wrapper from the Visual Basic For Applications-editor by unchecking the reference 
from the Tools/References... screen, closing the screen and then re-adding it. If you distribute a new version of the wrapper to clients you must also distribute a new version of the application that depends on the wrapper where
the reference has been re-added.

## Usage

### Basic usage
The wrapper contains a method GetCardData that will attempt to read data from all card slots on the system and return a CardData-object containing a list of Card-objects with the result of the read operation on each slot. Most systems will only have a single card slot and will therefore only return a single Card-object but the Wrapper was coded to handle multiple cards. A convenience property FirstCard is also available on the CardData-object that will contain the first valid card that was read.

### Add the reference
After installation a reference to EID_Wrapper is available from the Tools/References... dialog in the Visual Basic For Applications editor. You will then have access to the main classes in the EID_Wrapper namespace: Wrapper, CardData and Card.

### Checking for the presence of a valid card in the cardreader
```
Dim wrapper As New EID_Wrapper.Wrapper
Dim data As EID_Wrapper.CardData

Set data = wrapper.GetCardData()
If data.FirstCard Is Nothing Then
    MsgBox ("No valid card found!")
End If
```
Note that the FirstCard-property will only be available if the card was read successfully. If a card was placed in the reader but an error occurred while reading it, the FirstCard-property will not be set.
### Reading data
```
Dim wrapper As New EID_Wrapper.Wrapper
Dim data As EID_Wrapper.CardData

Set data = wrapper.GetCardData()
If Not data.FirstCard Is Nothing Then
	' Display the surname
	MsgBox (data.FirstCard.Surname)
End If
```

### Using the photograph on the EID
The Card-class contains both the raw data (as a byte array) for the photograph on the EID, as well as a utility method that can save the photo to a location on disk. The photo can then be retrieved from disk and displayed or used from your VBA code.
```
Dim wrapper As New EID_Wrapper.Wrapper
Dim data As EID_Wrapper.CardData

Set data = wrapper.GetCardData()
' Check that a card was read correctly
If Not data.FirstCard Is Nothing Then
	' Save the photo to the disk. Make sure this is a user-accessible directory!
	data.FirstCard.SavePhoto ("C:\photo.jpg")
	' Now display the picture in an imagebox
	ImageBox.Picture = "C:\photo.jpg"
End If
```

### Checking for exceptions
There are two places where exceptions can occur. In order to make debugging easy, the wrapper will try and catch any and all exceptions that occur and make them available on the CardData or Card, depending on where the exception occurred. It is most likely that the Card will contain an exception, reasons might include a non-EID card in the reader or a corrupted or unreadable card.

If an exception occurs when trying to get the card slots on the system, the CardData-object will have its CardDataStatus-field set to Error and the Error property will contain the exception.

If an exception occurs when reading a card, the CardStatus field on the Card will have its value set to Error to indicate an exception occurred. An Error-property will also be available containing the exception to help a developer debug the issue.

### A note on dealing with multiple cardreaders
The wrapper supports reading from and working with multiple concurrent cards. This is important for systems that contain different kinds of cards, such as systems outfitted with GPRS cards, as it is up to the developer using this wrapper to write the code to handle this situation. If multiple cards slots are present with different kinds of cards, the FirstCard property on CardData should give a reliable way of reading only the EID card and ignoring the other cards. 

Should you choose to, multiple EID-cards can be read and the data accessed.
```
Dim wrapper As New EID_Wrapper.Wrapper
Dim data As EID_Wrapper.CardData

Set data = wrapper.GetCardData()

Dim crd As Variant
For Each crd In data.cards
	MsgBox (crd.FirstNames)
Next
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
This code is based on the examples provided by the EID Middleware authors which uses the pkcs11-library. As outlined by the EID Middleware authors, some changes are needed to the code of this wrapper to make it work with the Belgian EID card. 

The sources for the pkcs11 wrapper are based on rev76, downloaded from http://sourceforge.net/projects/pkcs11net/ Changes were made according to the C# example readme on https://code.google.com/p/eid-mw/source/browse/trunk/sdk/Examples/CS/readme.txt?r=1188

These are the changes made according to the readme:

4.1) A change is needed to pkcs11net as our pkcs11 library uses the CDECL calling convention. Add "[System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(System.Runtime.InteropServices.CallingConvention.Cdecl)]"
before each delegate function. (all delegate functions are listed in a 'delegate' folder) e.g.
[System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(System.Runtime.InteropServices.CallingConvention.Cdecl)]
internal delegate CKR C_CloseAllSessions(
uint slotID
);

4.2) A second change might also be needed to pkcs11net, as some of its pkcs11 structs do not have their alignment set to 1. We (beidpkcs11.dll) package the pkcs11 structs with 1-byte alignment, but the pkcs11net wrapper uses the default. How to change the alignment of the pkcs11net wrapper structs: e.g. for the CK_ATTRIBUTE struct: in CK_ATTRIBUTE.cs change [StructLayout(LayoutKind.Sequential,Charset.Unicode)] to [StructLayout(LayoutKind.Sequential,Charset.Unicode,Pack=1)]

The pkcs11 code is compiled into this wrapper instead of in a library alongside it to reduce the complexities of dealing with assembly references in a COM-wrapper DLL. Limiting the wrapper to a single DLL with everything built inside limits complexities for deployment.

## For developers
### What's with the Error and CardStatus? 
Throwing exceptions from this wrapper is a bit...finicky. Consuming this in VBA might not always return the best available exception message and there are cases when a general VBA exception is thrown because of what is otherwise a very specific (and helpful) exception. So the wrapper simply 'eats up' the exception and puts them on the CardData object.

### Creating a release
In order to create a release, simply call the Build\Release.bat-script from a Visual Studio Command Prompt. 

### A note on testing
If you want to develop and test the wrapper, you might need to re-check the "Register for COM interop" in the Build-section of the EID.Wrapper project properties if you are doing Release builds. The regasm tool will generate the .tlb file needed on the client systems so a Release build from Visual Studio does not create and register the DLL.