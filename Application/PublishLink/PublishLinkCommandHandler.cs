namespace HN.Application
{
  using System;
  using System.Threading;
  using System.Threading.Tasks;
  using HN.Domain;
  using MediatR;

  public class PublishLinkCommandHandler : IRequestHandler<PublishLinkCommand, Guid>
  {
    private readonly ILinkRepository _repository;

    public PublishLinkCommandHandler(ILinkRepository repository)
    {
      // _repository = new HN.Infrastructure.LinkRepository();
      _repository = repository;
    }

    public async Task<Guid> Handle(PublishLinkCommand request, CancellationToken cancellationToken)
    {
      var link = new Link(request.Url);

      await _repository.AddAsync(link);

      return link.Id;
    }

    // public static void Usage()
    // {
    //   ILinkRepository repository;
    //   var handler = new PublishLinkCommandHandler(repository);
    //   handler.Handle(new PublishLinkCommand{ Url = "http://test.com" })
    // }
  }
}