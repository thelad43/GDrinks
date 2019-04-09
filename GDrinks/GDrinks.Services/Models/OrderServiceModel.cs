namespace GDrinks.Services.Models
{
    using System;

    public class OrderServiceModel
    {
        public int Id { get; set; }

        public DateTime OrderedOn { get; set; }

        public string AddressLine { get; set; }

        public string ZipCode { get; set; }

        public string Country { get; set; }

        public string User { get; set; }
    }
}