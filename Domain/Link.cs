namespace HN.Domain
{
  using System;

  public class Link
  {
    public Guid Id { get; }
    public string Url { get; }
    public DateTime CreatedAt { get; }

    public Link(string url)
    {
      Id = Guid.NewGuid();
      Url = url;

      // if(string.IsNullOrWhiteSpace(url)) {
      //   throw new UrlShouldNotBeEmpty();
      // }

      CreatedAt = DateTime.UtcNow;
    }
  }
}
