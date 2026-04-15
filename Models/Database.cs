namespace WebApplication2.Models;

public class Database
{
    public static List<Room> Rooms { get; set; } = new()
    {
        new Room { Id = 1, Name = "Sala A", BuildingCode = "A", Floor = 1, Capacity = 30, HasProjector = true, IsActive = true },
        new Room { Id = 2, Name = "Lab 204", BuildingCode = "B", Floor = 2, Capacity = 24, HasProjector = true, IsActive = true },
        new Room { Id = 3, Name = "Sala Konferencyjna", BuildingCode = "A", Floor = 5, Capacity = 100, HasProjector = true, IsActive = true },
        new Room { Id = 4, Name = "Pokój Cichy", BuildingCode = "C", Floor = 1, Capacity = 4, HasProjector = false, IsActive = true },
        new Room { Id = 5, Name = "Magazyn", BuildingCode = "C", Floor = -1, Capacity = 10, HasProjector = false, IsActive = false } // Nieaktywna
    };

    public static List<Reservation> Reservations { get; set; } = new()
    {
        new Reservation { Id = 1, RoomId = 1, OrganizerName = "Jan Kowalski", Topic = "Spotkanie Zarządu", Date = new DateOnly(2026, 5, 10), StartTime = new TimeSpan(10, 0, 0), EndTime = new TimeSpan(12, 0, 0), Status = "confirmed" },
        new Reservation { Id = 2, RoomId = 2, OrganizerName = "Anna Nowak", Topic = "Warsztaty .NET", Date = new DateOnly(2026, 5, 10), StartTime = new TimeSpan(13, 0, 0), EndTime = new TimeSpan(16, 0, 0), Status = "planned" },
        new Reservation { Id = 3, RoomId = 3, OrganizerName = "Piotr Wiśniewski", Topic = "Konferencja IT", Date = new DateOnly(2026, 5, 12), StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(17, 0, 0), Status = "confirmed" },
        new Reservation { Id = 4, RoomId = 1, OrganizerName = "Ewa Kaczmarek", Topic = "Szkolenie BHP", Date = new DateOnly(2026, 5, 11), StartTime = new TimeSpan(8, 0, 0), EndTime = new TimeSpan(10, 0, 0), Status = "cancelled" },
        new Reservation { Id = 5, RoomId = 4, OrganizerName = "Tomasz Lis", Topic = "Rozmowa rekrutacyjna", Date = new DateOnly(2026, 5, 10), StartTime = new TimeSpan(11, 0, 0), EndTime = new TimeSpan(11, 30, 0), Status = "confirmed" }
    };
}