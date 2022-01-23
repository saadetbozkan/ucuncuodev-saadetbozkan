﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Data.DataModel
{
    public class Container
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string ContainerName { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public long VehicleId { get; set; }

        [ForeignKey("VehicleId")]
        public Vehicle Vehicle { get; set; }
    }
}
