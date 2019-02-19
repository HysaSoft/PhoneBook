using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using phoneBookLib.Enum;
using phoneBookLib.Model;

namespace PhoneBook.UTest
{
    [TestFixture]
    public class PhoneBookTests
    {
        private static readonly string Path = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(
            TestContext.CurrentContext.TestDirectory)) + "\\TestFile.xml";
        [Test]
        public void Save_Entries_CompareSaved()
        {
            // Delete Test File
            if(File.Exists(Path))
                File.Delete(Path);

            // Arrange
            var phoneBook = new phoneBookLib.PhoneBook(Path);

            var entry1 = new PhoneEntry
            {
                FirstName = "Ordis",
                LastName = "Hysa",
                EntryType = EntryType.Cellphone,
                PhoneNumber = "+355674694707"
            };

            var root = new PhoneEntries
            {
                RootList = new List<PhoneEntry>{
                    entry1
                }
            };

            // Act
            phoneBook.Save(root);

            var saved =  phoneBook.Entries(SortBy.FirstName);

            // Assert
            Assert.AreEqual(root.RootList.Select(s=>s.FirstName == "Ordis").FirstOrDefault(),saved.Select(s=>s.FirstName == "Ordis").FirstOrDefault());

            // Then Clean Test File
            File.Delete(Path);
        }
    }
}
