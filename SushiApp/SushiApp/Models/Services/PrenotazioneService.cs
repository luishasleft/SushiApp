using Microsoft.EntityFrameworkCore;
using SushiApp.Models.Entities;

namespace SushiApp.Models.Services
{
    public class PrenotazioneService
    {
        private readonly SushiDbContext _context;

        public PrenotazioneService(SushiDbContext context)
        {
            _context = context;
        }

        public async Task<List<Prenotazione>> GetTutteLePrenotazioniAsync()
        {
            var lista = await _context.Prenotazioni
                .OrderBy(p => p.DataPrenotazione)
                .ToListAsync();

            // Ordina in memoria per OrarioPrenotazione (TimeSpan) dopo aver recuperato i dati
            return lista.OrderBy(p => p.OrarioPrenotazione).ToList();
        }

        public async Task<Prenotazione?> GetPrenotazioneByIdAsync(int id)
        {
            return await _context.Prenotazioni.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<bool> CreaPrenotazioneAsync(Prenotazione prenotazione)
        {
            try
            {
                _context.Prenotazioni.Add(prenotazione);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> AggiornaStatoPrenotazioneAsync(Prenotazione prenotazione)
        {
            try
            {
                _context.Prenotazioni.Update(prenotazione);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> EliminaPrenotazioneAsync(int id)
        {
            try
            {
                var prenotazione = await GetPrenotazioneByIdAsync(id);
                if (prenotazione != null)
                {
                    _context.Prenotazioni.Remove(prenotazione);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<Prenotazione>> GetPrenotazioniPerDataAsync(DateTime data)
        {
            return await _context.Prenotazioni
                .Where(p => p.DataPrenotazione.Date == data.Date)
                .OrderBy(p => p.OrarioPrenotazione)
                .ToListAsync();
        }

        public async Task<bool> ControllaDisponibilitaAsync(DateTime data, TimeSpan orario)
        {
            // Controlla se ci sono già 5 prenotazioni nello stesso orario (esempio di logica business)
            var prenotazioniStessoOrario = await _context.Prenotazioni
                .Where(p => p.DataPrenotazione.Date == data.Date && 
                           p.OrarioPrenotazione == orario &&
                           p.Stato != "Annullata")
                .CountAsync();

            return prenotazioniStessoOrario < 5;
        }
    }
}