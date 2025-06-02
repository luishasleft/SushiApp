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
            .Where(o => o.Tavolo == tavolo && o.Stato != "Completato" && o.Stato != "Chiuso")
            .OrderByDescending(o => o.DataOrdine)
            .FirstOrDefaultAsync();
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
    
    public async Task AggiornaStatoOrdineAsync(Ordine ordine)
    {
        var ordineEsistente = await _dbContext.Ordini.FindAsync(ordine.Id);

        if (ordineEsistente != null)
        {
            ordineEsistente.Stato = ordine.Stato;
            _dbContext.Ordini.Update(ordineEsistente);
            await _dbContext.SaveChangesAsync();
        }
    }
    public async Task<Ordine> ChiudiEApriNuovoOrdineAsync(string tavolo)
    {
        // Recupera l'ordine attuale del tavolo
        var ordineEsistente = await _dbContext.Ordini
            .Where(o => o.Tavolo == tavolo && o.Stato != "Completato" && o.Stato != "Chiuso")
            .OrderByDescending(o => o.DataOrdine)
            .FirstOrDefaultAsync();

        // Se c'è un ordine attivo, chiudilo
        if (ordineEsistente != null)
        {
            ordineEsistente.Stato = "Completato"; // o "Chiuso"
            _dbContext.Ordini.Update(ordineEsistente);
            await _dbContext.SaveChangesAsync();
        }

        // Crea un nuovo ordine vuoto per quel tavolo
        return await CreaOrdineAsync(tavolo);
    }
    
    public async Task<string> GetStatoOrdineAsync(int ordineId)
    {
        var ordine = await _dbContext.Ordini.FindAsync(ordineId);
        return ordine?.Stato ?? "Stato non disponibile";
    }
    public async Task<List<Ordine>> GetOrdiniPerTavoloAsync(string tavolo)
    {
        return await _dbContext.Ordini
            .Include(o => o.Dettagli)
            .ThenInclude(d => d.Piatto)
            .Where(o => o.Tavolo == tavolo)
            .OrderByDescending(o => o.DataOrdine)
            .ToListAsync();
    }



}

