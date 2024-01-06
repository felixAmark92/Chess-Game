using System.IO;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using Path = System.IO.Path;

namespace StockfishPath;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void BrowseBtn_OnClick(object sender, RoutedEventArgs e)
    {
        var fileDialog = new OpenFileDialog();

        if (fileDialog.ShowDialog() != false)
        {
            TextBox.Text = fileDialog.FileName;
        }
        
    }

    private void ConfirmBtn_OnClick(object sender, RoutedEventArgs e)
    {
        var localApp = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        var path = Path.Combine(localApp, "ChessGame");

        Directory.CreateDirectory(path);

        var file = Path.Combine(path, "StockfishPath");
        
        File.WriteAllText(file ,TextBox.Text);
        
        Close();
    }
}