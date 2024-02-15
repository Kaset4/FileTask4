using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BinaryReadWrite
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Student> studentsToRead = ReadStudentsFromBinFile("students.dat");

            foreach (Student studentProp in studentsToRead)
            {
                Console.WriteLine(studentProp.Name + " " + studentProp.Group + " " + studentProp.DateOfBirth + " " + studentProp.AverageScore);
            }

            WriteStudentsToTextFiles(studentsToRead);
        }

        static void WriteStudentsToTextFiles(List<Student> students)
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string studentsDirectory = Path.Combine(desktopPath, "Students");
            Directory.CreateDirectory(studentsDirectory);

            var groupedStudents = students.GroupBy(student => student.Group);

            foreach (var group in groupedStudents)
            {
                string groupFilePath = Path.Combine(studentsDirectory, $"{group.Key}.txt");

                using (StreamWriter sw = new StreamWriter(groupFilePath))
                {
                    foreach (var student in group)
                    {
                        sw.WriteLine(student.ToText());
                    }
                }
            }

            Console.WriteLine("Данные о студентах успешно записаны в текстовые файлы.");
        }

        static List<Student> ReadStudentsFromBinFile(string fileName)
        {
            List<Student> result = new();
            using FileStream fs = new FileStream(fileName, FileMode.Open);
            using StreamReader sr = new StreamReader(fs);

            Console.WriteLine(sr.ReadToEnd());

            fs.Position = 0;

            BinaryReader br = new BinaryReader(fs);

            while (fs.Position < fs.Length)
            {
                Student student = new Student();
                student.Name = br.ReadString();
                student.Group = br.ReadString();
                long dt = br.ReadInt64();
                student.DateOfBirth = DateTime.FromBinary(dt);
                student.AverageScore = br.ReadDecimal();

                result.Add(student);
            }

            fs.Close();
            return result;
        }
    }
}
