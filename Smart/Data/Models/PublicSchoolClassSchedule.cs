﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Smart.Data.Models
{
    [Table(nameof(PublicSchoolClassSchedule))]
    public class PublicSchoolClassSchedule
    {
        public int PublicSchoolClassScheduleId { get; set; }
        public int StudentPublicSchoolClassId { get; set; }
        public int ScheduleAvailabilityId { get; set; }

        public virtual StudentPublicSchoolClass StudentPublicSchoolClass { get; set; }
        public virtual ScheduleAvailability ScheduleAvailabilityd { get; set; }
    }
}
