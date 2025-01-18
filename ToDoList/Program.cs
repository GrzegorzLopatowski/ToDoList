using System.ComponentModel;


ToDoApp.Run();
//ToDoApp.Test();

public class ToDoApp
{
    public static void Run()
    {
        List<ToDoList> toDoLists = new List<ToDoList>();
        toDoLists.Add(new ToDoList("Test List 1 - Grzesiek", CategoryList.Shopping));
        toDoLists.Add(new ToDoList("Test list 2 - Ania", CategoryList.Other));
        toDoLists[0].TaskList.Add(new Task("TestTask1 - zakupy", PriorityList.Medium));
        toDoLists[0].TaskList.Add(new Task("TestTask2 - pranie", PriorityList.Low));

        ConsoleKeyInfo responseMainMenu;
        do
        {
            responseMainMenu = ConsoleReader.ShowMainMenu();
            switch (responseMainMenu.KeyChar)
            {
                case '1':
                    bool isExitClicked = false;
                    do
                    {
                        int responseList = ConsoleReader.ShowToDoLists(toDoLists);
                        if (responseList == 0)
                        {
                            isExitClicked = true;
                        }
                        else if (responseList > 0)
                        {
                            isExitClicked = ConsoleReader.ShowToDoList(toDoLists[responseList - 1]);
                        }
                    } while (!isExitClicked);
                    break;
                case '2':
                    string listName = ValidateInput.Name("list");
                    CategoryList listCategory = ValidateInput.GetEnumValue<CategoryList>();
                    toDoLists.Add(new ToDoList(listName, listCategory));
                    Console.ReadKey();
                    break;
                case '3':
                    int deleteList = ConsoleReader.ShowToDoLists(toDoLists);
                    if (deleteList > 0)
                    {
                        string message = toDoLists[deleteList - 1].ToString();
                        Console.Clear();
                        Console.WriteLine($"Are you sure you want to delete: {message}");
                        Console.WriteLine("Please enter 'y' or 'n':");
                        string userInput = Console.ReadLine().ToLower().Trim();

                        if (userInput == "y")
                        {
                            Console.Clear();
                            toDoLists.Remove(toDoLists[deleteList - 1]);
                            Console.WriteLine($"{message} has been deleted succesfully");
                            Console.ReadKey();
                        }
                        ConsoleReader.ShowToDoLists(toDoLists);
                    }
                    break;
                default:

                    break;
            }
        } while (responseMainMenu.KeyChar != '0');
    }

    //internal static void Test()
    //{
    //    DisplayEnumValues<CategoryList>();
    //    DisplayEnumValues<PriorityList>();

    //}

    //public static void DisplayEnumValues<T>() where T:Enum
    //{
    //    foreach (T item in Enum.GetValues(typeof(T)))
    //    {       
    //        Console.WriteLine($"{(int)(object)item}. {item}");
    //    }

    //}
}
