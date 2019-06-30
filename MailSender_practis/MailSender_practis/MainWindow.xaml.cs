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
using MailSender_practis.View;
using MailSender_Lib.Data;
using MailSender_Lib.Data.Linq2SQL;
using MailSender_Lib;
using System.Data;
using System.Windows.Threading;

namespace MailSender_practis
{ 
    public partial class MainWindow
    {
        ResipientsInfoViewer recInfoViewer;
        public MainWindow()
        {
            InitializeComponent();
            cbSenderSelect.ItemsSource = VariablesClass.Senders;
            cbSenderSelect.DisplayMemberPath = "Key";
            cbSenderSelect.SelectedValuePath = "Value";
            

            cbServerSelect.ItemsSource = SmtpServer.Servers;
            cbServerSelect.DisplayMemberPath = "Key";
            cbServerSelect.SelectedValuePath = "Value";
            cbServerSelect.SelectedIndex = 0;

            
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
        /// <summary>
        /// Отправка письма сразу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSendAtOnce_Click(object sender, RoutedEventArgs e)
        {
            recInfoViewer = new ResipientsInfoViewer();
            //вход на сервер
            string strLogin = cbSenderSelect.Text;                          // Login/address sender
            string strPassword = cbSenderSelect.SelectedValue.ToString();   // User password
            //отправитель
            string senderName = cbSenderSelect.Text;
            string senderAddress = cbSenderSelect.Text;
            //Сервер
            string serverAdress = cbServerSelect.Text;                                  // smpt server
            int serverPort = Convert.ToInt32(cbServerSelect.SelectedValue.ToString());  // port server
            //получатель
            var recipientSelect = (Recipient)recInfoViewer.dtRecipientSelect.SelectedValue;
            string recipName = recipientSelect.Name;
            string recipAddress = recipientSelect.Address;
            
            
            //письмо
            if (txtObject.Text == "")
            {
                MessageBox.Show("Нет темы письма", "Тема письма",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                MainTabControle.SelectedItem = tabPlanner;
                txtObject.Focus();
                return;
            }
            if (txtBody.Text == "")
            {
                MessageBox.Show("Письмо пустое", "Пусто",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                MainTabControle.SelectedItem = tabPlanner;
                txtBody.Focus();
                return;
            }
            string mailObject = txtObject.Text;
            string mailBody = txtBody.Text;

            var emailSend = new EmailSendServiceClass_Test(senderName, senderName, recipName, recipAddress);
            emailSend.CreateMailMessage(mailObject, mailBody);
            emailSend.SendMail(serverAdress, serverPort, senderAddress, strPassword, true);
        }

        private void BtnSendToPlan_OnClick(object sender, RoutedEventArgs e)
        {

            int serverPort = Convert.ToInt32(cbServerSelect.SelectedValue.ToString());  // port server
            //получатель
            var recipientSelect = (Recipient)recInfoViewer.dtRecipientSelect.SelectedValue;
            string recipName = recipientSelect.Name;
            string recipAddress = recipientSelect.Address;

            SchedulerClass sc = new SchedulerClass();
            TimeSpan tsSendTime = sc.GetSendTime(tbTimePicker.Text);

            if (tsSendTime == new TimeSpan())
            {
                MessageBox.Show("Некорректный формат даты");
                return;
            }
            DateTime dtSendDateTime = (cldSchedulDateTimes.SelectedDate ?? DateTime.Today).Add(tsSendTime);

            if (dtSendDateTime < DateTime.Now)
            {
                MessageBox.Show("Дата и время отправки писем не могут быть раньше, чем настоящее время");
                return;
            }
            //письмо
            if (txtObject.Text == "")
            {
                MessageBox.Show("Нет темы письма", "Тема письма",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                MainTabControle.SelectedItem = tabPlanner;
                txtObject.Focus();
                return;
            }
            if (txtBody.Text == "")
            {
                MessageBox.Show("Письмо пустое", "Пусто",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                MainTabControle.SelectedItem = tabPlanner;
                txtBody.Focus();
                return;
            }
            EmailSendServiceClass emailSender = new EmailSendServiceClass(cbSenderSelect.Text, cbSenderSelect.SelectedValue.ToString(), 
                                                                            cbServerSelect.Text, serverPort, txtBody.Text, txtObject.Text);
            sc.SendEmails(dtSendDateTime, emailSender,(IQueryable<Recipient>)recInfoViewer.dtRecipientSelect.SelectedValue);

        }
    }
}
