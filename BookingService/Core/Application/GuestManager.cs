using Application.Guest.DTO;
using Application.Guest.Ports;
using Application.Guest.Requests;
using Application.Guest.Responses;
using Domain.Exceptions;
using Domain.Ports;

namespace Application
{
    public class GuestManager : IGuestManager
    {
        private IGuestRepository _repository;
        public GuestManager(IGuestRepository repository)
        {
            _repository = repository;
        }
        public async Task<GuestResponse> CreateGuest(CreateGuestRequest request)
        {
            try
            {
                var guest = GuestDTO.MapToEntity(request.Data);

                await guest.Save(_repository);

                request.Data.Id = guest.Id;

                return new GuestResponse
                {
                    Data = request.Data,
                    Success = true,
                };
            }
            catch (InvalidPersonDocumentIdException)
            {
                return new GuestResponse
                {
                    Success = false,
                    ErrorCode = Response.ErrorCodes.INVALID_DOCUMENT,
                    Message = "Invalid Guest Document"
                };
            }
            catch (MissingRequiredInformationException)
            {
                return new GuestResponse
                {
                    Success = false,
                    ErrorCode = Response.ErrorCodes.MISSING_REQUIRED_INFORMATION,
                    Message = "Missing required information"
                };
            }
            catch (InvalidEmailException)
            {
                return new GuestResponse
                {
                    Success = false,
                    ErrorCode = Response.ErrorCodes.INVALID_EMAIL,
                    Message = "Invalid E-mail"
                };
            }
            catch (Exception)
            {
                return new GuestResponse
                {
                    Success = false,
                    ErrorCode = Response.ErrorCodes.COULD_NOT_STORE_DATA,
                    Message = "Could not store data"
                };
            }
        }
    }
}
