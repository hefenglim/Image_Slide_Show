using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Image_SlideShow
{
    public partial class SetDuration : Form
    {
        Form1 m_parent;
        bool parent_topMost;

        public SetDuration(Form1 parent)
        {
            InitializeComponent();
            m_parent = parent;
        }

        public void Show(int second)
        {
            this.Show();
            parent_topMost = m_parent.TopMost;
            m_parent.TopMost = false;
            this.TopMost = true;

            Point pt = new Point();
            pt.X = m_parent.Location.X + (m_parent.Width / 2) - (this.Width / 2);
            pt.Y = m_parent.Location.Y + (m_parent.Height / 2) - (this.Height / 2);
            this.Location = pt;

            textSecond.Text = second.ToString();
            textSecond.Focus();
            textSecond.SelectAll();
        }

        private void textSecond_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57)
            {
                    e.Handled = false;
            }
            else
            {
                if (e.KeyChar == 13)
                {
                    if (Int32.Parse(textSecond.Text) > 0)
                    {
                        m_parent.setDuration(Int32.Parse(textSecond.Text));
                        this.Close();
                    }
                }
                if (e.KeyChar != 8)
                    e.Handled = true;
            }
        }

        private void textSecond_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }

        private void SetDuration_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_parent.TopMost = parent_topMost;
        }

    }
}
