using System;

namespace HN.Application
{
  public class LinkDto
  {
    public Guid Id { get; set; }
    public string Url { get; set; }
    public DateTime CreatedAt { get; set; }
  }
}