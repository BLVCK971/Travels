using MediatR;
using Microsoft.AspNetCore.Mvc;
using Travels.Application.Features.BoardingCards.Queries.GetSortedBoardingCards;
using Travels.Application.Features.BoardingCards.Queries.GetSortedBoardingTicket;

namespace Travels.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BoardingCardController : ControllerBase
{
    private readonly IMediator _mediator;

    public BoardingCardController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("sortedboardingcards", Name = "GetSortedBoardingCards")]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<SortedBoardingCardListVm>> GetSortedBoardingCards(List<BoardingCardListVm> unsortedBoardingCards)
    {
        var dtos = await _mediator.Send(new GetSortedBoardingCardListQuery() { boardingCardListVms = unsortedBoardingCards });
        return Ok(dtos);
    }
}
