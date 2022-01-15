using System;
namespace test.espinosa_oscar.evertec.Models
{
    public class Tip
    {
        public int ID { get; set; }
        public DateTime Create_Date { get; set; }
        public DateTime Update_Date { get; set; }
        public string Autor { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
