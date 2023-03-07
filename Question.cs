using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telhai.CS.FinalProject
{
    public class Question
    {
        public int Qid { get; set; }
        public int AnswersCount { get; set; }
        public string content { get; set; }
        public int correct { get; set; }
        List<string> answers;
        public Question(int qid)
        {
            this.Qid = qid;
            answers = new List<string>();
        }
        public void add(string answer)
        {
            answers.Add(answer);
        }

    }
}
