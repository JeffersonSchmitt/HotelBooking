using Domain.ValueObjects;
using Entities = Domain.Entities;
namespace Application.Guest.DTO
{
    public class GuestDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string IdNumber { get; set; }
        public int DocumentType { get; set; }

        public static Entities.Guest MapToEntity(GuestDTO guestDTO)
        {
            return new Entities.Guest
            {
                Id = guestDTO.Id,
                Name = guestDTO.Name,
                Surname = guestDTO.Surname,
                Email = guestDTO.Email,
                Document = new PersonId
                {
                    IdNumber = guestDTO.IdNumber,
                    DocumentType = (Domain.Enums.DocumentsType)guestDTO.DocumentType
                }
            };
        }
        public static GuestDTO MapToDto(Entities.Guest guest)
        {
            return new GuestDTO
            {
                Id = guest.Id,
                Email = guest.Email,
                IdNumber = guest.Document.IdNumber,
                DocumentType = (int)guest.Document.DocumentType,
                Name = guest.Name,
                Surname = guest.Surname,
            };
        }
    }
}
