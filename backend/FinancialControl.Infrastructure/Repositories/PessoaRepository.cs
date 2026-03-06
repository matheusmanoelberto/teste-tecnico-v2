using System.Collections.Generic;
using System.Threading.Tasks;
using FinancialControl.Domain.Entities;
using FinancialControl.Domain.Repositories;
using FinancialControl.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FinancialControl.Infrastructure.Repositories;

public class PessoaRepository : IPessoaRepository
{
    private readonly ApplicationDbContext _context;

    public PessoaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Pessoa> GetByIdAsync(int id)
    {
        return await _context.Pessoas
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Pessoa>> GetAllAsync()
    {
        return await _context.Pessoas.ToListAsync();
    }

    public async Task<Pessoa> AddAsync(Pessoa pessoa)
    {
        _context.Pessoas.Add(pessoa);
        await _context.SaveChangesAsync();
        return pessoa;
    }

    public void Update(Pessoa pessoa)
    {
        _context.Pessoas.Update(pessoa);
    }

    public void Delete(Pessoa pessoa)
    {
        _context.Pessoas.Remove(pessoa);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
