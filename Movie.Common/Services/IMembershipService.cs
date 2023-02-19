using Movie.Common.DTO;

namespace Movie.Common.Services
{
    public interface IMembershipService
    {
        Task<DirectorDTO> GetDirectorAsync(int? id);
        Task<List<DirectorDTO>> GetDirectorAsync();
       
    }
}