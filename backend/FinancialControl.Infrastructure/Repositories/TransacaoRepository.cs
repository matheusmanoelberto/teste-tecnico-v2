using System.Collections.Generic;
using System.Threading.Tasks;
using FinancialControl.Domain.Entities;
using FinancialControl.Domain.Repositories;
using FinancialControl.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FinancialControl.Infrastructure.Repositories;

public class TransacaoRepository : ITransacaoRepository
{
    private readonly ApplicationDbContext _context;

    public TransacaoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Transacao>> GetAllAsync()
    {
        return await _context.Transacoes
            .Include(t => t.Pessoa)
            .Include(t => t.Categoria)
            .ToListAsync();
    }

    public async Task<Transacao> GetByIdAsync(int id)
    {
        return await _context.Transacoes.FindAsync(id);
    }

    public async Task<Transacao> AddAsync(Transacao transacao)
    {
        var result = await _context.Transacoes.AddAsync(transacao);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task DeleteAsync(int id)
    {
        var transacao = await _context.Transacoes.FindAsync(id);
        if (transacao != null)
        {
            _context.Transacoes.Remove(transacao);
            await _context.SaveChangesAsync();
        }
    }
}
