using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TaskManager.API.DTOs.Task;
using TaskManager.API.Exceptions;
using TaskManager.API.model;
using TaskManager.API.Repositories;

namespace TaskManager.API.Services
{
    public class TaskService
    {
        private readonly ITaskRepository _repo;
        private readonly IMapper _mapper;

        public TaskService(ITaskRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // GET ALL
        public async Task<List<TaskResponseDTO>> GetAllAsync()
        {
            try 
            {
                var tasks = await _repo.GetAllAsync();
                return _mapper.Map<List<TaskResponseDTO>>(tasks);
            }
            catch (Exception ex)
            {
                throw new BadRequestException($"Erro ao carregar tarefas: {ex.Message}");
            }
        }
                // GET BY ID

        public async Task<TaskResponseDTO?> GetByIdAsync(int id)
        {
            try
            {
                var task = await _repo.GetByIdAsync(id);
                if (task == null)
                    throw new NotFoundException($"Tarefa com ID {id} não encontrada.");

                return _mapper.Map<TaskResponseDTO>(task);
            }
            catch (NotFoundException) { throw; }
            catch (Exception ex)
            {
                throw new BadRequestException($"Erro ao buscar tarefa: {ex.Message}");
            }
        }

        // GET USER ID
        public async Task<List<TaskResponseDTO>> GetByUserIdAsync(int userId)
        {
            try
            {
                var tasks = await _repo.GetByUserIdAsync(userId);
                return _mapper.Map<List<TaskResponseDTO>>(tasks);
            }
            catch (Exception ex)
            {
                throw new BadRequestException($"Erro ao buscar tarefas do usuário: {ex.Message}");
            }
        }

        // CREATE

        public async Task<TaskResponseDTO> CreateAsync(TaskCreateDTO dto)
        {
            try
            {
                var task = _mapper.Map<TaskItem>(dto);
                await _repo.AddAsync(task);
                await _repo.SaveAsync();
                return _mapper.Map<TaskResponseDTO>(task);
            }
            catch (Exception ex)
            {
                throw new BadRequestException($"Erro ao criar nova tarefa: {ex.Message}");
            }
        }
                // UPDATE

        public async Task<bool> UpdateAsync(int id, TaskUpdateDTO dto)
        {
            try
            {
                var task = await _repo.GetByIdAsync(id);
                if (task == null)
                    throw new NotFoundException($"Não foi possível atualizar. Tarefa {id} não existe.");

                _mapper.Map(dto, task);
                await _repo.SaveAsync();
                return true;
            }
            catch (NotFoundException) { throw; }
            catch (Exception ex)
            {
                throw new BadRequestException($"Erro ao atualizar tarefa: {ex.Message}");
            }
        }

        // DELETE
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var task = await _repo.GetByIdAsync(id);
                if (task == null)
                    throw new NotFoundException($"Não foi possível deletar. Tarefa {id} não encontrada.");

                _repo.Delete(task);
                await _repo.SaveAsync();
                return true;
            }
            catch (NotFoundException) { throw; }
            catch (Exception ex)
            {
                throw new BadRequestException($"Erro ao deletar tarefa: {ex.Message}");
            }
        }
    }
}