namespace LevelEditor
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
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.editor1 = new LevelEditor.Editor();
            this.tileDisplay1 = new LevelEditor.TileDisplay(ref editor1);
            this.SuspendLayout();
            // 
            // editor1
            // 
            this.editor1.Dock = System.Windows.Forms.DockStyle.Left;
            this.editor1.Location = new System.Drawing.Point(0, 0);
            this.editor1.Name = "editor1";
            this.editor1.Size = new System.Drawing.Size(640, 480);
            this.editor1.TabIndex = 0;
            this.editor1.Text = "editor1";
            this.editor1.Click += new System.EventHandler(this.editor1_Click);
            // 
            // tileDisplay1
            // 
            this.tileDisplay1.Location = new System.Drawing.Point(657, 0);
            this.tileDisplay1.Name = "tileDisplay1";
            this.tileDisplay1.Size = new System.Drawing.Size(320, 480);
            this.tileDisplay1.TabIndex = 1;
            this.tileDisplay1.Text = "tileDisplay1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(989, 480);
            this.Controls.Add(this.tileDisplay1);
            this.Controls.Add(this.editor1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private Editor editor1;
        private TileDisplay tileDisplay1;
    }
}

