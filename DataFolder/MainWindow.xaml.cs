using System;
using System.IO;
using System.Windows;
using System.Windows.Forms;

namespace DataFolder
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		//enum Dateieigenschaft

		string filepathSource;
		string filepathDestination;
		public MainWindow()
		{
			InitializeComponent();
			StartPossible();
			CopyMove_Move.IsChecked = true;
		}

		private void ButtonFolderSource_Click(object sender, RoutedEventArgs e)
		{
			FolderBrowserDialog folderDlg = new FolderBrowserDialog();
			folderDlg.SelectedPath = filepathSource;
			DialogResult result = folderDlg.ShowDialog();
			if (result == System.Windows.Forms.DialogResult.OK)
			{
				filepathSource = folderDlg.SelectedPath;
				InputFolderSource.Text = filepathSource;
			}
			StartPossible();
		}

		private void ButtonFolderDestination_Click(object sender, RoutedEventArgs e)
		{
			FolderBrowserDialog folderDlg = new FolderBrowserDialog();
			DialogResult result = folderDlg.ShowDialog();
			if (result == System.Windows.Forms.DialogResult.OK)
			{
				filepathDestination = folderDlg.SelectedPath;
				InputFolderDestination.Text = filepathDestination;
			}
			StartPossible();
		}

		private void ButtonStart_Click(object sender, RoutedEventArgs e)
		{
			string[] files = new string[0];
			if (CheckSubFolder.IsChecked ?? false)
				files = Directory.GetFiles(filepathSource, "*", SearchOption.AllDirectories);
			else
				files = Directory.GetFiles(filepathSource, "*", SearchOption.TopDirectoryOnly);

			foreach(string file in files)
			{
				DateTime dt = File.GetLastWriteTime(file);
				string year = dt.Year.ToString();
				string month = dt.ToString("MM", System.Globalization.DateTimeFormatInfo.InvariantInfo);
				string day = dt.ToString("dd", System.Globalization.DateTimeFormatInfo.InvariantInfo);
				string newFolder = filepathDestination + "\\" + year + "\\" + year + "-" + month + "\\" + year + "-" + month + "-" + day;
				Directory.CreateDirectory(newFolder);
				try
				{
					if (CopyMove_Move.IsChecked ?? false)
						File.Move(file, newFolder + "\\" + Path.GetFileName(file));
					else
						File.Copy(file, newFolder + "\\" + Path.GetFileName(file));
				}
				catch(Exception ex)
				{
					System.Windows.Forms.MessageBox.Show(ex.ToString());
				}
			}
		}

		private void StartPossible()
		{
			ButtonStart.IsEnabled = filepathDestination != "" && filepathDestination != null && filepathSource != "" && filepathSource != null;
		}
	}
}
