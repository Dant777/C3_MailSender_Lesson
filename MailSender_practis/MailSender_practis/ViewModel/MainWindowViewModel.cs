using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using MailSender_Lib.Services;
using MailSender_Lib.Services.Linq2SQL;
using MailSender_Lib.Data.Linq2SQL;
using System.Collections;
using System.Collections.ObjectModel;

namespace MailSender_practis.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IRecipientsDataService _RecipientsDataService;
        private string _Title = "Расыльщик почты";

        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        private string _Status = "Готов!";

        public string Status
        {
            get => _Status;
            set => Set(ref _Status, value);

            //set
            //{
            //    if (_Title == value) return;
            //    _Title = value;
            //    RaisePropertyChanged();
            //}
        }

        private ObservableCollection<Recipient> _Recipients;
        public ObservableCollection<Recipient> Recipients
        {
            get => _Recipients;
            private set => Set(ref _Recipients, value);
        }

        public MainWindowViewModel(IRecipientsDataService RecipientsDataService)
        {
            _RecipientsDataService = RecipientsDataService;
            UpdateData();
        }

        public void UpdateData()
        {
            Recipients = new ObservableCollection<Recipient>(_RecipientsDataService.GetAll());
        }
    }
}
