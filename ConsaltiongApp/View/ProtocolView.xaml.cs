using System.Windows;

namespace ConsaltiongApp.View
{
    public partial class ProtocolView : Window
    {
        public ProtocolView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.PrintDialog dialog = new System.Windows.Controls.PrintDialog();
            if (dialog.ShowDialog() == true)
            {
                dialog.PrintVisual(PrintCanvas, "На печать");
            }
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}