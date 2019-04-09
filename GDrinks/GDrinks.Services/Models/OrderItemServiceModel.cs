namespace GDrinks.Services.Models
{
    using AutoMapper;
    using GDrinks.Common.Mapping;
    using GDrinks.Models;

    public class OrderItemServiceModel : IMapFrom<Order>, IHaveCustomMappings
    {
        public int Amount { get; set; }

        public decimal Price { get; set; }

        public int DrinkId { get; set; }

        public string Drink { get; set; }

        public decimal Subtotal => this.Amount * this.Price;

        public void CreateMappings(IMapperConfigurationExpression configuration)
            => configuration.CreateMap<OrderItem, OrderItemServiceModel>()
                .ForMember(src => src.Drink, cfg => cfg.MapFrom(dest => dest.Drink.Name));
    }
}
