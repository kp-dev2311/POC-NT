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
    public class AdminService
    {
        private readonly IRepository<Admin> _adminRepository;
        private readonly IMapper _mapper;

        public AdminService(IRepository<Admin> adminRepository, IMapper mapper)
        {
            _adminRepository = adminRepository ?? throw new ArgumentNullException(nameof(adminRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<AdminDto>> GetAdminsAsync()
        {
            var admins = await _adminRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<AdminDto>>(admins);
        }

        public async Task<AdminDto> GetByIdAsync(int id)
        {
            var admin = await _adminRepository.GetByIdAsync(id);
            return _mapper.Map<AdminDto>(admin);
        }

        public async Task CreateAdminAsync(AdminDto adminDto)
        {
            var admin = _mapper.Map<Admin>(adminDto);
            await _adminRepository.CreateAsync(admin);
        }

        public async Task UpdateAdminAsync(int id, AdminDto adminDto)
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
