using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata;
using POCNT.Application.DTOs;
using POCNT.Domain.Interfaces;
using POCNT.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCNT.Application.Services
{
    public class UsersService
    {
        private readonly IRepository<Users> _adminRepository;
        private readonly IMapper _mapper;

        public UsersService(IRepository<Users> adminRepository, IMapper mapper)
        {
            _adminRepository = adminRepository ?? throw new ArgumentNullException(nameof(adminRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<UsersDto>> GetAdminsAsync()
        {
            var admins = await _adminRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UsersDto>>(admins);
        }

        public async Task<UsersDto> GetByIdAsync(int id)
        {
            var admin = await _adminRepository.GetByIdAsync(id);
            return _mapper.Map<UsersDto>(admin);
        }

        public async Task CreateAdminAsync(UsersDto adminDto)
        {
            var admin = _mapper.Map<Users>(adminDto);
            await _adminRepository.CreateAsync(admin);
        }

        public async Task UpdateAdminAsync(int id, UsersDto adminDto)
        {
            var admin = await _adminRepository.GetByIdAsync(id);
            if(admin != null)
            {
                _mapper.Map(adminDto, admin);
                await _adminRepository.UpdateAsync(admin);
            }
        }

        public async Task DeleteAdminAsync(int id)
        {
            await _adminRepository.DeleteAsync(id);
        }
    }
}
