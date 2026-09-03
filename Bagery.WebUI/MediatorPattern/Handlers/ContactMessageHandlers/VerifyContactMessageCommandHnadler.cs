using Bagery.WebUI.Exceptions;
using Bagery.WebUI.MediatorPattern.Commands.ContactMessageCommands;
using Bagery.WebUI.Repositories.ContactMessageRepositories;
using Bagery.WebUI.UOW;
using MediatR;

public class VerifyContactMessageCommandHandler(IContactMessageRepository _contactMessageRepository,
                                                IUnitOfWork _unitOfWork)
                                                : IRequestHandler<VerifyContactMessageCommand>
{
    public async Task Handle(VerifyContactMessageCommand request, CancellationToken cancellationToken)
    {
        var values = await _contactMessageRepository.GetAllAsync();

        var item = values.Where(x => x.Email == request.Email && !x.IsVerified)
                         .OrderByDescending(x => x.Id)
                         .FirstOrDefault();

        if (item == null)
        {
            throw new IdentityException("Doğrulanacak bir mesaj bulunamadı.");
        }
        if (item.CodeExpireDate < DateTime.UtcNow)
        {
            throw new IdentityException("Kodun süresi doldu. Lütfen formu tekrar gönderin.");
        }
        if (item.VerificationCode != request.Code)
        {
            throw new IdentityException("Doğrulama kodu hatalı.");
        }

        item.IsVerified = true;
        item.VerificationCode = null;

        _contactMessageRepository.Update(item);
        await _unitOfWork.SaveChangesAsync();
    }
}