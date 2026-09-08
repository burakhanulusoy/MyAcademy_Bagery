using Bagery.WebUI.MediatorPattern.Commands.BlogCommands;
using Bagery.WebUI.Repositories.BlogRepositories;
using Bagery.WebUI.UOW;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.BlogHandlers
{
    public class RemoveBlogCommandHandler(IBlogRepository _blogRepository,
                                         IUnitOfWork _unitOfWork) : IRequestHandler<RemoveBlogCommand>
    {
        public async Task Handle(RemoveBlogCommand request, CancellationToken cancellationToken)
        {
           
            var blog= await _blogRepository.GetByIdAsync(request.Id);
            _blogRepository.Delete(blog);
            await _unitOfWork.SaveChangesAsync();


        }
    }
}
