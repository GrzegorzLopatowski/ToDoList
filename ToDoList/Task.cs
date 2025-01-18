



public class Task
{
    public string Name { get; private set; }
    public PriorityList Priority { get; private set; }

    public Task(string name, PriorityList priority)
    {
        Name = name;
        Priority = priority;
        ShowCreationDescription();
    }

    private void ShowCreationDescription()
    {
        Console.Clear();
        Console.WriteLine(ToString() + " has been created");
    }

    internal void RenameTask(string newName)
    {
        string oldName = Name;
        Name = newName;
        Console.Clear();
        Console.WriteLine($"Task: '{oldName}' has changed name to '{Name}' successfully");
        Console.ReadKey();
    }

    internal void ChangePriority(PriorityList newPriority) => Priority = newPriority;

    public override string ToString() => $"Task: '{Name}' with priority: '{Priority}'" ;


}

