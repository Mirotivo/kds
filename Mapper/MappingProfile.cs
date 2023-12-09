using AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<OrderItem, OrderItemDto>();
        CreateMap<PurchaseOrder, PurchaseOrderDto>();
        CreateMap<StationGroup, StationGroupDto>();
    }
}
