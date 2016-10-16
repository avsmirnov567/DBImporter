namespace DBImporterServer.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Diagnoses
    {
        public int id { get; set; }

        public int patient { get; set; }

        public int doctor { get; set; }

        public int disease { get; set; }

        [Column(TypeName = "date")]
        public DateTime date { get; set; }

        public virtual Diseases Diseases { get; set; }

        public virtual Doctors Doctors { get; set; }

        public virtual Patients Patients { get; set; }
    }
}
