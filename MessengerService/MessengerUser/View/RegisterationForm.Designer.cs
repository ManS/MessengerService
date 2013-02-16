namespace MessengerUser.Controller
{
    partial class RegisterationForm
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
            this.username_txtbox = new System.Windows.Forms.TextBox();
            this.lastname_txtbox = new System.Windows.Forms.TextBox();
            this.firstname_txtbox = new System.Windows.Forms.TextBox();
            this.password_txtbox = new System.Windows.Forms.TextBox();
            this.register_btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.avatar_lbl = new System.Windows.Forms.Label();
            this.avatar_txtbox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // username_txtbox
            // 
            this.username_txtbox.Location = new System.Drawing.Point(190, 24);
            this.username_txtbox.Name = "username_txtbox";
            this.username_txtbox.Size = new System.Drawing.Size(100, 20);
            this.username_txtbox.TabIndex = 0;
            // 
            // lastname_txtbox
            // 
            this.lastname_txtbox.Location = new System.Drawing.Point(190, 102);
            this.lastname_txtbox.Name = "lastname_txtbox";
            this.lastname_txtbox.Size = new System.Drawing.Size(100, 20);
            this.lastname_txtbox.TabIndex = 2;
            // 
            // firstname_txtbox
            // 
            this.firstname_txtbox.Location = new System.Drawing.Point(190, 76);
            this.firstname_txtbox.Name = "firstname_txtbox";
            this.firstname_txtbox.Size = new System.Drawing.Size(100, 20);
            this.firstname_txtbox.TabIndex = 3;
            // 
            // password_txtbox
            // 
            this.password_txtbox.Location = new System.Drawing.Point(190, 50);
            this.password_txtbox.Name = "password_txtbox";
            this.password_txtbox.Size = new System.Drawing.Size(100, 20);
            this.password_txtbox.TabIndex = 4;
            // 
            // register_btn
            // 
            this.register_btn.Location = new System.Drawing.Point(190, 161);
            this.register_btn.Name = "register_btn";
            this.register_btn.Size = new System.Drawing.Size(100, 32);
            this.register_btn.TabIndex = 5;
            this.register_btn.Text = "Register";
            this.register_btn.UseVisualStyleBackColor = true;
            this.register_btn.Click += new System.EventHandler(this.register_btn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "User name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Password:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "First name:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Last name:";
            // 
            // avatar_lbl
            // 
            this.avatar_lbl.AutoSize = true;
            this.avatar_lbl.Location = new System.Drawing.Point(12, 135);
            this.avatar_lbl.Name = "avatar_lbl";
            this.avatar_lbl.Size = new System.Drawing.Size(58, 13);
            this.avatar_lbl.TabIndex = 10;
            this.avatar_lbl.Text = "Avatar ID:";
            // 
            // avatar_txtbox
            // 
            this.avatar_txtbox.Location = new System.Drawing.Point(190, 128);
            this.avatar_txtbox.Name = "avatar_txtbox";
            this.avatar_txtbox.Size = new System.Drawing.Size(100, 20);
            this.avatar_txtbox.TabIndex = 11;
            // 
            // RegisterationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(295, 204);
            this.Controls.Add(this.avatar_txtbox);
            this.Controls.Add(this.avatar_lbl);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.register_btn);
            this.Controls.Add(this.password_txtbox);
            this.Controls.Add(this.firstname_txtbox);
            this.Controls.Add(this.lastname_txtbox);
            this.Controls.Add(this.username_txtbox);
            this.Name = "RegisterationForm";
            this.Text = "RegisterationForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox username_txtbox;
        private System.Windows.Forms.TextBox lastname_txtbox;
        private System.Windows.Forms.TextBox firstname_txtbox;
        private System.Windows.Forms.TextBox password_txtbox;
        private System.Windows.Forms.Button register_btn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label avatar_lbl;
        private System.Windows.Forms.TextBox avatar_txtbox;
    }
}