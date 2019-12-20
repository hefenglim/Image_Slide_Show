namespace PayPalDonate
{
    public static class Donate
    {
        public static void ShowDialog(string businessEmail,
                                      string donationDescription,
                                      string windowCaption,
                                      string currencyCode,
                                      bool showAsDialog)
        {
            var showForm = new FrmDonate
            {
                BusinessEmail = businessEmail,
                DonationDescription = donationDescription,
                DonationWindowCaption = windowCaption,
                CurrencyCode = currencyCode
            };

            if (showAsDialog)
            {
                showForm.ShowDialog();
            }
            else
            {
                showForm.Show();
            }
        }
    }
}