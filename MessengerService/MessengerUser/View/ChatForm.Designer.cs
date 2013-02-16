namespace MessengerUser.Controller
{
    partial class ChatForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChatForm));
            this.send_btn = new System.Windows.Forms.Button();
            this.iswriting_lbl = new System.Windows.Forms.Label();
            this.sendFile_btn = new System.Windows.Forms.Button();
            this.chat_txtbox = new Khendys.Controls.ExRichTextBox();
            this.message_txtbox = new Khendys.Controls.ExRichTextBox();
            this.ThumbsUp1 = new System.Windows.Forms.Button();
            this.DevilSmile1 = new System.Windows.Forms.Button();
            this.EmbarassedSmile1 = new System.Windows.Forms.Button();
            this.ConfusedSmile1 = new System.Windows.Forms.Button();
            this.CrySmile1 = new System.Windows.Forms.Button();
            this.Beer1 = new System.Windows.Forms.Button();
            this.BrokenHeart1 = new System.Windows.Forms.Button();
            this.AngelSmile1 = new System.Windows.Forms.Button();
            this.AngrySmile1 = new System.Windows.Forms.Button();
            this.userAvatar_pb = new System.Windows.Forms.PictureBox();
            this.friendAvatar_pb = new System.Windows.Forms.PictureBox();
            this.friendVideoScreen = new System.Windows.Forms.PictureBox();
            this.userVideoScreen = new System.Windows.Forms.PictureBox();
            this.startVideo_btn = new System.Windows.Forms.Button();
            this.stopVideo_btn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.userAvatar_pb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.friendAvatar_pb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.friendVideoScreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.userVideoScreen)).BeginInit();
            this.SuspendLayout();
            // 
            // send_btn
            // 
            this.send_btn.Location = new System.Drawing.Point(550, 374);
            this.send_btn.Name = "send_btn";
            this.send_btn.Size = new System.Drawing.Size(76, 85);
            this.send_btn.TabIndex = 2;
            this.send_btn.Text = "Send";
            this.send_btn.UseVisualStyleBackColor = true;
            this.send_btn.Click += new System.EventHandler(this.send_btn_Click);
            // 
            // iswriting_lbl
            // 
            this.iswriting_lbl.AutoSize = true;
            this.iswriting_lbl.Location = new System.Drawing.Point(98, 309);
            this.iswriting_lbl.Name = "iswriting_lbl";
            this.iswriting_lbl.Size = new System.Drawing.Size(0, 13);
            this.iswriting_lbl.TabIndex = 6;
            // 
            // sendFile_btn
            // 
            this.sendFile_btn.Location = new System.Drawing.Point(101, 465);
            this.sendFile_btn.Name = "sendFile_btn";
            this.sendFile_btn.Size = new System.Drawing.Size(73, 27);
            this.sendFile_btn.TabIndex = 7;
            this.sendFile_btn.Text = "Send File";
            this.sendFile_btn.UseVisualStyleBackColor = true;
            this.sendFile_btn.Click += new System.EventHandler(this.sendFile_btn_Click);
            // 
            // chat_txtbox
            // 
            this.chat_txtbox.HiglightColor = Khendys.Controls.RtfColor.White;
            this.chat_txtbox.Location = new System.Drawing.Point(101, 12);
            this.chat_txtbox.Name = "chat_txtbox";
            this.chat_txtbox.Size = new System.Drawing.Size(443, 294);
            this.chat_txtbox.TabIndex = 8;
            this.chat_txtbox.Text = "";
            this.chat_txtbox.TextColor = Khendys.Controls.RtfColor.Black;
            // 
            // message_txtbox
            // 
            this.message_txtbox.HiglightColor = Khendys.Controls.RtfColor.White;
            this.message_txtbox.Location = new System.Drawing.Point(101, 374);
            this.message_txtbox.Name = "message_txtbox";
            this.message_txtbox.Size = new System.Drawing.Size(443, 85);
            this.message_txtbox.TabIndex = 9;
            this.message_txtbox.Text = "";
            this.message_txtbox.TextColor = Khendys.Controls.RtfColor.Black;
            this.message_txtbox.TextChanged += new System.EventHandler(this.message_txtbox_TextChanged);
            this.message_txtbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.message_txtbox_KeyPress);
            this.message_txtbox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.message_txtbox_KeyUp);
            // 
            // ThumbsUp1
            // 
            this.ThumbsUp1.Image = global::MessengerUser.Properties.Resources.ThumbsUp1;
            this.ThumbsUp1.Location = new System.Drawing.Point(373, 336);
            this.ThumbsUp1.Name = "ThumbsUp1";
            this.ThumbsUp1.Size = new System.Drawing.Size(30, 32);
            this.ThumbsUp1.TabIndex = 20;
            this.ThumbsUp1.UseVisualStyleBackColor = true;
            this.ThumbsUp1.Click += new System.EventHandler(this.smileybtn_Click);
            // 
            // DevilSmile1
            // 
            this.DevilSmile1.Image = global::MessengerUser.Properties.Resources.DevilSmile1;
            this.DevilSmile1.Location = new System.Drawing.Point(305, 336);
            this.DevilSmile1.Name = "DevilSmile1";
            this.DevilSmile1.Size = new System.Drawing.Size(30, 32);
            this.DevilSmile1.TabIndex = 18;
            this.DevilSmile1.UseVisualStyleBackColor = true;
            this.DevilSmile1.Click += new System.EventHandler(this.smileybtn_Click);
            // 
            // EmbarassedSmile1
            // 
            this.EmbarassedSmile1.Image = global::MessengerUser.Properties.Resources.EmbarassedSmile1;
            this.EmbarassedSmile1.Location = new System.Drawing.Point(337, 336);
            this.EmbarassedSmile1.Name = "EmbarassedSmile1";
            this.EmbarassedSmile1.Size = new System.Drawing.Size(30, 32);
            this.EmbarassedSmile1.TabIndex = 17;
            this.EmbarassedSmile1.UseVisualStyleBackColor = true;
            this.EmbarassedSmile1.Click += new System.EventHandler(this.smileybtn_Click);
            // 
            // ConfusedSmile1
            // 
            this.ConfusedSmile1.Image = global::MessengerUser.Properties.Resources.ConfusedSmile1;
            this.ConfusedSmile1.Location = new System.Drawing.Point(237, 336);
            this.ConfusedSmile1.Name = "ConfusedSmile1";
            this.ConfusedSmile1.Size = new System.Drawing.Size(30, 32);
            this.ConfusedSmile1.TabIndex = 16;
            this.ConfusedSmile1.UseVisualStyleBackColor = true;
            this.ConfusedSmile1.Click += new System.EventHandler(this.smileybtn_Click);
            // 
            // CrySmile1
            // 
            this.CrySmile1.Image = global::MessengerUser.Properties.Resources.CrySmile1;
            this.CrySmile1.Location = new System.Drawing.Point(269, 336);
            this.CrySmile1.Name = "CrySmile1";
            this.CrySmile1.Size = new System.Drawing.Size(30, 32);
            this.CrySmile1.TabIndex = 15;
            this.CrySmile1.UseVisualStyleBackColor = true;
            this.CrySmile1.Click += new System.EventHandler(this.smileybtn_Click);
            // 
            // Beer1
            // 
            this.Beer1.Image = global::MessengerUser.Properties.Resources.Beer1;
            this.Beer1.Location = new System.Drawing.Point(169, 336);
            this.Beer1.Name = "Beer1";
            this.Beer1.Size = new System.Drawing.Size(30, 32);
            this.Beer1.TabIndex = 14;
            this.Beer1.UseVisualStyleBackColor = true;
            this.Beer1.Click += new System.EventHandler(this.smileybtn_Click);
            // 
            // BrokenHeart1
            // 
            this.BrokenHeart1.Image = global::MessengerUser.Properties.Resources.BrokenHeart1;
            this.BrokenHeart1.Location = new System.Drawing.Point(201, 336);
            this.BrokenHeart1.Name = "BrokenHeart1";
            this.BrokenHeart1.Size = new System.Drawing.Size(30, 32);
            this.BrokenHeart1.TabIndex = 13;
            this.BrokenHeart1.UseVisualStyleBackColor = true;
            this.BrokenHeart1.Click += new System.EventHandler(this.smileybtn_Click);
            // 
            // AngelSmile1
            // 
            this.AngelSmile1.Image = ((System.Drawing.Image)(resources.GetObject("AngelSmile1.Image")));
            this.AngelSmile1.Location = new System.Drawing.Point(101, 336);
            this.AngelSmile1.Name = "AngelSmile1";
            this.AngelSmile1.Size = new System.Drawing.Size(30, 32);
            this.AngelSmile1.TabIndex = 12;
            this.AngelSmile1.UseVisualStyleBackColor = true;
            this.AngelSmile1.Click += new System.EventHandler(this.smileybtn_Click);
            // 
            // AngrySmile1
            // 
            this.AngrySmile1.Image = ((System.Drawing.Image)(resources.GetObject("AngrySmile1.Image")));
            this.AngrySmile1.Location = new System.Drawing.Point(133, 336);
            this.AngrySmile1.Name = "AngrySmile1";
            this.AngrySmile1.Size = new System.Drawing.Size(30, 32);
            this.AngrySmile1.TabIndex = 11;
            this.AngrySmile1.UseVisualStyleBackColor = true;
            this.AngrySmile1.Click += new System.EventHandler(this.smileybtn_Click);
            // 
            // userAvatar_pb
            // 
            this.userAvatar_pb.Location = new System.Drawing.Point(10, 374);
            this.userAvatar_pb.Name = "userAvatar_pb";
            this.userAvatar_pb.Size = new System.Drawing.Size(82, 85);
            this.userAvatar_pb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.userAvatar_pb.TabIndex = 5;
            this.userAvatar_pb.TabStop = false;
            // 
            // friendAvatar_pb
            // 
            this.friendAvatar_pb.Location = new System.Drawing.Point(10, 12);
            this.friendAvatar_pb.Name = "friendAvatar_pb";
            this.friendAvatar_pb.Size = new System.Drawing.Size(82, 85);
            this.friendAvatar_pb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.friendAvatar_pb.TabIndex = 4;
            this.friendAvatar_pb.TabStop = false;
            // 
            // friendVideoScreen
            // 
            this.friendVideoScreen.Location = new System.Drawing.Point(647, 12);
            this.friendVideoScreen.Name = "friendVideoScreen";
            this.friendVideoScreen.Size = new System.Drawing.Size(360, 230);
            this.friendVideoScreen.TabIndex = 21;
            this.friendVideoScreen.TabStop = false;
            // 
            // userVideoScreen
            // 
            this.userVideoScreen.Location = new System.Drawing.Point(647, 265);
            this.userVideoScreen.Name = "userVideoScreen";
            this.userVideoScreen.Size = new System.Drawing.Size(360, 230);
            this.userVideoScreen.TabIndex = 22;
            this.userVideoScreen.TabStop = false;
            // 
            // startVideo_btn
            // 
            this.startVideo_btn.Location = new System.Drawing.Point(180, 465);
            this.startVideo_btn.Name = "startVideo_btn";
            this.startVideo_btn.Size = new System.Drawing.Size(155, 27);
            this.startVideo_btn.TabIndex = 23;
            this.startVideo_btn.Text = "Start Video Conversation";
            this.startVideo_btn.UseVisualStyleBackColor = true;
            this.startVideo_btn.Click += new System.EventHandler(this.video_btn_Click);
            // 
            // stopVideo_btn
            // 
            this.stopVideo_btn.Location = new System.Drawing.Point(337, 465);
            this.stopVideo_btn.Name = "stopVideo_btn";
            this.stopVideo_btn.Size = new System.Drawing.Size(155, 27);
            this.stopVideo_btn.TabIndex = 24;
            this.stopVideo_btn.Text = "Stop Video Conversation";
            this.stopVideo_btn.UseVisualStyleBackColor = true;
            this.stopVideo_btn.Click += new System.EventHandler(this.stopVideo_btn_Click);
            // 
            // ChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 507);
            this.Controls.Add(this.stopVideo_btn);
            this.Controls.Add(this.startVideo_btn);
            this.Controls.Add(this.userVideoScreen);
            this.Controls.Add(this.friendVideoScreen);
            this.Controls.Add(this.ThumbsUp1);
            this.Controls.Add(this.DevilSmile1);
            this.Controls.Add(this.EmbarassedSmile1);
            this.Controls.Add(this.ConfusedSmile1);
            this.Controls.Add(this.CrySmile1);
            this.Controls.Add(this.Beer1);
            this.Controls.Add(this.BrokenHeart1);
            this.Controls.Add(this.AngelSmile1);
            this.Controls.Add(this.AngrySmile1);
            this.Controls.Add(this.message_txtbox);
            this.Controls.Add(this.chat_txtbox);
            this.Controls.Add(this.sendFile_btn);
            this.Controls.Add(this.iswriting_lbl);
            this.Controls.Add(this.userAvatar_pb);
            this.Controls.Add(this.friendAvatar_pb);
            this.Controls.Add(this.send_btn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ChatForm";
            this.Text = "ChatForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ChatForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.userAvatar_pb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.friendAvatar_pb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.friendVideoScreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.userVideoScreen)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button send_btn;
        private System.Windows.Forms.PictureBox friendAvatar_pb;
        private System.Windows.Forms.PictureBox userAvatar_pb;
        private System.Windows.Forms.Label iswriting_lbl;
        private System.Windows.Forms.Button sendFile_btn;
        private Khendys.Controls.ExRichTextBox chat_txtbox;
        private Khendys.Controls.ExRichTextBox message_txtbox;
        private System.Windows.Forms.Button AngrySmile1;
        private System.Windows.Forms.Button AngelSmile1;
        private System.Windows.Forms.Button Beer1;
        private System.Windows.Forms.Button BrokenHeart1;
        private System.Windows.Forms.Button ConfusedSmile1;
        private System.Windows.Forms.Button CrySmile1;
        private System.Windows.Forms.Button DevilSmile1;
        private System.Windows.Forms.Button EmbarassedSmile1;
        private System.Windows.Forms.Button ThumbsUp1;
        private System.Windows.Forms.PictureBox friendVideoScreen;
        private System.Windows.Forms.PictureBox userVideoScreen;
        private System.Windows.Forms.Button startVideo_btn;
        private System.Windows.Forms.Button stopVideo_btn;

    }
}