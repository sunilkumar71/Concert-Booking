﻿using ConcertBooking.Entities;
using ConcertBooking.Repositories.Interfaces;
using ConcertBooking.WebHost.Models;
using ConcertBooking.WebHost.ViewModels.HomePageViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace ConcertBooking.WebHost.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConcertRepo _concertRepo;
        private readonly ITicketRepo _ticketRepo;
        private readonly IBookingRepo _bookingRepo;
       

        public HomeController(ILogger<HomeController> logger, IConcertRepo concertRepo, ITicketRepo ticketRepo, IBookingRepo bookingRepo)
        {
            _logger = logger;
            _concertRepo = concertRepo;
            _ticketRepo = ticketRepo;
            _bookingRepo = bookingRepo;
        }

        public async Task<IActionResult> Index()
        {
            DateTime today= DateTime.Today;
            var concerts = await _concertRepo.GetAll();
            var vm = concerts.Where(x => x.DateTime.Date >= today)
                             .Select(x => new HomeConcertViewModel
            {
                ConcertId = x.Id,
                ConcertName = x.Name,
                ArtistName = x.Artist.Name,
                ConcertImage = x.ImageUrl,
                Description = x.Description.Length>100?x.Description.Substring(0,100):x.Description

            }).ToList();
            return View(vm);
        }

        public async Task<IActionResult> Details(int id)
        {
            var concert= await _concertRepo.GetById(id);   
            if(concert == null)
            {
                return View("Error");
            }

            var vm = new HomeConcertDetailsViewModel
            {
                ConcertId = concert.Id,
                ConcertName = concert.Name,
                Description = concert.Description,
                ConcertDateTime = concert.DateTime,
                ArtistName = concert.Artist.Name,
                ArtistImage = concert.Artist.ImageUrl,
                VenueName = concert.Venue.Name,
                VenueAddress = concert.Venue.Address,
                ConcertImage = concert.ImageUrl,

            };
           return View(vm);
        }
        [Authorize]
        public async Task<IActionResult> AvailabelTickets(int id)
        {
            var concert = await _concertRepo.GetById(id);
            if (concert == null)
            {
                return NotFound();
            }

         
            var allSeats = Enumerable.Range(1,concert.Venue.SeatCapacity).ToList(); //1,2,3,4
            var bookedTicket = await _ticketRepo.GetBookedTickets(concert.Id);//2,3
           
            var availableSeats=allSeats.Except(bookedTicket).ToList();

            var viewModel = new AvailabelTicketViewModel
            {
                ConcertId = concert.Id,
                ConcertName = concert.Name,
                AvailabelSeats = availableSeats,
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> BookTickets(int ConcertId,List<int> selectSeats)
        {
            if (selectSeats == null || selectSeats.Count == 0)
            {
                ModelState.AddModelError(" ", "No Seats Selected");
                return RedirectToAction("AvailabelTickets", new { id = ConcertId });
            }
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim=claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var userId = claim.Value;

            var newBooking = new Booking
            {
                ConcertId = ConcertId,
                BookingDate = DateTime.Now,
                UserId = userId,

            };

           foreach(var seatNumber in selectSeats)
            {
                newBooking.Tickets.Add(new Ticket
                {
                    SeatNumber = seatNumber,
                    IsBooked = true,
                });
            }

            await _bookingRepo.AddBooking(newBooking);
            return RedirectToAction("Index");

        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
