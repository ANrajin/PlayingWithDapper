#nullable disable

using Dapper.Contrib.Extensions;

namespace Dapper.Data.Entities
{
    public class Contact
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Company { get; set; }

        public string Title { get; set; }

        [Computed]
        internal bool IsNew => Id == default;

        [Write(false)]
        public List<Address> Addresses { get; } = new List<Address>();
    }
}
