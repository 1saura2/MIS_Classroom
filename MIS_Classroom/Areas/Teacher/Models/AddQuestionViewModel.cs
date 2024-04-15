/*using MIS_Classroom.Models;

namespace MIS_Classroom.Areas.Teacher.Models
{
    public class AddQuestionViewModel
    {
        public List<TechengineeMisSubject> Subjects { get; set; }
    }
}
*/

using MIS_Classroom.Models;
using System.Collections.Generic;

namespace MIS_Classroom.Areas.Teacher.Models
{
    public class AddQuestionViewModel
    {
        public List<TechengineeMisSubject> Subjects { get; set; }
        public List<TechengineeMisQuestion> Questions { get; set; }
        public int SubjectId { get; set; }
    }
}
