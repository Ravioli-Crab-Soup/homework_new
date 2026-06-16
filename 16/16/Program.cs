// ============================================================
// ЗАДАНИЕ 16. СТРУКТУРЫ (ВАРИАНТ 4. СТРУКТУРА BUNDLE)
// ============================================================

// Bundle.cs
using System;

namespace BundleStruct
{
    public struct Bundle
    {
        private static readonly int[] ValidBanknotes = { 1, 2, 5, 10, 50, 100, 200, 500, 1000, 2000, 5000 };

        private int banknote;
        public int Banknote
        {
            get => banknote;
            set
            {
                if (Array.IndexOf(ValidBanknotes, value) == -1)
                    throw new ArgumentException("Недопустимый номинал купюры");
                banknote = value;
            }
        }

        private int count;
        public int Count
        {
            get => count;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Количество купюр не может быть отрицательным");
                count = value;
            }
        }

        public int Sum => Banknote * Count;

        public Bundle(int banknote, int count) : this()
        {
            Banknote = banknote;
            Count = count;
        }

        public override string ToString()
        {
            return $"{Count} x {Banknote} р.";
        }

        public override bool Equals(object obj)
        {
            if (obj is Bundle other)
                return Banknote == other.Banknote && Count == other.Count;
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Banknote, Count);
        }

        public static bool operator ==(Bundle a, Bundle b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Bundle a, Bundle b)
        {
            return !a.Equals(b);
        }

        public static Bundle operator +(Bundle a, Bundle b)
        {
            if (a.Banknote != b.Banknote)
                throw new InvalidOperationException("Нельзя сложить пачки с разными номиналами");
            return new Bundle(a.Banknote, a.Count + b.Count);
        }

        public static Bundle operator -(Bundle a, Bundle b)
        {
            if (a.Banknote != b.Banknote)
                throw new InvalidOperationException("Нельзя вычесть пачки с разными номиналами");
            if (a.Count < b.Count)
                throw new InvalidOperationException("Нельзя вычесть пачку с большим количеством купюр");
            return new Bundle(a.Banknote, a.Count - b.Count);
        }
    }
}

// ============================================================
// МОДУЛЬНЫЕ ТЕСТЫ ДЛЯ СТРУКТУРЫ BUNDLE (ЗАДАНИЕ 16)
// ============================================================

using NUnit.Framework;
using System;

namespace BundleStruct.UnitTests
{
    [TestFixture]
    public class BundleTests
    {
        [Test]
        public void ConstructorTest()
        {
            var b = new Bundle(100, 5);
            Assert.That(b.Banknote, Is.EqualTo(100));
            Assert.That(b.Count, Is.EqualTo(5));
            Assert.That(b.Sum, Is.EqualTo(500));
        }

        [TestCase(0)]
        [TestCase(3)]
        [TestCase(7)]
        [TestCase(2000)]
        public void Constructor_InvalidBanknote_ThrowsArgumentException(int invalidBanknote)
        {
            Assert.That(() => new Bundle(invalidBanknote, 1), Throws.ArgumentException);
        }

        [TestCase(-1)]
        [TestCase(-100)]
        public void Constructor_NegativeCount_ThrowsArgumentException(int invalidCount)
        {
            Assert.That(() => new Bundle(100, invalidCount), Throws.ArgumentException);
        }

        [Test]
        public void ToStringTest()
        {
            var b = new Bundle(50, 125);
            Assert.That(b.ToString(), Is.EqualTo("125 x 50 р."));
            var b2 = new Bundle(5000, 3);
            Assert.That(b2.ToString(), Is.EqualTo("3 x 5000 р."));
        }

        [Test]
        public void EqualsTest()
        {
            var b1 = new Bundle(100, 5);
            var b2 = new Bundle(100, 5);
            var b3 = new Bundle(200, 5);
            Assert.That(b1.Equals(b2), Is.True);
            Assert.That(b1.Equals(b3), Is.False);
            Assert.That(b1.Equals(null), Is.False);
        }

        [Test]
        public void GetHashCodeTest()
        {
            var b1 = new Bundle(100, 5);
            var b2 = new Bundle(100, 5);
            Assert.That(b1.GetHashCode(), Is.EqualTo(b2.GetHashCode()));

            var b3 = new Bundle(200, 5);
            Assert.That(b1.GetHashCode(), Is.Not.EqualTo(b3.GetHashCode()));
        }

        [Test]
        public void ComparisonOperatorsTest()
        {
            var b1 = new Bundle(100, 5);
            var b2 = new Bundle(100, 5);
            var b3 = new Bundle(200, 5);

            Assert.That(b1 == b2, Is.True);
            Assert.That(b1 != b2, Is.False);
            Assert.That(b1 == b3, Is.False);
            Assert.That(b1 != b3, Is.True);
        }

        [Test]
        public void AdditionOperatorTest()
        {
            var b1 = new Bundle(100, 3);
            var b2 = new Bundle(100, 7);
            var result = b1 + b2;
            Assert.That(result.Banknote, Is.EqualTo(100));
            Assert.That(result.Count, Is.EqualTo(10));
            Assert.That(result.Sum, Is.EqualTo(1000));
        }

        [Test]
        public void AdditionOperator_DifferentBanknotes_ThrowsInvalidOperationException()
        {
            var b1 = new Bundle(100, 3);
            var b2 = new Bundle(200, 7);
            Assert.That(() => b1 + b2, Throws.InvalidOperationException);
        }

        [Test]
        public void SubtractionOperatorTest()
        {
            var b1 = new Bundle(100, 10);
            var b2 = new Bundle(100, 4);
            var result = b1 - b2;
            Assert.That(result.Banknote, Is.EqualTo(100));
            Assert.That(result.Count, Is.EqualTo(6));
            Assert.That(result.Sum, Is.EqualTo(600));
        }

        [Test]
        public void SubtractionOperator_DifferentBanknotes_ThrowsInvalidOperationException()
        {
            var b1 = new Bundle(100, 10);
            var b2 = new Bundle(200, 4);
            Assert.That(() => b1 - b2, Throws.InvalidOperationException);
        }

        [Test]
        public void SubtractionOperator_CountTooBig_ThrowsInvalidOperationException()
        {
            var b1 = new Bundle(100, 3);
            var b2 = new Bundle(100, 7);
            Assert.That(() => b1 - b2, Throws.InvalidOperationException);
        }
    }
}