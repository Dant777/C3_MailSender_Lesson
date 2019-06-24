﻿using System;
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
using MailSender_Lib.Data;
using MailSender_Lib.Data.Linq2SQL;
using MailSender_Lib;
using System.Data;

namespace MailSender_practis
{ 
    public partial class MainWindow
    {
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
            //вход на сервер
            string strLogin = cbSenderSelect.Text;
            string strPassword = cbSenderSelect.SelectedValue.ToString();
            //отправитель
            string senderName = cbSenderSelect.Text;
            string senderAddress = cbSenderSelect.Text;
            //Сервер
            string serverAdress = cbServerSelect.Text;
            int serverPort = Convert.ToInt32(cbServerSelect.SelectedValue.ToString());
            //получатель
            var recipientSelect = (Recipient)dtRecipientSelect.SelectedValue;
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

            var emailSend = new EmailSendServiceClass(senderName, senderName, recipName, recipAddress);
            emailSend.CreateMailMessage(mailObject, mailBody);
            emailSend.SendMail(serverAdress, serverPort, senderAddress, strPassword, true);
        }
    }
}
