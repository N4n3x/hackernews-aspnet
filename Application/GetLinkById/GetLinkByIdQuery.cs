namespace HN.Application
{
  using System;
  using MediatR;

  public class GetLinkByIdQuery : IRequest<LinkDto>
  {
    public Guid LinkId { get; set; }

    public GetLinkByIdQuery(Guid linkId)
    {
      LinkId = linkId;
    }
  }
}