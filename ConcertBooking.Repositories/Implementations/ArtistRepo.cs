using ConcertBooking.Repositories.Interfaces;
using ConcertBooking.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnologyKeeda.ConcertBooking.Repositories;

namespace ConcertBooking.Repositories.Implementations
{
    public class ArtistRepo:IArtistRepo
    {
         private readonly ApplicationDbContext _context;

        public ArtistRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Artist>> GetAll()
        {
            return await _context.Artists.ToListAsync();
        }

        public async Task<Artist> GetById(int id)
        {
            return await _context.Artists.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task Save(Artist artist)
        {
            _context.Artists.Add(artist);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(Artist artist)
        {
            _context.Artists.Update(artist);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveData(Artist artist)
        {
            _context.Artists.Remove(artist);
            await _context.SaveChangesAsync();
        }
    }
}
