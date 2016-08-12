using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
//using System.Data.Spatial;

namespace ShelterAvailability.Models.ShelterModels
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class Shelter
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ShelterID { get; set; }
        //public DateTime InfoLastUpdated { get; set; }
        public string Name { get; set; }

        public int CurrentTotalSpaces { get; set; }
        public int SingleSpacesAvailable { get; set; }
        public int FamilySpacesAvailable { get; set; }
        public int CurrentPopulation { get; set; }
        public DateTime AvailabilityLastUpdated { get; set; }

        //Location
          

        //Shelter Status
        public ShelterStatus status { get; set; }
        //Type of Shelter
        //Shelter Information
        //Resident Restrictions

        //Basic Shelter Services

        //Additional Services

        //Facility Access(ADA standards)
        //Facility Restrictions
        //Child Notes
        //Languages
        //Gender Accepted
        //Family Accomodations
        //Intake Process
        //Intake Requirements
        //Intake Limitations
        //Contact Person
        //Shelter Address
        //Shelter City
        public string City { get; set; }
        //Shelter State
        //Shelter Zip
        //Shelter Main Phone
        public string PhoneNumber { get; set; }
        //Shelter Hotline Phone
        //Shelter Fax
        //Shelter Email
        //Shelter Web Address
        //Opening Date
        //Planned Closing Date

        //Sponsor/Parent Agency Name
        public virtual Agency Agency { get; set; }
    }

    public enum ShelterStatus
    {
        Active,
        Cancelled,
        Inactive,
        OnHold,
        New,
        Rejected,
        Suspended,
        Temporary,
        AgencyCancelled,
        Standby
    }
}
