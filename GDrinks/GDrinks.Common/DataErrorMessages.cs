namespace GDrinks.Common
{
    public static class DataErrorMessages
    {
        public const string DrinkNameErrorMessage = "Name is required!";
        public const string DrinkNameMinLengthErrorMessage = "Name cannot be less than 2 symbols!";
        public const string DrinkNameMaxLengthErrorMessage = "Name cannot be more than 50 symbols!";

        public const string DrinkDescriptionErrorMessage = "Description is required!";
        public const string DrinkDescriptionMinLengthErrorMessage = "Description cannot be less than 6 symbols!";
        public const string DrinkDescriptionMaxLengthErrorMessage = "Description cannot be more than 300 symbols!";

        public const string DrinkFullDescriptionErrorMessage = "Full Description is required!";
        public const string DrinkFullDescriptionMinLengthErrorMessage = "Full Description cannot be less than 20 symbols!";
        public const string DrinkFullDescriptionMaxLengthErrorMessage = "Full Description cannot be more than 2000 symbols!";

        public const string DrinkImageUrlErrorMessage = "Image URL is required!";
        public const string DrinkImageThumbnailUrlErrorMessage = "Image Thumbnail URL is required!";

        public const string DrinkPriceErrorMessage = "Price must be between 0 and 79228162514264337593543950335!";

        public const string AddressLineErrorMessage = "Please enter your address";
        public const string AddressLineMaxLengthErrotMessage = "Address length cannot be more than 100 symbols!";

        public const string ZipCodeErrorMessage = "Please enter your zip code";
        public const string ZipCodeMinLengthErrorMessage = "Zip code length cannot be less than 4 symbols!";
        public const string ZipCodeMaxLengthErrorMessage = "Zip code length cannot be more than 10 symbols!";

        public const string CountryMaxLengthErrorMessage = "Country name cannot be more than 50 symbols!";
    }
}