using Labb2_LINQ2.Models;
using System.Security.Cryptography;

namespace Labb2_LINQ2
{
    internal class Program
    {
        static void Main(string[] args)
        {

            #region
            //Course c1 = new Course();
            //c1.Name = "SUT21";
            //c1.SubjectID = 1;
            //c1.Subject = new Subject() { Name = "Math" };
            //Course c2 = new Course();
            //c2.Name = "SUT22";
            //c2.SubjectID = 2;
            //c2.Subject = new Subject() { Name = "Programming 1" };
            //Course c3 = new Course();
            //c3.Name = "SUT23";
            //c3.SubjectID = 3;
            //c3.Subject = new Subject() { Name = "Webdevelopment" };

            //LinqDbContext context = new LinqDbContext();
            //Subject s4 = new Subject();
            //s4.Name = "Programming 2";

            //context.Add(s4);
            //context.SaveChanges();

            //context.Courses.Add(c1);
            //context.Courses.Add(c2);
            //context.Courses.Add(c3);

            //Student s1 = new Student();
            //s1.Name = "Hans";
            //s1.CourseID = 3;
            //Student s2 = new Student();
            //s2.Name = "Fredrik";
            //s2.CourseID = 4;
            //Student s3 = new Student();
            //s3.Name = "Bertil";
            //s3.CourseID = 3;
            //Student s4 = new Student();
            //s4.Name = "Hannah";
            //s4.CourseID = 5;
            //Student s5 = new Student();
            //s5.Name = "Emma";
            //s5.CourseID = 5;

            //context.Students.Add(s1);
            //context.Students.Add(s2);
            //context.Students.Add(s3);
            //context.Students.Add(s4);
            //context.Students.Add(s5);

            //Teacher t1 = new Teacher();
            //t1.Name = "Reidar";
            //t1.CourseID = 3;
            //Teacher t2 = new Teacher();
            //t2.Name = "Tobias";
            //t2.CourseID = 4;
            //Teacher t3 = new Teacher();
            //t3.Name = "Anas";
            //t3.CourseID = 5;

            //context.Teachers.Add(t1);
            //context.Teachers.Add(t2);
            //context.Teachers.Add(t3);

            //context.SaveChanges();
            #endregion
            Menu();
            
        }

        public static void Menu()
        {
            int MenuCh;

            while (true)
            {
                Console.WriteLine("Hello and welcome to the school database");
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("1. Show all math teacher\n2. Show all students and their teachers\n3. Check if a subject is called Programming 1\n" +
                    "4. Change name from Programming 2 to OOP\n5. Change a students teacher from Anas to Reidar");
                MenuCh = int.Parse(Console.ReadLine());
                switch (MenuCh)
                {
                    case 1:
                        Console.Clear();
                        GetAllMathTeachers();
                        Console.Clear();
                        break;
                    case 2:
                        Console.Clear();
                        GetStudentsWithTeachers();
                        Console.Clear();
                        break;
                    case 3:
                        Console.Clear();
                        CheckSubjectsForProgramming();
                        Console.Clear();
                        break;
                    case 4:
                        Console.Clear();
                        ChangeNameOnSubject();
                        Console.Clear();
                        break;
                    case 5:
                        Console.Clear();
                        ChangeTeacher();
                        Console.Clear();
                        break;
                }
            }
            
        }

        public static void GetAllMathTeachers()
        {
            LinqDbContext context = new LinqDbContext();
            var MathTeacher = from t in context.Teachers
                              join c in context.Courses on t.CourseID equals c.ID
                              join s in context.Subjects on c.SubjectID equals s.ID
                              where s.Name == "Math"
                              select t.Name;
            Console.WriteLine("Showing all math teachers");
            foreach (var teach in MathTeacher)
            {
                Console.WriteLine(teach);
            }
            Console.ReadLine();
        }


        public static void GetStudentsWithTeachers()
        {
            LinqDbContext context = new LinqDbContext();

            var TnS = from c in context.Courses
                      join t in context.Teachers on c.ID equals t.CourseID
                      join s in context.Students on c.ID equals s.CourseID
                      into stuGroup
                      select new
                      {
                          Student = stuGroup,
                          Teacher = t.Name
                      };
            Console.WriteLine("Showing all teachers and their students");
            foreach (var t in TnS)
            {
                Console.WriteLine($"Teacher: {t.Teacher}");

                foreach (var s in t.Student)
                {
                    Console.WriteLine($"Student: {s.Name}");
                }
                Console.WriteLine();
            }
            Console.ReadLine();
        }

        public static void CheckSubjectsForProgramming()
        {
            LinqDbContext context = new LinqDbContext();
            var SubCheck = context.Subjects.FirstOrDefault(s => s.Name.Contains("Programming 1"));

            if (SubCheck != null)
            {
                Console.WriteLine("There's a subject called Programming 1");
            }
            Console.ReadLine();
        }

        public static void ChangeNameOnSubject()
        {
            LinqDbContext context = new LinqDbContext();

            var Before = (from sub in context.Subjects
                          select sub.Name);
            Console.WriteLine("BEFORE");
            foreach (var item in Before)
            {
                Console.WriteLine(item);
            }


            var Subject = (from s in context.Subjects
                           where s.Name == "Programming 2"
                           select s).SingleOrDefault();

            Subject.Name = "OOP";

            context.SaveChanges();
            Console.WriteLine();
            Console.WriteLine("Changed name of subject Programming 2 to OOP");
            Console.WriteLine();

            var After = from s in context.Subjects
                        select s.Name;
            Console.WriteLine("AFTER");
            foreach (var item in After)
            {
                Console.WriteLine(item);
            }
            Console.ReadLine();
        }

        public static void ChangeTeacher()
        {
            LinqDbContext context = new LinqDbContext();
            Console.WriteLine("BEFORE");
            GetStudentsWithTeachers();
            Console.WriteLine();
            var Student = (from s in context.Students
                           where s.Name == "Emma"
                           select s).SingleOrDefault();

            var Teacher = (from t in context.Teachers
                           where t.Name == "Reidar"
                           select t).SingleOrDefault();

            Student.CourseID = 5;

            context.SaveChanges();
            Console.WriteLine("Changed Emmas teacher from Anas to Reidar");
            Console.WriteLine();
            Console.WriteLine("AFTER");
            GetStudentsWithTeachers();
        }
    }
}