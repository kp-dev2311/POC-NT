using AutoMapper;
using POC.Helper;
using POCNT.Application.DTOs;
using POCNT.Domain.Interfaces;
using POCNT.Domain.Models;

namespace POCNT.Application.Services
{
    public class UserActivitiesService
    {
        private readonly IRepository<UserActivities> _userActivitieRepository;
        private readonly IRepository<Users> _userRepository;
        private readonly IMapper _mapper;

        public UserActivitiesService(IRepository<UserActivities> userActivitiesRepository, IRepository<Users> userRepository, IMapper mapper)
        {
            _userActivitieRepository = userActivitiesRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserActivitesDto>> GetUserActivitiesAsync()
        {
            var students = await _userActivitieRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserActivitesDto>>(students);
        }


        public async Task<IEnumerable<UserActivitesResponseDto>> GetMostActiveUserAsync()
        {
            var activities = await _userActivitieRepository.GetAllAsync();
            var users = await _userRepository.GetAllAsync(); // Fetch Users

            var mostActiveUsers = activities
                .GroupBy(a => a.UserId) // Group activities by UserId
                .Join(users,
                      activityGroup => activityGroup.Key, // Join on UserId
                      user => user.Id,
                      (activityGroup, user) => new UserActivitesResponseDto
                      {
                          Id = activityGroup.Key,
                          UserName = user.UserName, // Fetch UserName
                          ActivityCount = activityGroup.Count(), // Count activities per user
                          LastActivity = activityGroup.Max(a => a.ActivityTimeStamp) // Get latest activity timestamp
                      })
                .OrderByDescending(x => x.ActivityCount) // Sort by activity count (descending)
                .Take(10) // Get Top 10 most active users
                .ToList();

            return mostActiveUsers;
        }

        public async Task<IEnumerable<UserActivitesResponseDto>> GetAvgActivitesUserAsync()
        {
            var activities = await _userActivitieRepository.GetAllAsync();
            var users = await _userRepository.GetAllAsync(); // Fetch Users


            var avgActivityPerUserSession = activities
                .GroupBy(a => new { a.UserId, a.SessionId }) // Group by User and Session
                .Select(group => new
                {
                    UserId = group.Key.UserId,
                    SessionId = group.Key.SessionId,
                    ActivityCount = group.Count(),
                    LastActivity = group.Max(a => a.ActivityTimeStamp), // 
                    UserName = users.FirstOrDefault(u => u.Id == group.Key.UserId)?.UserName ?? "N/A",
                })
                .GroupBy(x => x.UserId) // Group again by User to get Avg per User
                .Select(userGroup => new UserActivitesResponseDto
                {
                    Id = userGroup.Key,
                    AvgCount = userGroup.Average(x => x.ActivityCount),
                    UserName = userGroup.FirstOrDefault()?.UserName,
                    LastActivity = userGroup.Max(x => x.LastActivity)
                })
                .ToList();

            return avgActivityPerUserSession;
        }


        public async Task<IEnumerable<UserActivitesResponseDto>> GetDurationBasedActiveUsersAsync(DateTime startDate, DateTime endDate, int minSessionDuration = 0)
        {

            var activities = await _userActivitieRepository.GetAllAsync();
            var users = await _userRepository.GetAllAsync(); // Fetch users separately

            var activeUsers = activities
                .Where(a => a.ActivityTimeStamp >= startDate && a.ActivityTimeStamp <= endDate)
                .GroupBy(a => new { a.UserId, a.SessionId })
                .Select(group => new UserActivitesResponseDto
                {
                    Id = group.Key.UserId,
                    UserName = users.FirstOrDefault(u => u.Id == group.Key.UserId)?.UserName ?? "N/A",
                    SessionId = group.Key.SessionId,
                    SessionDuration = (group.Max(a => a.ActivityTimeStamp) - group.Min(a => a.ActivityTimeStamp)).TotalMinutes
                })
                .Where(user => user.SessionDuration > minSessionDuration)
                .OrderByDescending(user => user.SessionDuration)
                .ToList();

            return activeUsers;
        }


        public async Task CreateUserActivitiesAsync(ActivityReqDto reqParms)
        {
          
            string guidString = Guid.NewGuid().ToString();            
            if (reqParms.NoOfActivity > 0)
            {
                List<UserActivities> activityList = new List<UserActivities>();
                for (int i = 0; i < reqParms.NoOfActivity; i++)
                {
                    var userAction = HelperExtensions.GetRandomUserAction();
                    int sec = reqParms.intervelOfActivity > 0 ? reqParms.intervelOfActivity : 1;
                    await Task.Delay(sec * 1000); // second delay
                    UserActivitesDto studentDto = new UserActivitesDto
                    {
                        ActivityType = userAction.ToString(),
                        SessionId = guidString,
                        UserId = reqParms.UserId,
                        OtherNotes = "N/A",
                        ActivityTimeStamp = DateTime.UtcNow
                    };
                    var newStudent = _mapper.Map<UserActivities>(studentDto);
                    activityList.Add(newStudent);
                }
                await _userActivitieRepository.CreateAsync(activityList);
            }

        }



       
    }
}
