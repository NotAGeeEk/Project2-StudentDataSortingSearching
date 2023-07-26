using System;
using System.Collections.Generic;
using System.IO;


namespace ConsoleAppRainbowSchoolSorting
{

    public class Student
    {
        public string Name { get; set; }
        public string Class { get; set; }
    }

    class Program
    {
        static List<Student> ReadStudentDataFromFile(string filename)
        {
            List<Student> students = new List<Student>();

            try
            {
                string[] lines = File.ReadAllLines(filename);

                foreach (string line in lines)
                {
                    string[] data = line.Split(',');
                    if (data.Length == 2)
                    {
                        Student student = new Student
                        {
                            Name = data[0].Trim(),
                            Class = data[1].Trim()
                        };
                        students.Add(student);
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading the file: " + ex.Message);
            }

            return students;
        }

        static void SortStudentsByName(List<Student> students)
        {
            students.Sort((s1, s2) => string.Compare(s1.Name, s2.Name));
        }

        static Student SearchStudentByName(List<Student> students, string name)
        {
            return students.Find(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        static void DisplayStudentData(List<Student> students)
        {
            foreach (var student in students)
            {
                Console.WriteLine($"Name: {student.Name}, Class: {student.Class}");
            }
        }

        static void Main(string[] args)
        {
            string filename = "students.txt";
            List<Student> students = ReadStudentDataFromFile(filename);

            SortStudentsByName(students);
            DisplayStudentData(students);

            Console.WriteLine("\nEnter a student name to search:");
            string searchName = Console.ReadLine();
            Student foundStudent = SearchStudentByName(students, searchName);
            if (foundStudent != null)
            {
                Console.WriteLine($"Student found - Name: {foundStudent.Name}, Class: {foundStudent.Class}");
            }
            else
            {
                Console.WriteLine($"Student with name '{searchName}' not found.");
            }

            Console.ReadKey();
        }
    }
}