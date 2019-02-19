using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using phoneBookLib.Enum;
using phoneBookLib.Model;

namespace phoneBookLib
{
    public class PhoneBook
    {
        private string PhoneBookFilename { get; }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="phoneBookFilename">Name of file to Save</param>
        public PhoneBook(string phoneBookFilename)
        {
            PhoneBookFilename = phoneBookFilename;
        }
        
        /// <summary>
        /// en-Save List to XML Format
        /// it-Salva la litsa in Formato XML
        /// sq-Ruaj listen ne Format XML
        /// </summary>
        /// <param name="entry">PhoneEntry Type</param>
        public void Save(PhoneEntries entry)
        {
            File.AppendAllText(PhoneBookFilename, Serialize(entry));
        }

        /// <summary>
        /// en-Update
        /// it-Aggiorna
        /// sq-Perditeso
        /// </summary>
        /// <param name="entry">PhoneEntry Type</param>
        public void Edit(PhoneEntry entry)
        {
            var en = Entries(SortBy.FirstName);

            en.Find(r => r.FirstName == entry.FirstName && r.LastName == entry.LastName).EntryType=entry.EntryType;
            en.Find(r => r.FirstName == entry.FirstName && r.LastName == entry.LastName).PhoneNumber=entry.PhoneNumber;

            File.Delete(PhoneBookFilename);

            Save(new PhoneEntries { RootList = en });
        }

        /// <summary>
        /// en-Delete
        /// it-Elimina
        /// sq-Fshij
        /// </summary>
        /// <param name="entry">PhoneEntry Type</param>
        public void Delete(PhoneEntry entry)
        {
            var en = Entries(SortBy.FirstName);

            en.RemoveAll(r=>r.FirstName==entry.FirstName && r.LastName==entry.LastName);

            File.Delete(PhoneBookFilename);

            Save(new PhoneEntries {RootList = en});
        }
       
        /// <summary>
        /// en-Get all Entries
        /// it-Ottieni tutte le voci
        /// sq-listo te gjitha
        /// </summary>
        /// <param name="sortBy">sort type. ex firstame</param>
        /// <returns></returns>
        public List<PhoneEntry> Entries(SortBy sortBy)
        {
            return sortBy == SortBy.FirstName ? Deserialize<PhoneEntries>(File.ReadAllText(PhoneBookFilename)).RootList.OrderBy(s=>s.FirstName).ToList() : Deserialize<PhoneEntries>(File.ReadAllText(PhoneBookFilename)).RootList.OrderBy(s=>s.LastName).ToList();
        }

        private static T Deserialize<T>(string toDeserialize)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            using (var textReader = new StringReader(toDeserialize))
            {
                return (T)xmlSerializer.Deserialize(textReader);
            }
        }

        private static string Serialize<T>(T toSerialize)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            using (var textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, toSerialize);
                return textWriter.ToString();
            }
        }
        
    }
}