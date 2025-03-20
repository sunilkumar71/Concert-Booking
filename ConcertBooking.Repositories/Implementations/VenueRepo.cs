﻿using ConcertBooking.Repositories.Interfaces;
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
    public class VenueRepo:IVenueRepo
    {
        private readonly ApplicationDbContext _context;

        public VenueRepo(ApplicationDbContext context)
        {
            _context = context;
        }

     public  async Task Edit(Venue venue)
        {
            _context.Venues.Update(venue);
            await _context.SaveChangesAsync();
        }

        public async  Task<IEnumerable<Venue>>GetAll()
        {
            return _context.Venues.ToList();
        }

       public  async Task<Venue> GetById(int id)
        {
            return await  _context.Venues.FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task RemoveData(Venue venue)
        {
           _context.Venues.Remove(venue);
            await _context.SaveChangesAsync();
        }

        public async Task Save(Venue venue)
        {
             _context.Venues.Add(venue);
            await _context.SaveChangesAsync();
        }
    }
}
