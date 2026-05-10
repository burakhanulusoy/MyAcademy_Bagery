using Bagery.WebUI.MediatorPattern.Commands.TestimonialCommands;
using Bagery.WebUI.Repositories.TestimonialRepositories;
using Bagery.WebUI.UOW;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.TestimonialHandlers
{
    public class RemoveTestimonialCommandHandler(ITestimonialRepository _testimonialRepository,
                                                IUnitOfWork _unitOfWork) : IRequestHandler<RemoveTestimonialCommand>
    {
        public async Task Handle(RemoveTestimonialCommand request, CancellationToken cancellationToken)
        {
            
            var item = await _testimonialRepository.GetByIdAsync(request.Id);
            _testimonialRepository.Delete(item);
            await _unitOfWork.SaveChangesAsync();


        }
    }
}
