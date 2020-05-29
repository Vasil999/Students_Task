using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Students_Task
{
    class Program
    {
        //setting width-value for the table for the option Show all
        static int tableWidth = 120;

        static void Main(string[] args)
        {
            //make dictionary with commands
            Dictionary<int, string> commands = new Dictionary<int, string>()
            {
                { 1, "SHOW ALL"},
                { 2, "SEARCH BY PERSONAL IDENTIFICATION NUMBER"},
                { 3, "SEARCH BY STUDENT NUMBER"},
                { 4, "DELETE BY PERSONAL IDENTIFICATION NUMBER"},
                { 5, "ADD STUDENT"},
                { 6, "SHOW AVERAGE GRADES"},
                { 0, "EXIT"},
            };

            //Welcome user
            Console.WriteLine("Welcome to Students App");

            //Create menu
            ShowMenu(commands);

            while (true)
            {
                Console.WriteLine("Please enter a number");

                //See if number is from type int
                try
                {
                    int command = int.Parse(Console.ReadLine());

                    //Make sure command is in the right range
                    if (!commands.ContainsKey(command))
                    {
                        Console.WriteLine("There is no such option");
                        continue;
                    }

                    //Making commands work, as they should
                    switch (command)
                    {
                        case 1:
                            ShowAllStudents();
                            break;
                        case 2:
                            SearchByPersonalIdentificationNumber();
                            break;
                        case 3:
                            SearchByStudentNumber();
                            break;
                        case 4:
                            DeleteByPersonalIdentificationNumber();
                            break;
                        case 5:
                            AddStudent();
                            break;
                        case 6:
                            ShowAvarageGrades();
                            break;
                        case 0:
                            Exit();
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Wrong format");
                    continue;
                }
            }

        }

        private static void AddStudent()
        {
            string studentInfo = Console.ReadLine().Trim();

            //It is very important to write it in the right order. Because of this an excel woul work fine :)
            Regex regex = new Regex(@"([a-zA-Z]+\s){3}[0-9]{6} [0-9]{7}( [2-6].[0-9][0-9])+");

            if (regex.IsMatch(studentInfo))
            {
                using (StreamWriter writer = new StreamWriter(@"C:\Users\vasil\source\repos\Students_Task\Files\Students.txt", true))
                {
                    writer.WriteLine(studentInfo);
                }
            }
            else
            {
                Console.WriteLine("Wrong format or input");
            }
        }

        private static void ShowAvarageGrades()
        {
            string[] students = File.ReadAllLines(@"C:\Users\vasil\source\repos\Students_Task\Files\Students.txt");

            foreach (string student in students)
            {
                string[] studentElements = student.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                double SumOfGrades = 0;

                for (int i = 5; i < studentElements.Length; i++)
                {
                    SumOfGrades += double.Parse(studentElements[i]);
                }

                Console.WriteLine($"{studentElements[0]} {studentElements[2]} with student number {studentElements[4]} has avarage grade - {(SumOfGrades/10.0).ToString("#.##")}");
            }
        }

        private static void DeleteByPersonalIdentificationNumber()
        {
            Console.WriteLine("Please enter the personal identification number of the student");
            string identNum = Console.ReadLine();
            
            string[] students = File.ReadAllLines(@"C:\Users\vasil\source\repos\Students_Task\Files\Students.txt");

            for (int i = 0; i < students.Length; i++)
            {
                string[] studentElements = students[i].Split(" ", StringSplitOptions.RemoveEmptyEntries);
                if (studentElements[3].Contains(identNum))
                {
                    Console.WriteLine($"We will delete the student {studentElements[0]} {studentElements[1]} {studentElements[2]}");
                    
                    students[i] = null;

                    string newFileText = "";

                    foreach (string student in students)
                    {
                        if (student != null)
                        {
                            newFileText += student + "\n";
                        }
                        continue;
                    }

                    using (StreamWriter streamWriter = new StreamWriter(@"C:\Users\vasil\source\repos\Students_Task\Files\Students.txt"))
                    {
                        streamWriter.Write(newFileText);
                    }

                    return;
                }
            }

            Console.WriteLine("There is no student with this personal identification number!");
        }

        private static void SearchByStudentNumber()
        {
            Console.WriteLine("Please enter the student number of the student");
            string identNum = Console.ReadLine();

            string[] students = File.ReadAllLines(@"C:\Users\vasil\source\repos\Students_Task\Files\Students.txt");

            for (int i = 0; i < students.Length; i++)
            {
                string[] studentElements = students[i].Split(" ", StringSplitOptions.RemoveEmptyEntries);
                if (studentElements[4].Contains(identNum))
                {
                    Console.WriteLine("We found the student!");

                    PrintLine();
                    PrintRow("last name", "middle name", "first name", "student number", "grades");

                    string grades = "";
                    for (int j = 0; j < studentElements.Length - 5; j++)
                    {
                        string[] grade = new string[studentElements.Length];
                        grade[j] = studentElements[j + 5];
                        grades += $" {grade[j]}";
                    }

                    PrintRow(studentElements[2], studentElements[1], studentElements[0], studentElements[4], grades);

                    return;
                }
            }
            Console.WriteLine("There is no student with this student number!");
        }

        private static void Exit()
        {
            Environment.Exit(0);
        }

        private static void SearchByPersonalIdentificationNumber()
        {
            Console.WriteLine("Please enter the personal identification number of the student");
            string identNum = Console.ReadLine();

            string[] students = File.ReadAllLines(@"C:\Users\vasil\source\repos\Students_Task\Files\Students.txt");

            for (int i = 0; i < students.Length; i++)
            {
                string[] studentElements = students[i].Split(" ", StringSplitOptions.RemoveEmptyEntries);
                if (studentElements[3].Contains(identNum))
                {
                    Console.WriteLine("We found the student!");
                    
                    PrintLine();
                    PrintRow("last name", "middle name", "first name", "student number", "grades");

                    string grades = "";
                    for (int j = 0; j < studentElements.Length - 5; j++)
                    {
                        string[] grade = new string[studentElements.Length];
                        grade[j] = studentElements[j + 5];
                        grades += $" {grade[j]}";
                    }

                    PrintRow(studentElements[2], studentElements[1], studentElements[0], studentElements[4], grades);

                    return;
                }
            }
            Console.WriteLine("There is no student with this personal identification number!");
        }

        private static void ShowAllStudents()
        {
            //We don't want to show the personal identification number, as it is private
            PrintLine();

            PrintRow("last name", "middle name", "first name", "student number", "grades");

            PrintLine();

            //separating the students 
            string[] students = File.ReadAllLines(@"C:\Users\vasil\source\repos\Students_Task\Files\Students.txt");

            foreach (string student in students)
            {
                //separating the students informations
                string[] studentElements = student.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                //informations, that can't be changed
                string firstName = studentElements[0];
                string middleName = studentElements[1];
                string lastName = studentElements[2];
                string personalIndentificationNumber = studentElements[3];
                string studentNumber = studentElements[4];
                string grades = "";

                //grades
                for (int i = 0; i < studentElements.Length - 5; i++)
                {
                    string[] grade = new string[studentElements.Length];
                    grade[i] = studentElements[i + 5];
                    grades += $" {grade[i]}";
                }

                PrintRow(lastName, middleName, firstName, studentNumber, grades);
            }
            PrintLine();
        }

        private static void PrintStudent()
        {
            throw new NotImplementedException();
        }

        private static void ShowMenu(Dictionary<int, string> commands)
        {
            //giving options to the user what to do 
            foreach (var command in commands)
            {
                Console.WriteLine($"{command.Key}) {command.Value}");
            }

        }

        static void PrintRow(params string[] columns)
        {
            int width = 16;
            string row = "|";

            for (int i = 0; i < columns.Length - 1; i++)
            {
                row += AlignCentre(columns[i], width) + "|";
            }

            row += AlignCentre(columns[columns.Length - 1], 192 - 9 * width) + "|";

            Console.WriteLine(row);
        }

        static string AlignCentre(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }

        static void PrintLine()
        {
            Console.WriteLine(new string('-', tableWidth));
        }
    }
}
