namespace GDrinks.Web.Areas.Admin.Models.Drinks
{
    using AutoMapper;
    using GDrinks.Common.Mapping;
    using GDrinks.Models;
    using System.ComponentModel.DataAnnotations;

    public class DrinkFormViewModel : IMapFrom<Category>, IHaveCustomMappings
    {
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(300)]
        public string Description { get; set; }

        [Required]
        [MinLength(20)]
        [MaxLength(2000)]
        public string FullDescription { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [Url]
        public string ImageUrl { get; set; }

        [Required]
        [Url]
        public string ImageThumbnailUrl { get; set; }

        public bool IsPreferred { get; set; }

        public bool IsInStock { get; set; }

        public string Category { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
            => configuration.CreateMap<Drink, DrinkFormViewModel>()
                .ForMember(src => src.Category, cfg => cfg.MapFrom(dest => dest.Category.Name));
    }
}