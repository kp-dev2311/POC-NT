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
    public class StudentService
    {
        private readonly IRepository<Student> _studentRepository;
        private readonly IMapper _mapper;

        public StudentService(IRepository<Student> studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StudentDto>> GetStudentsAsync()
        {
            var students = await _studentRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<StudentDto>>(students);
        }

        public async Task<StudentDto> GetByIdAsync(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            return _mapper.Map<StudentDto>(student);
        }

        public async Task CreateStudentAsync(StudentDto studentDto)
        {
            var newStudent = _mapper.Map<Student>(studentDto);
            await _studentRepository.CreateAsync(newStudent);
        }

        public async Task UpdateStudentAsync(int id, StudentDto studentDto)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            if(student != null)
            {
                _mapper.Map(studentDto, student);
                await _studentRepository.UpdateAsync(student);
            }
           
        }

        public async Task DeleteStudentAsync(int id)
        {
            await _studentRepository.DeleteAsync(id);
        }
    }
}
