namespace AppelOffreApp.Models
{
    public class AppelOffre
    {
        public int Id { get; set; }
        public string Titre { get; set; }
        public string Description { get; set; }
        public string Domaine { get; set; }
        public DateTime DatePublication { get; set; }
        public DateTime DateLimite { get; set; }
        public string Statut { get; set; } // "Ouvert", "Attribué", etc.
        public string FichierPdfId { get; set; } // ID du fichier dans LiteDB FileStorage
        public int CreeParId { get; set; }
    }
}