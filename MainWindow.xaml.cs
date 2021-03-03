using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FileSizeSpelunker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            // Get the location of the files we're iterating over
            var path = txtFilePath.Text.Trim();

            if (!Directory.Exists(path))
            {
                MessageBox.Show("Directory does not exist");
                return;
            }


            DirectoryInfo d = new DirectoryInfo(path);

            // List to store the file sizes found in loop below
            List<long> fileSizes = new List<long>();
            long totalSizeOfAllFiles = 0;

            var extension = txtFileExtension.Text.Trim();


            // Check if the extension field is empty.  If it is, search all files
            if (!String.IsNullOrEmpty(extension))
            {
                if (extension.Substring(0,1) != ".")
                {
                    MessageBox.Show("File extension should start w/ a period - .");
                    return;
                }

                extension = "*" + extension;
            }
            else // If nothing is put in the extension, search all files
            {
                extension = "*";
            }


            foreach (var file in d.GetFiles(extension))
            {
                fileSizes.Add(file.Length);
                totalSizeOfAllFiles += file.Length;
            }

            if (fileSizes.Count == 0)
            {
                MessageBox.Show("No files found.");
                return;
            }

            var averageSize = totalSizeOfAllFiles / fileSizes.Count;
            txtAverageFileSize.Text = averageSize.ToString("N0");
        }
    }
}
