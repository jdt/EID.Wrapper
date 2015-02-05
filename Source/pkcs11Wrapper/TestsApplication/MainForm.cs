using System;
using System.IO;
using System.Windows.Forms;

using Net.Sf.Pkcs11;
using Net.Sf.Pkcs11.Objects;
using Net.Sf.Pkcs11.Wrapper;
using Net.Sf.Pkcs11.EtokenExtensions;

using Org.BouncyCastle.X509;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Asn1.Pkcs;

namespace Tests
{
    /// <summary>
    /// Main form.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Constants.
        /// </summary>
        readonly byte[] GENERATED_OBJECTS_ID = new byte[] { 14 };

        /// <summary>
        /// Constructor.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            cbProviderDll.SelectedIndex = 0;
        }

        private void btGetTokenLabel_Click(object sender, EventArgs e)
        {
            
            DoWithFirstSlot(delegate(Slot aSlot)
                {
                    MessageBox.Show(this, "First inserted token has label: " + aSlot.Token.TokenInfo.Label);
                });
        }

        private void btGetKeysList_Click(object sender, EventArgs e)
        {
            btGetKeysList.Enabled = false;
            Application.DoEvents();

            DoWithFirstSlotWhileLoggedIn(delegate(Session aSession)
            {
                aSession.FindObjectsInit();

                P11Object[] objs = aSession.FindObjects(50);
                MessageBox.Show(this, string.Join<P11Object>(Environment.NewLine, objs));

                aSession.FindObjectsFinal();
            });

            btGetKeysList.Enabled = true;
        }
        
        static X509Certificate ReadCertificate(String filename)
        {
            X509CertificateParser certParser = new X509CertificateParser();

            Stream stream = new FileStream(filename, FileMode.Open);
            X509Certificate cert = certParser.ReadCertificate(stream);
            stream.Close();

            return cert;
        }

        private void btLAddCertificate_Click(object sender, EventArgs e)
        {
            btLAddCertificate.Enabled = false;
            Application.DoEvents();
            try
            {
                DoWithFirstSlotWhileLoggedIn(delegate(Session aSession)
                {
                    X509PublicKeyCertificate template = new X509PublicKeyCertificate();
                    template.CertificateType.CertificateType = CKC.X_509;
                    template.Token.Value = true;
                    template.Id.Value = GENERATED_OBJECTS_ID;
                    template.Label.Value = "my-certificate-label".ToCharArray();

                    Org.BouncyCastle.X509.X509Certificate cer = ReadCertificate(
                        Application.StartupPath + "\\TestCertificate.cer");
                    template.Subject.Value = cer.SubjectDN.GetEncoded();
                    template.Value.Value = cer.GetEncoded();
                    aSession.CreateObject(template);

                    MessageBox.Show(this, "Certificate generated successfully");
                });
            }
            finally
            {
                btLAddCertificate.Enabled = true;
            }
        }

        private void btDeleteGeneratedKey_Click(object sender, EventArgs e)
        {
            btDeleteGeneratedKey.Enabled = false;
            Application.DoEvents();
            try
            {
                DoWithFirstSlotWhileLoggedIn(delegate(Session aSession)
                {
                    P11Attribute[] lAttributes = new P11Attribute[]
                    {
                        new KeyTypeAttribute(CKK.GOST)
                        //new CharArrayAttribute(CKA.LABEL){ Value = "ITS - d32f62f6-5cf5-4c4b-9cb2-4b70eee82c10".ToCharArray() }
                        //new ObjectClassAttribute(CKO.CERTIFICATE)
                        //new ByteArrayAttribute(CKA.ID) { Value = GENERATED_OBJECTS_ID }
                    };

                    aSession.FindObjectsInit(lAttributes);

                    P11Object[] objs = aSession.FindObjects(50);
                    aSession.FindObjectsFinal();

                    if (objs.Length == 0)
                    {
                        MessageBox.Show(this, "Nothing to delete!");
                        return;
                    }

                    if (MessageBox.Show(this, "You sure, dude?" + Environment.NewLine +
                        string.Join<P11Object>(Environment.NewLine, objs), Application.ProductName,
                        MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                        return;

                    foreach (P11Object lObject in objs)
                    {
                        if (lObject is Key)
                        {
                            if (new string(((Key)lObject).Label.Value) != "Astra Etoken Client")
                                aSession.DestroyObject(lObject);
                        }
                        else if (!(lObject is DomainParameters))
                            aSession.DestroyObject(lObject);
                    }
                    MessageBox.Show(this, string.Format("Deleted {0} objects:" + Environment.NewLine +
                        string.Join<P11Object>(Environment.NewLine, objs),
                        objs.Length));
                });
            }
            finally
            {
                btDeleteGeneratedKey.Enabled = true;
            }
        }

        private void btGenerateKeyRSA_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "You sure, dude?", Application.ProductName,
                MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                return;

            btGenerateKeyRSA.Enabled = false;
            Application.DoEvents();
            try
            {
                DoWithFirstSlotWhileLoggedIn(delegate(Session aSession)
                {
                    byte[] pubExp = new byte[] { 3 };
                    uint modulusBits = 1024;

                    RSAPublicKey pubTemplate = new RSAPublicKey();
                    pubTemplate.Token.Value = true;
                    pubTemplate.Encrypt.Value = true;
                    pubTemplate.Verify.Value = true;
                    pubTemplate.Label.Value = "RSA PublicKey".ToCharArray();
                    pubTemplate.Id.Value = GENERATED_OBJECTS_ID;
                    pubTemplate.PublicExponent.Value = pubExp;
                    pubTemplate.ModulusBits.Value = modulusBits;

                    RSAPrivateKey privTemplate = new RSAPrivateKey();
                    privTemplate.Token.Value = true;
                    privTemplate.Sign.Value = true;
                    privTemplate.Decrypt.Value = true;
                    privTemplate.Label.Value = "RSA PrivateKey".ToCharArray();
                    privTemplate.Id.Value = GENERATED_OBJECTS_ID;
                    privTemplate.Sensitive.Value = true;

                    KeyPair kp = aSession.GenerateKeyPair(new Mechanism(CKM.RSA_PKCS_KEY_PAIR_GEN), pubTemplate, privTemplate);
                    MessageBox.Show(kp.PublicKey.ToString());
                });
            }
            finally
            {
                btGenerateKeyRSA.Enabled = true;
            }
        }

        private void btGenerateKeyGOST_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "You sure, dude?", Application.ProductName,
                 MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                return;

            btGenerateKeyGOST.Enabled = false;
            Application.DoEvents();
            try
            {
                DoWithFirstSlotWhileLoggedIn(delegate(Session aSession)
                {
                    GostPublicKey pubTemplate = new GostPublicKey();
                    pubTemplate.Token.Value = true;
                    pubTemplate.Encrypt.Value = true;
                    pubTemplate.Verify.Value = true;
                    pubTemplate.Label.Value = "GOST_PublicKey".ToCharArray();
                    pubTemplate.Id.Value = GENERATED_OBJECTS_ID;

                    GostPrivateKey privTemplate = new GostPrivateKey();
                    privTemplate.Token.Value = true;
                    privTemplate.Sign.Value = true;
                    privTemplate.Decrypt.Value = true;
                    privTemplate.Label.Value = "GOST_PrivateKey".ToCharArray();
                    privTemplate.Id.Value = GENERATED_OBJECTS_ID;
                    privTemplate.Sensitive.Value = true;

                    KeyPair kp = aSession.GenerateKeyPair(new Mechanism(CKM.GOSTR3410_KEY_PAIR_GEN), pubTemplate, privTemplate);

                    MessageBox.Show(this, "Generated Key with label: " + kp.PublicKey.Label.ToString());
                });
            }
            finally
            {
                btGenerateKeyGOST.Enabled = true;
            }
        }

        private void btGetSupportedMechanizms_Click(object sender, EventArgs e)
        {
            DoWithFirstSlot(delegate(Slot aSlot)
            {
                MessageBox.Show(this, string.Format("Mechs:" + Environment.NewLine +
                    string.Join<CKM>(Environment.NewLine, aSlot.Token.MechanismList),
                    aSlot.Token.MechanismList.Length));
            });

        }

        private void btSignAndVerify_Click(object sender, EventArgs e)
        {
            btSignAndVerify.Enabled = false;
            Application.DoEvents();
            try
            {
                DoWithFirstSlotWhileLoggedIn(delegate(Session aSession)
                {
                    byte[] data = System.Text.Encoding.UTF8.GetBytes("123456789123456789");
                    byte[] hash;
                    P11Object[] lObjects;

                    Mechanism m = new Mechanism(CKM.GOSTR3410);

                    // Digest.
                    aSession.DigestInit(new Mechanism(CKM.GOSTR3411));
                    hash = aSession.Digest(data);
                    MessageBox.Show(this, "Hash calculated cuccessfully!" + Environment.NewLine +
                        "Initial Data:" + BitConverter.ToString(data) + Environment.NewLine +
                        "Hash:" + Environment.NewLine + BitConverter.ToString(hash));

                    // Get private key.
                    aSession.FindObjectsInit(new P11Attribute[]{
			                        	        new ObjectClassAttribute(CKO.PRIVATE_KEY),
			                        	        new KeyTypeAttribute(CKK.GOST)
			                                });
                    lObjects = aSession.FindObjects(1);
                    if (lObjects.Length == 0)
                    {
                        MessageBox.Show(this, "No Private key found!");
                        return;
                    }
                    GostPrivateKey pk = lObjects[0] as GostPrivateKey;
                    aSession.FindObjectsFinal();

                    // Sign.
                    aSession.SignInit(m, pk);
                    byte[] signature = aSession.Sign(hash);


                    MessageBox.Show(this, "Signed successfully!" + Environment.NewLine +
                        BitConverter.ToString(signature));


                    // Get public key.
                    aSession.FindObjectsInit(new P11Attribute[]{
			                        	        new ObjectClassAttribute(CKO.PUBLIC_KEY),
			                        	        new KeyTypeAttribute(CKK.GOST),
			                        	        pk.Id
			                                });

                    lObjects = aSession.FindObjects(1);
                    if (lObjects.Length == 0)
                    {
                        MessageBox.Show(this, "No Public key found!");
                        return;
                    }
                    GostPublicKey pubKey = lObjects[0] as GostPublicKey;
                    aSession.FindObjectsFinal();

                    // Verify.
                    aSession.VerifyInit(m, pubKey);

                    if (aSession.Verify(hash, signature))
                        MessageBox.Show(this, "Signature verified!");
                    else
                        MessageBox.Show(this, "Signature is invalid!");

                });
            }
            finally
            {
                btSignAndVerify.Enabled = true;
            }
        }

        private void btIssueACertificateRequest_Click(object sender, EventArgs e)
        {
            btIssueACertificateRequest.Enabled = false;
            Application.DoEvents();
            try
            {
                DoWithFirstEtokenSlotWhileLoggedIn(delegate(EtokenModule aEtokenModule, Session aSession)
                {

                    // Get public key.
                    aSession.FindObjectsInit(new P11Attribute[]{
			                        	        new ObjectClassAttribute(CKO.PUBLIC_KEY),
			                        	        new KeyTypeAttribute(CKK.GOST)
			                                });

                    P11Object[] lObjects = aSession.FindObjects(1);
                    if (lObjects.Length == 0)
                    {
                        MessageBox.Show(this, "No Public key found!");
                        return;
                    }
                    GostPublicKey pubKey = lObjects[0] as GostPublicKey;
                    aSession.FindObjectsFinal();

                    // Generate cert request.
                    byte[] lCertificateRequest;
                    string[] lDN = new string[]
                    {
                        "CN", "Иванов Иван",
                        "C", "RU"
                    };
                    string[] lExtensions = new string[] {
                        "keyUsage", "digitalSignature,keyEncipherment",
                        "extendedKeyUsage", "clientAuth,emailProtection"
                    };
                    aEtokenModule.createCSR(aSession, pubKey, lDN, null, lExtensions, out lCertificateRequest, null);

                    // Display cert req.
                    Pkcs10CertificationRequestDelaySigned lReq = new Pkcs10CertificationRequestDelaySigned(lCertificateRequest);
                    CertificationRequestInfo lCertReqInfo = lReq.GetCertificationRequestInfo();
                    MessageBox.Show(lReq.GetCertificationRequestInfo().Subject.ToString());

                });
            }
            finally
            {
                btIssueACertificateRequest.Enabled = true;
            }
        }

        private void btPkcs7SignAndVerify_Click(object sender, EventArgs e)
        {
            btPkcs7SignAndVerify.Enabled = false;
            Application.DoEvents();
            try
            {
                DoWithFirstEtokenSlotWhileLoggedIn(delegate(EtokenModule aEtokenModule, Session aSession)
                {
                    //get private key
                    aSession.FindObjectsInit(new P11Attribute[]{
			                        	        new ObjectClassAttribute(CKO.PRIVATE_KEY),
			                        	        new KeyTypeAttribute(CKK.GOST)
			                                });
                    P11Object[] lObjects = aSession.FindObjects(1);
                    if (lObjects.Length == 0)
                    {
                        MessageBox.Show(this, "No Private key found!");
                        return;
                    }
                    GostPrivateKey pk = lObjects[0] as GostPrivateKey;
                    aSession.FindObjectsFinal();

                    //get public key
                    aSession.FindObjectsInit(new P11Attribute[]{
			                        	        new ObjectClassAttribute(CKO.PUBLIC_KEY),
			                        	        new KeyTypeAttribute(CKK.GOST)
			                                });

                    lObjects = aSession.FindObjects(1);
                    if (lObjects.Length == 0)
                    {
                        MessageBox.Show(this, "No Public key found!");
                        return;
                    }
                    GostPublicKey pubKey = lObjects[0] as GostPublicKey;
                    aSession.FindObjectsFinal();

                    //get certificate
                    aSession.FindObjectsInit(
                        new P11Attribute[]
                        {
			                new ObjectClassAttribute(CKO.CERTIFICATE)
			            });

                    lObjects = aSession.FindObjects(1);
                    if (lObjects.Length == 0)
                    {
                        MessageBox.Show(this, "No certificate found!");
                        return;
                    }
                    Certificate certificate = lObjects[0] as Certificate;
                    aSession.FindObjectsFinal();

                    // Sign.
                    byte[] data = System.Text.Encoding.UTF8.GetBytes("123456789123456789");
                    byte[] lEnvelope;
                    aEtokenModule.pkcs7Sign(aSession, data, certificate, out lEnvelope, pk, null,
                        Net.Sf.Pkcs11.EtokenExtensions.Wrapper.pkcs7SignFlags.DETACHED_SIGNATURE);

                    // Display.
                    MessageBox.Show(this, "Signed successfully!" + Environment.NewLine +
                        "Initial Data:" + BitConverter.ToString(data) + Environment.NewLine +
                        "Envelope:" + Environment.NewLine + BitConverter.ToString(lEnvelope));


                    // Verify.
                    aEtokenModule.pkcs7Verify(lEnvelope, data);
                    MessageBox.Show(this, "Signature successfully verified!");

                });
            }
            finally
            {
                btPkcs7SignAndVerify.Enabled = true;
            }
        }

        private void btGetCertificateInfo_Click(object sender, EventArgs e)
        {
            btGetCertificateInfo.Enabled = false;
            Application.DoEvents();
            try
            {
                DoWithFirstEtokenSlotWhileLoggedIn(delegate(EtokenModule aEtokenModule, Session aSession)
                {
                    //get certificate
                    aSession.FindObjectsInit(
                        new P11Attribute[]
                        {
			                new ObjectClassAttribute(CKO.CERTIFICATE)
			            });

                    P11Object[] lObjects = aSession.FindObjects(1);
                    if (lObjects.Length == 0)
                    {
                        MessageBox.Show(this, "No certificate found!");
                        return;
                    }
                    Certificate certificate = lObjects[0] as Certificate;
                    aSession.FindObjectsFinal();

                    // getInfo.
                    string lInfo;
                    aEtokenModule.getCertificateInfo(aSession, certificate, out lInfo);

                    // Display.
                    MessageBox.Show(this, lInfo);
                });
            }
            finally
            {
                btGetCertificateInfo.Enabled = true;
            }
        }

        private void btCertificateVerify_Click(object sender, EventArgs e)
        {
            btCertificateVerify.Enabled = false;
            Application.DoEvents();
            try
            {
                DoWithFirstEtokenSlotWhileLoggedIn(delegate(EtokenModule aEtokenModule, Session aSession)
                {
                    // Get certificate.
                    aSession.FindObjectsInit(
                        new P11Attribute[]
                        {
			                new ObjectClassAttribute(CKO.CERTIFICATE)
			            });

                    P11Object[] lObjects = aSession.FindObjects(1);
                    if (lObjects.Length == 0)
                    {
                        MessageBox.Show(this, "No certificate found!");
                        return;
                    }
                    Certificate certificate = lObjects[0] as Certificate;
                    aSession.FindObjectsFinal();

                    // GetInfo.
                    aEtokenModule.certVerify(aSession, certificate, null, null, IntPtr.Zero);

                    // Display.
                    MessageBox.Show(this, "Certificate successfully verified!");
                });
            }
            finally
            {
                btCertificateVerify.Enabled = true;
            }
        }
    }
}
