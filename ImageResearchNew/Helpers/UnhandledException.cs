using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Threading;

namespace ImageResearchNew
{
    public static class UnhandledException
    {
        public static class MINIDUMP_TYPE
        {
            public const int MiniDumpNormal = 0x00000000;
            public const int MiniDumpWithDataSegs = 0x00000001;
            public const int MiniDumpWithFullMemory = 0x00000002;
            public const int MiniDumpWithHandleData = 0x00000004;
            public const int MiniDumpFilterMemory = 0x00000008;
            public const int MiniDumpScanMemory = 0x00000010;
            public const int MiniDumpWithUnloadedModules = 0x00000020;
            public const int MiniDumpWithIndirectlyReferencedMemory = 0x00000040;
            public const int MiniDumpFilterModulePaths = 0x00000080;
            public const int MiniDumpWithProcessThreadData = 0x00000100;
            public const int MiniDumpWithPrivateReadWriteMemory = 0x00000200;
            public const int MiniDumpWithoutOptionalData = 0x00000400;
            public const int MiniDumpWithFullMemoryInfo = 0x00000800;
            public const int MiniDumpWithThreadInfo = 0x00001000;
            public const int MiniDumpWithCodeSegs = 0x00002000;
        }

        [DllImport("dbghelp.dll")]
        public static extern bool MiniDumpWriteDump(IntPtr hProcess,
                                                    Int32 ProcessId,
                                                    IntPtr hFile,
                                                    int DumpType,
                                                    IntPtr ExceptionParam,
                                                    IntPtr UserStreamParam,
                                                    IntPtr CallackParam);

        public static void DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            CreateMiniDump(e);
        }

        public static void DispatcherUnhandledException1(object sender, UnhandledExceptionEventArgs e)
        {
            CreateMiniDump1(e);
        }

        private static void CreateMiniDump(DispatcherUnhandledExceptionEventArgs e)
        {
            string fileName = string.Format("dump1 {0:yyyy-MM-dd-HH_mm_ss}.dmp", DateTime.Now);

            using (FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + fileName, FileMode.Create))
            {
                using (System.Diagnostics.Process process = System.Diagnostics.Process.GetCurrentProcess())
                {
                    string error = string.Format("Process: {0}{1}{1}Exception: {2}", process.ProcessName, Environment.NewLine, e.Exception);
                    byte[] errorB = Encoding.UTF8.GetBytes(error);
                    fs.Write(errorB, 0, errorB.Length);
                }
            }
        }

        private static void CreateMiniDump1(UnhandledExceptionEventArgs e)
        {
            string fileName = string.Format("dump2 {0:yyyy-MM-dd-HH_mm_ss}.dmp", DateTime.Now);

            using (FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + fileName, FileMode.Create))
            {
                using (System.Diagnostics.Process process = System.Diagnostics.Process.GetCurrentProcess())
                {
                    string error = string.Format("Process: {0}{1}{1}Exception: {2}", process.ProcessName, Environment.NewLine, e.ExceptionObject);
                    byte[] errorB = Encoding.UTF8.GetBytes(error);
                    fs.Write(errorB, 0, errorB.Length);
                }
            }
        }
    }
}
