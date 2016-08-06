using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShelterAvailability.Models.ShelterModels
{
    public class Agency
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AgencyID { get; set; }
        public string Name { get; set; }
        public DateTime LastUpdated { get; set; }

        public string PhoneNumber { get; set; }
    }
}
