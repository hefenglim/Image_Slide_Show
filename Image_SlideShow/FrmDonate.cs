using System;
using System.Windows.Forms;

namespace PayPalDonate
{
    public partial class FrmDonate : Form
    {
        public FrmDonate()
        {
            InitializeComponent();
        }

        private void RadioChecked(object sender, EventArgs e)
        {
            var radiobutton = sender as RadioButton;
            if (radiobutton != null)
            {
                if (sender is RadioButton && radiobutton.Text == "Other")
                {
                    DonateValue = "check";
                    return;
                }

                if (sender is RadioButton)
                {
                    DonateValue = radiobutton.Text;
                }
            }
        }
        
        public string BusinessEmail { get; set; }
        public string DonationDescription { get; set; }
        public string DonationWindowCaption { get; set; }
        public string CurrencyCode { get; set; }
        public string DonateValue { get; set; }

        private void FrmDonate_Load(object sender, EventArgs e)
        {
            Text = DonationWindowCaption;
        }

        private void BtnDonate_Click(object sender, EventArgs e)
        {
            if (DonateValue == "check")
            {
                if (string.IsNullOrEmpty(TxtOther.Text))
                {
                    MessageBox.Show("No Amount Specified", "Please specify an amount", MessageBoxButtons.OK,
                               MessageBoxIcon.Information);
                    return;
                }

                DonateValue = TxtOther.Text;
            }
            if (string.IsNullOrEmpty(DonateValue) && string.IsNullOrEmpty(TxtOther.Text))
            {
                MessageBox.Show("No Amount Specified", "Please specify an amount", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                return;
            }
            
            var donateUrl = "https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business={0}&lc={1}&item_name={2}&currency_code={3}&bn=PP%2dDonationsBF&amount={4}";
            donateUrl = string.Format(donateUrl, BusinessEmail, "US", DonationDescription, CurrencyCode, DonateValue);
            //Clipboard.SetText(donateUrl);
            System.Diagnostics.Process.Start(donateUrl);
            Dispose();
        }
    }
}
