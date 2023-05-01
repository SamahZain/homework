using Homework.Models;
using Homework.Services;

namespace Homework.Controllers
{
    internal class SubjectController
    {
        ISubjectService service;
        IDepartmentService departmentService;
        public SubjectController(ISubjectService ser, IDepartmentService departmentService)
        {
            service = ser;
            this.departmentService = departmentService;

        }


        public int CheckId(string method)
        {
            Console.WriteLine("Chosse Id of Department");
            List<Department> depts = departmentService.Index().ToList();
            foreach (Department item in depts)
            {
                Console.WriteLine("Id: " + item.Id + "\t Name:" + item.Name);
            }
            int id;
            Department d;
            do
            {
                Console.Write("Id: ");
                string temp = Console.ReadLine();
                if (method == "update" && temp == ".")
                {
                    return 0;
                }
                id = temp == "" ? -1 : Convert.ToInt32(temp);
                d = depts.FirstOrDefault(d => d.Id == id);
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
                temp = Console.ReadLine();
                if (temp == "." && method == "update")
                    return -1;
                if (temp == "")
                {
                    Console.WriteLine("The Data Must Be Unempty");
                }
            } while (temp == "");
            return int.Parse(temp);
        }
        public short CheckShort(string method, string data)
        {
            string temp;
            do
            {
                Console.Write(data + ": ");
                temp = Console.ReadLine();
                if (temp == "." && method == "update")
                    return -1;
                if (temp == "")
                {
                    Console.WriteLine("The " + data + " Must Be Unempty");
                }
            } while (temp == "");
            return Convert.ToInt16(temp);
        }


        public Subject GetSubject()
        {
            int id;
            Subject s;
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
            List<Subject> subjects = service.Index().ToList();
            Console.WriteLine("==============================================================================================================\r\n|\tid\t|\tName\t|\tMinDegree\t|\tyear\t|\tDepartment\t|\tterm\t|\tNumber Lecture\t|");

            foreach (Subject item in subjects)
            {
                Console.WriteLine(String.Format("---------------------------------------------------------------------------------------------------------\r\n|\t{0}\t|\t{1}\t|\t{2}\t\t|\t{3}\t|\t{4}\t\t|\t{5}\t|\t{6}\t  |",
                    item.Id,
                    item.Name,
                    item.MinDegree,
                    item.Year,
                    item.Department.Name,
                    item.Term,
                    item.SubjectLectures.Count
                    ));
            }
            Console.WriteLine();
            Console.WriteLine("choose options");
            Console.WriteLine("1. Create \n2. Update \n3. Delete \n4. Show \n5. Display By Department \n6. Display By Year \n7.Display By Term \n8.Back");
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
            string name;
            do
            {
                Console.Write("Name: ");
                name = Console.ReadLine();
                if (name == "")
                {
                    Console.WriteLine("Enter Name Please!");
                }
            } while (name == "");

            Console.Write("Minum Degree: ");
            int min = CheckInt("Create");
            Console.Write("Year: ");
            short year = CheckShort("create","Year");
            Console.Write("Term: ");
            short term = CheckShort("create","Term");

            int id = CheckId("create");


            Subject s = new Subject()
            {
                DeptId = id,
                Name = name,
                Term = term,
                Year = year,
                MinDegree = min,
            };
            await service.Create(s);
            Index();
        }



        public async void Update()
        {
            Console.WriteLine("Choose Id To Update");
            Subject s = GetSubject();

            Console.WriteLine("Important!!!!!!\n set (.) if youd don't want update attribute");
            string name;
            do
            {
                Console.Write("Name: ");
                name = Console.ReadLine();
                if (name == "")
                {
                    Console.WriteLine("Enter Name Please!");
                }
            } while (name == "");
            Console.Write("Minum Degree: ");
            int min = CheckInt("update");
            Console.Write("Year: ");
            short year = CheckShort("update","Year");
            Console.Write("Term: ");
            short term = CheckShort("update","Term");

            int deptId = CheckId("update");
            if (name != ".")
                s.Name = name;
            if (min != -1)
                s.MinDegree = min;
            if (year != -1)
                s.Year = year;
            if (term != -1)
                s.Term = term;
            if (deptId != 0)
                s.DeptId = deptId;
            await service.Update(s);
            Index();
        }

