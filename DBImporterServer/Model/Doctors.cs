namespace DBImporterServer.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Doctors
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Doctors()
        {
            Diagnoses = new HashSet<Diagnoses>();
        }

        public int id { get; set; }

        [Required]
        public string fullname { get; set; }

        public int? spec { get; set; }

        public string position { get; set; }

        public string adress { get; set; }

        public string telephone { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Diagnoses> Diagnoses { get; set; }

        public virtual Specs Specs { get; set; }
    }
}
