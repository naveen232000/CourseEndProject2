using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace CourseEndProject2
{
    public class Techers
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
        public string Section { get; set; }
    }

    public class Program
    {
        public static List<Techers> techers = new List<Techers>();
        public static Techers tc = new Techers();
        public static string filePath = @"D:\\Mphasis\\Phase1 C#\\Practice Problems\\Course_end_Project\\Project2\\TeachersData.txt";
        
        //Constructor to read once 
        
  
        static Program()
        {
            try
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');
                    tc.Id = Convert.ToInt32(parts[0]);
                    tc.Name = parts[1];
                    tc.Class = parts[2];
                    tc.Section = parts[3];
                    techers.Add(tc);
                }
            } 
            catch(Exception e) { Console.WriteLine(e.Message + " Add Atleast One Data or Check the file if first line is empty kindly remove that line"); }
          
        }
        //Add Teacher Details
        public static void AddTeacher(int id,string Name,string Class,string Section)
        {
            tc.Id = id;
            tc.Name = Name;
            tc.Class = Class;
            tc.Section = Section;
            techers.Add(tc);
            string data = $"{id}, {Name}, {Class}, {Section}";
            using (StreamWriter writer = File.AppendText(filePath))
            {
                writer.WriteLine(data);
                writer.Close();
            }
        }
        //Get All Teacher Details
        public static void GetAllTeachers()
        {
             List<Techers> te = new List<Techers>();
            try
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');
                    Techers tc = new Techers();
                    tc.Id = Convert.ToInt32(parts[0]);
                    tc.Name = parts[1];
                    tc.Class = parts[2];
                    tc.Section = parts[3];
                    te.Add(tc);
                }
                te = te.OrderBy(s => s.Id).ToList();
                foreach (Techers t in te)
                {
                    Console.WriteLine("{0,-5}{1,-15}{2,-15}{3,-15}", t.Id, t.Name, t.Class, t.Section);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + " Add Atleast One Data or Check the file if first line is empty kindly remove that line");
            }
       
        }
        //Update Teacher Detail Bassed on ID
        public static void UpdateData(int id)
        {
            try
            {
                string idToUpdate = Convert.ToString(id);
                Techers result = techers.Find(s => s.Id == id);
                if (result != null)
                {
                    Console.WriteLine("Update Fields (Name, Class, Section) :");
                    string name = Console.ReadLine();
                    string cls = Console.ReadLine();
                    string sec = Console.ReadLine();
                    string data = $"{id}, {name}, {cls}, {sec}";
                    string[] lines = File.ReadAllLines(filePath);
                    for (int i = 0; i < lines.Length; i++)
                    {
                        string[] fields = lines[i].Split(',');
                        if (fields[0] == idToUpdate)
                        {
                            lines[i] = data;
                            break;
                        }
                    }
                    File.WriteAllLines(filePath, lines);
                }
                else
                {
                    Console.WriteLine("\nID not found !");
                }
            }
            catch (Exception e)
            {
             Console.WriteLine(e.Message + "  Add Atleast One Data or Check the file if first line is empty kindly remove that line");
            }
            
        }
        static void Main(string[] args)
      
        {
            while (true)
            {
                Console.WriteLine("\nEnter Your Choice :\n1) Add Teacher\n2) Update Teacher Data\n3) Get All Teacher\n4) Exit\n");
                int n=int.Parse(Console.ReadLine());
                switch (n) { 
                case 1:
                        {
                            Console.WriteLine("Enter Techers Data(Id, Name, Class, Section)");
                            int id = Convert.ToInt32(Console.ReadLine());
                            string Name = Console.ReadLine();
                            string Class = Console.ReadLine();
                            string Section = Console.ReadLine();
                            AddTeacher(id, Name, Class, Section);
                            break;
                        }
                case 2:
                        {
                            Console.WriteLine("Enter Techers Id :");
                            int id = Convert.ToInt32(Console.ReadLine());
                            UpdateData(id);
                            break;
                        }
                case 3:
                        {
                            GetAllTeachers();
                            break;
                        }
                case 4:
                        {
                            Environment.Exit(0);
                            break;
                        }
                default: { Console.WriteLine("Enter Correct Choice"); break; }
                }
            }
        }
    }
}
