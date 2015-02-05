using System;
using System.Runtime.InteropServices;
using Net.Sf.Pkcs11.EtokenExtensions.Wrapper;
using Net.Sf.Pkcs11.Objects;

namespace Net.Sf.Pkcs11.EtokenExtensions
{
    /// <summary>
    /// Wrapper around Etoken version of Pkcs11Module (high level).
    /// </summary>
    public class EtokenModule : Net.Sf.Pkcs11.Module
    {
        /// <summary>
        /// Access to P11Module.
        /// </summary>
        public new EtokenPkcs11Module P11Module
        {
            get { return (EtokenPkcs11Module)p11Module; }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="p11Module"></param>
        protected EtokenModule(EtokenPkcs11Module p11Module): base(p11Module)
        {
            // Nothing here.
        }

        /// <summary>
        /// Creates an instance of Pkcs11Module
        /// </summary>
        /// <param name="moduleName">
        /// module to be loaded. it is the path of pkcs11 driver
        /// <example>
        /// <code>
        /// Pkcs11Module pm=Pkcs11Module.GetInstance("gclib.dll");
        /// </code>
        /// </example>
        /// </param>
        /// <returns></returns>
        public static new EtokenModule GetInstance(string moduleName)
        {
            if (moduleName == null)
            {
                throw new Exception("Argument \"pkcs11ModuleName\" must not be null.");
            }
            else
            {
                EtokenPkcs11Module pm = EtokenPkcs11Module.GetInstance(moduleName);

                return new EtokenModule(pm);
            }
        }

        /// <summary>
        /// Sign message according to pkcs7 specification.
        /// </summary>
        public void pkcs7Sign(
            Session aSession,
            byte[] pData,
            Certificate signCertificate,
            out byte[] aEnvelope,
            PrivateKey privateKey,
            Certificate[] aCertificates,
            pkcs7SignFlags aFlags = pkcs7SignFlags.None
            )
        {       
            uint lEnvelopeLength;

            // Certificates.
            uint[] lCertificateHandles;
            uint lCertificatesLength;
            if (aCertificates == null)
            {
                lCertificatesLength = 0;
                lCertificateHandles = null;
            }
            else
            {
                lCertificatesLength = (uint)aCertificates.Length;
                lCertificateHandles = new uint[aCertificates.Length];
                for (int i = 0; i < lCertificateHandles.Length; i++)
                    lCertificateHandles[i] = aCertificates[i].HObj;
            }

            // Call procedure.
            IntPtr lEnvelope;
            P11Module.pkcs7Sign(aSession.HSession, pData, (uint) pData.Length, signCertificate.HObj,
                out lEnvelope, out lEnvelopeLength, privateKey.HObj,
                lCertificateHandles, lCertificatesLength, (uint)aFlags);


            // Marshal.
            aEnvelope = new byte[lEnvelopeLength];
            Marshal.Copy(lEnvelope, aEnvelope, 0, (int)lEnvelopeLength);


            // Release buffer.
            P11Module.freeBuffer(lEnvelope);
        }

        /// <summary>
        /// Verify message according to pkcs7 specification.
        /// </summary>
        public void pkcs7Verify(
            byte[] envelope,
            byte[] data                   
            )
        {
            P11Module.pkcs7Verify(envelope, (uint)envelope.Length, data, (uint)data.Length);
        }

        /// <summary>
        /// Create a certificate request.
        /// </summary>
        public void createCSR(
            Session aSession,
            PublicKey publicKey,
            string[] dn,
            string[] attributes,
            string[] extensions,
            out byte[] aCertificateRequest,
            PrivateKey privateKey = null
            )
        {
            IntPtr lCertificateRequest;
            uint lCertificateRequestLength;
            
            // Private Key.
            uint lPrivateKeyHandle;           
            if (privateKey == null)
                lPrivateKeyHandle = 0;
            else
                lPrivateKeyHandle = privateKey.HObj;

            // dn.
            uint ldnLength;
            if (dn == null)
                ldnLength = 0;
            else
                ldnLength = (uint)dn.Length;

            // Extensions.
            uint lExtensionsLength;
            if (extensions == null)
                lExtensionsLength = 0;
            else
                lExtensionsLength = (uint) extensions.Length;

            // Attributes.
            uint lAttributesLength;
            if (attributes == null)
                lAttributesLength = 0;
            else
                lAttributesLength = (uint)attributes.Length;

            // Call procedure.
            P11Module.createCSR(aSession.HSession, publicKey.HObj,
                dn, ldnLength,
                out lCertificateRequest, out lCertificateRequestLength, lPrivateKeyHandle,
                attributes, lAttributesLength,
                extensions, lExtensionsLength);
            
            // Marshal.
            aCertificateRequest = new byte[lCertificateRequestLength];
            Marshal.Copy(lCertificateRequest, aCertificateRequest, 0, (int)lCertificateRequestLength);


            // Release buffer.
            P11Module.freeBuffer(lCertificateRequest);
        }

        /// <summary>
        /// Get information about a certificate in a form of a string.
        /// </summary>
        /// <param name="aSession"></param>
        /// <param name="aCertificate"></param>
        /// <param name="aCertificateInfo"></param>
        public void getCertificateInfo
        (
            Session aSession,
            Certificate aCertificate,
            out string aCertificateInfo
        )
        {
            uint lCertificateInfoLength;
            IntPtr lCertificateInfo;

            // Call procedure.
            P11Module.getCertificateInfo(aSession.HSession, aCertificate.HObj, out lCertificateInfo, out lCertificateInfoLength);

            // Marshal.
            aCertificateInfo = Marshal.PtrToStringAnsi(lCertificateInfo);

            // Release buffer.
            P11Module.freeBuffer(lCertificateInfo);
        }


        /// <summary>
        /// Verify a certificate.
        /// </summary>
        /// <param name="session"></param>
        /// <param name="certificateToVerify"></param>
        /// <param name="aTrustedCertificates"></param>
        /// <param name="aCertificateChain"></param>
        /// <param name="crls"></param>
        public void certVerify
        (
            Session session,
            Certificate certificateToVerify,
            Certificate[] aTrustedCertificates,
            Certificate[] aCertificateChain,
            IntPtr crls
        )
        {
            // TrustedCertificates.
            uint[] lTrustedCertificates;
            uint lTrustedCertificatesLength;

            if (aTrustedCertificates == null)
            {
                lTrustedCertificates = null;
                lTrustedCertificatesLength = 0;
            }
            else
            {
                lTrustedCertificatesLength = (uint) aTrustedCertificates.Length;
                lTrustedCertificates = new uint[aTrustedCertificates.Length];
                for (int i = 0; i < aTrustedCertificates.Length; i++)
                    lTrustedCertificates[i] = aTrustedCertificates[i].HObj;
            }
           
            // aCertificateChain.
            uint[] lCertificateChain;
            uint lCertificateChainLength;

            if (aCertificateChain == null)
            {
                lCertificateChain = null;
                lCertificateChainLength = 0;
            }
            else
            {
                lCertificateChainLength = (uint) aCertificateChain.Length;
                lCertificateChain = new uint[aCertificateChain.Length];
                for (int i = 0; i < aCertificateChain.Length; i++)
                    lCertificateChain[i] = aCertificateChain[i].HObj;
            }

            // Call method.
            P11Module.certVerify(session.HSession, certificateToVerify.HObj,
                lTrustedCertificates, lTrustedCertificatesLength,
                lCertificateChain, lCertificateChainLength,
                IntPtr.Zero, null, 0); // CRL's not implemented, they are supposed to be passed as bytes.
        }
    }
}
