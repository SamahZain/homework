using Homework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework.Services
{
    internal interface ISubjectService
    {
        public ICollection<Subject> Index();

        public Task<bool> Create(Subject subject);

        public Task<Subject> Update(Subject subject);

        public Task<bool> Delete(Subject s);

        public Subject? Show(int id);

        public List<Subject> DisplayByDepartment(int deptId);

        public List<Subject> DisplayByYear(int year);

        public List<Subject> DisplayByTerm(int term);

        public List<SubjectLecture> ViewLectures(int id);
    }
}
