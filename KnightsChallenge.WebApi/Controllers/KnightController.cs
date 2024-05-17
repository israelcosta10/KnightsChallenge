using KnightsChallenge.Commands.CreateKnight;
using KnightsChallenge.Commands.RemoveKnight;
using KnightsChallenge.Commands.UpdateKnight;
using KnightsChallenge.Queries.GetKnight;
using KnightsChallenge.Queries.GetKnights;
using KnightsChallenge.Queries.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KnightsChallenge.WebApi.Controllers;

[Tags("Knight")]
[Route("knights")]
[ApiController]
public class KnightController (IMediator mediator) : ControllerBase
{
  [HttpGet]
  public async Task<List<KnightView>> HandleList ([FromQuery] GetKnightsQueryParams queryParams)
  {
    var result = await mediator.Send(new GetKnightsQuery(queryParams));

    return result;
  }

  [HttpGet("{id}")]
  public async Task<KnightView> HandleListOne (string id)
  {
    var result = await mediator.Send(new GetKnightQuery(id));

    return result;
  }

  [HttpPost]
  public async Task<IActionResult> HandleCreate ([FromBody] CreateKnightCommandPayload payload)
  {
    await mediator.Send(new CreateKnightCommand(payload));

    return Created();
  }

  [HttpPatch("{id}")]
  public async Task<IActionResult> HandleUpdateNickname ([FromBody] UpdateKnightCommandPayload payload, string id)
  {
    await mediator.Send(new UpdateKnightCommand(id, payload));

    return Ok();
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> HandleRemove (string id)
  {
    await mediator.Send(new RemoveKnightCommand(id));

    return Ok();
  }
}