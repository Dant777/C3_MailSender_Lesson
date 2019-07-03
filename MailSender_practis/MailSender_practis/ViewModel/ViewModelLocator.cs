 /*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:MailSender_practis"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using MailSender_Lib.Services;
using MailSender_Lib.Services.Linq2SQL;
using MailSender_Lib.Services.InMemory;
using MailSender_Lib.Data.Linq2SQL;




namespace MailSender_practis.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Add design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Add run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            var services = SimpleIoc.Default;
            services.Register(() => new MailSenderDBContext());
            services.Register<MainViewModel>();
            services.Register<MainWindowViewModel>();
            //SimpleIoc.Default.Register<IRecipientsDataService, RecipientDataServiceLinq2SQL>();
            services.Register<IRecipientsDataService, RecipientsDataServiceInMemory>();
            services.Register<ISenderDataService, SendersDataInMemory>();
            services.Register<IServerDataService, ServerDataInMemory>();
            services.Register<IMailMessageDataService, MailMessagesDataInMemory>();
            services.Register<IMailSenderService, SmtpMailSenderServicr>();

        } 

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public MainWindowViewModel MainWindowViewModel => ServiceLocator.Current.GetInstance<MainWindowViewModel>();
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}