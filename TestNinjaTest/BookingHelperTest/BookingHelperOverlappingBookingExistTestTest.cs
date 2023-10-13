using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TestNinja.Mocking;

namespace TestNinjaTest.BookingHelperTest
{
    [TestFixture]
    public class BookingHelperTest
    {
        private Mock<IBookingRepository> _bookingRepositoryMock;
        private Booking _existingBooking;

        [SetUp]
        public void SetUp()
        {
            _existingBooking = new Booking
            {
                Reference = "b",
                ArrivalDate = DepartOn(2023, 10, 1),
                DepartureDate = DepartOn(2023, 10, 10)
            };

            _bookingRepositoryMock = new Mock<IBookingRepository>();

            _bookingRepositoryMock.Setup(b => b.GetActiveBookings(1))
                .Returns(new List<Booking>
                {
                    _existingBooking
                }.AsQueryable());
        }

        [Test]
        public void OverlappingBookingsExist_WhenBookingIsCancelled_ReturnsNullValue()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                Status = "Canceled"
            }, new BookingRepository());
            Assert.That(result, Is.EqualTo(string.Empty));
        }

        [Test]
        public void OverlappingBookingsExist_BookingStartsAndFinishesInTheMiddleOfAnExistingBooking_ReturnExistingBookReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                Reference = "a",
                Status = "Active",
                ArrivalDate = After(_existingBooking.ArrivalDate),
                DepartureDate = Before(_existingBooking.DepartureDate)
            }, _bookingRepositoryMock.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }


        [Test]
        public void OverlappingBookingsExist_WhenBookingStartAndFinishesAfterTheDepartureDate_ReturnEmptyString()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                Reference = "a",
                Status = "Active",
                ArrivalDate = After(_existingBooking.DepartureDate, 2),
                DepartureDate = After(_existingBooking.DepartureDate, 4)
            }, _bookingRepositoryMock.Object);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void OverlappingBookingExist_BookingStartsBeforeAndFinishesAfterAnExistingBooking_ReturnExistingBookingReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                Reference = "a",
                Status = "Active",
                ArrivalDate = Before(_existingBooking.ArrivalDate),
                DepartureDate = After(_existingBooking.DepartureDate)
            }, _bookingRepositoryMock.Object);

            Assert.That(result, Is.EqualTo("b"));
        }

        [Test]
        public void OverlappingBookingExist_BookingIsCancelledAndOverlaps_ReturnExistingBookingReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                Reference = "a",
                Status = "Cancelled",
                ArrivalDate = Before(_existingBooking.ArrivalDate),
                DepartureDate = After(_existingBooking.DepartureDate)
            }, _bookingRepositoryMock.Object);

            Assert.That(result, Is.Empty);
        }

        private DateTime ArriveOn(int year, int month, int day)
        {
            return new DateTime(year, month, day);
        }

        private DateTime DepartOn(int year, int month, int day)
        {
            return new DateTime(year, month, day);
        }

        private DateTime Before(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(-days);
        }

        private DateTime After(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(days);
        }


    }
}
