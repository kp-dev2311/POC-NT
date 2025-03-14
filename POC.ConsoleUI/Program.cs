using System;
using System.IO;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using POCNT.Application.DTOs;
using POCNT.Domain.Models;

namespace POC.ConsoleUI
{
    internal class Program
    {

        static async Task Main(string[] args)
        {
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
                baseUrl = config["AppSettings:APIBaseUrl"];
                string apiUrl = GetApiUrl(baseUrl, input);

                if (!string.IsNullOrEmpty(apiUrl))
                {
                    string response = await CallApiAsync(apiUrl);
                    var users = JsonSerializer.Deserialize<List<UserActivitesResponseDto>>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    ActivityPrint(users);
                    //Console.WriteLine("\nAPI Response:\n" + response);
                }
                else
                {
                    Console.WriteLine("Invalid option. Please select 1, 2, or 3.");
                }

                Console.WriteLine("\nPress any key to return to the menu...");
                Console.ReadKey();
            }
        }

        static void ActivityPrint(List<UserActivitesResponseDto>  users)
        {
            Console.WriteLine("------------------------------------------------------------------------------");
            Console.WriteLine("\tUserId \t Name\t\t Action\t\t\t Timestamp");
            Console.WriteLine("-----------------------------------------------------------------------------");

            foreach (var user in users)
            {
                Console.WriteLine($"|\t{user.Id} \t|{user.UserName} \t\t| {user.ActivityType} \t\t\t| {user.ActivityTimeStamp}");
            }

            Console.WriteLine("-----------------------------------------------------------------------------");

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
                "1" => $"{baseUrl}Activities/all",
                "2" => $"{baseUrl}Activities/all",
                "3" => $"{baseUrl}Activities/all",
                _ => string.Empty
            };
        }

        static async Task<string> CallApiAsync(string url)
        {
            using HttpClient client = new HttpClient();

            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException ex)
            {
                return $"Error: {ex.Message}";
            }
        }
    }
}