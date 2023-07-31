using System;
using System.Collections.Generic;
using System.IO;

namespace TeachersData
{

    public class Teacher
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
        public string Section { get; set; }

        public override string ToString()
        {
            return $"ID: {ID}, Name: {Name}, Class: {Class}, Section: {Section}";
        }
    }

    public class Program
    {
        private static List<Teacher> teachers = new List<Teacher>();

        public static void Main()
        {
            Console.Write("Enter the file path of the Teachers Data: \n ");
            string dataFilePath = Console.ReadLine();

            ReadTeacherDataFromFile(dataFilePath);
            DisplayTeacherData();

            bool continueAdding = true;

            while (continueAdding)
            {
                Console.WriteLine("Do you want to add or update teacher data? (yes/no)");
                string choice = Console.ReadLine();

                if (choice == "y")
                {
                    AddOrUpdateTeacherData();
                    SaveTeacherDataToFile(dataFilePath);
                }
                else if (choice == "n")
                {
                    continueAdding = false;
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please enter 'Y' to add/update data or 'N' to exit.");
                }
            }

            Console.ReadKey();
        }

        private static void ReadTeacherDataFromFile(string dataFilePath)
        {
            if (File.Exists(dataFilePath))
            {
                string[] lines = File.ReadAllLines(dataFilePath);
                foreach (string line in lines)
                {
                    string[] fields = line.Split(',');
                    if (fields.Length == 4 && int.TryParse(fields[0], out int id))
                    {
                        Teacher teacher = new Teacher
                        {
                            ID = id,
                            Name = fields[1],
                            Class = fields[2],
                            Section = fields[3]
                        };
                        teachers.Add(teacher);
                    }
                }
            }
        }

        private static void DisplayTeacherData()
        {
            Console.WriteLine("Teacher Data:");
            foreach (Teacher teacher in teachers)
            {
                Console.WriteLine(teacher);
            }
        }

        private static void AddOrUpdateTeacherData()
        {
            Console.WriteLine("Enter teacher details:");
            Console.Write("ID: ");
            int id = int.Parse(Console.ReadLine());

            Teacher existingTeacher = teachers.Find(t => t.ID == id);
            if (existingTeacher != null)
            {
                Console.WriteLine("Teacher found. Updating details.");
                teachers.Remove(existingTeacher);
            }

            Teacher teacher = new Teacher
            {
                ID = id
            };

            Console.Write("Name: ");
            teacher.Name = Console.ReadLine();

            Console.Write("Class: ");
            teacher.Class = Console.ReadLine();

            Console.Write("Section: ");
            teacher.Section = Console.ReadLine();

            teachers.Add(teacher);
        }

        private static void SaveTeacherDataToFile(string dataFilePath)
        {
            List<string> lines = new List<string>();
            foreach (Teacher teacher in teachers)
            {
                string line = $"{teacher.ID},{teacher.Name},{teacher.Class},{teacher.Section}";
                lines.Add(line);
            }
            File.WriteAllLines(dataFilePath, lines);
            Console.WriteLine("Teacher data saved successfully.");
        }
    }

}