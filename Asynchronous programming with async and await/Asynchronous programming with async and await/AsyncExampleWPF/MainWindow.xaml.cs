using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
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

namespace AsyncExampleWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        // *** Declare a System.Threading.CancellationTokenSource.
        CancellationTokenSource cts;



        public MainWindow()
        {
            InitializeComponent();
        }

        //private void startButton_Click(object sender, RoutedEventArgs e)
        //{
        //    resultsTextBox.Clear();
        //    SumPageSizes();
        //    resultsTextBox.Text += "\r\nControl returned to startButton_Click.";
        //}

        //private async void startButton_Click(object sender, RoutedEventArgs e)
        //{
        //    // Disable the button until the operation is complete.
        //    startButton.IsEnabled = false;

        //    resultsTextBox.Clear();

        //    // One-step async call.
        //    await SumPageSizesAsync();

        //    // Two-step async call.
        //    //Task sumTask = SumPageSizesAsync();
        //    //await sumTask;

        //    resultsTextBox.Text += "\r\nControl returned to startButton_Click.\r\n";

        //    // Reenable the button in case you want to run the operation again.
        //    startButton.IsEnabled = true;
        //}

        private async void startButton_Click(object sender, RoutedEventArgs e)
        {
            // This line is commented out to make the results clearer in the output.
            //ResultsTextBox.Clear();

            // *** If a download process is already underway, cancel it.
            if (cts != null)
            {
                cts.Cancel();
            }

            // *** Now set cts to cancel the current process if the button is chosen again.
            CancellationTokenSource newCTS = new CancellationTokenSource();
            cts = newCTS;

            try
            {
                // ***Send cts.Token to carry the message if there is a cancellation request.
                await AccessTheWebAsync(cts.Token);

            }
            // *** Catch cancellations separately.
            catch (OperationCanceledException)
            {
                resultsTextBox.Text += "\r\nDownloads canceled.\r\n";
            }
            catch (Exception)
            {
                resultsTextBox.Text += "\r\nDownloads failed.\r\n";
            }
            // *** When the process is complete, signal that another process can proceed.
            if (cts == newCTS)
                cts = null;
        }


        // *** Provide a parameter for the CancellationToken from StartButton_Click.
        private async Task AccessTheWebAsync(CancellationToken ct)
        {
            // Declare an HttpClient object.
            HttpClient client = new HttpClient();

            // Make a list of web addresses.
            List<string> urlList = SetUpURLList();

            var total = 0;
            var position = 0;

            foreach (var url in urlList)
            {
                // *** Use the HttpClient.GetAsync method because it accepts a
                // cancellation token.
                HttpResponseMessage response = await client.GetAsync(url, ct);

                // *** Retrieve the website contents from the HttpResponseMessage.
                byte[] urlContents = await response.Content.ReadAsByteArrayAsync();

                // *** Check for cancellations before displaying information about the
                // latest site.
                ct.ThrowIfCancellationRequested();

                DisplayResults(url, urlContents, ++position);

                // Update the total.
                total += urlContents.Length;
            }

            // Display the total count for all of the websites.
            resultsTextBox.Text +=
                $"\r\n\r\nTOTAL bytes returned:  {total}\r\n";
        }

        private void SumPageSizes()
        {
            // Make a list of web addresses.
            List<string> urlList = SetUpURLList();

            var total = 0;
            foreach (var url in urlList)
            {
                // GetURLContents returns the contents of url as a byte array.
                byte[] urlContents = GetURLContents(url);

                DisplayResults(url, urlContents);

                // Update the total.
                total += urlContents.Length;
            }

            // Display the total count for all of the web addresses.
            resultsTextBox.Text += $"\r\n\r\nTotal bytes returned:  {total}\r\n";
        }
        private async Task SumPageSizesAsync()
        {
            // Make a list of web addresses.
            List<string> urlList = SetUpURLList();

            var total = 0;
            foreach (var url in urlList)
            {
                // GetURLContents returns the contents of url as a byte array.
                byte[] urlContents = await GetURLContentsAsync(url);

                DisplayResults(url, urlContents);

                // Update the total.
                total += urlContents.Length;
            }

            // Display the total count for all of the web addresses.
            resultsTextBox.Text += $"\r\n\r\nTotal bytes returned:  {total}\r\n";
        }
        private List<string> SetUpURLList()
        {
            var urls = new List<string>
    {
        "https://msdn.microsoft.com/library/windows/apps/br211380.aspx",
        //"https://msdn.microsoft.com",
        //"https://msdn.microsoft.com/library/hh290136.aspx",
        //"https://msdn.microsoft.com/library/ee256749.aspx",
        //"https://msdn.microsoft.com/library/hh290138.aspx",
        //"https://msdn.microsoft.com/library/hh290140.aspx",
        //"https://msdn.microsoft.com/library/dd470362.aspx",
        //"https://msdn.microsoft.com/library/aa578028.aspx",
        //"https://msdn.microsoft.com/library/ms404677.aspx",
        "https://msdn.microsoft.com/library/ff730837.aspx"
    };
            return urls;
        }

        private byte[] GetURLContents(string url)
        {
            // The downloaded resource ends up in the variable named content.
            var content = new MemoryStream();

            // Initialize an HttpWebRequest for the current URL.
            var webReq = (HttpWebRequest)WebRequest.Create(url);

            // Send the request to the Internet resource and wait for
            // the response.
            // Note: you can't use HttpWebRequest.GetResponse in a Windows Store app.
            using (WebResponse response = webReq.GetResponse())
            {
                // Get the data stream that is associated with the specified URL.
                using (Stream responseStream = response.GetResponseStream())
                {
                    // Read the bytes in responseStream and copy them to content.
                    responseStream.CopyTo(content);
                }
            }

            // Return the result as a byte array.
            return content.ToArray();
        }


        private async Task<byte[]> /*byte[]*/ GetURLContentsAsync(string url)
        {
            // The downloaded resource ends up in the variable named content.
            var content = new MemoryStream();

            // Initialize an HttpWebRequest for the current URL.
            var webReq = (HttpWebRequest)WebRequest.Create(url);

            // Send the request to the Internet resource and wait for
            // the response.
            // Note: you can't use HttpWebRequest.GetResponse in a Windows Store app.
            //using (WebResponse response = webReq.GetResponse())
            using (WebResponse response = await webReq.GetResponseAsync())
            {
                // Get the data stream that is associated with the specified URL.
                using (Stream responseStream = response.GetResponseStream())
                {
                    // Read the bytes in responseStream and copy them to content.
                    //responseStream.CopyTo(content);
                    await responseStream.CopyToAsync(content);
                }
            }

            // Return the result as a byte array.
            return content.ToArray();
        }
        private void DisplayResults(string url, byte[] content)
        {
            // Display the length of each website. The string format
            // is designed to be used with a monospaced font, such as
            // Lucida Console or Global Monospace.
            var bytes = content.Length;
            // Strip off the "https://".
            var displayURL = url.Replace("https://", "");
            resultsTextBox.Text += $"\n{displayURL,-58} {bytes,8}";
        }
        private void DisplayResults(string url, byte[] content, int pos)
        {
            // Display the length of each website. The string format is designed
            // to be used with a monospaced font, such as Lucida Console or
            // Global Monospace.

            // Strip off the "https://".
            var displayURL = url.Replace("https://", "");
            // Display position in the URL list, the URL, and the number of bytes.
            resultsTextBox.Text += $"\n{pos}. {displayURL,-58} {content.Length,8}";
        }
    }
}
