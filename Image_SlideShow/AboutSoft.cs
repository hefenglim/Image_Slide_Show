using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

using PayPalDonate;

namespace Image_SlideShow
{
    partial class AboutSoft : Form
    {
        Form1 parent;

        public AboutSoft(Form1 param_parant)
        {
            InitializeComponent();
            this.Text = String.Format("About {0}", AssemblyProduct);
            this.labelProductName.Text = AssemblyProduct;
            this.labelVersion.Text = String.Format("Version {0}", AssemblyVersion);
            this.labelCopyright.Text = AssemblyCopyright;
            //this.labelCompanyName.Text = AssemblyCompany;
            //this.textBoxDescription.Text = AssemblyDescription;
            parent = param_parant;
        }

        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

        private void okButton_Click(object sender, EventArgs e)
        {
            Donate.ShowDialog("kevin_lcs2@hotmail.com", "Image Slideshow Donation", "Image Slideshow Donation", "USD", true);
        }

        private void labelCompanyName_Click(object sender, EventArgs e)
        {
            String url = "mailto:kevin_lcs2@hotmail.com";
            labelCompanyName.ForeColor = Color.DarkBlue;
            System.Diagnostics.Process.Start(url);
        }

        private void logoPictureBox_Click(object sender, EventArgs e)
        {
            String url = "http://goo.gl/Apbxg";
            System.Diagnostics.Process.Start(url);
        }

        private void textBoxDescription_MouseMove(object sender, MouseEventArgs e)
        {
            if (textBoxDescription.Focused == false)
            {
                textBoxDescription.Focus();
                textBoxDescription.Select(0, 0);
            }
        }

        private void AboutSoft_FormClosed(object sender, FormClosedEventArgs e)
        {
            parent.clearAboutForm();
        }

    }
}
