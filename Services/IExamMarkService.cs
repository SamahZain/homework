using Homework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework.Services
{
    internal interface IExamMarkService
    {
        public ICollection<ExamMark> Index();

        public Task<bool> Create(ExamMark exam_mark);

        public Task<ExamMark> Update(ExamMark exam_mark);

        public Task<bool> Delete(ExamMark em);
    }
}
