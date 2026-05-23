using apbd_2026_cw10.Data;
using apbd_2026_cw10.DTOs;
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
    
    
    
    
    
    
    
    
    
}