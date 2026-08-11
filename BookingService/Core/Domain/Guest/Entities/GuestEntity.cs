using Domain.Utils;
using Domain.Guest.ValueObjects;
using Domain.Guest.Ports;
using Domain.Guest.Exceptions;
using Domain.Shared.Exceptions;

namespace Domain.Guest.Entities
{
    public class GuestEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public PersonId Document { get; set; }

        private void ValidateState()
        {
            if (Document.IdNumber == null || string.IsNullOrEmpty(Document.IdNumber) || Document.IdNumber.Length <= 3 || Document.DocumentType <= 0)
            {
                throw new InvalidPersonDocumentIdException();
            }
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Surname) || string.IsNullOrEmpty(Email))
            {
                throw new MissingRequiredInformationException();
            }
            if (UtilsEmail.ValidateEmail(Email) == false)
            {
                throw new InvalidEmailException();
            }
        }
        public async Task Save(IGuestRepository guestRepository)
        {
            ValidateState();
            if (Id == 0)
            {
                Id = await guestRepository.Create(this);
            }
            else
            {
                // await guestRepository.Update(this);
            }
        }
    }
}
