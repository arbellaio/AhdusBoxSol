using System.Collections.Generic;

namespace AhdusBoxSol.Models
{
    public class Box
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public List<BoxDetail> BoxDetails { get; set; }
    }

    public class BoxDetail
    {
        public string BoxId { get; set; }
        public string Detail { get; set; }
    }
}