namespace MessengerUser
{
    partial class IMessenger
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
            this.signin_gb = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.status_cb = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.signin_btn = new System.Windows.Forms.Button();
            this.password_txtbox = new System.Windows.Forms.TextBox();
            this.password_lbl = new System.Windows.Forms.Label();
            this.username_txtbox = new System.Windows.Forms.TextBox();
            this.username_lbl = new System.Windows.Forms.Label();
            this.signup_lbl = new System.Windows.Forms.LinkLabel();
            this.donthave = new System.Windows.Forms.Label();
            this.signin_gb.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // signin_gb
            // 
            this.signin_gb.Controls.Add(this.label1);
            this.signin_gb.Controls.Add(this.status_cb);
            this.signin_gb.Controls.Add(this.pictureBox1);
            this.signin_gb.Controls.Add(this.signin_btn);
            this.signin_gb.Controls.Add(this.password_txtbox);
            this.signin_gb.Controls.Add(this.password_lbl);
            this.signin_gb.Controls.Add(this.username_txtbox);
            this.signin_gb.Controls.Add(this.username_lbl);
            this.signin_gb.Location = new System.Drawing.Point(2, 40);
            this.signin_gb.Name = "signin_gb";
            this.signin_gb.Size = new System.Drawing.Size(478, 199);
            this.signin_gb.TabIndex = 0;
            this.signin_gb.TabStop = false;
            this.signin_gb.Text = "Sign In";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(180, 119);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Sign in as:";
            // 
            // status_cb
            // 
            this.status_cb.FormattingEnabled = true;
            this.status_cb.Items.AddRange(new object[] {
            "Appear Offline",
            "Online",
            "Busy",
            "Away"});
            this.status_cb.Location = new System.Drawing.Point(258, 119);
            this.status_cb.Name = "status_cb";
            this.status_cb.Size = new System.Drawing.Size(121, 21);
            this.status_cb.TabIndex = 6;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Image = global::MessengerUser.Properties.Resources._10;
            this.pictureBox1.Location = new System.Drawing.Point(51, 41);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 90);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // signin_btn
            // 
            this.signin_btn.Location = new System.Drawing.Point(365, 146);
            this.signin_btn.Name = "signin_btn";
            this.signin_btn.Size = new System.Drawing.Size(75, 23);
            this.signin_btn.TabIndex = 4;
            this.signin_btn.Text = "Sign In";
            this.signin_btn.UseVisualStyleBackColor = true;
            this.signin_btn.Click += new System.EventHandler(this.signin_btn_Click);
            // 
            // password_txtbox
            // 
            this.password_txtbox.Location = new System.Drawing.Point(258, 81);
            this.password_txtbox.Name = "password_txtbox";
            this.password_txtbox.Size = new System.Drawing.Size(182, 20);
            this.password_txtbox.TabIndex = 3;
            // 
            // password_lbl
            // 
            this.password_lbl.AutoSize = true;
            this.password_lbl.Location = new System.Drawing.Point(180, 81);
            this.password_lbl.Name = "password_lbl";
            this.password_lbl.Size = new System.Drawing.Size(57, 13);
            this.password_lbl.TabIndex = 2;
            this.password_lbl.Text = "Password:";
            // 
            // username_txtbox
            // 
            this.username_txtbox.Location = new System.Drawing.Point(258, 41);
            this.username_txtbox.Name = "username_txtbox";
            this.username_txtbox.Size = new System.Drawing.Size(182, 20);
            this.username_txtbox.TabIndex = 1;
            // 
            // username_lbl
            // 
            this.username_lbl.AutoSize = true;
            this.username_lbl.Location = new System.Drawing.Point(180, 41);
            this.username_lbl.Name = "username_lbl";
            this.username_lbl.Size = new System.Drawing.Size(59, 13);
            this.username_lbl.TabIndex = 0;
            this.username_lbl.Text = "Username:";
            // 
            // signup_lbl
            // 
            this.signup_lbl.AutoSize = true;
            this.signup_lbl.Location = new System.Drawing.Point(296, 242);
            this.signup_lbl.Name = "signup_lbl";
            this.signup_lbl.Size = new System.Drawing.Size(43, 13);
            this.signup_lbl.TabIndex = 1;
            this.signup_lbl.TabStop = true;
            this.signup_lbl.Text = "Sign Up";
            this.signup_lbl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.signup_lbl_LinkClicked);
            // 
            // donthave
            // 
            this.donthave.AutoSize = true;
            this.donthave.Location = new System.Drawing.Point(141, 242);
            this.donthave.Name = "donthave";
            this.donthave.Size = new System.Drawing.Size(149, 13);
            this.donthave.TabIndex = 2;
            this.donthave.Text = "Don\'t have a IMessenger ID? ";
            // 
            // IMessenger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 264);
            this.Controls.Add(this.donthave);
            this.Controls.Add(this.signup_lbl);
            this.Controls.Add(this.signin_gb);
            this.Name = "IMessenger";
            this.Text = "IMessenger";
            this.signin_gb.ResumeLayout(false);
            this.signin_gb.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox signin_gb;
        private System.Windows.Forms.Label username_lbl;
        private System.Windows.Forms.TextBox password_txtbox;
        private System.Windows.Forms.Label password_lbl;
        private System.Windows.Forms.TextBox username_txtbox;
        private System.Windows.Forms.Button signin_btn;
        private System.Windows.Forms.LinkLabel signup_lbl;
        private System.Windows.Forms.Label donthave;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox status_cb;

    }
}

