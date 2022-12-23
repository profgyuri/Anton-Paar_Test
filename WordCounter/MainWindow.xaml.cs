namespace WordCounter
{
    using System.Windows;
    using System.Windows.Forms;
    using WordCounter.Lib.ViewModels;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            using var dialog = new OpenFileDialog();
            dialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            dialog.Title = "Select a file to analyze!";

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var viewModel = (MainWindowViewModel)this.DataContext;
                viewModel.FilePath = dialog.FileName;
            }
        }

        private void PathTextBox_Drop(object sender, System.Windows.DragEventArgs e)
        {
            var data = e.Data as System.Windows.DataObject;

            if (data.ContainsFileDropList())
            {
                var files = data.GetFileDropList();
                var path = files[0];
                var viewModel = (MainWindowViewModel)this.DataContext;
                viewModel.FilePath = path;
            }
        }

        private void PathTextBox_PreviewDragOver(object sender, System.Windows.DragEventArgs e)
        {
            e.Handled = true;
        }
    }
}