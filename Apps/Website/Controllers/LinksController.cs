
namespace Website.Controllers
{
  using Microsoft.AspNetCore.Mvc;
  using HN.Application;
  using MediatR;
  using System.Threading.Tasks;
  using System;

  public class LinksController : Controller
  {
    private readonly IMediator _bus;

    public LinksController(IMediator bus)
    {
      // var linkService = new LinkService(new HNDbContext());
      _bus = bus;
    }

    public async Task<IActionResult> Index()
    {
      var links = await _bus.Send(new GetLinksQuery());
      return View(links);
    }

    [HttpGet("{controller}/detail/{linkid:guid}")]
    public async Task<IActionResult> Show(Guid linkid)
    {
      var link = await _bus.Send(new GetLinkByIdQuery(linkid));
      return View(link);
    }

    // [HttpGet]
    public IActionResult Create()
    {
      return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PublishLinkCommand command)
    {
      if (!ModelState.IsValid)
      {
        // ModelState.AddModelError(/*"Url"*/ nameof(PublishLinkCommand.Url), "Oh une erreur !");
        return View(command);
      }

      // Dispatcher la commande
      var linkId = await _bus.Send(command);

      return RedirectToAction(nameof(Index));
    }
  }
}