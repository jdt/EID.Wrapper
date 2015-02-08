using Net.Sf.Pkcs11.Wrapper;
using System.Runtime.InteropServices;
using System;

namespace Net.Sf.Pkcs11.EtokenExtensions.Wrapper
{
    /// <summary>
    /// Constants for EtokenExtensions.
    /// </summary>
    public class PKCS11EtokenConstants
    {
        internal const uint PKCS7_DETACHED_SIGNATURE = 0x01;
    }

    /// <summary>
    /// Actually, this does not work with this extension.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public class CK_FUNCTION_LIST : Net.Sf.Pkcs11.Wrapper.CK_FUNCTION_LIST
    {
        // New functions.
        [MarshalAs(UnmanagedType.SysInt)]
        public IntPtr pkcs7Sign;

        [MarshalAs(UnmanagedType.SysInt)]
        public IntPtr pkcs7Verify;

        [MarshalAs(UnmanagedType.SysInt)]
        public IntPtr certVerify;

        [MarshalAs(UnmanagedType.SysInt)]
        public IntPtr createCSR;

        [MarshalAs(UnmanagedType.SysInt)]
        public IntPtr getCertificateInfo;

        [MarshalAs(UnmanagedType.SysInt)]
        public IntPtr freeBuffer;
    }
}

/// Delegates for EtokenExtensions.
namespace Net.Sf.Pkcs11.EtokenExtensions.Delegates
{
    /*
	/*
	Формирования PKCS#7 сообщения типа signed data.
	Параметры:
	in session - PKCS#11 сессия.
	in	data - данные для подписи.
	in	dataLength - длина данных для подписи.
	in	signCertificate - сертификат создателя сообщения.
	out	envelope - указатель на указатель на на буфер в который будет записано сообщение.
			Буффер создается внутри функции. После окончания работы с ним необходимо освободить его, вызвав функцию freeBuffer().
	out	envelopeLength - указатель на длину созданного буфера с сообщением.
	in	privateKey - закрытый ключ создателя сообщения. Может устанавливатся в 0, тогда поиск закрытого ключа будет осуществлятся по CKA_ID сертификата.
	in	certificates - указатель на массив сертификатов, которые следует добавить в сообщение.
	in	certificatesLength - количество сертификатов в параметре certificates.
	in	flags - флаги. Может принимать значение 0 и PKCS7_DETACHED_SIGNATURE.

	ETPKCS11_EXTENSIONS_EXPORT CK_RV pkcs7Sign(
		CK_SESSION_HANDLE session,
		CK_BYTE_PTR data,
		CK_ULONG dataLength,
		CK_OBJECT_HANDLE signCertificate,
		CK_BYTE_PTR* envelope,
		CK_ULONG_PTR envelopeLength,
		CK_OBJECT_HANDLE privateKey,
		CK_OBJECT_HANDLE_PTR certificates,
		CK_ULONG certificatesLength,
		CK_ULONG flags);
	*/
	internal delegate CKR pkcs7Sign
    (
		uint hSession,
		byte[] pData,
		uint dataLength,
		uint signCertificate,
        out IntPtr envelope,
        out uint envelopeLength,
		uint privateKey,
		uint[] certificates,
		uint certificatesLength,
		uint flags
	);


	/*
	Проверка подписи в PKCS#7 сообщениии типа signed data
	Параметры:
	in session - PKCS#11 сессия.
	in envelope - PKCS#7 сообщение.
	in enlevopeLength - длина PKCS#7 сообщения.
	in data - если сообщение не содержит самих данных, то необходимо передать их в этот параметр.
	in dataLength - длина данных.

	ETPKCS11_EXTENSIONS_EXPORT CK_RV pkcs7Verify(
		CK_BYTE_PTR envelope,
		CK_ULONG enlevopeLength,
		CK_BYTE_PTR data,
		CK_ULONG dataLength);
	*/
    internal delegate CKR pkcs7Verify
    (
        byte[] envelope,
		uint enlevopeLength,
		byte[] data,
		uint dataLength
    );

	/*
	Проверка пути сертификации.
	Параметры:
	in session - PKCS#11 сессия.
	in certificateToVerify - сертификат, который необходимо проверить.
	in trustedCertificates - массив доверенных сертификатов.
	in trustedCertificatesLength - количество сертификатов в trustedCertificates.
	in certificateChain - промежуточные сертификаты.
	in certificateChainLength - количество сертификатов в certificateChain.
	in crls - массив списков отозванных сертификатов.
	in crlsLengths - массив с длинами списков отозванных сертификатов.
	in crlsLength - количество списков отозванных сертификатов в clrs.

	ETPKCS11_EXTENSIONS_EXPORT CK_RV certVerify(
		CK_SESSION_HANDLE session,
		CK_OBJECT_HANDLE certificateToVerify,
		CK_OBJECT_HANDLE_PTR trustedCertificates,
		CK_ULONG trustedCertificatesLength,
		CK_OBJECT_HANDLE_PTR certificateChain,
		CK_ULONG certificateChainLength,
		CK_BYTE_PTR* crls,
		CK_ULONG_PTR crlsLengths, 
		CK_ULONG crlsLength);
	*/
    internal delegate CKR certVerify
    (
        uint session,
        uint certificateToVerify,
        uint[] trustedCertificates,
        uint trustedCertificatesLength,
        uint[] certificateChain,
        uint certificateChainLength,
        IntPtr crls,
        uint[] crlsLengths,
        uint crlsLength
    );

