﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Smart.Data.Models
{
    [Table(nameof(StudentMeeting))]
    public class StudentMeeting
    {
        public int StudentId { get; set; }
        public int MeetingId { get; set; }

        public virtual Student Student { get; set; }
        public virtual Meeting Meeting { get; set; }
    }
}
