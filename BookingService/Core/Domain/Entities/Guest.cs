using Domain.Exceptions;
using Domain.ValueObjects;
using Domain.Utils;
using Domain.Ports;

namespace Domain.Entities
{
    public class Guest
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
            if (UtilsEmail.ValidateEmail(this.Email) == false)
            {
                throw new InvalidEmailException();
            }
        }
        public async Task Save(IGuestRepository guestRepository)
        {
            ValidateState();
            if (this.Id == 0)
            {
                this.Id = await guestRepository.Create(this);
            }
            else
            {
                // await guestRepository.Update(this);
            }
        }
    }
}
