using Homework.Models;
using Homework.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework.Controllers
{
    internal class SubjectLecturesController
    {
        ISubjectLecturesService service;
        ISubjectService subjectservice;

        public SubjectLecturesController(ISubjectLecturesService ser, ISubjectService subjectservice)
        {
            service = ser;
            this.subjectservice = subjectservice;

        }

public void ShowLecture()
        {
            int id;
            SubjectLecture? lecture;
            do
            {
                Console.Write("Enter Lecture Id:\t");
                id = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Loading ...");
                lecture = service.Show(id);
                if (lecture == null)
                {
                    Console.WriteLine("Couldn't find Lecture... please try again");
                }
            } while (lecture == null);
            Console.WriteLine("Title:\t"+lecture.Title);
            Console.WriteLine("Content:");
            Console.WriteLine(lecture.Content);
        }

        public string Check(string check)
        {
            string data;
            do
            {
                Console.Write(check + ": ");
                data = Console.ReadLine();
                if (data == "")
                {
                    Console.WriteLine(String.Format("Enter {0} Please!", check));
                }
            } while (data == "");
            return data;
        }


        public int CheckId(string method)
        {
            Console.WriteLine("Chosse Id of Subject");
            List<Subject> subjects = subjectservice.Index().ToList();
            foreach (Subject item in subjects)
            {
                Console.WriteLine("Id: " + item.Id + "\t Name:" + item.Name);
            }
            int id;
            Subject d;
            do
            {
                Console.Write("Id: ");
                string temp = Console.ReadLine();
                if (method == "update" && temp == ".")
                {
                    return 0;
                }
                id = temp == "" ? -1 : Convert.ToInt32(temp);
                d = subjects.FirstOrDefault(d => d.Id == id);
                if (d == null || id == -1)
                {
                    Console.WriteLine("Choose a Correct Id");
                }
            } while (d == null || id == -1);
            return id;
        }


        public SubjectLecture GetSubjectLecture()
        {
            int id;
            SubjectLecture s;
            do
            {
                Console.Write("Id: ");
                string temp = Console.ReadLine();
                id = temp == "" ? -1 : Convert.ToInt32(temp);
                s = service.Index().FirstOrDefault(d => d.Id == id);
                if (s == null || id == -1)
                {
                    Console.WriteLine("Choose a Correct Id");
                }
            } while (s == null || id == -1);
            return s;
        }


        public async void Index()
        {
            Console.WriteLine("Loading...");
            List<SubjectLecture> subjectLectures = service.Index().ToList();
            Console.WriteLine("=================================================================================\r\n|\tId\t|\tTitle\t\t|\tContent\t\t|\tSubject\t|");

            foreach (SubjectLecture item in subjectLectures)
            {
                Console.WriteLine(String.Format("---------------------------------------------------------------------------------\r\n|\t{0}\t|\t{1}\t\t|\t{2}\t\t|\t{3}\t|",
                    item.Id,
                    item.Title,
                    item.Content,
                    item.Subject.Name
                    ));

            }

            Console.WriteLine();
            Console.WriteLine("choose options");
            Console.WriteLine("1. Create \n2. Update \n3. Delete \n4. Show \n5. back");
            Console.WriteLine();
            Console.Write("Choose: ");
            int chosse = Convert.ToInt32(Console.ReadLine());

            switch (chosse)
            {
                case 1:
                    {
                        Create();
                        break;
                    }
                case 2:
                    {
                        Update();
                        break;
                    }
                case 3:
                    {
                        Delete();
                        break;
                    }
                case 4:
                    {
                        ShowLecture();
                        break;
                    }
                default:
                    {
                        return;
                    }
            }
        }

        public async void Create()
        {
            string title, content;
            title = Check("Titel");
            content = Check("Content");
            int subjectId = CheckId("create");

            SubjectLecture s = new SubjectLecture()
            {
                SubjectId = subjectId,
                Content = content,
                Title = title
            };

            await service.Create(s);
            Index();
        }


        public async void Update()
        {
            Console.WriteLine("Choose Id To Update");
            SubjectLecture s = GetSubjectLecture();

            Console.WriteLine("Important!!!!!!\n set (.) if youd don't want update attribute");

            string title, content;
            title = Check("Titel");
            content = Check("Content");
            int subjectId = CheckId("update");

            if (title != ".")
                s.Title = title;

            if (content != ".")
                s.Content = content;

            if (subjectId != 0)
                s.SubjectId = subjectId;
            await service.Update(s);
            Index();
        }

        public async void Delete()
        {
            Console.WriteLine("Choose Id To Delete a Subject Lecture");
            SubjectLecture s = GetSubjectLecture();
            await service.Delete(s);
            Index();
        }

    }
}
