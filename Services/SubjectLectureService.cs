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
    internal class SubjectLectureService : ISubjectLecturesService
    {
        ApplicationDbContext context = new ApplicationDbContext();

        public ICollection<SubjectLecture> Index()
        {
            return context.SubjectLectures.Include(sl=>sl.Subject).ToList();
        }


        public async Task<bool> Create(SubjectLecture sl)
        {
            try
            {
                await context.SubjectLectures.AddAsync(sl);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }


        public async Task<SubjectLecture> Update(SubjectLecture sl)
        {
            context.Update(sl);
            context.SaveChanges();
            return sl;
        }


        public async Task<bool> Delete(SubjectLecture s)
        {
            context.SubjectLectures.Remove(s);
            context.SaveChanges();
            return true;
        }

        public SubjectLecture? Show(int id)
        {
            return context.SubjectLectures.FirstOrDefault(l => l.Id == id);
        } 
    }
}