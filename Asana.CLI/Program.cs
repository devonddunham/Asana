using Asana.Library.Models;
using Asana.Library.Services;
using System;

namespace Asana
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var todoService = ToDoServiceProxy.Current;
            var projects = new List<Project>();
            int projectCount = 0;
            int choiceInt = 0;
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
                            Console.Write("Name: ");
                            var name = Console.ReadLine();
                            Console.Write("Description: ");
                            var description = Console.ReadLine();

                            todoService.AddOrUpdate(new ToDo
                            {
                                Name = name,
                                Description = description,
                                IsCompleted = false,
                                Id = 0 // will be set by service
                            });
                            break;
                        case 2:
                            todoService.DisplayToDos(true);
                            break;
                        case 3:
                            todoService.DisplayToDos();
                            break;
                        case 4:

                            todoService.DisplayToDos(true);
                            Console.Write("ToDo to Delete: ");
                            var toDoChoice = int.Parse(Console.ReadLine() ?? "0");
                            var reference = todoService.GetById(toDoChoice);
                            todoService.DeleteToDo(reference);

                            break;
                        case 5:

                            todoService.DisplayToDos(true);
                            Console.Write("ToDo to Update: ");
                            var toDoChoiceUpdate = int.Parse(Console.ReadLine() ?? "0");
                            var referenceUpdate = todoService.GetById(toDoChoiceUpdate);

                            if (referenceUpdate != null)
                            {
                                Console.Write("Name: ");
                                referenceUpdate.Name = Console.ReadLine();
                                Console.Write("Description: ");
                                referenceUpdate.Description = Console.ReadLine();
                            }
                            todoService.AddOrUpdate(referenceUpdate);

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