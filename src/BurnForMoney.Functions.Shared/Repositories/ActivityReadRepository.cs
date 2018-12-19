﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BurnForMoney.Domain.Domain;
using BurnForMoney.Functions.Shared.Exceptions;
using BurnForMoney.Functions.Shared.Persistence;
using BurnForMoney.Functions.Shared.Repositories.Dto;
using Dapper;

namespace BurnForMoney.Functions.Shared.Repositories
{
    public class ActivityReadRepository : IReadFacade<ActivityRow>
    {
        private readonly string _sqlConnectionString;

        public ActivityReadRepository(string sqlConnectionString)
        {
            _sqlConnectionString = sqlConnectionString;
        }

        public async Task<ActivityRow> GetByExternalIdAsync(string externalId)
        {
            using (var conn = SqlConnectionFactory.Create(_sqlConnectionString))
            {
                await conn.OpenWithRetryAsync();

                var activity = await conn.QuerySingleOrDefaultAsync<ActivityRow>(
                    @"SELECT Id, AthleteId, ExternalId, Distance AS DistanceInMeters, MovingTime AS MovingTimeInMinutes, ActivityType, ActivityTime as StartDate, Source 
FROM dbo.Activities WHERE ExternalId=@ExternalId", new
                    {
                        ExternalId = externalId
                    });

                if (activity == null)
                {
                    throw new ActivityNotFoundException(externalId);
                }

                return activity;
            }
        }

        public async Task<IEnumerable<ActivityRow>> GetAthleteActivitiesAsync(Guid id, Source source, int month, int year)
        {
            using (var conn = SqlConnectionFactory.Create(_sqlConnectionString))
            {
                await conn.OpenWithRetryAsync();

                var activities = conn.Query<ActivityRow>(@"SELECT Id, AthleteId, ExternalId, Distance AS DistanceInMeters, MovingTime AS MovingTimeInMinutes, ActivityType, ActivityTime as StartDate, Source
FROM dbo.Activities WHERE AthleteId=@AthleteId AND Source=@Source AND MONTH(ActivityTime)=@Month AND YEAR(ActivityTime)=@Year",
                    new
                    {
                        AthleteId = id,
                        Source = source.ToString(),
                        Month = month,
                        Year = year
                    });
                return activities;
            }
        }
    }
}