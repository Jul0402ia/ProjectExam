using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateLib.Models;

namespace TemplateLib.Repositories
{
    public class RepositoryTemplate
    {
        //bruges som "database" for at gemme objekter lokalt i hukommelsen
        private List<ModelTemplate> items = new List<ModelTemplate>();

    private static int nextId = 1;
    
        public RepositoryTemplate() { } // Constructor kører når en instans af RepositoryTemplate oprettes

        public IEnumerable<ModelTemplate> GetAll() //IEnumerable gør at vi kan retunere en samling af objekter, uden at give adgang til private liste
        {
            return new List<ModelTemplate>(items); //laver en kopi, så andre ikke kan items.clear() på den orginale liste - begge lister peger på samme obj
        }
        public ModelTemplate? GetById(int id)//? - obj findes måske ikke
        {
            return items.FirstOrDefault(item => item.Id == id); //finder det første objekt i listen, hvor Id matcher, eller returnerer null 
        }
        public ModelTemplate Add(ModelTemplate item)
        {
            item.Id = nextId++; // giver nyt obj et unikt Id ved at tildele det næsteId og derefter øge nextId for næste objekt

            items.Add(item); // tilføjer det nye objekt til (private org) liste

            return item;
        }
        public ModelTemplate? Delete(int id)
        {
            ModelTemplate? item = GetById(id); // finder det objekt, der skal slettes ved hjælp af GetById-metoden

            if (item == null)
            {
                return null;
            }

            items.Remove(item);

            return item; // returnerer det slettede objekt, så vi kan se, hvilket objekt der blev fjernet
        }
        public ModelTemplate? Update(int id, ModelTemplate updatedItem)
        {
            ModelTemplate? existingItem = GetById(id);

            if (existingItem == null)
            {
                return null;
            }

            existingItem.Property1 = updatedItem.Property1; //overskrivelse af værdierne 
            existingItem.Property2 = updatedItem.Property2;
            existingItem.NumberProperty = updatedItem.NumberProperty;

            return existingItem; // returnerer det opdaterede objekt, så vi kan se de nye værdier
        }
    }
    
}