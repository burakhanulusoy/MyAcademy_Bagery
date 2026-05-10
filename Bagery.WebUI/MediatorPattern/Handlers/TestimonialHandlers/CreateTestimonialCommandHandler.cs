using Bagery.WebUI.Entities;
using Bagery.WebUI.Exceptions;
using Bagery.WebUI.MediatorPattern.Commands.TestimonialCommands;
using Bagery.WebUI.Repositories.TestimonialRepositories;
using Bagery.WebUI.UOW;
using FluentValidation;
using Mapster;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.TestimonialHandlers
{
    public class CreateTestimonialCommandHandler(ITestimonialRepository _testimonialRepository,
                                                IUnitOfWork _unitOfWork,
                                                IValidator<CreateTestimonialCommand> _validator) : IRequestHandler<CreateTestimonialCommand>
    {
        public async Task Handle(CreateTestimonialCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationUIException(validationResult.Errors);
            }

            var mapped = request.Adapt<Testimonial>();
            await _testimonialRepository.CreateAsync(mapped);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
