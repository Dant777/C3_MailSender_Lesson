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
using MailSender_practis.Components;

namespace MailSender_practis
{ 
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TabConroler_LeftButtonClick(object Sender, EventArgs e)
        {
            if (!(Sender is TabConroler tab_controller)) return;
            if (tab_controller.IsLeftButtonVisible)
                 MainTabControle.SelectedIndex--;
            tab_controller.IsLeftButtonVisible = MainTabControle.SelectedIndex > 0;
            tab_controller.IsRightButtonVisible = MainTabControle.SelectedIndex < MainTabControle.Items.Count;
        }

        private void TabConroler_RightButtonClick(object Sender, EventArgs e)
        {
            if (!(Sender is TabConroler tab_controller)) return;
            if (tab_controller.IsRightButtonVisible)
                MainTabControle.SelectedIndex++;

            tab_controller.IsLeftButtonVisible = MainTabControle.SelectedIndex > 0;
            tab_controller.IsRightButtonVisible = MainTabControle.SelectedIndex < MainTabControle.Items.Count - 1;

        }
    }
}
