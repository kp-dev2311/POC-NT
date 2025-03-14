using System;
using System.IO;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using POC.Helper;
using POCNT.Application.DTOs;
using POCNT.Domain.Models;

namespace POC.ConsoleUI
{
    internal class Program
    {
        const int ActionMostActiveUser = 1;
        const int ActionAvgActiveUser = 2;
        const int ActionMostDurationActiveUser = 3;
        static async Task Main(string[] args)
        {
            using HttpClient client = new HttpClient();
            HttpClientHelper clientHelper = new HttpClientHelper(client);
            string baseUrl = "";
            IConfiguration config = LoadConfiguration();
            while (true) // Loop to keep showing the menu
            {
                Console.Clear();
                Console.WriteLine("User Activity Report Console");
                Console.WriteLine("1. Most Active Users");
                Console.WriteLine("2. Average Activity per Session");
                Console.WriteLine("3. Daily/Weekly Active Users");
                Console.WriteLine("4. Exit");
                Console.Write("\nSelect an option: ");

                var input = Console.ReadLine();

                if (input == "4")
                {
                    Console.WriteLine("Exiting...");
                    break;
                }
                baseUrl = config["AppSettings:APIBaseUrl"]??string.Empty;
                string apiUrl = GetApiUrl(baseUrl, input);

                if (!string.IsNullOrEmpty(apiUrl))
                {
                    List<UserActivitesResponseDto>? users;
                    var queryParams = new Dictionary<string, string>();

                    switch (int.Parse(input))
                    {
                        case ActionMostActiveUser:
                            users  = await clientHelper.GetAsync<List<UserActivitesResponseDto>>(apiUrl, queryParams);
                            PrintMostActiveUsers(users);
                            break;
                        case ActionAvgActiveUser:
                            users = await clientHelper.GetAsync<List<UserActivitesResponseDto>>(apiUrl, queryParams);
                            AvgActivePerUser(users);
                            break;
                        case ActionMostDurationActiveUser:
                            Console.WriteLine("\n Please enter number of days data needs to retrive : ");
                            
                            var inputDays = Console.ReadLine();

                            queryParams.Add("days", inputDays??"1");
                            users = await clientHelper.GetAsync<List<UserActivitesResponseDto>>(apiUrl, queryParams);
                            DurationBasedActiveUser(users);
                            break;
                        
                    }
                    
                }
                else
                {
                    Console.WriteLine("Invalid option. Please select 1, 2, or 3.");
                }

                Console.WriteLine("\nPress any key to return to the menu...");
                Console.ReadKey();
            }
        }

        static void PrintMostActiveUsers(List<UserActivitesResponseDto>?  users)
        {
            Console.WriteLine("------------------------------------------------------------------------------");
            Console.WriteLine("| {0,-10} | {1,-20} | {2,-15} | {3,-25} |", "UserId", "Name", "ActivityCount", "LastActivity");
            Console.WriteLine("------------------------------------------------------------------------------");

            if (users?.Count > 0)
            {
                foreach (var user in users)
                {
                    Console.WriteLine("| {0,-10} | {1,-20} | {2,-15} | {3,-25} |",
                        user.Id,
                        user.UserName,
                        user.ActivityCount,
                        user.LastActivity.ToString("yyyy-MM-dd HH:mm:ss")); // Format date-time
                }
            }
            Console.WriteLine("------------------------------------------------------------------------------");

        }

        static void AvgActivePerUser(List<UserActivitesResponseDto>? users)
        {
            Console.WriteLine("------------------------------------------------------------------------------");
            Console.WriteLine("| {0,-10} | {1,-20} | {2,-15} | {3,-25} |", "UserId", "Name", "AvgCount", "LastActivity");
            Console.WriteLine("------------------------------------------------------------------------------");

            if (users?.Count > 0)
            {
                foreach (var user in users)
                {
                    Console.WriteLine("| {0,-10} | {1,-20} | {2,-15} | {3,-25} |",
                        user.Id,
                        user.UserName,
                        user.AvgCount,
                        user.LastActivity.ToString("yyyy-MM-dd HH:mm:ss")); // Format date-time
                }
            }
            Console.WriteLine("------------------------------------------------------------------------------");

        }

        static void DurationBasedActiveUser(List<UserActivitesResponseDto>? users)
        {
            Console.WriteLine("------------------------------------------------------------------------------");
            Console.WriteLine("| {0,-10} | {1,-20} | {2,-15} | {3,-25} |", "UserId", "Name", "SessionDuration", "SessionID");
            Console.WriteLine("------------------------------------------------------------------------------");

            if (users?.Count > 0)
            {
                foreach (var user in users)
                {
                    decimal sessionDuration = Math.Round(Convert.ToDecimal(user.SessionDuration), 2);


                    Console.WriteLine("| {0,-10} | {1,-20} | {2,-15} | {3,-25} |",
                        user.Id,
                        user.UserName,
                       sessionDuration,
                        user.SessionId); // Format date-time
                }
            }
            Console.WriteLine("------------------------------------------------------------------------------");

        }


        static IConfiguration LoadConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

        static string GetApiUrl(string baseUrl, string option)
        {
            return option switch
            {
                "1" => $"{baseUrl}Activities/most-active",
                "2" => $"{baseUrl}Activities/average-session",
                "3" => $"{baseUrl}Activities/active-users",
                _ => string.Empty
            };
        }

    }
}