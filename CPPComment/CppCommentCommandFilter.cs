using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.OLE;
using Microsoft.VisualStudio.Text.Editor;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TextManager.Interop;
using System.IO;

namespace CPPComment
{
    internal class KeyBindingCommandFilter : IOleCommandTarget
    {
        private readonly List<string> fileextensions = new List<string>() { ".C", ".c", ".cc", ".cxx", ".cpp", ".c++", ".h", ".hpp", ".h++", ".hxx" };
        private IWpfTextView m_textView;
        internal IOleCommandTarget m_nextTarget;
        internal bool isCOrCpp;
        internal bool m_added;


        private int tripleSlashCount = 0;
        public KeyBindingCommandFilter(IWpfTextView textView)
        {
            m_textView = textView;
            textView.TextBuffer.Properties.TryGetProperty(typeof(IVsTextBuffer), out IVsTextBuffer bufferAdapter);
            var persistFileFormat = bufferAdapter as IPersistFileFormat;
            persistFileFormat.GetCurFile(out string filePath, out _);


            if (fileextensions.IndexOf(Path.GetExtension(filePath)) > -1)
            {
                isCOrCpp = true;
            }
            else
            {
                isCOrCpp = false;
            }
            
             
        }

        int IOleCommandTarget.QueryStatus(ref Guid pguidCmdGroup, uint cCmds, OLECMD[] prgCmds, IntPtr pCmdText)
        {
            return m_nextTarget.QueryStatus(ref pguidCmdGroup, cCmds, prgCmds, pCmdText);
        }

        int IOleCommandTarget.Exec(ref Guid pguidCmdGroup, uint nCmdID, uint nCmdexecopt, IntPtr pvaIn, IntPtr pvaOut)
        {

            char typedChar = char.MinValue;


            if (pguidCmdGroup == VSConstants.VSStd2K && nCmdID == (uint)VSConstants.VSStd2KCmdID.TYPECHAR && isCOrCpp)
            {

                typedChar = (char)(ushort)Marshal.GetObjectForNativeVariant(pvaIn);

                if (typedChar.Equals('/'))
                {
                    Debug.WriteLine("Current / Count: " + tripleSlashCount.ToString());

                    tripleSlashCount++;
                }
                else
                {
                    tripleSlashCount = 0;
                }

            }



            if (tripleSlashCount > 2 && isCOrCpp)
            {

                tripleSlashCount = 0;
                new CommentGenerator(m_textView);

                return m_nextTarget.Exec(ref pguidCmdGroup, (uint)VSConstants.VSStd2KCmdID.RIGHT, nCmdexecopt, pvaIn, pvaOut);
            }
            else
            {
                return m_nextTarget.Exec(ref pguidCmdGroup, nCmdID, nCmdexecopt, pvaIn, pvaOut);
            }



        }
    }
}
