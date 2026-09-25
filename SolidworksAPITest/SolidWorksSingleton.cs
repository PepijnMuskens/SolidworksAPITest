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
            Guid clsid;
            string progId = "Sldworks.Application";

            if (swApp == null)
            {
                try
                {
                    NativeMethods.CLSIDFromProgIDEx(progId, out clsid);
                    
                }
                catch
                {
                    NativeMethods.CLSIDFromProgID(progId, out clsid);
                }
                NativeMethods.GetActiveObject(ref clsid, IntPtr.Zero, out var obj);
                swApp = (SldWorks)obj;
                swApp.Visible = true;

                return swApp;
            }
            return swApp;
        }

        internal static void Dispose()
        {
            if (swApp != null)
            {
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
