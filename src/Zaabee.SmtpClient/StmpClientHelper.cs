﻿using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Zaabee.SmtpClient
{
    public class StmpClientHelper
    {
        private string _host;
        private int _port;
        private string _userName;
        private string _password;
        private bool _enableSsl;
        private SmtpDeliveryMethod _deliveryMethod = SmtpDeliveryMethod.Network;
        private SmtpDeliveryFormat _smtpDeliveryFormat = SmtpDeliveryFormat.SevenBit;
        private string _pickupDirectoryLocation;
        private string _targetName = "SMTPSVC/";
        private TimeSpan? _timeout;

        /// <summary>
        /// SMTP server's IP
        /// </summary>
        /// <param name="smtpIp"></param>
        /// <returns></returns>
        public StmpClientHelper Host(string smtpIp)
        {
            _host = smtpIp;
            return this;
        }

        /// <summary>
        /// SMTP server's port
        /// </summary>
        /// <param name="smtpPort"></param>
        /// <returns></returns>
        public StmpClientHelper Port(int smtpPort)
        {
            _port = smtpPort;
            return this;
        }

        /// <summary>
        /// The userName for NetworkCredential
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public StmpClientHelper UserName(string userName)
        {
            _userName = userName;
            return this;
        }

        /// <summary>
        /// The password for NetworkCredential
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public StmpClientHelper Password(string password)
        {
            _password = password;
            return this;
        }

        public StmpClientHelper Ssl(bool enableSsl)
        {
            _enableSsl = enableSsl;
            return this;
        }

        public StmpClientHelper DeliveryMethod(SmtpDeliveryMethod smtpDeliveryMethod)
        {
            _deliveryMethod = smtpDeliveryMethod;
            return this;
        }

        public StmpClientHelper DeliveryFormat(SmtpDeliveryFormat smtpDeliveryMethod)
        {
            _smtpDeliveryFormat = smtpDeliveryMethod;
            return this;
        }

        public StmpClientHelper PickupDirectoryLocation(string pickupDirectoryLocation)
        {
            _pickupDirectoryLocation = pickupDirectoryLocation;
            return this;
        }

        public StmpClientHelper TargetName(string targetName)
        {
            _targetName = targetName;
            return this;
        }

        public StmpClientHelper Timeout(TimeSpan timeout)
        {
            _timeout = timeout;
            return this;
        }

        public void Send(SendMessage sendMessage)
        {
            if (sendMessage == null) return;
            using (var client = new System.Net.Mail.SmtpClient
            {
                Host = _host,
                Port = _port,
                Credentials = new NetworkCredential(_userName, _password),
                EnableSsl = _enableSsl,
                DeliveryMethod = _deliveryMethod,
                DeliveryFormat = _smtpDeliveryFormat,
                PickupDirectoryLocation = _pickupDirectoryLocation,
                TargetName = _targetName,
                Timeout = _timeout?.Milliseconds ?? 100000
            })
            using (var mailMessage = sendMessage.CreateMail())
            {
                client.Send(mailMessage);
            }
        }

        public async Task SendAsync(SendMessage sendMessage)
        {
            if (sendMessage == null) return;
            using (var client = new System.Net.Mail.SmtpClient
            {
                Host = _host,
                Port = _port,
                Credentials = new NetworkCredential(_userName, _password),
                EnableSsl = _enableSsl,
                DeliveryMethod = _deliveryMethod,
                DeliveryFormat = _smtpDeliveryFormat,
                PickupDirectoryLocation = _pickupDirectoryLocation,
                TargetName = _targetName,
                Timeout = _timeout?.Milliseconds ?? 100000
            })
            using (var mailMessage = sendMessage.CreateMail())
            {
                await client.SendMailAsync(mailMessage);
            }
        }
    }
}