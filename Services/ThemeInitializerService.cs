using AppelOffreApp.Models;
using LiteDB;

namespace AppelOffreApp.Services
{
    public class ThemeInitializerService
    {
        private readonly LiteDatabase _db;

        public ThemeInitializerService(LiteDatabase db)
        {
            _db = db;
        }

        public void InitialiserThemesSiVide()
        {
            var collection = _db.GetCollection<Theme>("themes");

            if (collection.Count() == 0)
            {
                var themesParDefaut = new List<Theme>
                {
                    new() { Nom = "Informatique" },
                    new() { Nom = "Électronique" },
                    new() { Nom = "Électricité" },
                    new() { Nom = "Plomberie" },
                    new() { Nom = "Construction et BTP" },
                    new() { Nom = "Fournitures de bureau" },
                    new() { Nom = "Mobilier" },
                    new() { Nom = "Papeterie" },
                    new() { Nom = "Maintenance industrielle" },
                    new() { Nom = "Transport & Logistique" },
                    new() { Nom = "Communication & Marketing" },
                    new() { Nom = "Sécurité & Surveillance" },
                    new() { Nom = "Nettoyage & Hygiène" },
                    new() { Nom = "Matériel Médical" },
                    new() { Nom = "Télécommunications" },
                    new() { Nom = "Énergies renouvelables" }
                };

                collection.InsertBulk(themesParDefaut);
            }
        }
    }
}

