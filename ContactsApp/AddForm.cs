﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContactsApp
{
    public partial class AddForm : Form
    {
        private static readonly Color BlueColor = Color.FromArgb(27, 93, 198);
        private static readonly Color WhiteColor = Color.FromArgb(255, 255, 255);
        private static readonly Color BlackColor = Color.FromArgb(35, 35, 35);

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TelephoneNumber { get; set; }

        public AddForm()
        {
            InitializeComponent();
        }

        private void Add_Form_Load(object sender, EventArgs e)
        {
            btnAdd.Enabled = false;
            btnAdd.BackColor = BlackColor;
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.FlatAppearance.BorderSize = 0;

            btnCancel.BackColor = BlackColor;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.FlatAppearance.BorderSize = 0;

            this.BackColor = BlackColor;
            this.ForeColor = BlueColor;
            groupBox1.ForeColor = BlueColor;
        }

        private void btn_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
            base.OnMouseEnter(e);
        }

        private void btn_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
            base.OnMouseLeave(e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FirstName = txtFirstName.Text.Trim();
            LastName = txtLastName.Text.Trim();
            TelephoneNumber = txtNumber.Text.Trim();
            DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void txtFirstName_Leave(object sender, EventArgs e)
        {
            if (!txtFirstName.Text.Trim().Equals(""))
            {
                var sb = new StringBuilder();
                txtFirstName.Text = sb.Append(char.ToUpper(txtFirstName.Text[0]))
                    .Append(txtFirstName.Text.Substring(1)).ToString();
            }
        }

        private void txtFirstName_Validating(object sender, CancelEventArgs e)
        {
            if (txtFirstName.Text.Trim().Equals(""))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFirstName, "You must enter a first name!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.Clear();
            }
        }

        private void txtLastName_Leave(object sender, EventArgs e)
        {
            if (!txtLastName.Text.Trim().Equals(""))
            {
                var sb = new StringBuilder();
                txtLastName.Text = sb.Append(char.ToUpper(txtLastName.Text[0]))
                    .Append(txtLastName.Text.Substring(1)).ToString();
            }
        }

        private void txtLastName_Validating(object sender, CancelEventArgs e)
        {
            if (txtLastName.Text.Trim().Equals(""))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtLastName, "You must enter a last name!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.Clear();
            }
        }

        private void txtNumber_Validating(object sender, CancelEventArgs e)
        {
            if (txtNumber.Text.Trim().Equals(""))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNumber, "You must enter a telephone number!");
            }
            else if (!Regex.IsMatch(txtNumber.Text.Trim(),
                @"^07[0-35-9]\s[0-9]{3}\s[0-9]{3}$", RegexOptions.IgnoreCase))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNumber, "Invalid Format (07X YYY ZZZ)");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.Clear();
            }
        }

        private void txt_KeyUp(object sender, KeyEventArgs e)
        {
            if (!txtFirstName.Text.Trim().Equals("") &&
                !txtLastName.Text.Trim().Equals("") &&
                !txtNumber.Text.Trim().Equals("") &&
                Regex.IsMatch(txtNumber.Text.Trim(),
                    @"^07[0-35-9]\s[0-9]{3}\s[0-9]{3}$", RegexOptions.IgnoreCase))
            {
                btnAdd.Enabled = true;
                errorProvider1.Clear();
            }
            else
            {
                btnAdd.Enabled = false;
            }
        }

        private void AddForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to discard the new contact?",
                    "Discard the contact",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                e.Cancel = false;
            else
                e.Cancel = true;
        }
    }
}