namespace PreciseAirCursor
    {
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.videoPictureBox = new System.Windows.Forms.PictureBox();
            this.glyphPictureBox = new System.Windows.Forms.PictureBox();
            this.framesPerSecondLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.videoPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glyphPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // videoPictureBox
            // 
            this.videoPictureBox.Location = new System.Drawing.Point(14, 12);
            this.videoPictureBox.Name = "videoPictureBox";
            this.videoPictureBox.Size = new System.Drawing.Size(352, 250);
            this.videoPictureBox.TabIndex = 9;
            this.videoPictureBox.TabStop = false;
            // 
            // glyphPictureBox
            // 
            this.glyphPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("glyphPictureBox.Image")));
            this.glyphPictureBox.Location = new System.Drawing.Point(436, 12);
            this.glyphPictureBox.Name = "glyphPictureBox";
            this.glyphPictureBox.Size = new System.Drawing.Size(250, 250);
            this.glyphPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.glyphPictureBox.TabIndex = 10;
            this.glyphPictureBox.TabStop = false;
            // 
            // framesPerSecondLabel
            // 
            this.framesPerSecondLabel.AutoSize = true;
            this.framesPerSecondLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.framesPerSecondLabel.Location = new System.Drawing.Point(158, 274);
            this.framesPerSecondLabel.Name = "framesPerSecondLabel";
            this.framesPerSecondLabel.Size = new System.Drawing.Size(16, 16);
            this.framesPerSecondLabel.TabIndex = 11;
            this.framesPerSecondLabel.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 274);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 16);
            this.label1.TabIndex = 12;
            this.label1.Text = "Frames per second";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(874, 379);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.framesPerSecondLabel);
            this.Controls.Add(this.glyphPictureBox);
            this.Controls.Add(this.videoPictureBox);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Precise air cursor (demo)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.videoPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glyphPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

            }

        #endregion

        private System.Windows.Forms.PictureBox videoPictureBox;
        private System.Windows.Forms.PictureBox glyphPictureBox;
        private System.Windows.Forms.Label framesPerSecondLabel;
        private System.Windows.Forms.Label label1;
        }
    }

