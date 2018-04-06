using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Formatting;
using Microsoft.VisualStudio.TextManager.Interop;

namespace CPPComment
{
    /// <summary>
    /// KeyBindingTest places red boxes behind all the "a"s in the editor window
    /// </summary>
    internal sealed class CommentGenerator
    {
        /// <summary>
        /// The layer of the adornment.
        /// </summary>
        private readonly IAdornmentLayer layer;

        /// <summary>
        /// Text view where the adornment is created.
        /// </summary>
        private readonly IWpfTextView view;



        /// <summary>
        /// Initializes a new instance of the <see cref="KeyBindingTest"/> class.
        /// </summary>
        /// <param name="view">Text view to create the adornment for</param>
        public CommentGenerator(IWpfTextView view)
        {
            if (view == null)
            {
                throw new ArgumentNullException("view");
            }

            this.layer = view.GetAdornmentLayer("CommentGenerator");

            this.view = view;


            // Create the pen and brush to color the box behind the a's

            ParseFunction();
          


        }
      
        public void ParseFunction()
        {

            int currentPosition = this.view.Caret.Position.VirtualBufferPosition.Position;
            this.view.Caret.MoveToNextCaretPosition();
            string textOnNextLine = this.view.Caret.Position.BufferPosition.GetContainingLine().GetText();


            CPPFunctionParser parser = new CPPFunctionParser();
            string comment = parser.GenerateComment(textOnNextLine);

            ITextEdit edit = this.view.TextBuffer.CreateEdit();

            // edit.Delete(currentPosition - 5, 4);
            // edit.Apply();

            //  edit = this.view.TextBuffer.CreateEdit();

            edit.Delete(currentPosition, 2);
            edit.Insert(currentPosition, comment);
            edit.Apply();
        }
    }
}