	/*
	Сформировать запрос на сертификат.
	Параметры:
	in	session - PKCS#11 сессия.
	in	publicKey - открытый ключ для создания сертификата.
	in	dn - distinguished name. В параметр должнен переваваться массив строк. В первой строке должен распологаться тип поля в тексторой форме, или
			OID, например, "CN". Во второй строке должно распологаться значение поля в UTF8.
			Последующие поля передаются в следующих строках. Количество строк должно быть четным.
	in	dnLength - количество строк в dn.
	out	csr - указатель на указатель на на буфер в который будет записан запрос на сертификат.
			Буффер создается внутри функции. После окончания работы с ним необходимо освободить его, вызвав функцию freeBuffer().
	out	csrLength - длина буффера в который будет записан запрос на сертификат.
	in	privateKey - закрытый ключ, парный publicKey. Если значение установленно в 0, то поиск закрытого ключа будет осуществляться
		по CKA_ID открытого ключа.
	in	attributes - дополнительные атрибуты для включения в запрос. Формат аналогичен dn.
	in	attributesLength - количество строк в attributes.
	in	extensions - расширения для включения в запрос. Формат аналогичен dn.
	in	extensionsLength - количество строк вextensions.

	ETPKCS11_EXTENSIONS_EXPORT CK_RV createCSR(
		CK_SESSION_HANDLE session,
		CK_OBJECT_HANDLE publicKey,
		CK_CHAR_PTR* dn,
		CK_ULONG dnLength,
		CK_BYTE_PTR* csr,
		CK_ULONG_PTR csrLength,
		CK_OBJECT_HANDLE privateKey,
		CK_CHAR_PTR* attributes,
		CK_ULONG attributesLength,
		CK_CHAR_PTR* extensions,
		CK_ULONG extensionsLength);
	*/
    internal delegate CKR createCSR
    (
		uint session,
        uint publicKey,
        IntPtr dn,
		uint dnLength,
        out IntPtr csr,
        out uint csrLength,
        uint privateKey,
        [MarshalAsAttribute(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] attributes,
        uint attributesLength,
        [MarshalAsAttribute (UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] extensions,
        uint extensionsLength
    );

	/*
	Получить информацио о сертификате в текстовом виде.
	Параметры:
	in	session - PKCS#11 сессия.
	in	certificate - сертификат.
	out	certificateInfo - указатель на указатель на на буфер в который будет записана информация о сертификате.
		Буффер создается внутри функции. После окончания работы с ним необходимо освободить его, вызвав функцию freeBuffer().
	out	certificateInfoLength - длина буффера в который будет записана информация о сертификате.

	ETPKCS11_EXTENSIONS_EXPORT CK_RV getCertificateInfo(
		CK_SESSION_HANDLE session,
		CK_OBJECT_HANDLE certificate,
		CK_CHAR_PTR* certificateInfo,
		CK_ULONG* certificateInfoLength);
	*/

	/*
	Освободить буффер, выделенный в одной из других функций.
	Параметры:
	in	buffer - буффер.

	ETPKCS11_EXTENSIONS_EXPORT CK_RV freeBuffer(
		CK_BYTE_PTR buffer);
	*/
    internal delegate CKR freeBuffer
    (
        IntPtr buffer
    );

    /*
	Получить информацио о сертификате в текстовом виде.
	Параметры:
	in	session - PKCS#11 сессия.
	in	certificate - сертификат.
	out	certificateInfo - указатель на указатель на на буфер в который будет записана информация о сертификате.
		Буффер создается внутри функции. После окончания работы с ним необходимо освободить его, вызвав функцию freeBuffer().
	out	certificateInfoLength - длина буффера в который будет записана информация о сертификате.

	ETPKCS11_EXTENSIONS_EXPORT CK_RV getCertificateInfo(
		CK_SESSION_HANDLE session,
		CK_OBJECT_HANDLE certificate,
		CK_CHAR_PTR* certificateInfo,
		CK_ULONG* certificateInfoLength);
	*/
    internal delegate CKR getCertificateInfo
    (
		uint session,
		uint certificate,
		out IntPtr certificateInfo,
		out uint certificateInfoLength
    );
}
