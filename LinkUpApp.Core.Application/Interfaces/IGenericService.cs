namespace LinkUpApp.Core.Application.Interfaces
{
    public interface IGenericService<DtoModel, SaveDtoModel> 
        where DtoModel : class
        where SaveDtoModel : class
    {
        Task<SaveDtoModel?> AddAsync(SaveDtoModel dto);
        Task<bool> DeleteAsync(int id);
        Task<List<DtoModel>> GetAll();
        Task<DtoModel?> GetById(int id);
        Task<SaveDtoModel?> UpdateAsync(SaveDtoModel dto, int id);
    }
}