namespace ImageCrop
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
            this.txtFolderPath = new System.Windows.Forms.TextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.btnFolderPath = new System.Windows.Forms.Button();
            this.btnCutImage = new System.Windows.Forms.Button();
            this.txtResult = new System.Windows.Forms.Label();
            this.linkOutputFolder = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // txtFolderPath
            // 
            this.txtFolderPath.Location = new System.Drawing.Point(204, 48);
            this.txtFolderPath.Name = "txtFolderPath";
            this.txtFolderPath.Size = new System.Drawing.Size(446, 20);
            this.txtFolderPath.TabIndex = 0;
            this.txtFolderPath.TextChanged += new System.EventHandler(this.txtFolderPath_TextChanged);
            // 
            // btnFolderPath
            // 
            this.btnFolderPath.Location = new System.Drawing.Point(71, 45);
            this.btnFolderPath.Name = "btnFolderPath";
            this.btnFolderPath.Size = new System.Drawing.Size(110, 23);
            this.btnFolderPath.TabIndex = 1;
            this.btnFolderPath.Text = "Chọn thư mục ảnh";
            this.btnFolderPath.UseVisualStyleBackColor = true;
            this.btnFolderPath.Click += new System.EventHandler(this.btnFolderPath_Click);
            // 
            // btnCutImage
            // 
            this.btnCutImage.Location = new System.Drawing.Point(204, 86);
            this.btnCutImage.Name = "btnCutImage";
            this.btnCutImage.Size = new System.Drawing.Size(95, 28);
            this.btnCutImage.TabIndex = 2;
            this.btnCutImage.Text = "Cắt ảnh";
            this.btnCutImage.UseVisualStyleBackColor = true;
            this.btnCutImage.Click += new System.EventHandler(this.btnCutImage_Click);
            // 
            // txtResult
            // 
            this.txtResult.AutoSize = true;
            this.txtResult.Location = new System.Drawing.Point(134, 155);
            this.txtResult.Name = "txtResult";
            this.txtResult.Size = new System.Drawing.Size(47, 13);
            this.txtResult.TabIndex = 5;
            this.txtResult.Text = "Kết quả:";
            // 
            // linkOutputFolder
            // 
            this.linkOutputFolder.AutoSize = true;
            this.linkOutputFolder.Location = new System.Drawing.Point(201, 155);
            this.linkOutputFolder.Name = "linkOutputFolder";
            this.linkOutputFolder.Size = new System.Drawing.Size(65, 13);
            this.linkOutputFolder.TabIndex = 6;
            this.linkOutputFolder.TabStop = true;
            this.linkOutputFolder.Text = "place-holder";
            this.linkOutputFolder.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.outputFolder_LinkClicked);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(732, 223);
            this.Controls.Add(this.linkOutputFolder);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.btnCutImage);
            this.Controls.Add(this.btnFolderPath);
            this.Controls.Add(this.txtFolderPath);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFolderPath;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button btnFolderPath;
        private System.Windows.Forms.Button btnCutImage;
        private System.Windows.Forms.Label txtResult;
        private System.Windows.Forms.LinkLabel linkOutputFolder;
    }
}

