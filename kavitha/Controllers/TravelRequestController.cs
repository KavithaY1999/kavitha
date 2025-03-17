using kavitha.Data;
using kavitha.DTOS;
using kavitha.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace kavitha.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class TravelRequestController : ControllerBase
  {
    private readonly AppDbContext _context;

    public TravelRequestController(AppDbContext context)
    {
      _context = context;
    }

    // ✅ Get All Travel Requests (Admin/Manager only)
    [HttpGet]
    //[Authorize(Roles = "Admin,Manager")]
    public async Task<ActionResult<IEnumerable<TravelRequest>>> GetTravelRequests()
    {
      var requests = await _context.TravelRequests.ToListAsync();
      return Ok(requests);
    }

    // ✅ Get Travel Request by ID
    [HttpGet("{id}")]
    public async Task<ActionResult<TravelRequest>> GetTravelRequest(int id)
    {
      var request = await _context.TravelRequests.Include(tr => tr.Employee).FirstOrDefaultAsync(tr => tr.RequestId == id);

      if (request == null)
      {
        return NotFound(new { message = "Travel request not found." });
      }

      return Ok(request);
    }

    // ✅ Create Travel Request (Only Employees)
    [HttpPost]
    //[Authorize(Roles = "Employee")]
    public async Task<ActionResult<TravelRequest>> CreateTravelRequest([FromBody] TravelRequestDTO requestDto)
    {
      var employee = await _context.Users.FindAsync(requestDto.EmployeeId);
      if (employee == null)
      {
        return NotFound(new { message = "Employee not found." });
      }

      var request = new TravelRequest
      {
        EmployeeId = requestDto.EmployeeId,
        Destination = requestDto.Destination,
        StartDate = requestDto.StartDate,
        EndDate = requestDto.EndDate,
        Purpose = requestDto.Purpose,
        Status = "Pending"
      };

      _context.TravelRequests.Add(request);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetTravelRequest), new { id = request.RequestId }, request);
    }

    // ✅ Approve/Reject Travel Request (Admin/Manager only)
    //[HttpPut("{id}/status")]
    ////[Authorize(Roles = "Admin,Manager")]
    //public async Task<IActionResult> UpdateTravelRequestStatus(int id, [FromBody] string status, [FromBody] DateTime StartDate, [FromBody] DateTime EndDate, [FromBody] string Purpose, [FromBody] string Destination)
    //{
    //  var request = await _context.TravelRequests.FindAsync(id);
    //  if (request == null)
    //  {
    //    return NotFound(new { message = "Travel request not found." });
    //  }

    //  if (status != "Approved" && status != "Rejected")
    //  {
    //    return BadRequest(new { message = "Invalid status. Only 'Approved' or 'Rejected' allowed." });
    //  }

    //  request.StartDate = StartDate;
    //  request.EndDate = EndDate;
    //  request.Purpose = Purpose;
    //  request.Destination = Destination;
    //  request.Status = status;
    //  await _context.SaveChangesAsync();

    //  return Ok(new { message = $"Request {status} successfully." });
    //}

    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateTravelRequestStatus(int id, [FromBody] TravelRequestDTO updateRequest)
    {
      var request = await _context.TravelRequests.FindAsync(id);
      if (request == null)
      {
        return NotFound(new { message = "Travel request not found." });
      }

      if (updateRequest.Status != "Approved" && updateRequest.Status != "Rejected")
      {
        return BadRequest(new { message = "Invalid status. Only 'Approved' or 'Rejected' allowed." });
      }

      // ✅ Update fields
      request.Status = updateRequest.Status;
      request.StartDate = updateRequest.StartDate;
      request.EndDate = updateRequest.EndDate;
      request.Purpose = updateRequest.Purpose;
      request.Destination = updateRequest.Destination;

      await _context.SaveChangesAsync();

      return Ok(new { message = $"Request {updateRequest.Status} successfully." });
    }





    // ✅ Delete Travel Request (Admin only)
    [HttpDelete("{id}")]
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteTravelRequest(int id)
    {
      var request = await _context.TravelRequests.FindAsync(id);
      if (request == null)
      {
        return NotFound(new { message = "Travel request not found." });
      }

      _context.TravelRequests.Remove(request);
      await _context.SaveChangesAsync();

      return Ok(new { message = "Travel request deleted successfully." });
    }
  }
}
