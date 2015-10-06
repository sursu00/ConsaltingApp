using System.Windows;

namespace ConsaltiongApp.View
{
    public partial class AddQuestionView : Window
    {
        public AddQuestionView()
        {
            InitializeComponent();
            LoginButton.Click += (sender, e) => { Close(); };
            CloseButton.Click += (sender, e) => { Close(); };
        }
    }
}