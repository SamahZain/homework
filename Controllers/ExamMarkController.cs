using Homework.Models;
using Homework.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework.Controllers
{
    internal class ExamMarkController
    {
        IExamMarkService service;
        IExamService examService;
        IStudentService studentService;
        public ExamMarkController(IExamMarkService ser, IExamService examService,IStudentService studentService)
        {
            service = ser;
            this.examService = examService;
            this.studentService = studentService;

        }


        public int CheckStudentId(string method)
        {
            Console.WriteLine("Chosse Id of Student");
            List<Student> students = studentService.Index().ToList();
            foreach (Student item in students)
            {
                Console.WriteLine($"Id:  {item.Id} \t FullName:{item.FirstName} {item.LastName}");
            }
            int id;
            Student d;
            do
            {
                Console.Write("Id: ");
                string temp = Console.ReadLine();
                if (method == "update" && temp == ".")
                {
                    return 0;
                }
                id = temp == "" ? -1 : Convert.ToInt32(temp);
                d = students.FirstOrDefault(d => d.Id == id);
                if (d == null || id == -1)
                {
                    Console.WriteLine("Choose a Correct Id");
                }
            } while (d == null || id == -1);
            return id;
        }


        public int CheckExamtId(string method)
        {
            Console.WriteLine("Chosse Id of Exame");
            List<Exam> exams = examService.Index().ToList();
            foreach (Exam item in exams)
            {
                Console.WriteLine($"Id:  {item.Id} \t Name:{item.Subject.Name} \t Term: {item.Term}");
            }
            int id;
            Exam d;
            do
            {
                Console.Write("Id: ");
                string temp = Console.ReadLine();
                if (method == "update" && temp == ".")
                {
                    return 0;
                }
                id = temp == "" ? -1 : Convert.ToInt32(temp);
                d = exams.FirstOrDefault(d => d.Id == id);
                if (d == null || id == -1)
                {
                    Console.WriteLine("Choose a Correct Id");
                }
            } while (d == null || id == -1);
            return id;
        }


        public int CheckInt(string method)
        {
            string temp;
            do
            {
                Console.Write("Mark: ");
                temp = Console.ReadLine();
                if (temp == "." && method == "update")
                    return -1;
                if (temp == "")
                {
                    Console.WriteLine("Enter Mark Please!");
                }
            } while (temp == "");
            return int.Parse(temp);
        }


        public ExamMark GetExamMark()
        {
            int id;
            ExamMark d;
            do
            {
                Console.Write("Id: ");
                string temp = Console.ReadLine();
                id = temp == "" ? -1 : Convert.ToInt32(temp);
                d = service.Index().FirstOrDefault(d => d.Id == id);
                if (d == null || id == -1)
                {
                    Console.WriteLine("Choose a Correct Id");
                }
            } while (d == null || id == -1);
            return d;
        }

        public async void Index()
        {
            Console.WriteLine("Loading...");
            List<ExamMark> examMarks = service.Index().ToList();
            Console.WriteLine("=================================================================================\r\n|\tId\t|\tStudent\t|\tExam\t|\tTerm\t|\tMark\t|");

            foreach (ExamMark item in examMarks)
            {
                Console.WriteLine(String.Format("-----------------------------------------------------------------\r\n|\t{0}\t|\t{1}\t|\t{2}\t|\t{3}\t|\t{4}\t",
                    item.Id,
                    (item.Student.FirstName + item.Student.LastName),
                    item.Exam.Subject.Name,
                    item.Exam.Term,
                    item.Mark
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
                default:
                    {
                        return;
                    }

            }
        }


        public async void Create()
        {
            int studentId = CheckStudentId("create");
            int examId = CheckExamtId("create");
            int mark = CheckInt("create");
   

            ExamMark e = new ExamMark()
            {
                StudentId = studentId,
                ExamId = examId,
                Mark = mark
            };


            await service.Create(e);
            Index();
        }


        public async void Update()
        {
            Console.WriteLine("Choose Id To Update");
            ExamMark e = GetExamMark();
            Console.WriteLine("Important!!!!!!\n set (.) if youd don't want update attribute");
            int studentId = CheckStudentId("update");
            int examId = CheckExamtId("update");
            int mark = CheckInt("update");

            if (studentId != 0)
                e.StudentId = studentId;

            if (examId != 0)
                e.ExamId = examId;

            if (mark != -1)
                e.Mark = mark;

            await service.Update(e);
            Index();
        }


        public async void Delete()
        {
            Console.WriteLine("Choose Id To Delete an Exam Mark");
            ExamMark e = GetExamMark();
            await service.Delete(e);
            Index();
        }


    }
}
