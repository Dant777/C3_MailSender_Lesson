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
            get => (IEnumerable)GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }
        #endregion

        #region CreateCommand : ICommand - Команда - создать новый элемент 
        public static readonly DependencyProperty CreateCommandProperty =
            DependencyProperty.Register(
                nameof(CreateCommand),
                typeof(ICommand),
                typeof(ListController),
                new PropertyMetadata(default(ICommand)));
        public ICommand CreateCommand
        {
            get => (ICommand)GetValue(CreateCommandProperty);
            set => SetValue(CreateCommandProperty, value);
        }
        #endregion

        #region EditCommand : ICommand - Команда - Редактированть элемент 
        public static readonly DependencyProperty EditCommandProperty =
            DependencyProperty.Register(
                nameof(EditCommand),
                typeof(ICommand),
                typeof(ListController),
                new PropertyMetadata(default(ICommand)));
        public ICommand EditCommand
        {
            get => (ICommand)GetValue(EditCommandProperty);
            set => SetValue(EditCommandProperty, value);
        }
        #endregion

        #region RemoveCommand : ICommand - Команда - Удалить элемент 
        public static readonly DependencyProperty RemoveCommandProperty =
            DependencyProperty.Register(
                nameof(RemoveCommand),
                typeof(ICommand),
                typeof(ListController),
                new PropertyMetadata(default(ICommand)));
        public ICommand RemoveCommand
        {
            get => (ICommand)GetValue(RemoveCommandProperty);
            set => SetValue(RemoveCommandProperty, value);
        }
        #endregion

        #region SelectedIndex : int - Номер выбранного элемента 
        public static readonly DependencyProperty SelectedIndexProperty =
            DependencyProperty.Register(
                nameof(SelectedIndex),
                typeof(int),
                typeof(ListController),
                new PropertyMetadata(default(int)));
        public int SelectedIndex
        {
            get => (int)GetValue(SelectedIndexProperty);
            set => SetValue(SelectedIndexProperty, value);
        }
        #endregion

        #region ItemTemplete : DataTemplate - Шаблон визуализации
        /// <summary> название панели </summary>
        public static readonly DependencyProperty ItemTempleteProperty =
            DependencyProperty.Register(
                nameof(ItemTemplete),
                typeof(DataTemplate),
                typeof(ListController),
                new PropertyMetadata(default(DataTemplate)));

        public DataTemplate ItemTemplete
        {
            get => (DataTemplate)GetValue(ItemTempleteProperty);
            set => SetValue(ItemTempleteProperty, value);
        }

        #endregion
        public ListController()
        {
            InitializeComponent();
        }
    }
}
