using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleHelpers.Models.NationalRegisterNumber;

namespace SimpleHelpers.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void NationalRegisterNumberTest1()
        {
            // Arrange
            NationalRegisterNumber nrn;

            // Act
            nrn = new NationalRegisterNumber("88080127151");

            // Assert
            Assert.AreEqual("88.08.01-271.51", nrn.NRN);
            Assert.AreEqual("88.08.01-271.51", nrn.ToString());
            Assert.AreEqual("01/08/1988", nrn.Birthdate);
            Assert.AreEqual("M", nrn.Genre);
            Assert.AreEqual(BirthdateType.Ok, nrn.BirthdateType);
            Assert.AreEqual(false, nrn.IsBisNumber);
            Assert.AreEqual(true, nrn.IsBornBefore2000);
            Assert.AreEqual(false, nrn.IsBornAfter2000);
            Assert.AreEqual(true, nrn.IsGenreKnown);
        }

        [TestMethod]
        public void NationalRegisterNumberTest2()
        {
            // Arrange
            NationalRegisterNumber nrn;

            // Act
            nrn = new NationalRegisterNumber("09092917215");

            // Assert
            Assert.AreEqual("09.09.29-172.15", nrn.NRN);
            Assert.AreEqual("09.09.29-172.15", nrn.ToString());
            Assert.AreEqual("29/09/2009", nrn.Birthdate);
            Assert.AreEqual("F", nrn.Genre);
            Assert.AreEqual(BirthdateType.Ok, nrn.BirthdateType);
            Assert.AreEqual(false, nrn.IsBisNumber);
            Assert.AreEqual(false, nrn.IsBornBefore2000);
            Assert.AreEqual(true, nrn.IsBornAfter2000);
            Assert.AreEqual(true, nrn.IsGenreKnown);
        }

        [TestMethod]
        public void NationalRegisterNumberTest3()
        {
            // Arrange
            NationalRegisterNumber nrn;

            // Act
            nrn = new NationalRegisterNumber("40000095579");

            // Assert
            Assert.AreEqual("40.00.00-955.79", nrn.NRN);
            Assert.AreEqual("40.00.00-955.79", nrn.ToString());
            Assert.AreEqual("00/00/1940", nrn.Birthdate);
            Assert.AreEqual("M", nrn.Genre);
            Assert.AreEqual(BirthdateType.Incomplete, nrn.BirthdateType);
            Assert.AreEqual(false, nrn.IsBisNumber);
            Assert.AreEqual(true, nrn.IsBornBefore2000);
            Assert.AreEqual(false, nrn.IsBornAfter2000);
            Assert.AreEqual(true, nrn.IsGenreKnown);
        }

        [TestMethod]
        public void NationalRegisterNumberTest4()
        {
            // Arrange
            NationalRegisterNumber nrn;

            // Act
            nrn = new NationalRegisterNumber("40000100162");

            // Assert
            Assert.AreEqual("40.00.01-001.62", nrn.NRN);
            Assert.AreEqual("40.00.01-001.62", nrn.ToString());
            Assert.AreEqual("01/00/2040", nrn.Birthdate);
            Assert.AreEqual("M", nrn.Genre);
            Assert.AreEqual(BirthdateType.Incomplete, nrn.BirthdateType);
            Assert.AreEqual(false, nrn.IsBisNumber);
            Assert.AreEqual(false, nrn.IsBornBefore2000);
            Assert.AreEqual(true, nrn.IsBornAfter2000);
            Assert.AreEqual(true, nrn.IsGenreKnown);
        }

        [TestMethod]
        public void NationalRegisterNumberTest5()
        {
            // Arrange
            NationalRegisterNumber nrn;

            // Act
            nrn = new NationalRegisterNumber("00000100166");

            // Assert
            Assert.AreEqual("00.00.01-001.66", nrn.NRN);
            Assert.AreEqual("00.00.01-001.66", nrn.ToString());
            Assert.AreEqual("01/00/0000", nrn.Birthdate);
            Assert.AreEqual("M", nrn.Genre);
            Assert.AreEqual(BirthdateType.Unknown, nrn.BirthdateType);
            Assert.AreEqual(false, nrn.IsBisNumber);
            Assert.AreEqual(false, nrn.IsBornBefore2000);
            Assert.AreEqual(false, nrn.IsBornAfter2000);
            Assert.AreEqual(true, nrn.IsGenreKnown);
        }

        [TestMethod]
        public void NationalRegisterNumberTest6()
        {
            // Arrange
            NationalRegisterNumber nrn;

            // Act
            nrn = new NationalRegisterNumber("00400100155");

            // Assert
            Assert.AreEqual("00.40.01-001.55", nrn.NRN);
            Assert.AreEqual("00.40.01-001.55", nrn.ToString());
            Assert.AreEqual("01/00/0000", nrn.Birthdate);
            Assert.AreEqual("M", nrn.Genre);
            Assert.AreEqual(BirthdateType.Unknown, nrn.BirthdateType);
            Assert.AreEqual(true, nrn.IsBisNumber);
            Assert.AreEqual(false, nrn.IsBornBefore2000);
            Assert.AreEqual(false, nrn.IsBornAfter2000);
            Assert.AreEqual(true, nrn.IsGenreKnown);
        }

        [TestMethod]
        public void NationalRegisterNumberTest7()
        {
            // Arrange
            NationalRegisterNumber nrn;

            // Act
            nrn = new NationalRegisterNumber("00200100166");

            // Assert
            Assert.AreEqual("00.20.01-001.66", nrn.NRN);
            Assert.AreEqual("00.20.01-001.66", nrn.ToString());
            Assert.AreEqual("01/00/0000", nrn.Birthdate);
            Assert.AreEqual(null, nrn.Genre);
            Assert.AreEqual(BirthdateType.Unknown, nrn.BirthdateType);
            Assert.AreEqual(true, nrn.IsBisNumber);
            Assert.AreEqual(false, nrn.IsBornBefore2000);
            Assert.AreEqual(false, nrn.IsBornAfter2000);
            Assert.AreEqual(false, nrn.IsGenreKnown);
        }
    }
}
