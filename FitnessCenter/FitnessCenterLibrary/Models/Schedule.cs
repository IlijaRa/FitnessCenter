﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FitnessCenterLibrary.Models
{
    public class Schedule
    {
        //navigation properties
        //composite key
        public int FitnessCenterHallId { get; set; }
        public FitnessCenterHall FitnessCenterHall { get; set; }
        public int TermId { get; set; }
        public Term Term { get; set; }
    }
}
