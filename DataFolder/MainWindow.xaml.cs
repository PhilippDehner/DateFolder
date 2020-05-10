using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
		}

		private void ButtonStart_Click(object sender, RoutedEventArgs e)
		{
			List<string> files = new List<string>();

		}
	}
}
