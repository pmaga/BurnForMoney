﻿using System;
using System.Threading.Tasks;
using BurnForMoney.Infrastructure.Events;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace BurnForMoney.Functions.Functions.EventGridSubscriptions
{
    public class AthletePointsSubscription
    {
        [FunctionName("EventGrid_AchievementsSubscription")]
        public static async Task<IActionResult> EventGrid_AchievementsSubscription([EventGridTrigger] EventGridEvent @event, ILogger log)
        {
            log.LogInformation("-------Event data reviewed-------\n");
            log.LogInformation($"Event => {@event.EventType} Subject => {@event.Subject}\n");

            if (!(@event.Data is JObject eventData))
            {
                throw new ArgumentException(nameof(@event.Data));
            }

            var receivedEvent = eventData.ToObject(Type.GetType(@event.EventType));
            if (receivedEvent is ActivityAdded)
            {
                    
            }

            return new OkResult();
        }
    }
}