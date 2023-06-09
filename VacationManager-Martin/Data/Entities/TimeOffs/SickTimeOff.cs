﻿using System.ComponentModel.DataAnnotations.Schema;

namespace VacationManager_Martin.Data.Entities.TimeOffs
{
    public class SickTimeOff : BaseTimeOff
    {
        public string AttachmentPath { get; set; }

        [NotMapped]
        public override bool IsHalfDay { get; set; }
    }
}
