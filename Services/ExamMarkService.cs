using Homework.Data;
using Homework.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework.Services
{
    internal class ExamMarkService : IExamMarkService
    {
        ApplicationDbContext context = new ApplicationDbContext();

        public ICollection<ExamMark> Index()
        {
            return context.ExamMarks.Include(e=>e.Student).Include(e=>e.Exam).ThenInclude(e=>e.Subject).ToList();
        }


        public async Task<bool> Create(ExamMark em)
        {
            try
            {
                await context.ExamMarks.AddAsync(em);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }


        public async Task<ExamMark> Update(ExamMark em)
        {
            context.Update(em);
            context.SaveChanges();
            return em;
        }

        public async Task<bool> Delete(ExamMark em)
        {
            context.ExamMarks.Remove(em);
            context.SaveChanges();
            return true;
        }
    }
}
