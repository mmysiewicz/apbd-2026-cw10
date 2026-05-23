using System.Globalization;
using apbd_2026_cw10.Data;
using apbd_2026_cw10.DTOs;
using apbd_2026_cw10.Entities;
using Microsoft.EntityFrameworkCore;

namespace apbd_2026_cw10.Services;

public class DbService : IDbService
{
    private readonly AppDbContext _dbContext;
    
    public DbService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<GetPCDto>> GetAllPCsAsync()
    {
        var PCs = await _dbContext.PCs.Select(e => new GetPCDto()
        {
            Id = e.Id,
            Name = e.Name,
            Weight = e.Weight,
            Warranty =  e.Warranty,
            CreatedAt = e.CreatedAt,
            Stock = e.Stock
            
        }).ToListAsync(); 
        
        return PCs;
    }

    public async Task<GetPCWithComponentsDto> GetPCWithComponentsById(int id)
    {
        var result = await _dbContext.PCs
            .Where(e => e.Id == id)
            .Select(e => new GetPCWithComponentsDto
            {
                Id = e.Id,
                Name = e.Name,
                Weight = e.Weight,
                Warranty = e.Warranty,
                CreatedAt = e.CreatedAt,
                Stock = e.Stock,
                PCComponents = e.PCComponents.Select(pcc => new PCComponentsDto
                {
                    Amount = pcc.Amount,
                    Components = new ComponentsDto
                    {
                        Code = pcc.Component.Code,
                        Name = pcc.Component.Name,
                        Description = pcc.Component.Description,
                        ComponentManufacturers = new ComponentManufacturerDto
                        {
                            Id = pcc.Component.ComponentManufacturer.Id,
                            Abbreviation =  pcc.Component.ComponentManufacturer.Abbreviation,
                            FullName = pcc.Component.ComponentManufacturer.FullName,
                            FoundationDate =  pcc.Component.ComponentManufacturer.FoundationDate
                        },
                        ComponentTypes = new ComponentTypeDto
                        {   
                            Id = pcc.Component.ComponentType.Id,
                            Abbreviation = pcc.Component.ComponentType.Abbreviation,
                            Name = pcc.Component.ComponentType.Abbreviation
                        }
                    }
                }).ToList()
            }).FirstOrDefaultAsync();
        
        return result;
    }

    public async Task<PCResponseDto> CreatePCAsync(CreatePCDto createPcDto)
    {
        var PC = new PC
        {
            Name = createPcDto.Name,
            Weight = createPcDto.Weight,
            Warranty = createPcDto.Warranty,
            CreatedAt = createPcDto.CreatedAt,
            Stock = createPcDto.Stock
        };
        
        _dbContext.PCs.Add(PC);
        await _dbContext.SaveChangesAsync();

        var result = new PCResponseDto
        {
            Id = PC.Id,
            Name = PC.Name,
            Weight = PC.Weight,
            Warranty = PC.Warranty,
            CreatedAt = PC.CreatedAt,
            Stock = PC.Stock
        };

        return result;
    }


    public async Task UpdatePCAsync(int id, UpdatePCDto updatePcDto)
    {
        var existsPC =  await _dbContext.PCs.FirstOrDefaultAsync(e => e.Id == id);

        if (existsPC == null)
        {
            throw new Exception();
            
        }
        existsPC.Name = updatePcDto.Name;
        existsPC.Weight = updatePcDto.Weight;
        existsPC.Warranty = updatePcDto.Warranty;
        existsPC.CreatedAt = updatePcDto.CreatedAt;
        existsPC.Stock = updatePcDto.Stock;
        
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeletePCAsync(int id)
    {
        var PC = await _dbContext.PCs.FirstOrDefaultAsync(e => e.Id == id);

        if (PC == null)
        {
            throw new Exception();
        }
        
        _dbContext.PCs.Remove(PC);
        await _dbContext.SaveChangesAsync();
    }
    
}