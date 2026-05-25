using Tut7Solution.DTOs;

namespace Tut7Solution.Services;

public interface IPcService
{
    Task<List<PcResponseDto>> GetAllAsync();
    Task<PcComponentsResponseDto?> GetComponentsAsync(int id);
    Task<PcResponseDto> CreateAsync(PcCreateDto dto);
    Task<bool> UpdateAsync(int id, PcUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}
