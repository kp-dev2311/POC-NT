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
        private readonly IMapper _mapper;

        public UserActivitiesService(IRepository<UserActivities> userActivitiesRepository, IMapper mapper)
        {
            _userActivitieRepository = userActivitiesRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserActivitesDto>> GetUserActivitiesAsync()
        {
            var students = await _userActivitieRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserActivitesDto>>(students);
        }


        public async Task CreateStudentAsync(ActivityReqDto reqParms)
        {
          
            string guidString = Guid.NewGuid().ToString();            
            if (reqParms.NoOfActivity > 0)
            {
                List<UserActivities> activityList = new List<UserActivities>();
                for (int i = 0; i < reqParms.NoOfActivity; i++)
                {
                    var userAction = HelperExtensions.GetRandomUserAction();
                    int sec = reqParms.intervelOfActivity > 0 ? reqParms.intervelOfActivity : 1;
                    await Task.Delay(sec * 1000); // 2-second delay
                    UserActivitesDto studentDto = new UserActivitesDto
                    {
                        ActivityType = userAction.ToString(),
                        SessionId = guidString,
                        UserId = reqParms.UserId,
                        OtherNotes = "Test",
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
