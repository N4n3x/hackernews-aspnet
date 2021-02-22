namespace HN.Application
{
  using System;
  using System.ComponentModel.DataAnnotations;
  using MediatR;

  public class PublishLinkCommand : IRequest<Guid>
  {
    [Required]
    [Url]
    public string Url { get; set; }
  }
}