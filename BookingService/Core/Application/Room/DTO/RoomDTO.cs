using Domain.Room.Entities;
using Domain.Room.Enums;
using Domain.Shared.ValueObjects;

namespace Application.Room.DTO
{
    public class RoomDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int level { get; set; }
        public bool InMaintenance { get; set; }
        public AcceptedCurrencies Currency { get; set; }
        public decimal PriceValue { get; set; }

        public static RoomEntity MapToEntity(RoomDTO roomDTO)
        {
            return new RoomEntity
            {
                Id = roomDTO.Id,
                Name = roomDTO.Name,
                level = roomDTO.level,
                InMaintenance = roomDTO.InMaintenance,
                Price = new Price
                {
                    Value = roomDTO.PriceValue,
                    Currency = roomDTO.Currency
                }
            };
        }
        public static RoomDTO MapToDto(RoomEntity room)
        {
            return new RoomDTO
            {
                Id = room.Id,
                Name = room.Name,
                level = room.level,
                InMaintenance = room.InMaintenance,
                PriceValue = room.Price.Value,
                Currency = room.Price.Currency
            };
        }
    }
}
