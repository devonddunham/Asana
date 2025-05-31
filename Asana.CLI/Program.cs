using Asana.Library.Models;
using System;

namespace Asana
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var toDos = new List<ToDo>();
            var projects = new List<Project>();
            int choiceInt;
            var itemCount = 0;
            var projectCount = 0;
            var toDoChoice = 0;
            do
            {
                Console.WriteLine("Choose a menu option:");
                Console.WriteLine("1. Create a ToDo");
                Console.WriteLine("2. List all ToDos");
                Console.WriteLine("3. List all outstanding ToDos");
                Console.WriteLine("4. Delete a ToDo");
                Console.WriteLine("5. Update a ToDo");
                Console.WriteLine("6. Create a Project");
                Console.WriteLine("7. List all Projects");
                Console.WriteLine("8. Delete a Project");
                Console.WriteLine("9. Update a Project");
                Console.WriteLine("10. List all Todos in a Project");
                Console.WriteLine("11. Exit");
                Console.Write("Choice: ");

                var choice = Console.ReadLine() ?? "11";

                if (int.TryParse(choice, out choiceInt))
                {
                    switch (choiceInt)
                    {
                        case 1:
                            Console.Write("Name:");
                            var name = Console.ReadLine();
                            Console.Write("Description:");
                            var description = Console.ReadLine();

                            toDos.Add(new ToDo
                            {
                                Name = name,
                                Description = description,
                                IsCompleted = false,
                                Id = ++itemCount
                            });
                            break;
                        case 2:
                            toDos.ForEach(Console.WriteLine);
                            break;
                        case 3:
                            toDos.Where(t => (t != null) && !(t?.IsCompleted ?? false))
                                .ToList()
                                .ForEach(Console.WriteLine);
                            break;
                        case 4:

                            toDos.ForEach(Console.WriteLine);
                            Console.Write("ToDo to Delete: ");
                            toDoChoice = int.Parse(Console.ReadLine() ?? "0");

                            var reference = toDos.FirstOrDefault(t => t.Id == toDoChoice);
                            if (reference != null)
                            {
                                toDos.Remove(reference);
                            }

                            break;
                        case 5:

                            toDos.ForEach(Console.WriteLine);
                            Console.Write("ToDo to Update: ");
                            toDoChoice = int.Parse(Console.ReadLine() ?? "0");
                            var updateReference = toDos.FirstOrDefault(t => t.Id == toDoChoice);

                            if (updateReference != null)
                            {
                                Console.Write("Name:");
                                updateReference.Name = Console.ReadLine();
                                Console.Write("Description:");
                                updateReference.Description = Console.ReadLine();
                            }

                            break;
                        case 6:
                            Console.Write("Name: ");
                            var projectName = Console.ReadLine();
                            Console.Write("Description: ");
                            var projectDescription = Console.ReadLine();
                            projects.Add(new Project
                            {
                                Name = projectName,
                                Description = projectDescription,
                                Id = ++projectCount,
                                ToDoCount = 0,
                            });
                            break;
                        case 7:
                            projects.ForEach(Console.WriteLine);
                            break;
                        case 8:
                            projects.ForEach(Console.WriteLine);
                            Console.Write("Project to Delete: ");
                            var projectChoice = int.Parse(Console.ReadLine() ?? "0");

                            var projectReference = projects.FirstOrDefault(p => p.Id == projectChoice);
                            if (projectReference != null)
                            {
                                projects.Remove(projectReference);
                            }

                            break;
                        case 9:
                            projects.ForEach(Console.WriteLine);
                            Console.Write("Project to Update: ");
                            projectChoice = int.Parse(Console.ReadLine() ?? "0");
                            var updateProjectReference = projects.FirstOrDefault(p => p.Id == projectChoice);

                            if (updateProjectReference != null)
                            {
                                Console.Write("Name: ");
                                updateProjectReference.Name = Console.ReadLine();
                                Console.Write("Description: ");
                                updateProjectReference.Description = Console.ReadLine();

                                Console.Write("Add a ToDo to this project? (y/n): ");
                                var addToDoChoice = Console.ReadLine()?.ToLower();
                                while (addToDoChoice == "y")
                                {
                                    Console.Write("ToDo Name: ");
                                    var toDoName = Console.ReadLine();
                                    Console.Write("ToDo Description: ");
                                    var toDoDescription = Console.ReadLine();
                                    var newToDo = new ToDo
                                    {
                                        Name = toDoName,
                                        Description = toDoDescription,
                                        IsCompleted = false,
                                        Id = ++updateProjectReference.ToDoCount
                                    };
                                    AddToDoToProject(updateProjectReference, newToDo);
                                    Console.Write("Add another ToDo? (y/n): ");
                                    addToDoChoice = Console.ReadLine()?.ToLower();
                                }
                            }

                            break;
                        case 10:
                            projects.ForEach(Console.WriteLine);
                            Console.Write("Project to List ToDos: ");
                            projectChoice = int.Parse(Console.ReadLine() ?? "0");
                            var projectToList = projects.FirstOrDefault(p => p.Id == projectChoice);

                            if (projectToList != null && projectToList.ToDos != null)
                            {
                                projectToList.ToDos.ForEach(Console.WriteLine);
                            }
                            else
                            {
                                Console.WriteLine("No ToDos found for this project.");
                            }

                            break;
                        case 11:
                            Console.WriteLine("Exiting the application...");
                            break;
                        default:
                            Console.WriteLine("ERROR: Unknown menu selection");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine($"ERROR: {choice} is not a valid menu selection");
                }

            } while (choiceInt != 11);

        }

        // Function to add a ToDo to a Project
        public static void AddToDoToProject(Project project, ToDo toDo)
        {
            if (project.ToDos == null)
            {
                project.ToDos = new List<ToDo>();
            }
            project.ToDos.Add(toDo);
        }
    }
}