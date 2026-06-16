// ============================================================
// ЗАДАНИЕ 13. БИБЛИОТЕКА КЛАССОВ (ВАРИАНТ 4. ГОСТИНИЦА)
// ============================================================

// WindowOrientation.cs
using System;

namespace HotelLibrary
{
    public enum WindowOrientation
    {
        North,
        South,
        West,
        East
    }
}

// Room.cs
using System;

namespace HotelLibrary
{
    public class Room
    {
        public int Number { get; private set; }
        public int BedsCount { get; set; }
        public WindowOrientation Orientation { get; set; }
        public double PricePerDay { get; set; }
        public DateTime LiberationDateTime { get; set; }

        public Room(int number, int bedsCount, WindowOrientation orientation)
        {
            Number = number;
            BedsCount = bedsCount;
            Orientation = orientation;
        }

        public virtual string[] GetInfo()
        {
            return new string[]
            {
                $"Номер: {Number}",
                $"Количество кроватей: {BedsCount}",
                $"Ориентация окон: {Orientation}",
                $"Цена за сутки: {PricePerDay:F2} руб.",
                $"Дата освобождения: {LiberationDateTime:dd.MM.yyyy HH:mm}"
            };
        }
    }
}

// ============================================================
// МОДУЛЬНЫЕ ТЕСТЫ ДЛЯ КЛАССА ROOM (ЗАДАНИЕ 13)
// ============================================================

using NUnit.Framework;
using System;

namespace HotelLibrary.UnitTests
{
    [TestFixture]
    public class RoomTests
    {
        private Room GetTestRoom()
        {
            var room = new Room(101, 2, WindowOrientation.South);
            room.PricePerDay = 2500.50;
            room.LiberationDateTime = new DateTime(2025, 12, 25, 12, 0, 0);
            return room;
        }

        [Test]
        public void ConstructorTest()
        {
            var room = GetTestRoom();
            Assert.That(room.Number, Is.EqualTo(101));
            Assert.That(room.BedsCount, Is.EqualTo(2));
            Assert.That(room.Orientation, Is.EqualTo(WindowOrientation.South));
        }

        [Test]
        public void GetInfoTest()
        {
            var room = GetTestRoom();
            var info = room.GetInfo();
            Assert.That(info.Length, Is.EqualTo(5));
            Assert.That(info[0], Is.EqualTo("Номер: 101"));
            Assert.That(info[1], Is.EqualTo("Количество кроватей: 2"));
            Assert.That(info[2], Is.EqualTo("Ориентация окон: South"));
            Assert.That(info[3], Is.EqualTo("Цена за сутки: 2500,50 руб."));
            Assert.That(info[4], Is.EqualTo("Дата освобождения: 25.12.2025 12:00"));
        }
    }
} 