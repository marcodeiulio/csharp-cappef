using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace csharp_cappef
{
    internal class Program
    {
        public static System.Collections.Generic.IEnumerable<int> Power(int number, int exponent)
        {
            int result = 1;
            for (int i = 0; i < exponent; i++)
            {
                result = result * number;
                yield return result;
            }
        }

        public static System.Collections.Generic.IEnumerable<long> Genera(long n)
        {
            long result = 0;
            while (n-- > 0)
            {
                yield return result++;
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            foreach(int n in Power(2, 16))
            {
                Console.WriteLine(n);
            }

            //Contare tutti i numeri che contengono la cifra 2 compresi tra 1 e 1000000

            //Soluzione 1
            long count = 1;
            for (long i = 0; i < 1000000; ++i)
            {
                if (i.ToString().Contains('2'))
                    count++;
            }
            Console.WriteLine("I numeri compresi tra 1 e 1000000 che contengono la cifra 2 sono {0}", count);

            //Soluzione 2
            long count1 = 1;
            foreach (int n in Genera(1000000))
            {
                if (n.ToString().Contains('2'))
                    count1++;
            }
            Console.WriteLine("[Metodo IEnumerable] I numeri compresi tra 1 e 1000000 che contengono la cifra 2 sono {0}", count);

            //Usando esclusivamente Genera(), stampare la somma dei numeri pari da 1 a 10000000
            Console.WriteLine(Genera(10000000).Where(n => n % 2 == 0).Sum());

            return;

            using (SchoolContext db = new SchoolContext())
            {
                // Create
                Student nuovoStudente =
                    new Student { Name = "Francesco", Surname = "Costa", Email = "fracosta@ilpost.it" };
                db.Add(nuovoStudente);
                db.SaveChanges();

                // Read
                Console.WriteLine("Ottenere lista di Studenti");
                List<Student> students = db.Students
                   .OrderBy(student => student.Name).ToList<Student>();

                students.ForEach(s => Console.WriteLine(s));
            }
        }
    }

    [Table("student")]
    [Index(nameof(Email), IsUnique = true)]
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Surname { get; set; }
        [Column("student_email")]
        public string Email { get; set; }

        public List<Course> FrequentedCourses { get; set; }
    }
    [Table("course")]
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        public string Name { get; set; }
        public CourseImage CourseImage { get; set; }

        public List<Student> StudentsEnrolled { get; set; }
    }
    [Table("course_image")]
    public class CourseImage
    {
        [Key]
        public int CourseImageId { get; set; }
        public byte[] Image { get; set; }
        public string Caption { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
    [Table("review")]
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
    public class SchoolContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseImage> CourseImages { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=EnnesimoDB;Integrated Security=True;Pooling=False");
        }
    }
}