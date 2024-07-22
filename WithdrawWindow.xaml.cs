using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CryptoWalletMiner
{
    /// <summary>
    /// Interaction logic for WithdrawWindow.xaml
    /// </summary>
    public partial class WithdrawWindow : Window
    {
        public decimal WithdrawAmount { get; private set; }
        public string DestinationAddress { get; private set; }
        private decimal availableBalance;
        private string cryptoCurrency;

        public WithdrawWindow(decimal balance, string crypto)
        {
            InitializeComponent();
            availableBalance = balance;
            cryptoCurrency = crypto;
        }

        private void btnConfirmWithdraw_Click(object sender, RoutedEventArgs e)
        {
            if (decimal.TryParse(txtAmount.Text, out decimal amount) && !string.IsNullOrWhiteSpace(txtDestinationAddress.Text))
            {
                if (amount > availableBalance)
                {
                    MessageBox.Show("Insufficient balance for withdrawal.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                WithdrawAmount = amount;
                DestinationAddress = txtDestinationAddress.Text;
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Please enter a valid amount and destination address.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
