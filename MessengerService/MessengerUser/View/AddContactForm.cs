using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MessengerUser.Controller
{
    public partial class AddContactForm : Form
    {
        public string contactName = "";
        public bool addAccept = false;
       
        public AddContactForm()
        {
            InitializeComponent();
        }

        private void AddFriend_btn_Click(object sender, EventArgs e)
        {
            contactName = friendname_txtbox.Text;
            this.addAccept = true;
            this.Close();
        }

        private void cancel_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
