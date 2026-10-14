using AppelOffreApp.Models;
using LiteDB;

namespace AppelOffreApp.Services
{
    public class DatabaseService
    {
        private readonly LiteDatabase _db;

        public DatabaseService(LiteDatabase db)
        {
            _db = db;
        }

        public ILiteCollection<Utilisateur> Utilisateurs =>
            _db.GetCollection<Utilisateur>("utilisateurs");

        public ILiteCollection<AppelOffre> AppelsOffres =>
            _db.GetCollection<AppelOffre>("appels_offres");

        public ILiteCollection<Soumission> OffresSoumises =>
            _db.GetCollection<Soumission>("soumissions");

        public ILiteCollection<Marche> Marches =>
            _db.GetCollection<Marche>("marches");

        public ILiteCollection<Theme> Themes =>
            _db.GetCollection<Theme>("themes");
        public LiteDatabase Database => _db;

        // Ajoute ici d'autres collections selon ton projet
    }
}
