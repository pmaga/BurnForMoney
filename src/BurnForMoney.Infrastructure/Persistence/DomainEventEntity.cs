﻿using Microsoft.WindowsAzure.Storage.Table;

namespace BurnForMoney.Infrastructure.Persistence
{
    public class DomainEventEntity : TableEntity
    {
        public string Type { get; set; }
        public string Data { get; set; }
    }
}