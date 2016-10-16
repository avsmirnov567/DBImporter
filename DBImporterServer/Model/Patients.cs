namespace DBImporterServer.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Patients
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Patients()
        {
            Diagnoses = new HashSet<Diagnoses>();
        }

        public int id { get; set; }

        [Required]
        public string fullname { get; set; }

        public string adress { get; set; }

        public string telephone { get; set; }

        public string insuranse { get; set; }

        public string passport { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Diagnoses> Diagnoses { get; set; }
    }
}
