using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telhai.CS.FinalProject
{
    public class Exam
    {
        public string examName { get; set; }
        public string id { get; set; }
        public DateTime date { get; set; }
        public string TeacherName { get; set; }
        public DateTime BeginTime { get; set; }
        public float duration { get; set; }
        public bool isRandom { get; set; }
        public List<Question> questions { get; set; }

        public Exam(string exName)
        {
            this.examName = exName;
            this.id = Guid.NewGuid().ToString();
        }
    }
}
