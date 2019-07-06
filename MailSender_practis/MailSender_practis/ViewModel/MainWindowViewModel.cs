using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using MailSender_Lib.Services;
using MailSender_Lib.Services.Linq2SQL;
using MailSender_Lib.Data;
using System.Collections;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using System.ComponentModel;
using System.Windows.Data;
using MailSender_Lib.Data.BaseEntityes;

namespace MailSender_practis.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IRecipientsDataService _RecipientsDataService;
        private readonly ISenderDataService _SenderDataService;
        private readonly IServerDataService _ServerDataService;
        private readonly IMailMessageDataService _MailMessageDataService;
        private readonly IMailSenderService _MailSenderService;

        private string _Title = "Расыльщик почты";

        
        public ICollectionView ItemsView { get; set; }
        private CollectionViewSource _itemSourceList;

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
        public ObservableCollection<Server> Servers { get; } = new ObservableCollection<Server>();
        public ObservableCollection<Sender> Senders { get; } = new ObservableCollection<Sender>();
        public ObservableCollection<MailMessage> MailMessages { get; } = new ObservableCollection<MailMessage>();

        private Recipient _CurrentRecipient;

        /// <summary>
        /// Свойство для связи с DataGrid
        /// </summary>
        public Recipient CurrentRecipient
        {
            get => _CurrentRecipient;
            set => Set(ref _CurrentRecipient, value);
        }


        public MainWindowViewModel(
            IRecipientsDataService RecipientsDataService,
            ISenderDataService SenderDataService,
            IServerDataService ServerDataService,
            IMailMessageDataService MailMessageDataService, 
            IMailSenderService MailSenderService
            )
        {
            
            _RecipientsDataService = RecipientsDataService;
            _SenderDataService = SenderDataService;
            _ServerDataService = ServerDataService;
            _MailMessageDataService = MailMessageDataService;
            _MailSenderService = MailSenderService;


            UpdateData();
            UpdateDateCommand = new RelayCommand(OnUpdateDataCommandExecute, CanUpdateDataCommandExecute);

            _itemSourceList = new CollectionViewSource() { Source = Recipients };
            ItemsView = _itemSourceList.View;
            CreateRecipientCommand =
                new RelayCommand(OnCreateRecipientCommandExecuted, CanCreateRecipientCommandExecuted);
            SaveRecipientCommand =
                new RelayCommand<Recipient>(OnSaveRecipientCommandExecuted, CanSaveRecipientCommandExecuted);
            ApplicationExitCommand = new RelayCommand(OnApplicationExitCommandExecuted, () => true, true);
            FilterDateCommand = new RelayCommand(OnFilterDateCommandExecute, CanFilterDateCommandExecute);
        }


        #region Выход

        public ICommand ApplicationExitCommand { get; }

        private static void OnApplicationExitCommandExecuted()
        {
            Application.Current.Shutdown();
        }

        #endregion

        #region Редактирование получателя

        public ICommand SaveRecipientCommand { get; }

        private void OnSaveRecipientCommandExecuted(Recipient recipient)
        {
            _RecipientsDataService.Edit(recipient);
        }

        private bool CanSaveRecipientCommandExecuted(Recipient recipient)
        {
            return true;
        }

        #endregion

        #region Соманды создания отправителя

        /// <summary>
        /// Команда создание получателя
        /// </summary>
        public ICommand CreateRecipientCommand { get; }

        private void OnCreateRecipientCommandExecuted()
        {
            var new_recipient = new Recipient()
            {
                Name = "New recipient",
                Address = "new_Email@mail.ru"
            };
            _RecipientsDataService.Add(new_recipient);
            _Recipients.Add(new_recipient);
            CurrentRecipient = new_recipient;
        }

        private bool CanCreateRecipientCommandExecuted()
        {
            return true;
        }

        #endregion

        #region Команда обновления данных в DataGrid

      
        public ICommand UpdateDateCommand { get; }

        private bool CanUpdateDataCommandExecute()
        {
            return true;
        }

        private void OnUpdateDataCommandExecute()
        {
            FilterRecipientNameString = "";
            OnFilterDateCommandExecute();
            UpdateData();
        }

        #endregion

        public void UpdateData()
        {
            
            Recipients = new ObservableCollection<Recipient>(_RecipientsDataService.GetAll());

            void UpdateData<T>(IDataService<T> Service, ObservableCollection<T> Collection)
                where T : Entity
            {
                Collection.Clear();
                foreach (var entity in Service.GetAll())
                {
                  Collection.Add(entity);  
                }
            }
            UpdateData(_SenderDataService,Senders);
            UpdateData(_ServerDataService,Servers);
            UpdateData(_MailMessageDataService,MailMessages);
        }

        #region Фильтер по имени

        public ICommand FilterDateCommand { get; }

        private void OnFilterDateCommandExecute()
        {
            if (_FilterRecipientNameString == null) return;
            var filterByName = new Predicate<object>(item => ((Recipient) item).Name.Contains(_FilterRecipientNameString));
            ItemsView.Filter = filterByName;
        }

        private bool CanFilterDateCommandExecute()
        {
            return true;
        }
        
        private string _FilterRecipientNameString;

        public string FilterRecipientNameString
        {
            get => _FilterRecipientNameString;
            set => Set(ref _FilterRecipientNameString  , value);
        }

        

        #endregion
    }
}