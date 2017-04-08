using System;
using System.Windows;
using System.Runtime.InteropServices;

namespace CMD_Run.Engine
{
    public class InputListener : Application
    {
        public event EventHandler<KeyTypedEventArgs> OnKeyDown = null;
        public event EventHandler<KeyTypedEventArgs> OnKeyUp = null;

        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;

        //Speichert den Pointer zur hookId dieser 
        private IntPtr hookId = IntPtr.Zero;

        /// <summary>
        /// Fängt an die WML nach Events nach Tastatureingaben zu durchsuchen
        /// </summary>
        public void Start()
        {
            hookId = KeyboardInterceptor.SetHook(ProcessMessage);
        }

        /// <summary>
        /// Hört auf WML-Events Tastatureingaben zu durchsuchen
        /// </summary>
        public void Stop()
        {
            if (hookId != IntPtr.Zero)
            {
                if(KeyboardInterceptor.UnhookWindowsHookEx(hookId))
                {
                    hookId = IntPtr.Zero;
                }
            }
        }

        /// <summary>
        /// Funktion, die <see cref="KeyboardInterceptor.SetHook(KeyboardProcedure)"/> als Parameter zum Verarbeiten der eingehenden Windows-Message-Loop-Nachrichten übergeben wird
        /// </summary>
        /// <param name="nCode"></param>
        /// <param name="wParam">Allgemein: weitere Informationen zur WML-Nachricht, speziell: Art der WML-Nachricht</param>
        /// <param name="lParam">Allgemein: weitere Informationen zur WML-Nachricht, speziell: Tastencode für die gedrückte Taste</param>
        /// <returns></returns>
        private IntPtr ProcessMessage(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                switch(wParam.ToInt32())
                {
                    case WM_KEYDOWN:
                        OnKeyDown?.Invoke(this, new KeyTypedEventArgs(Marshal.ReadInt32(lParam)));
                        break;
                    case WM_KEYUP:
                        OnKeyUp?.Invoke(this, new KeyTypedEventArgs(Marshal.ReadInt32(lParam)));
                        break;

                    default:
                        break;
                }
            }
            return KeyboardInterceptor.CallNextHookEx(hookId, nCode, wParam, lParam);
        }
    }
}
