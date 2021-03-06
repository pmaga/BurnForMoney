﻿using System.Collections.Generic;
using System.Threading.Tasks;
using BurnForMoney.Functions.Configuration;
using BurnForMoney.Functions.Shared.Extensions;
using BurnForMoney.Functions.Shared.Functions.Extensions;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;
using BurnForMoney.Functions.CommandHandlers;
using BurnForMoney.Functions.Repositories;
using BurnForMoney.Functions.Commands;
using BurnForMoney.Functions.Infrastructure.Queues;
using BurnForMoney.Infrastructure.Persistence.Repositories;

namespace BurnForMoney.Functions.Functions.CommandHandlers
{
    public class DeactivateAthleteCommandHandlerFunc
    {
        [FunctionName(FunctionsNames.Q_DeactivateAthlete)]
        public static async Task Q_DeactivateAthleteAsync(ILogger log, ExecutionContext executionContext,
            [Queue(AppQueueNames.NotificationsToSend)] CloudQueue notificationsQueue,
            [QueueTrigger(AppQueueNames.DeactivateAthleteRequests)] DeactivateAthleteCommand message,
            [Configuration] ConfigurationRoot configuration)
        {
            var repository = AthleteRepositoryFactory.Create();
            var commandHandler = new DeactivateAthleteCommandHandler(repository);
            await commandHandler.HandleAsync(message);

            var athleteRepository = new AthleteReadRepository(configuration.ConnectionStrings.SqlDbConnectionString);
            var athlete = await athleteRepository.GetAthleteByIdAsync(message.AthleteId);

            var notification = new Notification
            {
                Recipients = new List<string> { configuration.Email.DefaultRecipient },
                Subject = "Athlete revoked authorization",
                HtmlContent = $@"
            <p>Hi there,</p>
            <p>Athlete: {athlete.FirstName} {athlete.LastName} [{message.AthleteId}] revoked authorization.</p>"
            };
            await notificationsQueue.AddMessageAsync(new CloudQueueMessage(JsonConvert.SerializeObject(notification)));
        }
    }
}