using System.Collections.Generic;
using phoneBookLib;
using phoneBookLib.Enum;
using phoneBookLib.Model;

namespace Console.App
{
    internal static class Program
    {
        private static readonly string path = "C:\\Users\\pc\\Desktop\\HysaPhoneBook.xml";

        static void Main(string[] args)
        {
            var phoneBook = new PhoneBook(path);

            var entry1 = new PhoneEntry
            {
                FirstName = "Ordis",
                LastName = "Hysa",
                EntryType = EntryType.Cellphone,
                PhoneNumber = "+355674694707"
            };
            var entry2 = new PhoneEntry
            {
                FirstName = "Orvis",
                LastName = "Uku",
                EntryType = EntryType.Cellphone,
                PhoneNumber = "+355674694707"
            };
            var entry3 = new PhoneEntry
            {
                FirstName = "Geriola",
                LastName = "Zeta",
                EntryType = EntryType.Home,
                PhoneNumber = "+355674694666"
            };

            var root = new PhoneEntries
            {
                RootList = new List<PhoneEntry>{
                    entry1,
                    entry3,
                    entry2
                }
            };

            phoneBook.Save(root);
            phoneBook.Delete(entry2);
            phoneBook.Edit(entry3);

            foreach (var dataEntry in phoneBook.Entries(phoneBookLib.Enum.SortBy.LastName))
            {
                System.Console.WriteLine(dataEntry.FirstName);
                System.Console.WriteLine(dataEntry.LastName);
                System.Console.WriteLine(dataEntry.EntryType);
                System.Console.WriteLine(dataEntry.PhoneNumber);
            }
            System.Console.ReadLine();
        }
    }
}
