using Microsoft.AspNetCore.Mvc;
using WebApplication2.DTOs;
using WebApplication2.Models;

namespace WebApplication2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnimalsController : ControllerBase
{
    public static List<Animal> animals = [
        new Animal()
        {
            Id = 1,
            Name = "Pimpek",
            Species = "Mammal",
            Weight = 1.5,
        },
        new Animal()
        {
            Id = 2,
            Name = "Bartek",
            Species = "Mammal",
            Weight = 1.5,
        }
    ];

    // GET /api/animals
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(animals.Select(e => new AnimalDto()
        {
            Id = e.Id,
            Name = e.Name,
            Species = e.Species,
            Weight = e.Weight
        }));
    }
    
    // GET /api/animals/30
    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var animal = animals.FirstOrDefault(e => e.Id == id);

        if (animal is null)
        {
            return NotFound($"Zwierze o id {id} mie istnieje!");
        }
        
        return Ok(new AnimalDto
        {
            Id = animal.Id,
            Name = animal.Name,
            Species = animal.Species,
            Weight = animal.Weight
        });
    }
    
    // POST /api/animals
    [HttpPost]
    public IActionResult Add([FromBody] CreateAnimalDto animalDto)
    {
        var animal = new Animal()
        {
            Id = animals.Max(e => e.Id) + 1,
            Name = animalDto.Name,
            Species = animalDto.Species,
            Weight = animalDto.Weight
        };
        
        animals.Add(animal);

        return CreatedAtAction(nameof(GetById), new { id = animal.Id }, animal);
    }
    
    // PUT /api/animals/3
    [HttpPut("{id:int}")]
    public IActionResult Update(int id, [FromBody] UpdateAnimalDto animalDto)
    {
        var animal = animals.FirstOrDefault(e => e.Id == id);

        if (animal is null)
        {
            return NotFound($"Zwierze o id {id} mie istnieje!");
        }
        
        animal.Name = animalDto.Name;
        animal.Species = animalDto.Species;
        animal.Weight = animalDto.Weight;

        return NoContent();
    }
    
    // DELETE /api/animals/3
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var animal = animals.FirstOrDefault(e => e.Id == id);
        
        if (animal is null)
        {
            return NotFound($"Zwierze o id {id} mie istnieje!");
        }
        
        animals.Remove(animal);
        
        return NoContent();
    }
    
    
    // GET /api/animals/hello
    [HttpGet("hello")]
    // [Route("hello")]
    public IActionResult SayHello()
    {
        return Ok("Hello");
    }
}