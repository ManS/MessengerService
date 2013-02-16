using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MessengerUser.Controller
{
    public partial class AvatarsForm : Form
    {
        public int selectedavatarID;
        public int currID;
        public AvatarsForm(Image currentImage, int avatarID)
        {
            InitializeComponent();
            selected_avatar.Image = currentImage;
            this.selectedavatarID = avatarID;
        }
        private void ok_btn_Click(object sender, EventArgs e)
        {
            
            if (currID != selectedavatarID)
            {
                selectedavatarID = currID;
            }
            this.Close();
        }

        private void close_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void picture_Click(object sender, EventArgs e)
        {
            selected_avatar.Image = ((PictureBox)sender).Image;
            currID = int.Parse(((PictureBox)sender).Name.Split('_')[1]);

        }
    }
}
