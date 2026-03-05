using AutoMapper;
using LinkUpApp.Core.Application.Interfaces;
using LinkUpApp.Infraesctructure.Persistence.Repositories;

namespace LinkUpApp.Core.Application.Services
{
    public class GenericService<Entity, DtoModel, SaveDtoModel> : IGenericService<DtoModel,SaveDtoModel> where Entity : class
        where DtoModel : class
        where SaveDtoModel : class
    {
        private readonly IGenericRepository<Entity> _repository;
        private readonly IMapper _mapper;

        public GenericService(IGenericRepository<Entity> repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public virtual async Task<SaveDtoModel?> AddAsync(SaveDtoModel dto)
        {
            try
            {
                Entity entity = _mapper.Map<Entity>(dto);
                Entity? returnEntity = await _repository.AddAsync(entity);
                if (returnEntity == null)
                {
                    return null;
                }

                return _mapper.Map<SaveDtoModel>(returnEntity);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public virtual async Task<SaveDtoModel?> UpdateAsync(SaveDtoModel dto, int id)
        {
            try
            {
                Entity entity = _mapper.Map<Entity>(dto);
                Entity? returnEntity = await _repository.UpdateAsync(id, entity);
                if (returnEntity == null)
                {
                    return null;
                }

                return _mapper.Map<SaveDtoModel>(returnEntity);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            try
            {
                await _repository.DeleteAsync(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public virtual async Task<DtoModel?> GetById(int id)
        {
            try
            {
                var entity = await _repository.GetById(id);
                if (entity == null)
                {
                    return null;
                }

                DtoModel dto = _mapper.Map<DtoModel>(entity);
                return dto;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public virtual async Task<List<DtoModel>> GetAll()
        {
            try
            {
                var listEntities = await _repository.GetAllList();
                var listEntitiesDtos = _mapper.Map<List<DtoModel>>(listEntities);

                return listEntitiesDtos;
            }
            catch (Exception)
            {
                return [];
            }
        }
    }
}
