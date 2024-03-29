﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryReadWrite
{
    internal class Student
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public DateTime DateOfBirth { get; set; }
        public decimal AverageScore { get; set; }

        public string ToText()
        {
            return $"{Name}, {DateOfBirth.ToString("dd.MM.yyyy")}, {AverageScore}";
        }
    }
}
