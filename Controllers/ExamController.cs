using Homework.Models;
using Homework.Services;

namespace Homework.Controllers
{
    internal class ExamController
    {
        IExamService service;
        ISubjectService subjectService;
        public ExamController(IExamService ser, ISubjectService subjectService)
        {
            service = ser;
            this.subjectService = subjectService;
        }



        public int CheckId(string method)
        {
            Console.WriteLine("Chosse Id of Subject");
            List<Subject> subjects = subjectService.Index().ToList();
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



        public short CheckShort(string method)
        {
            string temp;
            do
            {
                Console.WriteLine("Term: ");
                temp = Console.ReadLine();
                if (temp == "." && method == "update")
                    return -1;
                if (temp == "")
                {
                    Console.WriteLine("The Term Must Be Unempty");
                }
                else if (temp != "1" && temp != "2")
                {
                    Console.WriteLine("The Term Must Be 1 OR 2");
                }
            } while (temp == "" || (temp != "1" && temp != "2"));
            return Convert.ToInt16(temp);
        }



        public DateTime CheckDate(string method, DateTime date)
        {
            string temp;
            int day = 0, month = 0, year = 0;
            do
            {
                Console.Write("Day: ");
                temp = Console.ReadLine();
                if (temp == "")
                {
                    Console.WriteLine("Please Enter Day");
                }
                else if (method == "update" && temp == ".")
                {
                    day = date.Day;
                    break;
                }
                else
                {
                    day = Convert.ToInt32(temp);
                }
                if (day <= 0 || day > 31)
                {
                    Console.WriteLine("Enter Correct Day");
                }
            } while (temp == "" || day <= 0 || day > 31);


            do
            {
                Console.WriteLine("Month: ");
                temp = Console.ReadLine();
                if (temp == "")
                {
                    Console.WriteLine("Please Enter Month");
                }

                else if (method == "update" && temp == ".")
                {
                    month = date.Month;
                    break;
                }

                else
                {
                    month = Convert.ToInt32(temp);
                }

                if (month <= 0 || month > 12)
                {
                    Console.WriteLine("Enter Correct Month");
                }

            } while (temp == "" || month <= 0 || month > 12);

            do
            {
                Console.WriteLine("Year");
                temp = Console.ReadLine();
                if (temp == "")
                {
                    Console.WriteLine("Please Enter Year");
                }

                else if (method == "update" && temp == ".")
                {
                    year = date.Year;
                    break;
                }

                else
                {
                    year = Convert.ToInt32(temp);
                }

                if (year < 2000)
                {
                    Console.WriteLine("Enter Correct Year");
                }

            } while (temp == "" || year < 2000);

            string d = $"{month}/{day}/{year}";
            return DateTime.Parse(d);

        }


        public Exam GetExam()
        {
            int id;
            Exam d;
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
            List<Exam> exams = service.Index().ToList();
            Console.WriteLine("=================================================================\r\n|\tId\t|\tSubject\t|\tTerm\t|\tDate\t|");

            foreach (Exam item in exams)
            { 
                Console.WriteLine(String.Format("-----------------------------------------------------------------\r\n|\t{0}\t|\t{1}\t|\t{2}\t|\t{3}\t|",
                    item.Id,
                    item.Subject.Name,
                    item.Term,
                    item.Date.ToLongDateString()
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
                        Show();
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
            int subjectId = CheckId("create");
            short term = CheckShort("create");
            DateTime date = CheckDate("create", DateTime.Now);

            Exam e = new Exam()
            {
                Date = date,
                SubjectId = subjectId,
                Term = term,
            };


            await service.Create(e);
            Index();
        }


        public async void Update()
        {
            Console.WriteLine("Choose Id To Update");
            Exam e = GetExam();
            Console.WriteLine("Important!!!!!!\n set (.) if youd don't want update attribute");
            int subjectId = CheckId("update");
            short term = CheckShort("update");
            DateTime date = CheckDate("update", e.Date);

            if (subjectId != 0)
                e.SubjectId = subjectId;

            if (term != -1)
                e.Term = term;

            e.Date = date;

            await service.Update(e);
            Index();
        }



        public async void Delete()
        {
            Console.WriteLine("Choose Id To Delete an Exam");
            Exam e = GetExam();
            await service.Delete(e);
            Index();
        }


        public void Show()
        {
            int id;
            Exam? exam;
            do
            {
                Console.Write("Enter Exam Id:\t");
                id = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Loading ...");
                exam = service.Show(id);
                if (exam == null)
                {
                    Console.WriteLine("Couldn't find Exam... please try again");
                }
            } while (exam == null);
            Console.WriteLine("|Id\t|Subject\t|Term\t|Date\t\t|Number of Students\t|\r\n-------------------------------------------------------------------------");
            Console.WriteLine(String.Format("|{0}\t|{1}\t|{2}\t|{3}\t|\t{4}\t\t|",
                exam.Id,
                exam.Subject.Name,
                exam.Term,
                exam.Date.ToShortDateString(),
                exam.ExamMarks.Count
                ));
            Console.WriteLine("Choose option:");
            Console.WriteLine("1. Show Marks");
            Console.WriteLine("2. Show Students without exam");
            Console.WriteLine("3. Show Students with exam");
            Console.WriteLine("4. Go Back");
            int option = Convert.ToInt32(Console.ReadLine());
            switch (option)
            {
                case 1:
                    GetMarks(id);
                    break;
                case 2:
                    GetExamStudents(id,false);
                    break;
                case 3:
                    GetExamStudents(id,true);
                    break;
                default:
                    return;
            }
        }



        public void GetExamStudents(int id,bool with)
        {
            Console.WriteLine("Loading ...");
            List<Student> students;
            if (with)
            {
                students = service.GetStudentsWithExam(id);
            }
            else
            {
                students = service.GetStudentsWithoutExam(id);
            }
            Console.WriteLine("|Id\t|Name\t\t|\n-------------------------");
            foreach (var item in students)
            {
                Console.WriteLine(String.Format("|{0}\t|{1}\t|",item.Id,item.FirstName+" "+item.LastName));
            }
            Console.WriteLine("-------------------------");
        }

        public void GetMarks(int id)
        {
            Console.WriteLine("Loading ...");
            var marks = service.ShowMarks(id);
            Console.WriteLine("|Id\t|Student\t|Mark\t|\r\n---------------------------------");
            foreach (var item in marks)
            {
                Console.WriteLine(String.Format("|{0}\t|{1}\t|{2}\t|",
                    item.Id,
                    item.Student.FirstName + " " + item.Student.LastName,
                    item.Mark));
            }
            Console.WriteLine("---------------------------------");
        }
    }
}
