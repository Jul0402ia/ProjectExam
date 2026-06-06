using Microsoft.AspNetCore.Mvc;
using TemplateLib.Models;
using TemplateLib.Repositories;

namespace TemplateREST.Controllers
{
    [Route("api/[controller]")] //url starter med api/ derefter controllerens navn 
    [ApiController] //gør det til en api controller, håndtagerer automatisk modelvalidering og binding af data fra HTTP-anmodninger
    public class TemplateController : ControllerBase //får api funktioner som f.eks. Ok(), NotFound(), BadRequest() 
    {
        private readonly RepositoryTemplate _repository; //controlleren har et repo, som den kan bruge til data. Readonly betyder at det kun kan tildeles i constructoren og ikke ændres senere

        public TemplateController(RepositoryTemplate repository) //constructor, der tager et RepositoryTemplate-objekt som parameter. Dette objekt vil blive injiceret af ASP.NET Core's afhængighedsinjektion, når controlleren oprettes
        {
            _repository = repository; //tildeler det injicerede repository til den private readonly field, så det kan bruges i controllerens metoder
        }

        [HttpGet] //denne metode reagerer på http get anmodninger til api/template
        public ActionResult<IEnumerable<ModelTemplate>> GetAll() //ActionResult giver mulighed for at retunere http status koder sammen med data
        {
            return Ok(_repository.GetAll()); //retunere alle ModelTemplate objekter fra repositoryet og pakker det i en Ok() response, som automatisk sætter statuskoden til 200
        }
        [HttpGet("{id}")] //denne metode reagerer på http get anmodninger til api/template/{id}, hvor {id} er en variabel del af URL'en, der repræsenterer id'et for det ønskede objekt
        public ActionResult<ModelTemplate> GetById(int id)
        {
            ModelTemplate? item = _repository.GetById(id);

            if (item == null)
            {
                return NotFound("Objekt med dette id blev ikke fundet.");
            }

            return Ok(item);
        }
        [HttpPost]
        public ActionResult<ModelTemplate> Add(ModelTemplate newItem) //dataen for det nye objekt kommer fra HTTP-anmodningens body, og ASP.NET Core binder det automatisk til newItem-parameteren
        {
            ModelTemplate createdItem = _repository.Add(newItem); //tilføjer ny objekt til repo og får det objekt tilbage (med tildelt id)
            return CreatedAtAction(nameof(GetById), new { id = createdItem.Id }, createdItem); //retunere en CreatedAtAction response, som automatisk sætter statuskoden til 201 og inkluderer en Location-header, der peger på den nye ressource (ved at bruge GetById-metoden og det nye objekts id)

        }
        [HttpPut("{id}")]
        public ActionResult<ModelTemplate> Update(int id, ModelTemplate itemData) //data for det opdaterede obj kommer fra HTTP-anmodningens body, og id'et kommer fra URL'en
        {
            ModelTemplate? updatedItem = _repository.Update(id, itemData); //opdaterer objektet i repo og får det opdaterede objekt tilbage

            if (updatedItem == null) 
            {
                return NotFound("Objekt med dette id findes ikke.");
            }

            return Ok(updatedItem); 
        }
        [HttpDelete("{id}")]
        public ActionResult<ModelTemplate> Delete(int id)
        {
            ModelTemplate? deletedItem = _repository.Delete(id); //sletter objektet i repo og får det slettede objekt tilbage

            if (deletedItem == null)
            {
                return NotFound("Objekt med dette id findes ikke.");
            }

            return Ok(deletedItem);
        }
    }
}