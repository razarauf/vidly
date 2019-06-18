using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsSubscribedToNewsletter { get; set; }

        // Navigation property - allows us to navigate from one type to another
        // useful when we want to load an object and its related objects from the db
        public MembershipType Membership { get; set; }
        // foreign key - not loading the whole object as above
        public byte MembershipTypeId { get; set; }
    }
}