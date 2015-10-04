using System.Windows;

namespace ConsaltiongApp.View
{
    /// <summary>
    /// Description for ProtokolView.
    /// </summary>
    public partial class ProtokolView : Window
    {
        /// <summary>
        /// Initializes a new instance of the ProtokolView class.
        /// </summary>
        public ProtokolView()
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