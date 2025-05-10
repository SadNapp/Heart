using System.Threading;

namespace MainForm
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (_components != null))
            {
                _components.Dispose();
            }
            base.Dispose(disposing);
            if (_animationThreads != null)
            {
                foreach (Thread thread in _animationThreads)
                {
                    thread.Abort();  
                }
                _animationThreads.Clear();
                _animationThreads = null;
            }
        }

            #region Windows Form Designer generated code

            /// <summary>
            /// Required method for Designer support - do not modify
            /// the contents of this method with the code editor.
            /// </summary>
            private void InitializeComponent()
            {
                this.SuspendLayout();
                // 
                // Form1
                // 
                this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
                this.ClientSize = new System.Drawing.Size(784, 411);
                this.Cursor = System.Windows.Forms.Cursors.Arrow;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
                this.Name = "Form1";
                this.Text = "Form1";
                this.ResumeLayout(false);

            }

            #endregion
    }
}

