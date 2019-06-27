using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace MailSender_WpfTest.TestClasses
{
    class TestFilterClass
    {

    }
    public class ItemViewModel
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
    public class ApplicationViewModel
    {
        public ApplicationViewModel()
        {
            Items.Add(new ItemViewModel { Name = "John", Age = 18 });
            Items.Add(new ItemViewModel { Name = "Mary", Age = 30 });
            Items.Add(new ItemViewModel { Name = "Richard", Age = 28 });
            Items.Add(new ItemViewModel { Name = "Elizabeth", Age = 45 });
            Items.Add(new ItemViewModel { Name = "Patrick", Age = 6 });
            Items.Add(new ItemViewModel { Name = "Philip", Age = 11 });

            ItemsView = CollectionViewSource.GetDefaultView(Items);
        }

        public ApplicationCommand ApplicationCommand
        {
            get { return new ApplicationCommand(this); }
        }

        private ObservableCollection<ItemViewModel> Items =
                                         new ObservableCollection<ItemViewModel>();

        public ICollectionView ItemsView { get; set; }

        public void ExecuteCommand(string command)
        {
            ListCollectionView list = (ListCollectionView)ItemsView;
            switch (command)
            {
                case "SortByName":
                    list.CustomSort = new ItemSorter("Name");
                    return;
                case "SortByAge":
                    list.CustomSort = new ItemSorter("Age");
                    return;
                case "ApplyFilter":
                    list.Filter = new Predicate<object>(x =>
                                                      ((ItemViewModel)x).Age > 21);
                    return;
                case "RemoveFilter":
                    list.Filter = null;
                    return;
                default:
                    return;
            }
        }
    }
    public class ItemSorter : IComparer
    {
        private string PropertyName { get; set; }

        public ItemSorter(string propertyName)
        {
            PropertyName = propertyName;
        }
        public int Compare(object x, object y)
        {
            ItemViewModel ix = (ItemViewModel)x;
            ItemViewModel iy = (ItemViewModel)y;

            switch (PropertyName)
            {
                case "Name":
                    return string.Compare(ix.Name, iy.Name);
                case "Age":
                    if (ix.Age > iy.Age) return 1;
                    if (iy.Age > ix.Age) return -1;
                    return 0;
                default:
                    throw new InvalidOperationException("Cannot sort by " +
                                                         PropertyName);
            }
        }
    }
    public class ApplicationCommand : ICommand
    {
        private ApplicationViewModel _ApplicationViewModel;

        public ApplicationCommand(ApplicationViewModel avm)
        {
            _ApplicationViewModel = avm;
        }

        public void Execute(object parameter)
        {
            _ApplicationViewModel.ExecuteCommand(parameter.ToString());
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
    }

}
