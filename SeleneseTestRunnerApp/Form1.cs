using SeleneseTestRunner.Razor;
using SeleneseTestRunner.Suites;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeleneseTestRunnerApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            cmbBrowser.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSuitePath.Text))
            {
                MessageBox.Show("Select suite file.");
                return;
            }
            if (string.IsNullOrEmpty(txtUrl.Text))
            {
                MessageBox.Show("Enter base url");
                return;
            }

            try
            {
                RunTests();
            }
            catch (Exception ex)
            {
                do
                {
                    Console.WriteLine(ex.Message);
                    ex = ex.InnerException;
                } while (ex != null);
            }
        }

        private void RunTests()
        {
            var suitePath = txtSuitePath.Text;
            var baseUrl = txtUrl.Text;
            var resultsFile = ".\\test-results.html";
            var browserName = cmbBrowser.SelectedItem.ToString();

            var result = SuiteExecutor.Execute(suitePath, baseUrl, browserName);
            var view = new RazorParser().Parse("SuiteResult", result);
            File.WriteAllText(resultsFile, view);

            System.Diagnostics.Process.Start(resultsFile);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var result = fileChooser.ShowDialog();
            if (result != DialogResult.OK)
                return;

            txtSuitePath.Text = fileChooser.FileName;
        }
    }
}
