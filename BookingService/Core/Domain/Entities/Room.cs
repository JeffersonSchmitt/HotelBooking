using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Room
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
                if (this.InMaintenance || this.hasGuest)
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
    }
}
