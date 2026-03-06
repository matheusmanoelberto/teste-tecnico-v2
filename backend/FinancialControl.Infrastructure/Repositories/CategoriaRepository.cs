using System.Collections.Generic;
using System.Threading.Tasks;
using FinancialControl.Domain.Entities;
using FinancialControl.Domain.Repositories;
using FinancialControl.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FinancialControl.Infrastructure.Repositories;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly ApplicationDbContext _context;

    public CategoriaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Categoria> GetByIdAsync(int id)
    {
        return await _context.Categorias.FindAsync(id);
    }

    public async Task<IEnumerable<Categoria>> GetAllAsync()
    {
        return await _context.Categorias.ToListAsync();
    }

    public async Task<Categoria> AddAsync(Categoria categoria)
    {
        var result = await _context.Categorias.AddAsync(categoria);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task DeleteAsync(int id)
    {
        var categoria = await _context.Categorias.FindAsync(id);
        if (categoria != null)
        {
            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();
        }
    }
}
