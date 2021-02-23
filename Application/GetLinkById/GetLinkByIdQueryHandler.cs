namespace HN.Application
{
  using System.Threading;
  using System.Threading.Tasks;
  using System.Linq;
  using MediatR;

  public class GetLinkByIdQueryHandler : IRequestHandler<GetLinkByIdQuery, LinkDto>
  {
    private readonly IHNContext _context;

    public GetLinkByIdQueryHandler(IHNContext context)
    {
      _context = context;
    }

    public Task<LinkDto> Handle(GetLinkByIdQuery request, CancellationToken cancellationToken)
    {
      var links = from link in _context.Links
                  where link.Id == request.LinkId
                  select new LinkDto
                  {
                    Id = link.Id,
                    Url = link.Url,
                    CreatedAt = link.CreatedAt
                  };

      // var linkdto = _context.Links.Where(link => link.Id == request.LinkId).Select(link => new LinkDto
      // {
      //   Id = link.Id,
      //   Url = link.Url,
      //   CreatedAt = link.CreatedAt
      // }).Single();

      var linkdto = links.Single();

      return Task.FromResult(linkdto);
    }
  }
}