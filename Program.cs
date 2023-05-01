using Homework.Controllers;
using Homework.Services;

namespace Homework
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int choice;
            while (true)
            {

                Console.WriteLine(" Welcom To TCC \n Which Table Do You Want Show?");
                Console.WriteLine(" 1. Student \n 2. Subject \n 3. Subject Lecture \n 4. Department \n 5. Exam \n 6. Exam Mark \n 7. Exit");
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        {
                            StudentController st = new StudentController(new StudentService(), new DepartmentService());
                            st.Index();
                            break;
                        }
                    case 2:
                        {
                            SubjectController su = new SubjectController(new SubjectService(), new DepartmentService());
                            su.Index();
                            break;
                        }
                    case 3:
                        {
                            SubjectLecturesController sl = new SubjectLecturesController(new SubjectLectureService(), new SubjectService());
                            sl.Index();
                            break;
                        }
                    case 4:
                        {
                            DepartmentController d = new DepartmentController(new DepartmentService());
                            d.Index();
                            break;
                        }
                    case 5:
                        {
                            ExamController e = new ExamController(new ExamService(), new SubjectService());
                            e.Index();
                            break;
                        }
                    case 6:
                        {
                            ExamMarkController em = new ExamMarkController(new ExamMarkService(), new ExamService(), new StudentService());
                            em.Index();
                            break;
                        }
                    case 7:
                        {
                            Console.WriteLine("Thanks For Using Our Application ^_^");
                            return;                          
                        }
                    default:
                        {
                            Console.WriteLine("Please Enter A Correct Choice");
                            break;
                        }

                }
            }

        }
    }
}