﻿using EasySoft.Core.Config.ConfigAssist;
using RabbitMQ.Client;

namespace EasySoft.Core.RabbitMQ
{
    public static class MessageStorage
    {
        private static readonly object Locker = new();
        private static IConnection? _connection;

        private static ConnectionFactory GetFactory()
        {
            return new ConnectionFactory
            {
                HostName = RabbitMQConfigAssist.GetHostName(),
                UserName = RabbitMQConfigAssist.GetUserName(),
                Password = RabbitMQConfigAssist.GetPassword(),
                VirtualHost = RabbitMQConfigAssist.GetVirtualHost(),
                RequestedConnectionTimeout = TimeSpan.FromSeconds(RabbitMQConfigAssist.GetConnectionTimeout()),
                AutomaticRecoveryEnabled = true
            };
        }

        public static IConnection CreateConnection()
        {
            if (_connection != null)
            {
                return _connection;
            }

            lock (Locker)
            {
                if (_connection is { IsOpen: true })
                {
                    return _connection;
                }

                var factory = GetFactory();

                _connection = factory.CreateConnection();
            }

            return _connection;
        }
    }
}