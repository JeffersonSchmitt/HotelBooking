using Application;
using Application.Guest.DTO;
using Application.Guest.Requests;
using Domain.Entities;
using Domain.Ports;
using Moq;

namespace ApplicationTests
{
    public class GuestManagerTests
    {
        GuestManager _guestManager;

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public async Task HappyPath()
        {

            var guestDTO = new GuestDTO
            {
                Name = "John",
                Surname = "Doe",
                Email = "ab@gmail.com",
                IdNumber = "123456789",
                DocumentType = 1
            };

            int expectedId = 222;

            var fakeRepository = new Mock<IGuestRepository>();
            fakeRepository.Setup(x => x.Create(It.IsAny<Guest>())).Returns(Task.FromResult(expectedId));
            _guestManager = new GuestManager(fakeRepository.Object);


            var request = new CreateGuestRequest
            {
                Data = guestDTO
            };
            var res = await _guestManager.CreateGuest(request);
            Assert.IsNotNull(res);
            Assert.True(res.Success);
            Assert.That(expectedId, Is.EqualTo(res.Data.Id));
            Assert.That(guestDTO.Name, Is.EqualTo(res.Data.Name));
        }

        [TestCase("")]
        [TestCase("a")]
        [TestCase("ab")]
        [TestCase("abc")]
        [TestCase(null)]
        public async Task Should_Return_InvalidPersonDocumentIdException_When_Document_Is_Invalid(string? docNumber)
        {

            var guestDTO = new GuestDTO
            {
                Name = "John",
                Surname = "Doe",
                Email = "ab@gmail.com",
                IdNumber = docNumber,
                DocumentType = 1
            };

            var fakeRepository = new Mock<IGuestRepository>();
            fakeRepository.Setup(x => x.Create(It.IsAny<Guest>())).Returns(Task.FromResult(222));
            _guestManager = new GuestManager(fakeRepository.Object);


            var request = new CreateGuestRequest
            {
                Data = guestDTO
            };
            var res = await _guestManager.CreateGuest(request);
            Assert.IsNotNull(res);
            Assert.False(res.Success);
            Assert.That(res.ErrorCode, Is.EqualTo(Response.ErrorCodes.INVALID_DOCUMENT));
            Assert.That(res.Message, Is.EqualTo("Invalid Guest Document"));
        }

        [TestCase("", "surnametest", "asdf@gmail.com")]
        [TestCase(null, "surnametest", "asdf@gmail.com")]
        [TestCase("Fulano", "", "asdf@gmail.com")]
        [TestCase("Fulano", null, "asdf@gmail.com")]
        [TestCase("Fulano", "surnametest", "")]
        [TestCase("Fulano", "surnametest", null)]
        public async Task Should_Return_MissingRequiredInformation_When_Docs_Is_Invalid(string? name, string? surname, string? email)
        {

            var guestDTO = new GuestDTO
            {
                Name = name,
                Surname = surname,
                Email = email,
                IdNumber = "12345",
                DocumentType = 1
            };

            var fakeRepository = new Mock<IGuestRepository>();
            fakeRepository.Setup(x => x.Create(It.IsAny<Guest>())).Returns(Task.FromResult(222));
            _guestManager = new GuestManager(fakeRepository.Object);


            var request = new CreateGuestRequest
            {
                Data = guestDTO
            };
            var res = await _guestManager.CreateGuest(request);
            Assert.IsNotNull(res);
            Assert.False(res.Success);
            Assert.That(res.ErrorCode, Is.EqualTo(Response.ErrorCodes.MISSING_REQUIRED_INFORMATION));
            Assert.That(res.Message, Is.EqualTo("Missing required information"));
        }

        [TestCase("a")]
        [TestCase("a@")]
        public async Task Should_Return_InvalidEmail_When_Docs_Is_Invalid(string? email)
        {

            var guestDTO = new GuestDTO
            {
                Name = "John",
                Surname = "Doe",
                Email = email,
                IdNumber = "12345",
                DocumentType = 1
            };

            var fakeRepository = new Mock<IGuestRepository>();
            fakeRepository.Setup(x => x.Create(It.IsAny<Guest>())).Returns(Task.FromResult(222));
            _guestManager = new GuestManager(fakeRepository.Object);


            var request = new CreateGuestRequest
            {
                Data = guestDTO
            };
            var res = await _guestManager.CreateGuest(request);
            Assert.IsNotNull(res);
            Assert.False(res.Success);
            Assert.That(res.ErrorCode, Is.EqualTo(Response.ErrorCodes.INVALID_EMAIL));
            Assert.That(res.Message, Is.EqualTo("Invalid E-mail"));
        }

        [Test]
        public async Task Should_Return_GuestNotFound_When_GuestDoesntExist()
        {
            var fakeRepository = new Mock<IGuestRepository>();
            fakeRepository.Setup(x => x.Get(323)).Returns(Task.FromResult<Guest>(null));
            _guestManager = new GuestManager(fakeRepository.Object);
            var res = await _guestManager.GetGuest(323);
            Assert.IsNotNull(res);
            Assert.False(res.Success);
            Assert.That(res.ErrorCode, Is.EqualTo(Response.ErrorCodes.NOT_FOUND));
            Assert.That(res.Message, Is.EqualTo("Guest not found"));
        }

        [Test]
        public async Task Should_Return_Guest_Sucess()
        {
            var fakeRepository = new Mock<IGuestRepository>();

            var guest = new Guest
            {
                Id = 323,
                Name = "John",
                Document = new Domain.ValueObjects.PersonId
                {
                    IdNumber = "123456789",
                    DocumentType = Domain.Enums.DocumentsType.Passport
                }
            };

            fakeRepository.Setup(x => x.Get(323)).Returns(Task.FromResult(guest));
            _guestManager = new GuestManager(fakeRepository.Object);
            var res = await _guestManager.GetGuest(323);
            Assert.IsNotNull(res);
            Assert.True(res.Success);
            Assert.That(guest.Id, Is.EqualTo(res.Data.Id));
            Assert.That(guest.Name, Is.EqualTo(res.Data.Name));
        }
    }

}