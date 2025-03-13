using AutoMapper;
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
    public class TeacherService
    {
        private readonly IRepository<Teacher> _teacherRepository;
        private readonly IMapper _mapper;

        public TeacherService(IRepository<Teacher> teacherRepository, IMapper mapper)
        {
            _teacherRepository = teacherRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TeacherDto>> GetTeachersAsync()
        {
            var teachers = await _teacherRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TeacherDto>>(teachers);
        }

        public async Task<TeacherDto> GetByIdAsync(int id)
        {
            var teacher = await _teacherRepository.GetByIdAsync(id);
            return _mapper.Map<TeacherDto>(teacher);
        }

        public async Task CreateTeacherAsync(TeacherDto teacherDto)
        {
            var newTeacher = _mapper.Map<Teacher>(teacherDto);
            await _teacherRepository.CreateAsync(newTeacher);
        }

        public async Task UpdateTeacherAsync(int id, TeacherDto teacherDto)
        {
            var teacher = await _teacherRepository.GetByIdAsync(id);
            if(teacher != null)
            {
                _mapper.Map(teacherDto, teacher);
                await _teacherRepository.UpdateAsync(teacher);
            }
        }

        public async Task DeleteTeacherAsync(int id)
        {
            await _teacherRepository.DeleteAsync(id);
        }
    }
}
