namespace MessengerUser.Controller
{
    partial class MessengerForm
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Online");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Offline");
            this.pm_txtbox = new System.Windows.Forms.TextBox();
            this.status_cb = new System.Windows.Forms.ComboBox();
            this.addFriend_btn = new System.Windows.Forms.Button();
            this.status_lbl = new System.Windows.Forms.Label();
            this.profilePic_picbox = new System.Windows.Forms.PictureBox();
            this.contactsTree = new System.Windows.Forms.TreeView();
            ((System.ComponentModel.ISupportInitialize)(this.profilePic_picbox)).BeginInit();
            this.SuspendLayout();
            // 
            // pm_txtbox
            // 
            this.pm_txtbox.Location = new System.Drawing.Point(75, 12);
            this.pm_txtbox.Name = "pm_txtbox";
            this.pm_txtbox.Size = new System.Drawing.Size(205, 20);
            this.pm_txtbox.TabIndex = 3;
            this.pm_txtbox.Text = "What is in your mind?...";
            this.pm_txtbox.Leave += new System.EventHandler(this.pm_txtbox_Leave);
            // 
            // status_cb
            // 
            this.status_cb.FormattingEnabled = true;
            this.status_cb.Items.AddRange(new object[] {
            "Appear Offline",
            "Online",
            "Busy",
            "Away"});
            this.status_cb.Location = new System.Drawing.Point(127, 73);
            this.status_cb.Name = "status_cb";
            this.status_cb.Size = new System.Drawing.Size(153, 21);
            this.status_cb.TabIndex = 3;
            this.status_cb.SelectedIndexChanged += new System.EventHandler(this.status_cb_SelectedIndexChanged);
            // 
            // addFriend_btn
            // 
            this.addFriend_btn.Location = new System.Drawing.Point(219, 38);
            this.addFriend_btn.Name = "addFriend_btn";
            this.addFriend_btn.Size = new System.Drawing.Size(61, 23);
            this.addFriend_btn.TabIndex = 4;
            this.addFriend_btn.Text = "+Friend";
            this.addFriend_btn.UseVisualStyleBackColor = true;
            this.addFriend_btn.Click += new System.EventHandler(this.addFriend_btn_Click);
            // 
            // status_lbl
            // 
            this.status_lbl.AutoSize = true;
            this.status_lbl.Location = new System.Drawing.Point(72, 76);
            this.status_lbl.Name = "status_lbl";
            this.status_lbl.Size = new System.Drawing.Size(38, 13);
            this.status_lbl.TabIndex = 5;
            this.status_lbl.Text = "Status";
            // 
            // profilePic_picbox
            // 
            this.profilePic_picbox.Image = global::MessengerUser.Properties.Resources._10;
            this.profilePic_picbox.Location = new System.Drawing.Point(2, 3);
            this.profilePic_picbox.Name = "profilePic_picbox";
            this.profilePic_picbox.Size = new System.Drawing.Size(67, 64);
            this.profilePic_picbox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.profilePic_picbox.TabIndex = 0;
            this.profilePic_picbox.TabStop = false;
            this.profilePic_picbox.Click += new System.EventHandler(this.profilePic_picbox_Click);
            // 
            // contactsTree
            // 
            this.contactsTree.Location = new System.Drawing.Point(2, 121);
            this.contactsTree.Name = "contactsTree";
            treeNode1.Name = "onlineContacts";
            treeNode1.Text = "Online";
            treeNode2.Name = "offlineContacts";
            treeNode2.Text = "Offline";
            this.contactsTree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            this.contactsTree.Size = new System.Drawing.Size(278, 376);
            this.contactsTree.TabIndex = 6;
            this.contactsTree.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.contactsTree_NodeMouseDoubleClick);
            // 
            // MessengerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 502);
            this.Controls.Add(this.contactsTree);
            this.Controls.Add(this.status_lbl);
            this.Controls.Add(this.addFriend_btn);
            this.Controls.Add(this.status_cb);
            this.Controls.Add(this.pm_txtbox);
            this.Controls.Add(this.profilePic_picbox);
            this.Name = "MessengerForm";
            this.Text = "MessengerForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MessengerForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.profilePic_picbox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox profilePic_picbox;
        private System.Windows.Forms.TextBox pm_txtbox;
        private System.Windows.Forms.ComboBox status_cb;
        private System.Windows.Forms.Button addFriend_btn;
        private System.Windows.Forms.Label status_lbl;
        private System.Windows.Forms.TreeView contactsTree;
    }
}