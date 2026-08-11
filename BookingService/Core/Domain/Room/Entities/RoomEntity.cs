using Domain.Room.Ports;
using Domain.Shared.Exceptions;
using Domain.Shared.ValueObjects;

namespace Domain.Room.Entities
{
    public class RoomEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int level { get; set; }
        public bool InMaintenance { get; set; }
        public Price Price { get; set; }

        public bool IsAvailable
        {
            get
            {
                if (InMaintenance || hasGuest)
                {
                    return false;
                }
                return true;
            }
        }
        public bool hasGuest
        {
            //Verificar se existem reservas ativas para este quarto
            get { return true; }
        }

        private void ValidateState()
        {
            if (string.IsNullOrEmpty(Name) || level < 0)
            {
                throw new MissingRequiredInformationException();
            }
            if (Price == null || Price.Value < 0 || Price.Currency < 0)
            {
                throw new InvalidPriceException();
            }
        }
        public async Task Save(IRoomRepository roomRepository)
        {
            ValidateState();
            if (Id == 0)
            {
                Id = await roomRepository.CreateRoom(this);
            }
            else
            {
                // await roomRepository.Update(this);
            }
        }
    }
}
