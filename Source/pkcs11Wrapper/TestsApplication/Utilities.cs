using System;
using System.IO;
using System.Windows.Forms;

using Net.Sf.Pkcs11;
using Net.Sf.Pkcs11.Objects;
using Net.Sf.Pkcs11.Wrapper;
using Net.Sf.Pkcs11.EtokenExtensions;

namespace Tests
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Slot event.
        /// </summary>
        /// <param name="aSlot"></param>
        public delegate void SlotAction(Slot aSlot);
        
        /// <summary>
        /// Etoken Slot event.
        /// </summary>
        /// <param name="aSlot"></param>
        public delegate void EtokenSlotAction(EtokenModule aEtokenModule, Slot aSlot);

        /// <summary>
        /// Session event.
        /// </summary>
        /// <param name="aSlot"></param>
        public delegate void SessionAction(Session aSession);

        /// <summary>
        /// Etoken Session event.
        /// </summary>
        /// <param name="aSlot"></param>
        public delegate void EtokenSessionAction(EtokenModule aEtokenModule, Session aSession);

        /// <summary>
        /// Main utility procedure of this form.
        /// </summary>
        /// <param name="aAction"></param>
        public void DoWithFirstSlot(SlotAction aAction)
        {
            try
            {
                // Initialize module.
                Module lModule = Module.GetInstance(cbProviderDll.Text);

                // Call event.
                using (lModule)
                {
                    // GetSlotList.
                    Slot[] lSlots = lModule.GetSlotList(true);
                    if (lSlots.Length == 0)
                    {
                        MessageBox.Show(this, "No inserted tokens");
                        return;
                    }

                    if (aAction != null)
                        aAction(lSlots[Convert.ToInt32(udTokenIndex.Value)]);
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(this, E.ToString());
            }
        }

        /// <summary>
        /// Main utility procedure of this form.
        /// </summary>
        /// <param name="aAction"></param>
        public void DoWithFirstSlotWhileLoggedIn(SessionAction aAction)
        {
            DoWithFirstSlot(delegate(Slot aSlot)
            {
                // Log in.
                Session session = aSlot.Token.OpenSession(false);
                session.Login(UserType.USER, tbTokenPassword.Text);

                // Do something while logged in.
                try
                {
                    if (aAction != null)
                        aAction(session);
                }
                finally
                {
                    // Log out.
                    session.Logout();
                }
            });
        }

        /// <summary>
        /// Main utility procedure of this form.
        /// </summary>
        /// <param name="aAction"></param>
        public void DoWithFirstEtokenSlot(EtokenSlotAction aAction)
        {
            try
            {
                // Initialize module.
                EtokenModule lModule = EtokenModule.GetInstance(cbProviderDll.Text);

                using (lModule)
                {
                    // GetSlotList.
                    Slot[] lSlots = lModule.GetSlotList(true);
                    if (lSlots.Length == 0)
                    {
                        MessageBox.Show(this, "No inserted tokens");
                        return;
                    }

                    if (aAction != null)
                        aAction(lModule, lSlots[Convert.ToInt32(udTokenIndex.Value)]);
                }
                
            }
            catch (Exception E)
            {
                MessageBox.Show(this, E.ToString());
            }
        }

        /// <summary>
        /// Main utility procedure of this form.
        /// </summary>
        /// <param name="aAction"></param>
        public void DoWithFirstEtokenSlotWhileLoggedIn(EtokenSessionAction aAction)
        {
            DoWithFirstEtokenSlot(delegate(EtokenModule aEtokenModule, Slot aSlot)
            {
                // Log in.
                Session session = aSlot.Token.OpenSession(false);
                session.Login(UserType.USER, tbTokenPassword.Text);

                // Do something while logged in.
                try
                {
                    if (aAction != null)
                        aAction(aEtokenModule, session);
                }
                finally
                {
                    // Log out.
                    session.Logout();
                }
            });
        }
    }
}
