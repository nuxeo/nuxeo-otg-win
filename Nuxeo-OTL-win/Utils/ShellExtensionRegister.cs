using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace Nuxeo.Otg.Win
{
    internal class ShellExtensionRegister
    {
        protected const String CONTEXTMENU_FILE_KEY = @"*\shellex\ContextMenuHandlers\{0}";
        protected const String CONTEXTMENU_FOLDER_KEY = @"Folder\shellex\ContextMenuHandlers\{0}";
        protected const String APPROVED_KEY = @"Software\Microsoft\Windows\CurrentVersion\Shell Extensions\Approved";

        /// <summary>
        /// Force the key to approve shell extension
        /// </summary>
        /// <param name="clsid"></param>
        /// <param name="keyValue"></param>
        protected static void AddApprovedShellExtension(Guid clsid, string keyValue)
        {
            using (RegistryKey key = Registry.LocalMachine.CreateSubKey(APPROVED_KEY))
            {
                if (key != null)
                {
                    key.SetValue(clsid.ToString("B"), keyValue);
                }
            }
        }

        /// <summary>
        /// Remove the key from approved extension
        /// </summary>
        /// <param name="clsid"></param>
        protected static void RemoveApprochedShellExtension(Guid clsid)
        {
            using (RegistryKey key = Registry.LocalMachine.CreateSubKey(APPROVED_KEY))
            {
                if (key != null)
                {
                    key.DeleteValue(clsid.ToString("B"));
                }
            }
        }

        public static void RegisterShellExtIconOverlay(Guid clsid)
        {
            if (clsid == Guid.Empty)
            {
                throw new ArgumentException("clsid must not be empty");
            }

            string keyName = @"Software\Microsoft\Windows\CurrentVersion\Explorer\ShellIconOverlayIdentifiers\1NuxeoOtg";
            using (RegistryKey key = Registry.LocalMachine.CreateSubKey(keyName))
            {
                // Set the default value of the key.
                if (key != null && !string.IsNullOrEmpty(clsid.ToString("B")))
                {
                    key.SetValue(null, clsid.ToString("B"));
                }
            }
        }

        public static void UnregisterShellExtIconOverlay(Guid clsid)
        {
            if (clsid == null)
            {
                throw new ArgumentException("clsid must not be null");
            }

            string keyName = @"Software\Microsoft\Windows\CurrentVersion\Explorer\ShellIconOverlayIdentifiers\1NuxeoOtg";
            Registry.LocalMachine.DeleteSubKeyTree(keyName, false);
        }

        /// <summary>
        /// Register Shell Extension Contexte Menu Handler
        /// </summary>
        /// <param name="clsid"></param>
        /// <param name="friendlyName"></param>
        public static void RegisterShellExtContextMenuHandler(Guid clsid, String friendlyName)
        {
            if (clsid == Guid.Empty) { throw new ArgumentException("clsid must not be empty"); }
            if (string.IsNullOrEmpty(friendlyName))
            {
                throw new ArgumentException("friendlyName must not be null or empty");
            }

            // Register for * file
            string keyName = string.Format(CONTEXTMENU_FILE_KEY, friendlyName);
            using (RegistryKey key = Registry.ClassesRoot.CreateSubKey(keyName))
            {
                if (key != null) { key.SetValue(null, clsid.ToString("B")); }
            }
            // Register for folder
            keyName = string.Format(CONTEXTMENU_FOLDER_KEY, friendlyName);
            using (RegistryKey key = Registry.ClassesRoot.CreateSubKey(keyName))
            {
                if (key != null) { key.SetValue(null, clsid.ToString("B")); }
            }
            AddApprovedShellExtension(clsid, friendlyName);
        }

        /// <summary>
        /// Unregister a shellExt handler
        /// </summary>
        /// <param name="clsid"></param>
        /// <param name="friendlyName"></param>
        public static void UnregisterShellExtContextMenuHandler(Guid clsid, String friendlyName)
        {
            if (clsid == Guid.Empty) { throw new ArgumentException("clsid must not be null"); }

            // Unregister for * file
            string keyName = string.Format(CONTEXTMENU_FILE_KEY, friendlyName);
            Registry.ClassesRoot.DeleteSubKeyTree(keyName, false);
            // Unregister for Folder file
            keyName = string.Format(CONTEXTMENU_FOLDER_KEY, friendlyName);
            Registry.ClassesRoot.DeleteSubKeyTree(keyName, false);
            RemoveApprochedShellExtension(clsid);
        }
    }
}
