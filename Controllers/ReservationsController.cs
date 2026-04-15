using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetReservations([FromQuery] DateOnly? date, [FromQuery] string? status, [FromQuery] int? roomId)
    {
        var query = Database.Reservations.AsQueryable();

        if (date.HasValue)
        {
            query = query.Where(r => r.Date == date.Value);
        }

        if (!string.IsNullOrWhiteSpace(status))
        {
            query = query.Where(r => r.Status.Equals(status, StringComparison.OrdinalIgnoreCase));
        }

        if (roomId.HasValue)
        {
            query = query.Where(r => r.RoomId == roomId.Value);
        }

        return Ok(query.ToList());
    }
    
    [HttpGet("{id}")]
    public IActionResult GetReservationById(int id)
    {
        var reservation = Database.Reservations.FirstOrDefault(r => r.Id == id);
        
        if (reservation == null)
        {
            return NotFound($"Reservation {id} not found.");
        }

        return Ok(reservation);
    }
    
    [HttpPost]
    public IActionResult CreateReservation([FromBody] Reservation reservation)
    {
        var room = Database.Rooms.FirstOrDefault(r => r.Id == reservation.RoomId);
        if (room == null)
        {
            return BadRequest($"Room {reservation.RoomId} not found.");
        }
        
        if (!room.IsActive)
        {
            return BadRequest($"Room {reservation.RoomId} not active.");
        }
        
        bool isOverlap = Database.Reservations.Any(r => 
            r.RoomId == reservation.RoomId && 
            r.Date == reservation.Date && 
            r.StartTime < reservation.EndTime && 
            r.EndTime > reservation.StartTime);

        if (isOverlap)
        {
            return Conflict("There's already an existing reservation in that time.");
        }
        
        reservation.Id = Database.Reservations.Any() ? Database.Reservations.Max(r => r.Id) + 1 : 1;
        
        Database.Reservations.Add(reservation);

        return CreatedAtAction(nameof(GetReservationById), new { id = reservation.Id }, reservation);
    }
    
    [HttpPut("{id}")]
    public IActionResult UpdateReservation(int id, [FromBody] Reservation updatedReservation)
    {
        var existingReservation = Database.Reservations.FirstOrDefault(r => r.Id == id);
        
        if (existingReservation == null)
        {
            return NotFound($"Reservation {id} not found.");
        }
        
        var room = Database.Rooms.FirstOrDefault(r => r.Id == updatedReservation.RoomId);
        if (room == null) return BadRequest($"Room {updatedReservation.RoomId} not found.");
        if (!room.IsActive) return BadRequest($"Room{updatedReservation.RoomId} not active.");

        bool isOverlap = Database.Reservations.Any(r => 
            r.Id != id &&
            r.RoomId == updatedReservation.RoomId && 
            r.Date == updatedReservation.Date && 
            r.StartTime < updatedReservation.EndTime && 
            r.EndTime > updatedReservation.StartTime);

        if (isOverlap)
        {
            return Conflict("There's already an existing reservation in that time.");
        }
        
        existingReservation.RoomId = updatedReservation.RoomId;
        existingReservation.OrganizerName = updatedReservation.OrganizerName;
        existingReservation.Topic = updatedReservation.Topic;
        existingReservation.Date = updatedReservation.Date;
        existingReservation.StartTime = updatedReservation.StartTime;
        existingReservation.EndTime = updatedReservation.EndTime;
        existingReservation.Status = updatedReservation.Status;

        return Ok(existingReservation);
    }
    
    [HttpDelete("{id}")]
    public IActionResult DeleteReservation(int id)
    {
        var reservation = Database.Reservations.FirstOrDefault(r => r.Id == id);
        
        if (reservation == null)
        {
            return NotFound($"Reservation {id} not found.");
        }

        Database.Reservations.Remove(reservation);
        
        return NoContent();
    }
}