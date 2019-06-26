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
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Windows;

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


        private Recipient _CurrentRecipient;
        /// <summary>
        /// Свойство для связи с DataGrid
        /// </summary>
        public Recipient CurrentRecipient
        {
            get => _CurrentRecipient;
            set => Set(ref _CurrentRecipient, value);
        }


      
        public MainWindowViewModel(IRecipientsDataService RecipientsDataService)
        {
            _RecipientsDataService = RecipientsDataService;
            UpdateDateCommand = new RelayCommand(OnUpdateDataCommandExecute, CanUpdateDataCommandExecute);
            CreateRecipientCommand = new RelayCommand(OnCreateRecipientCommandExecuted, CanCreateRecipientCommandExecuted);
            SaveRecipientCommand = new RelayCommand<Recipient>(OnSaveRecipientCommandExecuted, CanSaveRecipientCommandExecuted);
            ApplicationExitCommand = new RelayCommand(OnApplicationExitCommandExecuted, () => true, true);
        }

        private static void OnApplicationExitCommandExecuted()
        {
            Application.Current.Shutdown();
        }

        #region MyRegion
        public ICommand ApplicationExitCommand { get; }

        #endregion

        #region Редактирование получателя
        public ICommand SaveRecipientCommand { get; }

        private void OnSaveRecipientCommandExecuted(Recipient recipient)
        {
            _RecipientsDataService.Update(recipient);
        }
        private bool CanSaveRecipientCommandExecuted(Recipient recipient) => true;

        #endregion

        #region Соманды создания отправителя
        /// <summary>
        /// Соманда создание получателя
        /// </summary>
        public ICommand CreateRecipientCommand { get; }
        private void OnCreateRecipientCommandExecuted()
        {
            var new_recipient = new Recipient()
            {
                Name = "New recipient",
                Address = "new_Email@mail.ru"
            };
            _RecipientsDataService.Create(new_recipient);
            _Recipients.Add(new_recipient);
            CurrentRecipient = new_recipient;
        }
        private bool CanCreateRecipientCommandExecuted() => true;

        #endregion

        #region Команда обновления данных в DataGrid

        /// <summary>
        /// Свойство команды для кнопок
        /// </summary>
        public ICommand UpdateDateCommand { get; }
        private bool CanUpdateDataCommandExecute() => true;
        private void OnUpdateDataCommandExecute()
        {
            UpdateData();
        }
        #endregion

        public void UpdateData()
        {
            Recipients = new ObservableCollection<Recipient>(_RecipientsDataService.GetAll());
        }
    }
}
