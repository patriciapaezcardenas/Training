using System;
using System.Collections.Generic;
using System.Linq;
using TestNinja.HousekeeperHelper;

namespace TestNinja.Mocking
{
    public static class BookingHelper
    {
        public static string OverlappingBookingsExist(Booking booking, IBookingRepository bookingRepository)
        {
            if (booking.Status == "Cancelled")
                return string.Empty;

            var bookings = bookingRepository.GetActiveBookings(booking.Id);

            if (bookings is null || !bookings.Any())
            {
                return string.Empty;
            }

            //bool overlap = tStartA < tEndB && tStartB < tEndA;

            var overlappingBooking =
                bookings.FirstOrDefault(
                    b =>
                        booking.ArrivalDate < b.DepartureDate && b.ArrivalDate < booking.DepartureDate);

            return overlappingBooking == null ? string.Empty : overlappingBooking.Reference;
        }
    }



    public class UnitOfWork : IUnitOfWork
    {
        public IQueryable<T> Query<T>()
        {
            return new List<T>().AsQueryable();
        }
    }

    public class Booking
    {
        public string Status { get; set; }
        public int Id { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public string Reference { get; set; }
    }


    public interface IBookingRepository
    {
        IQueryable<Booking> GetActiveBookings(int excludeBookingId);
    }

    public class BookingRepository : IBookingRepository
    {
        private readonly UnitOfWork _unitOfWork;

        public BookingRepository()
        {
            _unitOfWork = new UnitOfWork();
        }

        public IQueryable<Booking> GetActiveBookings(int excludeBookingId)
        {
            var query = _unitOfWork.Query<Booking>().Where(b => b.Status != "Cancelled");

            if (excludeBookingId != 0)
            {
                query = query.Where(q => q.Id != excludeBookingId);
            }

            return query;
        }
    }
}