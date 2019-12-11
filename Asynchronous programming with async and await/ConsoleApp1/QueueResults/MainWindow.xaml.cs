using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace QueueResults
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        // ***Declare the following variables where all methods can access them.
        private Task pendingWork = null;
        private char group = (char)('A' - 1);


        public MainWindow()
        {
            InitializeComponent();
        }

        private async void startButton_Click(object sender, RoutedEventArgs e)
        {
            // ***Verify that each group's results are displayed together, and that
            // the groups display in order, by marking each group with a letter.
            group = (char)(group + 1);
            resultsTextBox.Text += $"\r\n\r\n#Starting group {group}.";

            try
            {
                // *** Pass the group value to AccessTheWebAsync.
                char finishedGroup = await AccessTheWebAsync(group);

                // The following line verifies a successful return from the download and
                // display procedures.
                resultsTextBox.Text += $"\r\n\r\n#Group {finishedGroup} is complete.\r\n";
            }
            catch (Exception)
            {
                resultsTextBox.Text += "\r\nDownloads failed.";
            }
        }

        private async Task<char> AccessTheWebAsync(char grp)
        {
            HttpClient client = new HttpClient();

            // Make a list of the web addresses to download.
            List<string> urlList = SetUpURLList();

            // ***Kick off the downloads. The application of ToArray activates all the download tasks.
            Task<byte[]>[] getContentTasks = urlList.Select(url => client.GetByteArrayAsync(url)).ToArray();

            // ***Call the method that awaits the downloads and displays the results.
            // Assign the Task that FinishOneGroupAsync returns to the gatekeeper task, pendingWork.
            pendingWork = FinishOneGroupAsync(urlList, getContentTasks, grp);

            resultsTextBox.Text += $"\r\n#Task assigned for group {grp}. Download tasks are active.\r\n";

            // ***This task is complete when a group has finished downloading and displaying.
            await pendingWork;

            // You can do other work here or just return.
            return grp;
        }

        private async Task FinishOneGroupAsync(List<string> urls, Task<byte[]>[] contentTasks, char grp)
        {
            // ***Wait for the previous group to finish displaying results.
            if (pendingWork != null) await pendingWork;

            int total = 0;

            // contentTasks is the array of Tasks that was created in AccessTheWebAsync.
            for (int i = 0; i < contentTasks.Length; i++)
            {
                // Await the download of a particular URL, and then display the URL and
                // its length.
                byte[] content = await contentTasks[i];
                DisplayResults(urls[i], content, i, grp);
                total += content.Length;
            }

            // Display the total count for all of the websites.
           resultsTextBox.Text +=
                $"\r\n\r\nTOTAL bytes returned:  {total}\r\n";
        }
        private List<string> SetUpURLList()
        {
            List<string> urls = new List<string>
            {
                "https://msdn.microsoft.com/library/hh191443.aspx",
                "https://msdn.microsoft.com/library/aa578028.aspx",
                "https://msdn.microsoft.com/library/jj155761.aspx",
                "https://msdn.microsoft.com/library/hh290140.aspx",
                "https://msdn.microsoft.com/library/hh524395.aspx",
                "https://msdn.microsoft.com/library/ms404677.aspx",
                "https://msdn.microsoft.com",
                "https://msdn.microsoft.com/library/ff730837.aspx"
            };
            return urls;
        }
        // ***Add a parameter for the group label.
        private void DisplayResults(string url, byte[] content, int pos, char grp)
        {
            // Display the length of a website. The string format is designed to be 
            // used with a monospaced font, such as Lucida Console or Global Monospace.

            // Strip off the "http://".
            var displayURL = url.Replace("http://", "");
            // Display position in the URL list, the URL, and the number of bytes.
           resultsTextBox.Text += string.Format("\r\n{0}-{1}. {2,-58} {3,8}", grp, pos + 1, displayURL, content.Length);
        }
    }
}
