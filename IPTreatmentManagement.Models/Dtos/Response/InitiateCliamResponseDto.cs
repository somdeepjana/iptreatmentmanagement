﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPTreatmentManagement.Models.Dtos.Response
{
    public class InitiateCliamResponseDto
    {
        [Required]
        public int AmountToBePaid { get; set; }
    }
}
