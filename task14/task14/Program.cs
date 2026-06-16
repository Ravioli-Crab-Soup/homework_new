// ============================================================
// ЗАДАНИЕ 14. НАСЛЕДОВАНИЕ И ПОЛИМОРФИЗМ (ВАРИАНТ 4. ГОСТИНИЦА)
// ============================================================

// SemiLux.cs
using System;

namespace HotelLibrary
{
    public class SemiLux : Room
    {
        public string ExtraAmenities { get; set; }

        public SemiLux(int number, int bedsCount, WindowOrientation orientation, double pricePerDay, string liberationDateTime, string extraAmenities)
            : base(number, bedsCount, orientation)
        {
            PricePerDay = pricePerDay;
            if (!DateTime.TryParse(liberationDateTime, out DateTime parsed))
                throw new ArgumentException("Неверный формат даты освобождения");
            LiberationDateTime = parsed;
            ExtraAmenities = extraAmenities;
        }

        public override string[] GetInfo()
        {
            var baseInfo = base.GetInfo();
            var newInfo = new string[baseInfo.Length + 1];
            baseInfo.CopyTo(newInfo, 0);
            newInfo[baseInfo.Length] = $"Дополнительные удобства: {ExtraAmenities}";
            return newInfo;
        }
    }
}

// Lux.cs
using System;

namespace HotelLibrary
{
    public class Lux : SemiLux
    {
        public int RoomsCount { get; set; }
        public int MinRentDays { get; set; }

        public Lux(int number, int bedsCount, WindowOrientation orientation, double pricePerDay, string liberationDateTime, string extraAmenities, int roomsCount, int minRentDays)
            : base(number, bedsCount, orientation, pricePerDay, liberationDateTime, extraAmenities)
        {
            RoomsCount = roomsCount;
            MinRentDays = minRentDays;
        }

        public override string[] GetInfo()
        {
            var baseInfo = base.GetInfo();
            var newInfo = new string[baseInfo.Length + 2];
            baseInfo.CopyTo(newInfo, 0);
            newInfo[baseInfo.Length] = $"Количество комнат: {RoomsCount}";
            newInfo[baseInfo.Length + 1] = $"Минимальный срок сдачи: {MinRentDays} дней";
            return newInfo;
        }
    }
}

// ============================================================
// МОДУЛЬНЫЕ ТЕСТЫ ДЛЯ КЛАССОВ SEMILUX И LUX (ЗАДАНИЕ 14)
// ============================================================

using NUnit.Framework;
using System;

namespace HotelLibrary.UnitTests
{
    [TestFixture]
    public class SemiLuxTests
    {
        private SemiLux GetTestSemiLux()
        {
            return new SemiLux(202, 3, WindowOrientation.East, 5000, "10.11.2025 14:00", "Кондиционер, мини-бар");
        }

        [Test]
        public void ConstructorTest()
        {
            var sl = GetTestSemiLux();
            Assert.That(sl.Number, Is.EqualTo(202));
            Assert.That(sl.BedsCount, Is.EqualTo(3));
            Assert.That(sl.Orientation, Is.EqualTo(WindowOrientation.East));
            Assert.That(sl.PricePerDay, Is.EqualTo(5000));
            Assert.That(sl.LiberationDateTime, Is.EqualTo(new DateTime(2025, 11, 10, 14, 0, 0)));
            Assert.That(sl.ExtraAmenities, Is.EqualTo("Кондиционер, мини-бар"));
        }

        [Test]
        public void GetInfoTest()
        {
            var sl = GetTestSemiLux();
            var info = sl.GetInfo();
            Assert.That(info.Length, Is.EqualTo(6));
            Assert.That(info[0], Is.EqualTo("Номер: 202"));
            Assert.That(info[1], Is.EqualTo("Количество кроватей: 3"));
            Assert.That(info[2], Is.EqualTo("Ориентация окон: East"));
            Assert.That(info[3], Is.EqualTo("Цена за сутки: 5000,00 руб."));
            Assert.That(info[4], Is.EqualTo("Дата освобождения: 10.11.2025 14:00"));
            Assert.That(info[5], Is.EqualTo("Дополнительные удобства: Кондиционер, мини-бар"));
        }
    }

    [TestFixture]
    public class LuxTests
    {
        private Lux GetTestLux()
        {
            return new Lux(303, 4, WindowOrientation.West, 12000, "05.05.2026 18:00", "Сауна, бассейн", 3, 7);
        }

        [Test]
        public void ConstructorTest()
        {
            var lux = GetTestLux();
            Assert.That(lux.Number, Is.EqualTo(303));
            Assert.That(lux.BedsCount, Is.EqualTo(4));
            Assert.That(lux.Orientation, Is.EqualTo(WindowOrientation.West));
            Assert.That(lux.PricePerDay, Is.EqualTo(12000));
            Assert.That(lux.LiberationDateTime, Is.EqualTo(new DateTime(2026, 5, 5, 18, 0, 0)));
            Assert.That(lux.ExtraAmenities, Is.EqualTo("Сауна, бассейн"));
            Assert.That(lux.RoomsCount, Is.EqualTo(3));
            Assert.That(lux.MinRentDays, Is.EqualTo(7));
        }

        [Test]
        public void GetInfoTest()
        {
            var lux = GetTestLux();
            var info = lux.GetInfo();
            Assert.That(info.Length, Is.EqualTo(8));
            Assert.That(info[0], Is.EqualTo("Номер: 303"));
            Assert.That(info[1], Is.EqualTo("Количество кроватей: 4"));
            Assert.That(info[2], Is.EqualTo("Ориентация окон: West"));
            Assert.That(info[3], Is.EqualTo("Цена за сутки: 12000,00 руб."));
            Assert.That(info[4], Is.EqualTo("Дата освобождения: 05.05.2026 18:00"));
            Assert.That(info[5], Is.EqualTo("Дополнительные удобства: Сауна, бассейн"));
            Assert.That(info[6], Is.EqualTo("Количество комнат: 3"));
            Assert.That(info[7], Is.EqualTo("Минимальный срок сдачи: 7 дней"));
        }
    }
}