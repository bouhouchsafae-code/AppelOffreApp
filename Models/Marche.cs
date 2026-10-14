namespace AppelOffreApp.Models
{
    public class Marche
    {
        public int Id { get; set; }
        public int AppelOffreId { get; set; }
        public int EntrepriseId { get; set; }
        public decimal MontantFinal { get; set; }
        public int DureeMois { get; set; } = 6;
    }
}