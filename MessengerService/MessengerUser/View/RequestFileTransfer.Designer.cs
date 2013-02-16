namespace MessengerUser.View
{
    partial class RequestFileTransfer
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
            this.friendName_lbl = new System.Windows.Forms.Label();
            this.accept_btn = new System.Windows.Forms.Button();
            this.reject_btn = new System.Windows.Forms.Button();
            this.browse_btn = new System.Windows.Forms.Button();
            this.path_txtbox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // friendName_lbl
            // 
            this.friendName_lbl.AutoSize = true;
            this.friendName_lbl.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.friendName_lbl.Location = new System.Drawing.Point(2, 20);
            this.friendName_lbl.Name = "friendName_lbl";
            this.friendName_lbl.Size = new System.Drawing.Size(61, 23);
            this.friendName_lbl.TabIndex = 0;
            this.friendName_lbl.Text = "label1";
            // 
            // accept_btn
            // 
            this.accept_btn.Location = new System.Drawing.Point(95, 109);
            this.accept_btn.Name = "accept_btn";
            this.accept_btn.Size = new System.Drawing.Size(75, 23);
            this.accept_btn.TabIndex = 1;
            this.accept_btn.Text = "Accept";
            this.accept_btn.UseVisualStyleBackColor = true;
            this.accept_btn.Click += new System.EventHandler(this.accept_btn_Click);
            // 
            // reject_btn
            // 
            this.reject_btn.Location = new System.Drawing.Point(191, 109);
            this.reject_btn.Name = "reject_btn";
            this.reject_btn.Size = new System.Drawing.Size(75, 23);
            this.reject_btn.TabIndex = 2;
            this.reject_btn.Text = "Reject";
            this.reject_btn.UseVisualStyleBackColor = true;
            this.reject_btn.Click += new System.EventHandler(this.reject_btn_Click);
            // 
            // browse_btn
            // 
            this.browse_btn.Location = new System.Drawing.Point(288, 55);
            this.browse_btn.Name = "browse_btn";
            this.browse_btn.Size = new System.Drawing.Size(75, 23);
            this.browse_btn.TabIndex = 3;
            this.browse_btn.Text = "Browse";
            this.browse_btn.UseVisualStyleBackColor = true;
            this.browse_btn.Click += new System.EventHandler(this.browse_btn_Click);
            // 
            // path_txtbox
            // 
            this.path_txtbox.Location = new System.Drawing.Point(45, 60);
            this.path_txtbox.Name = "path_txtbox";
            this.path_txtbox.ReadOnly = true;
            this.path_txtbox.Size = new System.Drawing.Size(237, 20);
            this.path_txtbox.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.path_txtbox);
            this.groupBox1.Controls.Add(this.browse_btn);
            this.groupBox1.Controls.Add(this.friendName_lbl);
            this.groupBox1.Location = new System.Drawing.Point(-1, -2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(369, 105);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Transfer Info";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Path:";
            // 
            // RequestFileTransfer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(368, 144);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.reject_btn);
            this.Controls.Add(this.accept_btn);
            this.Name = "RequestFileTransfer";
            this.Text = "RequestFileTransfer";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label friendName_lbl;
        private System.Windows.Forms.Button accept_btn;
        private System.Windows.Forms.Button reject_btn;
        private System.Windows.Forms.Button browse_btn;
        private System.Windows.Forms.TextBox path_txtbox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
    }
}