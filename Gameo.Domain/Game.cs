using System;

namespace Gameo.Domain
{
    public class Game : Entity
    {
        public string CustomerName { get; set; }
        public string CustomerContactNumber { get; set; }
        public DateTime InTime { get; set; }
        public DateTime OutTime { get; set; }
        public decimal Price { get; set; }
        public string ConsoleName { get; set; }
        public GamePaymentType GamePaymentType { get; set; }
    }
}