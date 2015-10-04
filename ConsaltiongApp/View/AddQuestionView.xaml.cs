using System.Windows;

namespace ConsaltiongApp.View
{
    /// <summary>
    /// Description for AddQuestionView.
    /// </summary>
    public partial class AddQuestionView : Window
    {
        /// <summary>
        /// Initializes a new instance of the AddQuestionView class.
        /// </summary>
        public AddQuestionView()
        {
            InitializeComponent();
            AddSaveButton.Click += new RoutedEventHandler(AddSaveButton_Click);
        }

        void AddSaveButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}