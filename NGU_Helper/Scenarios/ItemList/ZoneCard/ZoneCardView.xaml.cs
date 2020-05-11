using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NGU_Helper.Scenarios.ItemList.ZoneCard
{
    /// <summary>
    /// Логика взаимодействия для ZoneCardView.xaml
    /// </summary>
    public partial class ZoneCardView : UserControl
    {
        public ZoneCardView()
        {
            InitializeComponent();
            StartedElement.Focus();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Dispatcher.BeginInvoke(new Action(() => textBox.SelectAll()));
        }
    }
}
