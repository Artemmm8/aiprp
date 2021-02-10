using System;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;

namespace bondarchuk_zhukovskyLab1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void DoAction()
        {
            string text = textBox1.Text, path = @"source_html";
            if (string.IsNullOrEmpty(text))
            {
                MessageBox.Show("nothing entered!");
                return;
            }
            char[] separators = { ' ', ',' };
            string[] wordsToSearch = text.Split(separators);
            List<string> listOfFiles = new List<string>();
            List<string> listOfWords = new List<string>();
            listOfWords.Add(wordsToSearch[0]);
            for (int i = 1; i < wordsToSearch.Length; i++)
            {
                for (int j = 0; j < listOfWords.Count; j++)
                {
                    if (listOfWords[j] != wordsToSearch[i])
                    {
                        listOfWords.Add(wordsToSearch[j]);
                    }
                }
            }

            ProcessStartInfo process = new ProcessStartInfo
            {
                FileName = @"C:\Program Files\Google\Chrome\Application\chrome.exe"
            };
            int counter, max = 0;

            if (Directory.Exists(path))
            {
                string[] filesFounded = Directory.GetFiles(path);
                try
                {
                    foreach (var file in filesFounded)
                    {
                        counter = 0;
                        using (StreamReader sr = new StreamReader(file))
                        {
                            string[] textFromFile = sr.ReadToEnd().Split(separators);
                            for (int i = 0; i < textFromFile.Length; i++)
                            {
                                for (int j = 0; j < listOfWords.Count; j++)
                                {
                                    if (string.Compare(textFromFile[i], listOfWords[j]) == 0)
                                    {
                                        counter++;
                                    }
                                }
                            }

                            if (counter > max)
                            {
                                max = counter;
                                listOfFiles.Clear();
                                listOfFiles.Add(file);
                            }
                            else if (counter == max)
                            {
                                listOfFiles.Add(file);
                            }
                        }
                    }

                    foreach (var file in listOfFiles)
                    {
                        process.Arguments = file;
                        Process.Start(process);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Search_Click(object sender, EventArgs e)
        {
            this.DoAction();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
