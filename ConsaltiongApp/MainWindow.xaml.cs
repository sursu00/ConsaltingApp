using System.Windows;
using ConsaltiongApp.View;
using ConsaltiongApp.ViewModel;

namespace ConsaltiongApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            Closing += (s, e) => ViewModelLocator.Cleanup();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var addW = new AddQuestionView();
            addW.ShowDialog();
        }
    }
}
