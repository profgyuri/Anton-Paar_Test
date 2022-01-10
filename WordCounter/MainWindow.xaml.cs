namespace WordCounter
{
    using System.Windows;
    using System.Windows.Forms;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void browseButton_Click(object sender, RoutedEventArgs e)
        {
            using var dialog = new OpenFileDialog();
        }
    }
}