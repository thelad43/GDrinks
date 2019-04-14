namespace GDrinks.Common
{
    public static class DataConstants
    {
        public const int DrinkNameMinLength = 2;
        public const int DrinkNameMaxLength = 50;

        public const int DrinkDescriptionMinLength = 6;
        public const int DrinkDescriptionMaxLength = 300;

        public const int DrinkFullDescriptionMinLength = 20;
        public const int DrinkFullDescriptionMaxLength = 2000;

        public const double DrinkPriceMinLength = 0;
        public const double DrinkPriceMaxLength = double.MaxValue;

        public const int AddressLineMaxLength = 100;

        public const int ZipCodeMinLength = 4;
        public const int ZipCodeMaxLength = 10;

        public const int CountryMaxLength = 50;
    }
}