        public async void Delete()
        {
            Console.WriteLine("Choose Id To Delete a Subject");
            Subject s = GetSubject();
            await service.Delete(s);
            Index();
        }

        public void Show()
        {
            int id;
            Subject? subject;
            do
            {
                Console.Write("Enter Subject Id:\t");
                id = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Loading ...");
                subject = service.Show(id);
                if (subject == null)
                {
                    Console.WriteLine("Couldn't find Subject... please try again");
                }
            } while (subject == null);
            Console.WriteLine("|Id\t|Name\t\t|Min Degree\t|Term\t|Year\t|Department\t|Number of Lectures\t|\r\n-------------------------------------------------------------------------------------------------");
            Console.WriteLine(String.Format("|{0}\t|{1}\t|\t{2}\t|{3}\t|{4}\t|{5}\t|\t{6}\t\t|",
                subject.Id,
                subject.Name,
                subject.MinDegree,
                subject.Term,
                subject.Year,
                subject.Department.Name,
                subject.SubjectLectures.Count
                ));

            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Get Lectures");
            Console.WriteLine("3. Go Back");

            int option = Convert.ToInt32(Console.ReadLine());

            switch (option)
            {
                case 1:
                    GetLectures(id);
                    break;
                default:
                    return;
            }
        }

        public void DisplayByDepartment()
        {
            int id = CheckId("show");
            List<Subject> subjects = service.DisplayByDepartment(id);
            Console.WriteLine("**************************************************************************************************************\r\n|\tid\t|\tName\t|\tMinDegree\t|\tyear\t|\tDepartment\t|\tterm\t|\tNumber Lecture\t|");

            foreach (Subject item in subjects)
            {
                Console.WriteLine(String.Format("---------------------------------------------------------------------------------------------------------\r\n|\t{0}\t|\t{1}\t|\t{2}\t\t|\t{3}\t|\t{4}\t\t|\t{5}\t|\t{6}\t  |",
                    item.Id,
                    item.Name,
                    item.MinDegree,
                    item.Year,
                    item.Department.Name,
                    item.Term,
                    item.SubjectLectures.Count
                    ));
            }
        }

        public void DisplayByYear()
        {
            short year = CheckShort("show", "Year");
            List<Subject> subjects = service.DisplayByYear(year);
            Console.WriteLine("**************************************************************************************************************\r\n|\tid\t|\tName\t|\tMinDegree\t|\tyear\t|\tDepartment\t|\tterm\t|\tNumber Lecture\t|");

            foreach (Subject item in subjects)
            {
                Console.WriteLine(String.Format("---------------------------------------------------------------------------------------------------------\r\n|\t{0}\t|\t{1}\t|\t{2}\t\t|\t{3}\t|\t{4}\t\t|\t{5}\t|\t{6}\t  |",
                    item.Id,
                    item.Name,
                    item.MinDegree,
                    item.Year,
                    item.Department.Name,
                    item.Term,
                    item.SubjectLectures.Count
                    ));
            }
        }
        public void ShowByTerm()
        {
            short term = CheckShort("show", "Term");
            List<Subject> subjects = service.DisplayByTerm(term);
            Console.WriteLine("**************************************************************************************************************\r\n|\tid\t|\tName\t|\tMinDegree\t|\tyear\t|\tDepartment\t|\tterm\t|\tNumber Lecture\t|");

            foreach (Subject item in subjects)
            {
                Console.WriteLine(String.Format("---------------------------------------------------------------------------------------------------------\r\n|\t{0}\t|\t{1}\t|\t{2}\t\t|\t{3}\t|\t{4}\t\t|\t{5}\t|\t{6}\t  |",
                    item.Id,
                    item.Name,
                    item.MinDegree,
                    item.Year,
                    item.Department.Name,
                    item.Term,
                    item.SubjectLectures.Count
                    ));
            }
        }


        public void GetLectures(int id)
        {
            Console.WriteLine("Loading ...");
            var lectures = service.ViewLectures(id);
            Console.WriteLine("|Id\t|Title\r\n-----------------------------------------------------------------");
            foreach (var item in lectures)
            {
                Console.WriteLine(String.Format("|{0}\t|{1}", item.Id, item.Title));
            }
            Console.WriteLine("-----------------------------------------------------------------");
        }
    }
}
