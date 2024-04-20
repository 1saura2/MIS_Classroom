using System;
using System.Collections.Generic;

namespace MIS_Classroom.Models
{
    public partial class TechengineeMisAnswer
    {
        public int AnswerId { get; set; }
        public int? QuestionId { get; set; }
        public int? StudentId { get; set; }
        public string? AnswerTxt { get; set; }

        // Navigation properties
        public TechengineeMisQuestion Question { get; set; }
        public TechengineeMisStudent Student { get; set; }
    }
}
