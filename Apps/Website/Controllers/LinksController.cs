
namespace Website.Controllers
{
  using Microsoft.AspNetCore.Mvc;
  using HN.Application;
  using MediatR;
  using System.Threading.Tasks;

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
      return Ok(await _bus.Send(new GetLinksQuery()));
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