namespace GDrinks.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static GDrinks.Common.DataConstants;
    using static GDrinks.Common.DataErrorMessages;

    public class Order
    {
        public int Id { get; set; }

        public DateTime OrderedOn { get; set; }

        [Required(ErrorMessage = AddressLineErrorMessage)]
        [MaxLength(AddressLineMaxLength, ErrorMessage = AddressLineMaxLengthErrotMessage)]
        [Display(Name = "Address Line")]
        public string AddressLine { get; set; }

        [Required(ErrorMessage = ZipCodeErrorMessage)]
        [Display(Name = "Zip code")]
        [MinLength(ZipCodeMinLength, ErrorMessage = ZipCodeMinLengthErrorMessage)]
        [MaxLength(ZipCodeMaxLength, ErrorMessage = ZipCodeMaxLengthErrorMessage)]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "Please enter your country")]
        [MaxLength(CountryMaxLength, ErrorMessage = CountryMaxLengthErrorMessage)]
        public string Country { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}