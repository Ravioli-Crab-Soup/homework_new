// ============================================================
// ЗАДАНИЕ 15. ИНТЕРФЕЙСЫ (ВАРИАНТ 4. ГОСТИНИЦА)
// ============================================================

// RoomPriceComparer.cs
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HotelLibrary
{
    public class RoomPriceComparer : IComparer<Room>
    {
        public int Compare(Room x, Room y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return -1;
            if (y == null) return 1;
            return y.PricePerDay.CompareTo(x.PricePerDay);
        }
    }
}

// Hotel.cs
using System.Collections;
using System.Collections.Generic;

namespace HotelLibrary
{
    public class Hotel : IEnumerable<Room>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int Count => rooms.Count;
        private List<Room> rooms;

        public Hotel(string name, string address, IEnumerable<Room> roomsCollection)
        {
            Name = name;
            Address = address;
            rooms = new List<Room>(roomsCollection);
        }

        public IEnumerator<Room> GetEnumerator()
        {
            return rooms.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

// ============================================================
// МОДУЛЬНЫЕ ТЕСТЫ ДЛЯ ЗАДАНИЯ 15
// ============================================================

using NUnit.Framework;
using System.Linq;

namespace HotelLibrary.UnitTests
{
    [TestFixture]
    public class HotelTests
    {
        private Room[] testRooms;
        private Hotel hotel;

        [SetUp]
        public void Setup()
        {
            testRooms = new Room[]
            {
                new Room(1, 2, WindowOrientation.North),
                new Room(2, 1, WindowOrientation.South),
                new Room(3, 3, WindowOrientation.West)
            };
            testRooms[0].PricePerDay = 3000;
            testRooms[0].LiberationDateTime = new System.DateTime(2026, 1, 1);
            testRooms[1].PricePerDay = 2500;
            testRooms[1].LiberationDateTime = new System.DateTime(2026, 1, 1);
            testRooms[2].PricePerDay = 4000;
            testRooms[2].LiberationDateTime = new System.DateTime(2026, 1, 1);

            hotel = new Hotel("Центральная", "ул. Ленина, 10", testRooms);
        }

        [Test]
        public void ConstructorTest()
        {
            Assert.That(hotel.Name, Is.EqualTo("Центральная"));
            Assert.That(hotel.Address, Is.EqualTo("ул. Ленина, 10"));
            Assert.That(hotel.Count, Is.EqualTo(3));
        }

        [Test]
        public void IEnumerableTest()
        {
            int i = 0;
            foreach (var room in hotel)
            {
                Assert.That(room, Is.SameAs(testRooms[i]));
                i++;
            }
            Assert.That(i, Is.EqualTo(3));
        }
    }

    [TestFixture]
    public class RoomPriceComparerTests
    {
        [Test]
        public void CompareDescendingPriceTest()
        {
            var rooms = new Room[]
            {
                new Room(1, 2, WindowOrientation.North),
                new Room(2, 1, WindowOrientation.South),
                new Room(3, 3, WindowOrientation.West)
            };
            rooms[0].PricePerDay = 3000;
            rooms[0].LiberationDateTime = new System.DateTime(2026, 1, 1);
            rooms[1].PricePerDay = 2500;
            rooms[1].LiberationDateTime = new System.DateTime(2026, 1, 1);
            rooms[2].PricePerDay = 4000;
            rooms[2].LiberationDateTime = new System.DateTime(2026, 1, 1);

            var comparer = new RoomPriceComparer();
            var sorted = rooms.OrderBy(r => r, comparer).ToArray();
            Assert.That(sorted[0].PricePerDay, Is.EqualTo(4000));
            Assert.That(sorted[1].PricePerDay, Is.EqualTo(3000));
            Assert.That(sorted[2].PricePerDay, Is.EqualTo(2500));
        }
    }
}