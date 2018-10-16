﻿using System;

namespace BurnForMoney.Functions.Persistence.DatabaseSchema
{
    public class Athlete
    {
        public int AthleteId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AccessToken { get; set; }
        public bool Active { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}