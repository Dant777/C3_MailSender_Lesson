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

namespace MailSender_practis.Components
{
    /// <summary>
    /// Логика взаимодействия для TabConroler.xaml
    /// </summary>
    public partial class TabConroler : UserControl
    {
        public TabConroler() =>InitializeComponent();

        public bool IsLeftButtonVisible
        {
            get => MoveLeft.Visibility == Visibility.Visible;
            set => MoveLeft.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
        }
        public bool IsRightButtonVisible
        {
            get => MoveRight.Visibility == Visibility.Visible;
            set => MoveRight.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
        }
        public event EventHandler LeftButtonClick;
        public event EventHandler RightButtonClick;
        public void OnButtonClick(object Sender, RoutedEventArgs E)
        {
            if (!(Sender is Button button)) return;
            switch (button.Name)
            {
                case "MoveLeft":
                    LeftButtonClick?.Invoke(this, EventArgs.Empty);
                    break;
                case "MoveRight":
                    RightButtonClick?.Invoke(this, EventArgs.Empty);
                    break;
                
            }
        }
    }
}
