namespace AppelOffreApp.Models
{
    public class Soumission
    {
        public int Id { get; set; }
        public int AppelOffreId { get; set; }
        public int EntrepriseId { get; set; }
        public DateTime DateSoumission { get; set; }
        public decimal MontantPropose { get; set; }
        public string FichierOffre { get; set; }
    }
}
