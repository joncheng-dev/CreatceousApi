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

    // POST api/animals
    [HttpPost] // HTTP action method specified with Http verb template [HttpPost]
    public async Task<ActionResult<Animal>> Post(Animal animal) // takes an Animal parameter; Comes from the body of the request
    {
      _db.Animals.Add(animal);
      await _db.SaveChangesAsync();
      // CreatedAtAction() - A ControllerBase class method; handles returning HTTP 201 Created. Lets us specify Location of newly created object.
      // In sum, Post() controller action returns newly created Animal object to user, along with "201 Created" status code.
      return CreatedAtAction(nameof(GetAnimal), new { id = animal.AnimalId }, animal);
      // CreatedAtAction() method takes 3 arguments to specify location of new object
      // 1. Name of controller action: GetAnimal()
      // 2. Route values required for controller action: `id` parameter for GetAnimal() action
      // 3. Resource that was created in this action.
    } // Does not return a view
  }
}
