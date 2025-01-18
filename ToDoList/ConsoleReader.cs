public class ConsoleReader
{
    public static ConsoleKeyInfo ShowMainMenu()
    {
        Console.Clear();
        Console.WriteLine("1. Show all lists");
        Console.WriteLine("2. Add list");
        Console.WriteLine("3. Delete list");
        Console.WriteLine("0. Quit");
        return Console.ReadKey();
    }

    public static int ShowToDoLists(List<ToDoList> toDoLists)
    {

        if (toDoLists.Count == 0)
        {
            Console.Clear();
            Console.WriteLine("There are currently no lists on your app.");
            Console.ReadKey();
            return 0;
        }

        int longestName = 0;
        foreach (ToDoList item in toDoLists)
        {
            if (item.Name.Length > longestName)
            {
                longestName = item.Name.Length;
            }
        }

        int response;
        bool result = false;
        do
        {
            Console.Clear();
            Console.WriteLine("Current lists:");
            Console.WriteLine("--------------");
            Console.Write("Nb. Name:");
            Console.Write(new string(' ', longestName));
            Console.WriteLine("Category:");
            Console.WriteLine(new string('-', longestName + 18));
            for (int i = 0; i < toDoLists.Count; i++)
            {
                Console.Write($" {i + 1}. {toDoLists[i].Name} ");
                Console.Write(new string(' ', (longestName - toDoLists[i].Name.Length) + 3));
                Console.Write($" {toDoLists[i].Category}");
                Console.WriteLine();
            }
            Console.WriteLine(new string('-', longestName + 18));
            Console.WriteLine("Choose number of the list or type 0 to exit to main menu");

            bool isResponseOk = int.TryParse(Console.ReadLine(), out response);
            if (isResponseOk)
            {
                result = (response >= 0 && response <= toDoLists.Count) ? true : false;
                if (result)
                {
                    return response;
                }
            }

            Console.WriteLine("Incorrect input!");
            Console.ReadKey();

        } while (!result);
        return response;
    }

    public static bool ShowToDoList(ToDoList toDoList)
    {
        int response;
        bool isExitClicked = false;

        do
        {
            if (toDoList.TaskList.Count == 0)
            {
                bool isFirstTaskAdded = false;
                do
                {
                    Console.Clear();
                    Console.WriteLine(toDoList);
                    Console.WriteLine();
                    Console.WriteLine("------------------------------------------");
                    Console.WriteLine("There are currently no tasks on your list.");
                    Console.WriteLine("------------------------------------------");
                    Console.WriteLine();
                    Console.WriteLine("1. Add task");
                    Console.WriteLine("0. Exit to main menu");
                    Console.WriteLine();
                    Console.WriteLine("Choose number from menu");

                    bool isResponseOk = int.TryParse(Console.ReadLine(), out response);
                    if (isResponseOk)
                    {
                        if (response == 0)
                        {
                            isExitClicked = true;
                        }
                        else if (response == 1)
                        {
                            string name = ValidateInput.Name("task");
                            PriorityList priority = ValidateInput.GetEnumValue<PriorityList>();
                            toDoList.AddTask(name, priority);
                            Console.ReadKey();
                            isFirstTaskAdded = true;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Incorrect input!");
                        Console.ReadKey();
                    }
                } while (!(isExitClicked || isFirstTaskAdded));
            }
            else
            {
                bool result = false;
                do
                {
                    ShowAllTasks(toDoList);
                    Console.WriteLine(" A. Add new task");
                    Console.WriteLine(" B. Delete task");
                    Console.WriteLine(" C. Go to Lists menu");
                    Console.WriteLine(" D. Go to Main menu");
                    Console.WriteLine();
                    Console.WriteLine("Choose number of the task to show details or type letter from menu");

                    string input = Console.ReadLine();
                    if (ValidateInput.IsLetterBetweenAAndLastMenuItem(input, 'D'))
                    {
                        switch (input[0].ToString().ToUpper())
                        {
                            case "A":
                                AddNewTask(toDoList);
                                break;
                            case "B":
                                DeleteTask(toDoList);
                                break;
                            case "C":
                                return false;
                            case "D":
                                return true;
                            default:
                                throw new NotImplementedException("New menu list item not setup outcome for");
                        }
                    }
                    else if (ValidateInput.IsNumberFromTaskList(input, toDoList.TaskList.Count))
                    {
                        isExitClicked = ShowTask(toDoList.TaskList[int.Parse(input) - 1]);
                        if (isExitClicked)
                        {
                            return isExitClicked;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Incorrect input!");
                        Console.ReadKey();
                    }

                } while (!result);
            }
        } while (!isExitClicked);
        return isExitClicked;
    }

    private static void ShowAllTasks(ToDoList toDoList)
    {
        int longestName = 0;
        foreach (Task item in toDoList.TaskList)
        {
            if (item.Name.Length > longestName)
            {
                longestName = item.Name.Length;
            }
        }

        Console.Clear();
        Console.WriteLine("Current tasks:");
        Console.WriteLine("--------------");
        Console.WriteLine();
        Console.Write("Nb. Name:");
        Console.Write(new string(' ', longestName));
        Console.WriteLine("Priority:");
        Console.WriteLine(new string('-', longestName + 17));
        for (int i = 0; i < toDoList.TaskList.Count; i++)
        {
            Console.Write($" {i + 1}. {toDoList.TaskList[i].Name} ");
            Console.Write(new string(' ', (longestName - toDoList.TaskList[i].Name.Length) + 3));
            Console.Write($" {toDoList.TaskList[i].Priority}");
            Console.WriteLine();
        }
        Console.WriteLine(new string('-', longestName + 17));
        Console.WriteLine();
    }

    private static void DeleteTask(ToDoList toDoList)
    {
        ShowAllTasks(toDoList);
        Console.WriteLine("Choose number of task to delete:");
        string input = Console.ReadLine();
        if (ValidateInput.IsNumberFromTaskList(input, toDoList.TaskList.Count))
        {
            int taskNumber = int.Parse(input) - 1;
            Console.Clear();
            Console.WriteLine($"Are you sure you want to delete: {toDoList.TaskList[taskNumber].Name}");
            Console.WriteLine("Please enter 'y' or 'n':");
            string userInput = Console.ReadLine().ToLower().Trim();

            if (userInput == "y")
            {
                string taskDescription = toDoList.TaskList[taskNumber].ToString();
                toDoList.DeleteTask(taskNumber);
                Console.Clear();
                Console.WriteLine($"{taskDescription} has been deleted succesfully");
                Console.ReadKey();
            }
        }
        else
        {
            Console.WriteLine("Incorrect input!");
            Console.ReadKey();
        }
    }

    private static void AddNewTask(ToDoList toDoList)
    {
        string name = ValidateInput.Name("task");
        PriorityList priority = ValidateInput.GetEnumValue<PriorityList>();
        toDoList.AddTask(name, priority);
        Console.ReadKey();
    }

    private static bool ShowTask(Task task)
    {
        bool result = false;

        do
        {
            Console.Clear();
            Console.WriteLine($"Task: {task.Name}");
            Console.WriteLine($"Priority: {task.Priority}");
            Console.WriteLine(new string('-', task.Name.Length + 6));
            Console.WriteLine(" A. Rename task");
            Console.WriteLine(" B. Change task priority");
            Console.WriteLine(" C. Go to Tasks menu");
            Console.WriteLine(" D. Go to Main menu");
            Console.WriteLine();
            Console.WriteLine("Choose letter from menu");

            string input = Console.ReadLine();
            if (ValidateInput.IsLetterBetweenAAndLastMenuItem(input, 'D'))
            {
                switch (input[0].ToString().ToUpper())
                {
                    case "A":
                        string newName = ValidateInput.Name("task");
                        if (!string.Equals(task.Name, newName))
                        {
                            task.RenameTask(newName);
                        }
                        else
                        {
                            Console.WriteLine("Name has not been changed as it can't be empty string or the same name as previously!");
                        }
                        result = true;
                        break;
                    case "B":
                        PriorityList newPriority = ValidateInput.GetEnumValue<PriorityList>();
                        if (newPriority != task.Priority)
                        {
                            task.ChangePriority(newPriority);
                            Console.Clear();
                            Console.WriteLine($"Priority of task: '{task.Name}' has been changed to: '{task.Priority}'");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("New priority can't be the same as previously!");
                        }
                        result = true;
                        break;
                    case "C":
                        return false;
                    case "D":
                        return true;
                    default:
                        throw new NotImplementedException("New menu list item not setup outcome for");
                }
            }

            else
            {
                Console.WriteLine("Incorrect input!");
                Console.ReadKey();
            }

        } while (!result);
        return false;
    }

    public static void DisplayEnumValues<T>() where T : Enum
    {
        foreach (T item in Enum.GetValues(typeof(T)))
        {
            Console.WriteLine($"{(int)(object)item}. {item}");
        }
    }
}


