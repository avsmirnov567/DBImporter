namespace Server.Model
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

        public int number { get; set; }

        public virtual Diseases Diseases { get; set; }

        public virtual Doctors Doctors { get; set; }

        public virtual Patients Patients { get; set; }
    }
}
