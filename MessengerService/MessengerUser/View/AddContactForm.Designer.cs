namespace MessengerUser.Controller
{
    partial class AddContactForm
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
            this.AddFriend_btn = new System.Windows.Forms.Button();
            this.frnusername_lbl = new System.Windows.Forms.Label();
            this.friendname_txtbox = new System.Windows.Forms.TextBox();
            this.cancel_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // AddFriend_btn
            // 
            this.AddFriend_btn.Location = new System.Drawing.Point(260, 50);
            this.AddFriend_btn.Name = "AddFriend_btn";
            this.AddFriend_btn.Size = new System.Drawing.Size(81, 21);
            this.AddFriend_btn.TabIndex = 0;
            this.AddFriend_btn.Text = "Send";
            this.AddFriend_btn.UseVisualStyleBackColor = true;
            this.AddFriend_btn.Click += new System.EventHandler(this.AddFriend_btn_Click);
            // 
            // frnusername_lbl
            // 
            this.frnusername_lbl.AutoSize = true;
            this.frnusername_lbl.Location = new System.Drawing.Point(12, 12);
            this.frnusername_lbl.Name = "frnusername_lbl";
            this.frnusername_lbl.Size = new System.Drawing.Size(92, 13);
            this.frnusername_lbl.TabIndex = 1;
            this.frnusername_lbl.Text = "Friend Username:";
            // 
            // friendname_txtbox
            // 
            this.friendname_txtbox.Location = new System.Drawing.Point(110, 12);
            this.friendname_txtbox.Name = "friendname_txtbox";
            this.friendname_txtbox.Size = new System.Drawing.Size(231, 20);
            this.friendname_txtbox.TabIndex = 2;
            // 
            // cancel_btn
            // 
            this.cancel_btn.Location = new System.Drawing.Point(173, 50);
            this.cancel_btn.Name = "cancel_btn";
            this.cancel_btn.Size = new System.Drawing.Size(81, 21);
            this.cancel_btn.TabIndex = 3;
            this.cancel_btn.Text = "Cancel";
            this.cancel_btn.UseVisualStyleBackColor = true;
            this.cancel_btn.Click += new System.EventHandler(this.cancel_btn_Click);
            // 
            // AddContractForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(343, 74);
            this.Controls.Add(this.cancel_btn);
            this.Controls.Add(this.friendname_txtbox);
            this.Controls.Add(this.frnusername_lbl);
            this.Controls.Add(this.AddFriend_btn);
            this.Name = "AddContractForm";
            this.Text = "AddContractForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button AddFriend_btn;
        private System.Windows.Forms.Label frnusername_lbl;
        private System.Windows.Forms.TextBox friendname_txtbox;
        private System.Windows.Forms.Button cancel_btn;
    }
}