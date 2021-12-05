using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageCrop
{
    public partial class Form1 : Form
    {
        string _inputFolder;
        string _outputFolder;
        string _workingFolder;
        string _outputChild;
        public Form1()
        {
            InitializeComponent();
            linkOutputFolder.Text = "";
            txtResult.Text = "";
        }

        private void btnFolderPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtFolderPath.Text = folderBrowserDialog1.SelectedPath;
                _inputFolder = folderBrowserDialog1.SelectedPath;
            }
        }

        private void btnCutImage_Click(object sender, EventArgs e)
        {
            txtFolderPath.Enabled = false;
            btnCutImage.Enabled = false;
            linkOutputFolder.Text = "";
            txtResult.Text = "Đang cắt ảnh...";

            ThreadPool.QueueUserWorkItem(u =>
            {
                List<string> folderPaths = new List<string>();
                try
                {
                    folderPaths = Directory.GetDirectories(_inputFolder).ToList();
                }
                catch (Exception ex)
                {
                    return;
                }

                _outputFolder = CreateOutputFolder(_inputFolder);

                foreach (string folder in folderPaths)
                {
                    _workingFolder = folder;
                    _outputChild = _outputFolder + "\\" + GetName(folder);
                    if (!Directory.Exists(_outputChild))
                    {
                        Directory.CreateDirectory(_outputChild);
                    }

                    ConvertImage("a");
                    ConvertImage("b");
                    ConvertImage("c");
                }

                SafeInvoke(this, new MethodInvoker(delegate
                {
                    txtFolderPath.Enabled = true;
                    btnCutImage.Enabled = true;
                    linkOutputFolder.Text = _outputFolder;
                    txtResult.Text = "Kết quả:";
                }));
            });
        }

        void SafeInvoke(Control control, MethodInvoker invoker)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(invoker);
            }
            else
            {
                invoker.Invoke();
            }
        }

        string GetName(string folderPath)
        {
            return new DirectoryInfo(folderPath).Name;
        }

        private static Image CropImage(Image img, Rectangle cropArea)
        {
            Bitmap bmpImage = new Bitmap(img);
            return bmpImage.Clone(cropArea, bmpImage.PixelFormat);
        }

        private void ConvertImage(string fileNameWithoutExt)
        {
            string workingFilePath = "";


            foreach (string ext in new string []{ "jpg", "png", "jpge"}){
                string path = _workingFolder + $"\\{fileNameWithoutExt}.{ext}";
                if (File.Exists(path))
                {
                    workingFilePath = path;
                    break;
                }
            }

            if (workingFilePath == "")
            {
                return;
            }

            string destinationPath = _outputChild + $"\\{fileNameWithoutExt}.jpg";


            using (Image image = Image.FromFile(workingFilePath))
            {
                int cutHeigh = image.Height / 33;

                var outImage = CropImage(image, new Rectangle(new Point(0, 0), new Size(image.Width, image.Height - cutHeigh)));
                outImage.Save(destinationPath);
            }
        }

        private string CreateOutputFolder(string input)
        {
            string surfix = "-output";
            int i = 1;
            while (true)
            {
                if (i > 1)
                {
                    surfix = $"-output({i.ToString()})";
                }
                string outputFolder = input + surfix;
                if (!Directory.Exists(outputFolder))
                {
                    Directory.CreateDirectory(outputFolder);
                    return outputFolder;
                }
                i++;
            }
        }

        private void txtFolderPath_TextChanged(object sender, EventArgs e)
        {

        }

        private void outputFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(_outputFolder);
        }
    }
}
