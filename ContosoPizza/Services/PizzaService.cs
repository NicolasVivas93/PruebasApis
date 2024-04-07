using ContosoPizza.Models;

namespace ContosoPizza.Services;

public static class PizzaService {

    static List<Pizza> Pizzas { get; }
    static int nextId = 3;

    static PizzaService( ) {
        Pizzas = new List<Pizza> {
            new Pizza { Id = 1, Name = "Classic Italian", IsGlutenFree = false} ,
            new Pizza { Id = 2, Name = "Veggie Lovers", IsGlutenFree = true }
        };
    }

    public static List<Pizza> GetAll()  => Pizzas; // Retorna todas las pizzas

    public static  Pizza? GetById(int id) =>  Pizzas.FirstOrDefault(p => p.Id == id); // Devuelve la pizza con el ID proporcionado

    public static void SavePizza(Pizza pizza) {
        pizza.Id = nextId++;
        Pizzas.Add(pizza);
    }

    public static void DeletePizza(int id) {
        var pizza = GetById(id);
        if (pizza is null)
            return;

        Pizzas.Remove(pizza);
    }

    public static void UpdatePizza(Pizza pizza) {
        var index = Pizzas.FindIndex(x => x.Id == pizza.Id );
        if (index != -1)
            return;
            
        Pizzas[index] = pizza;

    }



}