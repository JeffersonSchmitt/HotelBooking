using Domain.Guest.Entities;
using Domain.Guest.Enums;
using Domain.Guest.ValueObjects;
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

        public static GuestEntity MapToEntity(GuestDTO guestDTO)
        {
            return new GuestEntity
            {
                Id = guestDTO.Id,
                Name = guestDTO.Name,
                Surname = guestDTO.Surname,
                Email = guestDTO.Email,
                Document = new PersonId
                {
                    IdNumber = guestDTO.IdNumber,
                    DocumentType = (DocumentsType)guestDTO.DocumentType
                }
            };
        }
        public static GuestDTO MapToDto(GuestEntity guest)
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
