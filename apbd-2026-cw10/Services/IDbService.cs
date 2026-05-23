using apbd_2026_cw10.DTOs;

namespace apbd_2026_cw10.Services;

public interface IDbService
{
    Task<IEnumerable<GetPCDto>> GetAllPCsAsync();
    Task<GetPCWithComponentsDto> GetPCWithComponentsById(int id);

}