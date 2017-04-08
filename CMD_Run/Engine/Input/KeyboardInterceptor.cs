using System;
using System.Diagnostics;
using System.Windows.Input;
using System.Runtime.InteropServices;

namespace CMD_Run.Engine
{
    public delegate IntPtr KeyboardProcedure(int nCode, IntPtr wParam, IntPtr lParam);
    internal static class KeyboardInterceptor
    {
        private const int WH_KEYBOARD_LL = 13;

        public static IntPtr SetHook(KeyboardProcedure procedure)
        {
            string mainModuleName = Process.GetCurrentProcess().MainModule.ModuleName;
            return SetWindowsHookEx(WH_KEYBOARD_LL, procedure, GetModuleHandle(mainModuleName), 0);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SetWindowsHookEx(int idHook, KeyboardProcedure lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);
    }

    public class KeyTypedEventArgs : EventArgs
    {
        public KeyTypedEventArgs(int vkCode)
        {
            this.VkCode = vkCode;
            this.KeyCode = KeyInterop.KeyFromVirtualKey(vkCode);
        }

        public int VkCode { get; private set; }
        public Key KeyCode { get; private set; }
    }
}
