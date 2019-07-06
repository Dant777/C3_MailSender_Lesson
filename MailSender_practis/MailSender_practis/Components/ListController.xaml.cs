using System;
using System.Collections;
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
  
    public partial class ListController : UserControl
    {
        #region PanelName : string - название панели
        /// <summary> название панели </summary>
        public static readonly DependencyProperty PanelNameProperty=
            DependencyProperty.Register(
                nameof(PanelName),
                typeof(string),
                typeof(ListController),
                new PropertyMetadata(default(string)));

        public string PanelName
        {
            get => (string) GetValue(PanelNameProperty);
            set => SetValue(PanelNameProperty, value);
        }

        #endregion

        #region ItemSoirse : IEnumerable - управляемый список
        public static readonly DependencyProperty ItemSourceProperty =
            DependencyProperty.Register(
                "ItemSource",
                typeof(IEnumerable),
                typeof(ListController),
                new PropertyMetadata(default(IEnumerable)));
        public IEnumerable ItemSource
        {
            get => (IEnumerable)GetValue(ItemSourceProperty);
            set => SetValue(ItemSourceProperty, value);
        }
        #endregion

        #region ItemSoirse : object - Выбранный элемент
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register(
                nameof(SelectedItem),
                typeof(object),
                typeof(ListController),
                new PropertyMetadata(default(object)));
        public object SelectedItem
        {
            get => (IEnumerable)GetValue(ItemSourceProperty);
            set => SetValue(ItemSourceProperty, value);
        }
        #endregion

        public ListController()
        {
            InitializeComponent();
        }
    }
}
