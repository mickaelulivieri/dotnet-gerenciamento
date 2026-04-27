using AutoMapper;
using TaskManager.API.DTOs;
using TaskManager.API.DTOs.user;
using TaskManager.API.Exceptions;
using TaskManager.API.model;
using TaskManager.API.Repositories;

namespace TaskManager.API.Services
{
    public class UserService
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // GET ALL
        public async Task<List<UserResponseDTO>> GetAllAsync()
        {
            try
            {
                var users = await _repo.GetAllAsync();
                return _mapper.Map<List<UserResponseDTO>>(users);
            }
            catch (Exception ex)
            {
                throw new BadRequestException($"Erro ao buscar usuários: {ex.Message}");
            }
        }

        // GET BY ID
        public async Task<UserResponseDTO?> GetByIdAsync(int id)
        {
            try
            {
                var user = await _repo.GetByIdAsync(id);

                if (user == null)
                    throw new NotFoundException($"Usuário {id} não encontrado.");

                return _mapper.Map<UserResponseDTO>(user);
            }
            catch (NotFoundException) { throw; }
            catch (Exception ex)
            {
                throw new BadRequestException($"Erro ao buscar usuário: {ex.Message}");
            }
        }

        // CREATE
        public async Task<UserResponseDTO> CreateAsync(UserCreateDTO dto)
        {
            try
            {
                var user = _mapper.Map<User>(dto);

                await _repo.AddAsync(user);
                await _repo.SaveAsync();

                return _mapper.Map<UserResponseDTO>(user);
            }
            catch (Exception ex)
            {
                throw new BadRequestException($"Erro ao criar usuário: {ex.Message}");
            }
        }

        // DELETE
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var user = await _repo.GetByIdAsync(id);

                if (user == null)
                    throw new NotFoundException($"Usuário {id} não encontrado.");

                _repo.Delete(user);
                await _repo.SaveAsync();

                return true;
            }
            catch (NotFoundException) { throw; }
            catch (Exception ex)
            {
                throw new BadRequestException($"Erro ao deletar usuário: {ex.Message}");
            }
        }

        // UPDATE
        public async Task<bool> UpdateAsync(int id, UserUpdateDTO dto)
        {
            try
            {
                var user = await _repo.GetByIdAsync(id);

                if (user == null)
                    throw new NotFoundException($"Usuário {id} não encontrado.");

                _mapper.Map(dto, user);
                await _repo.SaveAsync();

                return true;
            }
            catch (NotFoundException) { throw; }
            catch (Exception ex)
            {
                throw new BadRequestException($"Erro ao atualizar usuário: {ex.Message}");
            }
        }
    }
}