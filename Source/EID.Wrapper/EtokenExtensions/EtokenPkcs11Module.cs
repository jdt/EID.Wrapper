using System;
using Net.Sf.Pkcs11.Wrapper;
using Net.Sf.Pkcs11.EtokenExtensions.Delegates;
using System.Runtime.InteropServices;
using System.Text;

namespace Net.Sf.Pkcs11.EtokenExtensions.Wrapper
{
    [Flags]
    public enum pkcs7SignFlags: uint
    {
        None = 0,
        DETACHED_SIGNATURE = PKCS11EtokenConstants.PKCS7_DETACHED_SIGNATURE
    }

    /// <summary>
    /// Wrapper around Etoken version of Pkcs11Module (low level).
    /// </summary>
    public class EtokenPkcs11Module: Pkcs11Module
    {
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
        internal static EtokenPkcs11Module GetInstance(string moduleName)
        {
            IntPtr hLib;
            if ((hLib = KernelUtil.LoadLibrary(moduleName)) == IntPtr.Zero)
                throw new Exception("Could not load module. Module name:" + moduleName);
            return new EtokenPkcs11Module(hLib);
        }

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="hLib"></param>
        protected EtokenPkcs11Module(IntPtr hLib)
            : base(hLib)
        {
            // Nothing here.
        }

        /// <summary>
        /// Sign message according to pkcs7 specification.
        /// </summary>
        internal void pkcs7Sign(
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
            )
        {
            pkcs7Sign proc = (pkcs7Sign)DelegateUtil.GetDelegate(this.hLib, typeof(pkcs7Sign));
            checkCKR(proc(hSession, pData, dataLength, signCertificate, out envelope, out envelopeLength,
                privateKey, certificates, certificatesLength, flags));
        }

        /// <summary>
        /// Verify message according to pkcs7 specification.
        /// </summary>
        internal void pkcs7Verify(
            byte[] envelope,
            uint enlevopeLength,
            byte[] data,
            uint dataLength            
            
            )
        {
            pkcs7Verify proc = (pkcs7Verify)DelegateUtil.GetDelegate(this.hLib, typeof(pkcs7Verify));
            checkCKR(proc(envelope, enlevopeLength, data, dataLength));
        }

        /// <summary>
        /// Converts a string to an IntPtr using a rare UTF8 encoding.
        /// </summary>
        /// <param name="aInputStrArray"></param>
        /// <returns></returns>
        public static IntPtr StringArrayToIntPtr(string[] aInputStrArray)
        {
            int size = aInputStrArray.Length;
            IntPtr[] InPointers = new IntPtr[size];
            int dim = IntPtr.Size * size;
            IntPtr rRoot = Marshal.AllocHGlobal(dim);
            for (int i = 0; i < size; i++)
            {
                byte[] lBytes = Encoding.UTF8.GetBytes(aInputStrArray[i] + Convert.ToChar(0));
                InPointers[i] = Marshal.AllocHGlobal(lBytes.Length);
                Marshal.Copy(lBytes, 0, InPointers[i], lBytes.Length);
            }
            //copy the array of pointers
            Marshal.Copy(InPointers, 0, rRoot, size);
            return rRoot;
        }

        /// <summary>
        /// Create a certificate request.
        /// </summary>
        internal void createCSR(
            uint session,
            uint publicKey,
            string[] dn,
            uint dnLength,
            out IntPtr csr,
            out uint csrLength,
            uint privateKey,
            string[] attributes,
            uint attributesLength,
            string[] extensions,
            uint extensionsLength
            )
        {
            IntPtr lPrt = StringArrayToIntPtr(dn);

            createCSR proc = (createCSR)DelegateUtil.GetDelegate(this.hLib, typeof(createCSR));
            checkCKR(proc(session, publicKey, lPrt, dnLength, out csr, out csrLength, privateKey,
                attributes, attributesLength, extensions, extensionsLength));
        }

        /// <summary>
        /// Free a buffer, created by one of the previously called functions.
        /// </summary>
        internal void freeBuffer(
            IntPtr buffer
            )
        {
            freeBuffer proc = (freeBuffer)DelegateUtil.GetDelegate(this.hLib, typeof(freeBuffer));
            checkCKR(proc(buffer));
        }

        /// <summary>
        /// Get certificate info.
        /// </summary>
        internal void getCertificateInfo(
            uint session,
            uint certificate,
            out IntPtr certificateInfo,
            out uint certificateInfoLength)
        {
            getCertificateInfo proc = (getCertificateInfo)DelegateUtil.GetDelegate(this.hLib, typeof(getCertificateInfo));
            checkCKR(proc(session, certificate, out certificateInfo, out certificateInfoLength));
        }

        /// <summary>
        /// Verify a Certificate.
        /// </summary>
        /// <param name="session"></param>
        /// <param name="certificateToVerify"></param>
        /// <param name="trustedCertificates"></param>
        /// <param name="trustedCertificatesLength"></param>
        /// <param name="certificateChain"></param>
        /// <param name="certificateChainLength"></param>
        /// <param name="crls"></param>
        /// <param name="crlsLengths"></param>
        /// <param name="crlsLength"></param>
        internal void certVerify
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
        )
        {
            certVerify proc = (certVerify)DelegateUtil.GetDelegate(this.hLib, typeof(certVerify));
            checkCKR(proc(session, certificateToVerify, trustedCertificates, trustedCertificatesLength,
                certificateChain, certificateChainLength, crls, crlsLengths, crlsLength));
        }
    }
}
