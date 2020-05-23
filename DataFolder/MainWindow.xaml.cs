using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
			TrennzeichenGeaendert();
			CopyMove_Move.IsChecked = true;
			DatumGeaendert.IsChecked = true;
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
			ButtonStart.IsEnabled = false;
			Fortschritt.Value = 0;

			string[] files = new string[0];
			if (CheckSubFolder.IsChecked ?? false)
				files = Directory.GetFiles(filepathSource, "*", SearchOption.AllDirectories);
			else
				files = Directory.GetFiles(filepathSource, "*", SearchOption.TopDirectoryOnly);
			Fortschritt.Maximum = files.Length;

			foreach (string file in files)
			{
				DateTime dt = new DateTime();
				if(DatumGeaendert.IsChecked ?? false)
					dt = File.GetLastWriteTime(file);
				if (DatumCreated.IsChecked ?? false)
					dt = File.GetCreationTime(file);

				string year = dt.Year.ToString();
				string month = dt.ToString("MM", System.Globalization.DateTimeFormatInfo.InvariantInfo);
				string day = dt.ToString("dd", System.Globalization.DateTimeFormatInfo.InvariantInfo);
				string tz = InputTrennzeichen.Text;
				string newFolder = filepathDestination + "\\" + year + "\\" + year + tz + month + "\\" + year + tz + month + tz + day;
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
				Fortschritt.Value++;
			}
			Process.Start(filepathDestination);
			ButtonStart.IsEnabled = true;
		}

		private void StartPossible()
		{
			ButtonStart.IsEnabled = filepathDestination != "" && filepathDestination != null && filepathSource != "" && filepathSource != null;
		}

		private void InputTrennzeichen_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
		{
			if (App.Current.MainWindow.IsInitialized)
			{
				TrennzeichenGeaendert();
			}
		}

		void TrennzeichenGeaendert()
		{
			string tz = InputTrennzeichen.Text;
			Variante1.Content = "Variante 1: 3 Ebenen (Stufe 1: JJJJ | Stufe 2: JJJJ" + tz + "MM | Stufe 3: JJJJ" + tz + "MM" + tz + "TT)";
		}
	}
}
