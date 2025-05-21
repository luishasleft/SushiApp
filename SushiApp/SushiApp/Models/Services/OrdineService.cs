using SushiApp.Models.Entities;
using Microsoft.EntityFrameworkCore;
namespace SushiApp.Models.Services;
public class OrdineService
{
    
    private readonly SushiDbContext _dbContext;

    public OrdineService(SushiDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Ordine?> GetOrdinePerTavoloAsync(string tavolo)
    {
        return await _dbContext.Ordini
            .Include(o => o.Dettagli)
            .ThenInclude(d => d.Piatto)
            .FirstOrDefaultAsync(o => o.Tavolo == tavolo);
    }

    public async Task<Ordine> CreaOrdineAsync(string tavolo)
    {
        var nuovoOrdine = new Ordine
        {
            Tavolo = tavolo,
            DataOrdine = DateTime.Now,
            Stato = "Ricevuto",
            Dettagli = new List<OrdineDettaglio>()
        };

        _dbContext.Ordini.Add(nuovoOrdine);
        await _dbContext.SaveChangesAsync();

        return nuovoOrdine;
    }

    public async Task AggiungiProdottoAsync(string tavolo, int piattoId)
    {
        var ordine = await GetOrdinePerTavoloAsync(tavolo);
        if (ordine == null)
        {
            ordine = await CreaOrdineAsync(tavolo);
        }

        var dettaglio = ordine.Dettagli.FirstOrDefault(d => d.PiattoId == piattoId);
        if (dettaglio != null)
        {
            dettaglio.Quantita++;
            _dbContext.OrdineDettagli.Update(dettaglio);
        }
        else
        {
            dettaglio = new OrdineDettaglio
            {
                PiattoId = piattoId,
                OrdineId = ordine.Id,
                Quantita = 1
            };
            _dbContext.OrdineDettagli.Add(dettaglio);
        }

        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<Ordine>> GetTuttiGliOrdiniAsync()
    {
        return await _dbContext.Ordini
            .Include(o => o.Dettagli)
            .ThenInclude(d => d.Piatto)
            .ToListAsync();
    }
}