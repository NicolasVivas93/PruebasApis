using Microsoft.AspNetCore.Mvc;
using ContosoPizza.Models;
using ContosoPizza.Services;

namespace ContosoPizza.Controllers;

[ApiController] // Sería como el @RestController
[Route("[controller]")] // Probablemente sea como el @RequestMapping("")
public class PizzaController : ControllerBase {
    
    public PizzaController() {

    }

    // GET all action   
    [HttpGet] // Es como si se pusiera @GetMapping("/weatherforecast") en Java
    public ActionResult<List<Pizza>> GetAll() => PizzaService.GetAll(); 

    // GET by Id action
    [HttpGet("{id}")]
    public ActionResult<Pizza> GetById(int id) {
        var pizza = PizzaService.GetById(id);
        if (pizza == null) 
            return NotFound(); // Devuelve un status code de no encontrado, HTTP/1.1 404
        
        return pizza;
    }

    // POST action
    [HttpPost]
    public IActionResult CreatePizza([FromBody] Pizza newPizza) { // El [FromBody] es el equivalente al @RequestBody de Spring
        PizzaService.SavePizza(newPizza);
        return CreatedAtAction(nameof(GetById), new {id = newPizza.Id}, newPizza);  // Devuelve una respuesta con un código    
    }

    // PUT action
    [HttpPut("{id}")]
    public IActionResult UpdatePizza(int id, Pizza updatedPizza) {
        if(id !=  updatedPizza.Id)
            return  BadRequest(); // Devuelve un error bad request, HTTP/1.1 400
        
        var pizzaExistente = PizzaService.GetById(id);
        if(pizzaExistente is null) 
            return NotFound();
        
        PizzaService.UpdatePizza(updatedPizza);
        return NoContent(); //Devuelve una respuesta sin contenido, HTTP/2.0 204
    }

    // DELETE action
    [HttpDelete("{id}")]
    public IActionResult DeletePizza(int id) {
        var pizza = PizzaService.GetById(id);
        if(pizza is null) 
            return NotFound();
        
        PizzaService.DeletePizza(id);
        return NoContent();
    }
}
