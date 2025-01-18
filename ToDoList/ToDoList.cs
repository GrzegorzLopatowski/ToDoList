

public class ToDoList
{
    private string _name;
    public string Name
    {
        get { return _name; }
        private set
        {
            _name = string.IsNullOrEmpty(value) ? "Unknown name" : value;
        }
    }
    public CategoryList Category { get; private set; }
    public List<Task> TaskList { get; set; } = new List<Task>();

    public ToDoList(string name, CategoryList category)
    {
        Name = name;
        Category = category;
        ShowCreationDescription();
    }

    private void ShowCreationDescription()
    {
        Console.Clear();
        Console.WriteLine($"List: '{_name}' with category: '{Category}' has been created");
    }

    internal void AddTask(string name, PriorityList priority)
    {
        TaskList.Add(new Task(name, priority));
    }
    internal void DeleteTask(int taskNumber)
    {
        TaskList.RemoveAt(taskNumber);
    }
    public override string ToString() => $"List: '{Name}'. Category: '{Category}'";


}