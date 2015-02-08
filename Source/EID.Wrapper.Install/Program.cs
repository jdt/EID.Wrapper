using System;
using System.Diagnostics;
using System.IO;
using System.Security.Principal;

namespace EID.Wrapper.Register
{
    class Program
    {
        static void Main(string[] args)
        {
            if(!HasAdministratorRights())
            {
                Console.WriteLine("This tool needs administrator privileges to copy and register files. Please run this tool as Administrator");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
                return;
            }

            var destination = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.SystemX86), "EID.Wrapper.dll");
            var tlb = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.SystemX86), "EID.Wrapper.tlb");

            var regasm = Path.Combine(System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory(), "regasm.exe");

            Console.WriteLine("Copying EID.Wrapper.dll to " + destination + "...");
            File.Copy("EID.Wrapper.dll", destination, true);

            Console.WriteLine("Registering EID.Wrapper.dll in " + destination + " as codebase and generating tlb with regasm...");
            Console.WriteLine("--------------------------------------------------------------------------------");

            var info = new ProcessStartInfo(regasm, string.Format("{0} /tlb:{1} /codebase", destination, tlb));
            info.UseShellExecute = false;
            var proc = Process.Start(info);
            proc.WaitForExit();

            Console.WriteLine("--------------------------------------------------------------------------------");
            if (proc.ExitCode == 0)
                Console.WriteLine("SUCCESS: Wrapper registered successfully");
            else
                Console.WriteLine("WARNING: The DLL was copied but regasm returned an unexpected return value. Check to make sure that the call to regasm succeeded.");

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        private static bool HasAdministratorRights()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
}
