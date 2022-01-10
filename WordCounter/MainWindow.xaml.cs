namespace WordCounter
{
    using System.Windows;
    using System.Windows.Forms;
    using WordCounter.Lib.ViewModels;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
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
    }
}