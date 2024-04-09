using MIS_Classroom.Models;

namespace MIS_Classroom.Areas.Student.Models
{
    public class ViewQuestionsViewModel
    {
        public List<TechengineeMisSubject> Subjects { get; set; }
        public List<TechengineeMisQuestion> Questions { get; set; }
    }
}
