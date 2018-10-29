﻿using System;

namespace BurnForMoney.Functions.Shared.Queues
{
    public class PendingActivity
    {
        public long SourceActivityId { get; set; }
        public int SourceAthleteId { get; set; }
        public DateTime StartDate { get; set; }
        public string ActivityType { get; set; }
        public double DistanceInMeters { get; set; }
        public double MovingTimeInMinutes { get; set; }
        public ActivityCategory Category { get; set; }
        public double Points { get; set; }
        public string Source { get; set; }
    }
}