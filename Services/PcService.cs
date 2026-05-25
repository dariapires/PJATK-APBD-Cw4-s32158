using Microsoft.EntityFrameworkCore;
using Tut7Solution.Data;
using Tut7Solution.DTOs;
using Tut7Solution.Models;

namespace Tut7Solution.Services;

public class PcService : IPcService
{
    private readonly AppDbContext _context;

    public PcService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<PcResponseDto>> GetAllAsync()
    {
        return await _context.PCs
            .Select(pc => new PcResponseDto
            {
                Id = pc.Id,
                Name = pc.Name,
                Weight = pc.Weight,
                Warranty = pc.Warranty,
                CreatedAt = pc.CreatedAt,
                Stock = pc.Stock
            })
            .ToListAsync();
    }

    public async Task<PcComponentsResponseDto?> GetComponentsAsync(int id)
    {
        return await _context.PCs
            .Where(pc => pc.Id == id)
            .Select(pc => new PcComponentsResponseDto
            {
                Id = pc.Id,
                Name = pc.Name,
                Weight = pc.Weight,
                Warranty = pc.Warranty,
                CreatedAt = pc.CreatedAt,
                Stock = pc.Stock,
                Components = pc.PCComponents.Select(pcComponent => new PcComponentDto
                {
                    Amount = pcComponent.Amount,
                    Component = new ComponentDto
                    {
                        Code = pcComponent.Component.Code,
                        Name = pcComponent.Component.Name,
                        Description = pcComponent.Component.Description,
                        Manufacturer = new ManufacturerDto
                        {
                            Id = pcComponent.Component.Manufacturer.Id,
                            Abbreviation = pcComponent.Component.Manufacturer.Abbreviation,
                            FullName = pcComponent.Component.Manufacturer.FullName,
                            FoundationDate = pcComponent.Component.Manufacturer.FoundationDate
                        },
                        Type = new ComponentTypeDto
                        {
                            Id = pcComponent.Component.Type.Id,
                            Abbreviation = pcComponent.Component.Type.Abbreviation,
                            Name = pcComponent.Component.Type.Name
                        }
                    }
                }).ToList()
            })
            .FirstOrDefaultAsync();
    }

    public async Task<PcResponseDto> CreateAsync(PcCreateDto dto)
    {
        var pc = new PC
        {
            Name = dto.Name,
            Weight = dto.Weight,
            Warranty = dto.Warranty,
            CreatedAt = dto.CreatedAt,
            Stock = dto.Stock
        };

        _context.PCs.Add(pc);
        await _context.SaveChangesAsync();

        return MapToResponse(pc);
    }

    public async Task<bool> UpdateAsync(int id, PcUpdateDto dto)
    {
        var pc = await _context.PCs.FindAsync(id);
        if (pc == null)
        {
            return false;
        }

        pc.Name = dto.Name;
        pc.Weight = dto.Weight;
        pc.Warranty = dto.Warranty;
        pc.CreatedAt = dto.CreatedAt;
        pc.Stock = dto.Stock;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var pc = await _context.PCs.FindAsync(id);
        if (pc == null)
        {
            return false;
        }

        _context.PCs.Remove(pc);
        await _context.SaveChangesAsync();
        return true;
    }

    private static PcResponseDto MapToResponse(PC pc)
    {
        return new PcResponseDto
        {
            Id = pc.Id,
            Name = pc.Name,
            Weight = pc.Weight,
            Warranty = pc.Warranty,
            CreatedAt = pc.CreatedAt,
            Stock = pc.Stock
        };
    }
}
