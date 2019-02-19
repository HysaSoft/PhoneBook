using phoneBookLib.Enum;

namespace phoneBookLib.Model
{
    public class PhoneEntry
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public EntryType EntryType { get; set; }
        public string PhoneNumber { get; set; }
    }
}