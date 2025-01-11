using Microsoft.AspNetCore.Mvc;

namespace YeapApi.Controllers;

[ApiController]
[Route("[controller]")]
public class LeavesController : ControllerBase
{
    [HttpGet(Name = "GetLeaves")]
    public IEnumerable<LeaveDto> Get()
    {
        return Enumerable.Range(1, 5).Select(
                index => new LeaveDto
                {
                    Start = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    End = DateOnly.FromDateTime(DateTime.Now.AddDays(index + 2)),
                    Type = LeaveType.CP
                })
            .ToArray();
    }
}

public class LeaveDto
{
    public DateOnly Start { get; set; }
    public DateOnly End { get; set; }
    public LeaveType Type { get; set; }
}

public enum LeaveType
{
    RTT = 1,
    CP = 2,
    Other = 3
}
