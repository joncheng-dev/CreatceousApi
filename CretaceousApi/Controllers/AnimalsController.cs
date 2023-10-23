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

    // PUT: api/Animals/5
    // [HttpPut] verb template specifies the action request made to controller
    [HttpPut("{id}")] // {id} parameter determines which animal is to be updated
    public async Task<IActionResult> Put(int id, Animal animal)
    {
      if (id != animal.AnimalId) // check if {id} in request URL matches animal.AnimalId
      {
        return BadRequest(); // returns HTTP status code 400 Bad Request
      }

      _db.Animals.Update(animal); // If correct, update animal in database

      try
      {
        await _db.SaveChangesAsync(); // try to save asynchronously
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!AnimalExists(id))  
        // handle error if AnimalId does not exist; custom created private method
        {
          return NotFound(); // via ControllerBase.NotFound() method
          // returns 404 Not Found HTTP Status code
        }
        else
        {
          throw; // throw what? How can I follow this?
        }
      }
      return NoContent(); // HTTP status code 204 No Content. Request successful, but no need to navigate away from page.
    }

    private bool AnimalExists(int id)
    {
      return _db.Animals.Any(e => e.AnimalId == id);
    }

    // DELETE: api/Animals/5
    [HttpDelete("{id}")] // [HttpDelete] verb template in new controller action DeleteAnimal(). Takes argument "{id}". We rely on URL to get animal's id.
    // DeleteAnimal() method is asynchronous; makes use of ControllerBase class methods to return HTTP status codes
    public async Task<IActionResult> DeleteAnimal(int id)
    {
      Animal animal = await _db.Animals.FindAsync(id);
      if (animal == null)
      {
        return NotFound(); // via ControllerBase.NotFound() method
        // returns 404 Not Found HTTP Status code
      }

      _db.Animals.Remove(animal);
      await _db.SaveChangesAsync();

      return NoContent(); // HTTP status code 204 No Content if successful
    }    
  }
}
