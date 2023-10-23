using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CretaceousApi.Models;

namespace CretaceousApi.Controllers
{
  [Route("api/[controller]")] // specifies that the base request URL for AnimalsController is /api/animals
  [ApiController]
  public class AnimalsController : ControllerBase
  {
    private readonly CretaceousApiContext _db;

    public AnimalsController(CretaceousApiContext db)
    {
      _db = db;
    }

    // GET api/animals
    [HttpGet] // Get() action returns ActionResult type <IEnumerable<Animal>>
    public async Task<ActionResult<IEnumerable<Animal>>> Get()
    {
      return await _db.Animals.ToListAsync(); // Get() endpoint returns C# object, but .NET auto converts it into JSON
    }

    // GET: api/Animals/5
    // GetAnimal()'s action attribute [HttpGet("{id}")] - accepts an argument "{id}", configuring the endpoint to expect another value added: /api/animals/{id}
    [HttpGet("{id}")] // GetAnimal() action returns ActionResult type <Animal>
    public async Task<ActionResult<Animal>> GetAnimal(int id) // specifies that id should be an integer
    {
      Animal animal = await _db.Animals.FindAsync(id);

      if (animal == null) // when we want to generate another type of HTTP status code, we use ControllerBase methods
      {
        return NotFound(); // a ControllerBase class method. It sends a 404 Not Found HTTP status code 
      }

      return animal; // GetAnimal() endpoint returns C# object, but .NET auto converts it into JSON
    }
  }
}
