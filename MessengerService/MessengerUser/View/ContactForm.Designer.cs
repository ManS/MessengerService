namespace MessengerUser.View
{
    partial class ContactForm
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
            this.accept_btn = new System.Windows.Forms.Button();
            this.reject_btn = new System.Windows.Forms.Button();
            this.username_lbl = new System.Windows.Forms.Label();
            this.lastname_lbl = new System.Windows.Forms.Label();
            this.firstname_lbl = new System.Windows.Forms.Label();
            this.avatar_pb = new System.Windows.Forms.PictureBox();
            this.later_btn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.avatar_pb)).BeginInit();
            this.SuspendLayout();
            // 
            // accept_btn
            // 
            this.accept_btn.Location = new System.Drawing.Point(132, 135);
            this.accept_btn.Name = "accept_btn";
            this.accept_btn.Size = new System.Drawing.Size(75, 23);
            this.accept_btn.TabIndex = 1;
            this.accept_btn.Text = "Accept";
            this.accept_btn.UseVisualStyleBackColor = true;
            this.accept_btn.Click += new System.EventHandler(this.accept_btn_Click);
            // 
            // reject_btn
            // 
            this.reject_btn.Location = new System.Drawing.Point(213, 135);
            this.reject_btn.Name = "reject_btn";
            this.reject_btn.Size = new System.Drawing.Size(75, 23);
            this.reject_btn.TabIndex = 2;
            this.reject_btn.Text = "Reject";
            this.reject_btn.UseVisualStyleBackColor = true;
            this.reject_btn.Click += new System.EventHandler(this.reject_btn_Click);
            // 
            // username_lbl
            // 
            this.username_lbl.AutoSize = true;
            this.username_lbl.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.username_lbl.Location = new System.Drawing.Point(12, 12);
            this.username_lbl.Name = "username_lbl";
            this.username_lbl.Size = new System.Drawing.Size(80, 17);
            this.username_lbl.TabIndex = 3;
            this.username_lbl.Text = "User name: ";
            // 
            // lastname_lbl
            // 
            this.lastname_lbl.AutoSize = true;
            this.lastname_lbl.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lastname_lbl.Location = new System.Drawing.Point(12, 68);
            this.lastname_lbl.Name = "lastname_lbl";
            this.lastname_lbl.Size = new System.Drawing.Size(73, 17);
            this.lastname_lbl.TabIndex = 4;
            this.lastname_lbl.Text = "Last name:";
            // 
            // firstname_lbl
            // 
            this.firstname_lbl.AutoSize = true;
            this.firstname_lbl.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.firstname_lbl.Location = new System.Drawing.Point(12, 42);
            this.firstname_lbl.Name = "firstname_lbl";
            this.firstname_lbl.Size = new System.Drawing.Size(73, 17);
            this.firstname_lbl.TabIndex = 5;
            this.firstname_lbl.Text = "First name:";
            // 
            // avatar_pb
            // 
            this.avatar_pb.Location = new System.Drawing.Point(259, 12);
            this.avatar_pb.Name = "avatar_pb";
            this.avatar_pb.Size = new System.Drawing.Size(110, 110);
            this.avatar_pb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.avatar_pb.TabIndex = 0;
            this.avatar_pb.TabStop = false;
            // 
            // later_btn
            // 
            this.later_btn.Location = new System.Drawing.Point(294, 135);
            this.later_btn.Name = "later_btn";
            this.later_btn.Size = new System.Drawing.Size(75, 23);
            this.later_btn.TabIndex = 6;
            this.later_btn.Text = "Reply Later";
            this.later_btn.UseVisualStyleBackColor = true;
            this.later_btn.Click += new System.EventHandler(this.later_btn_Click);
            // 
            // ContactForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 170);
            this.Controls.Add(this.later_btn);
            this.Controls.Add(this.firstname_lbl);
            this.Controls.Add(this.lastname_lbl);
            this.Controls.Add(this.username_lbl);
            this.Controls.Add(this.reject_btn);
            this.Controls.Add(this.accept_btn);
            this.Controls.Add(this.avatar_pb);
            this.Name = "ContactForm";
            this.Text = "ContactForm";
            ((System.ComponentModel.ISupportInitialize)(this.avatar_pb)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox avatar_pb;
        private System.Windows.Forms.Button accept_btn;
        private System.Windows.Forms.Button reject_btn;
        private System.Windows.Forms.Label username_lbl;
        private System.Windows.Forms.Label lastname_lbl;
        private System.Windows.Forms.Label firstname_lbl;
        private System.Windows.Forms.Button later_btn;
    }
}