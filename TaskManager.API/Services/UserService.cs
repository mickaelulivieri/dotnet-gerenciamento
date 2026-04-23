using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TaskManager.API.DTOs;
using TaskManager.API.DTOs.user;
using TaskManager.API.Mappings;
using TaskManager.API.model;
using TaskManager.API.Repositories;

namespace TaskManager.API.Services
{

    // INDEPENDENCY INJECTION
     public class UserService
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // METHODS

        // GET ALL
        public async Task<List<UserResponseDTO>> GetAllAsync()
        {
            var users = await _repo.GetAllAsync();
            return _mapper.Map<List<UserResponseDTO>>(users);
        }

        // GET BY ID
        public async Task<UserResponseDTO?> GetByIdAsync(int id)
        {
            var user = await _repo.GetByIdAsync(id);
            if (user == null) return null;

            return _mapper.Map<UserResponseDTO>(user);
        }

        // CREATE
        public async Task<UserResponseDTO> CreateAsync(UserCreateDTO dto)
        {
            var user = _mapper.Map<User>(dto);

            await _repo.AddAsync(user);
            await _repo.SaveAsync();

            return _mapper.Map<UserResponseDTO>(user);
        }

        // DELETE
        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _repo.GetByIdAsync(id);
            if (user == null) return false;

            _repo.Delete(user);
            await _repo.SaveAsync();

            return true;
        }

        // UPDATE
        public async Task<bool> UpdateAsync(int id, UserUpdateDTO dto)
        {
            var user = await _repo.GetByIdAsync(id);
            if (user == null) return false;

            _mapper.Map(dto, user);
            await _repo.SaveAsync();

            return true;
        }
    }
}