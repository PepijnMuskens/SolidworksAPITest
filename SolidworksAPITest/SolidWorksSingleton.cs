using SolidWorks.Interop.sldworks;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;

namespace SolidworksAPITest
{
    internal class SolidWorksSingleton
    {
        private static SldWorks swApp;

        private SolidWorksSingleton()
        {
        }

        internal static SldWorks GetApplication()
        {
            // Already connected
            if (swApp != null)
                return swApp;

            Guid clsid;
            string progId = "Sldworks.Application";

            try
            {
                // Try to get the CLSID
                try
                {
                    NativeMethods.CLSIDFromProgIDEx(progId, out clsid);
                }
                catch
                {
                    NativeMethods.CLSIDFromProgID(progId, out clsid);
                }

                // Try to connect to an already running SolidWorks instance
                NativeMethods.GetActiveObject(
                    ref clsid,
                    IntPtr.Zero,
                    out var obj
                );

                swApp = (SldWorks)obj;
                swApp.Visible = true;

                return swApp;
            }
            catch (COMException ex)
            {
                Console.WriteLine(
                    $"[WARNING] SolidWorks was not found or is not running. " +
                    $"Continuing without SolidWorks. ({ex.Message})"
                );

                Debug.WriteLine(
                    $"SolidWorks connection failed: {ex}"
                );

                swApp = null;
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"[WARNING] Could not connect to SolidWorks. " +
                    $"Continuing without SolidWorks. ({ex.Message})"
                );

                Debug.WriteLine(
                    $"SolidWorks connection failed: {ex}"
                );

                swApp = null;
                return null;
            }
        }

        internal static void Dispose()
        {
            if (swApp != null)
            {
                try
                {
                    Marshal.FinalReleaseComObject(swApp);
                }
                catch
                {
                    // Ignore cleanup errors
                }

                swApp = null;
            }
        }
    }

    internal static class NativeMethods
    {
        private const string OLEAUT32 = "oleaut32.dll";
        private const string OLE32 = "ole32.dll";

        [DllImport(OLE32, PreserveSig = false)]
        [SuppressUnmanagedCodeSecurity]
        public static extern void CLSIDFromProgIDEx(
            [MarshalAs(UnmanagedType.LPWStr)] string progId,
            out Guid clsid
        );

        [DllImport(OLE32, PreserveSig = false)]
        [SuppressUnmanagedCodeSecurity]
        public static extern void CLSIDFromProgID(
            [MarshalAs(UnmanagedType.LPWStr)] string progId,
            out Guid clsid
        );

        [DllImport(OLEAUT32, PreserveSig = false)]
        [SuppressUnmanagedCodeSecurity]
        public static extern void GetActiveObject(
            ref Guid rclsid,
            IntPtr reserved,
            [MarshalAs(UnmanagedType.Interface)] out object ppunk
        );
    }
}