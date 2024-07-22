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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using NBitcoin;

namespace CryptoWalletMiner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isMining = false;
        private DispatcherTimer miningTimer;
        private string selectedCrypto = "BTC";
        private decimal totalMined = 0.0m;
        private decimal balance = 0.0m;
        private List<string> minedPhrases = new List<string>();
        private List<string> notMinedPhrases = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
            miningTimer = new DispatcherTimer();
            miningTimer.Interval = TimeSpan.FromSeconds(1);
            miningTimer.Tick += MiningTimer_Tick;

            // Example phrases
            notMinedPhrases.Add("example-not-mined-phrase-1");
            notMinedPhrases.Add("example-not-mined-phrase-2");
            notMinedPhrases.Add("example-not-mined-phrase-3");

            UpdatePhrasesDisplay();
        }

        private void MiningTimer_Tick(object sender, EventArgs e)
        {
            // Simulate mining by incrementing the total mined
            totalMined += 0.001m;
            balance += 0.001m;

            // Simulate mining a new phrase
            if (notMinedPhrases.Count > 0)
            {
                string newMinedPhrase = notMinedPhrases[0];
                notMinedPhrases.RemoveAt(0);
                minedPhrases.Add(newMinedPhrase);
            }

            lblHashRate.Text = "Hash Rate: 1000 H/s"; // Simulated hash rate
            lblTotalMined.Text = $"Total Mined: {totalMined} {selectedCrypto}";
            lblBalance.Text = $"Balance: {balance} {selectedCrypto}";

            UpdatePhrasesDisplay();
        }

        private void btnStartStopMining_Click(object sender, RoutedEventArgs e)
        {
            if (isMining)
            {
                miningTimer.Stop();
                btnStartStopMining.Content = "Start Mining";
                lblStatus.Text = "Mining stopped.";
            }
            else
            {
                miningTimer.Start();
                btnStartStopMining.Content = "Stop Mining";
                lblStatus.Text = "Mining started.";
            }
            isMining = !isMining;
        }

        private void btnWithdraw_Click(object sender, RoutedEventArgs e)
        {
            var withdrawWindow = new WithdrawWindow(balance, selectedCrypto);
            if (withdrawWindow.ShowDialog() == true)
            {
                decimal amount = withdrawWindow.WithdrawAmount;
                string destinationAddress = withdrawWindow.DestinationAddress;
                if (amount <= balance)
                {
                    // Process the withdrawal (simulated)
                    balance -= amount;
                    lblBalance.Text = $"Balance: {balance} {selectedCrypto}";
                    MessageBox.Show($"Withdrawn {amount} {selectedCrypto} to {destinationAddress}", "Withdrawal Successful", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Insufficient balance for withdrawal.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void cmbCrypto_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            selectedCrypto = (cmbCrypto.SelectedItem as System.Windows.Controls.ComboBoxItem)?.Content.ToString();
        }

        private void UpdatePhrasesDisplay()
        {
            lblMinedPhrases.Text = minedPhrases.Count > 0 ? string.Join(", ", minedPhrases) : "No mined phrases yet.";
            lblNotMinedPhrases.Text = notMinedPhrases.Count > 0 ? string.Join(", ", notMinedPhrases) : "No not mined phrases yet.";
        }
    }
}
