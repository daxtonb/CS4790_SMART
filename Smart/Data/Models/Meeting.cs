using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System;
using Smart.Extensions;

namespace Smart.Data.Models
{
    [Table(nameof(Meeting))]
    public class Meeting
    {
        public int MeetingId { get; set; }
        public int ClassId { get; set; }
        public int ScheduleAvailabilityId { get; set; }
        public int MeetingOrderNum { get; set; }

        public virtual Class Class { get; set; }
        public virtual ScheduleAvailability ScheduleAvailability { get; set; }
        public virtual ICollection<StudentMeeting> StudentMeetings { get; set; }

       
    }
}