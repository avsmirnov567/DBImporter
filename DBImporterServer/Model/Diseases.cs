namespace DBImporterServer.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Diseases
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Diseases()
        {
            Diagnoses = new HashSet<Diagnoses>();
        }

        public int id { get; set; }

        [Required]
        public string name { get; set; }

        public string profilactic { get; set; }

        public string treatment { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Diagnoses> Diagnoses { get; set; }
    }
}
