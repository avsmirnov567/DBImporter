namespace DBImporterServer.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Diagnoses
    {
        public Guid id { get; set; }

        public Guid doctor { get; set; }

        public Guid patient { get; set; }

        public Guid disease { get; set; }

        [Column(TypeName = "date")]
        public DateTime date { get; set; }

        public virtual Diseases Diseases { get; set; }

        public virtual Doctors Doctors { get; set; }

        public virtual Patients Patients { get; set; }
    }
}
