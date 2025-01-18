


using System.Runtime.CompilerServices;

public static class ValidateInput
{
    public static T GetEnumValue<T>() where T : Enum
    {
        ConsoleKeyInfo keyInfo;
        int userTries = 3;
        do
        {
            Console.Clear();
            Console.WriteLine("Choose category number from the below: ");
            ConsoleReader.DisplayEnumValues<T>();
            keyInfo = Console.ReadKey();
            int intValue = (int)Char.GetNumericValue(keyInfo.KeyChar);
            if (0 < intValue && intValue <= Enum.GetValues(typeof(T)).Length)
            {
                return (T)Enum.ToObject(typeof(T), intValue);
            }

            --userTries;
            if (userTries > 0)
            {
                Console.Clear();
                Console.WriteLine($"{keyInfo.KeyChar} is an incorrect category number. Please try again! ");
                Console.ReadKey();
            }


        } while (userTries != 0);
        if (typeof(T) == typeof(CategoryList))
        {
            return (T)Enum.ToObject(typeof(T),CategoryList.Other);
        }
        else if (typeof(T) == typeof(PriorityList))
        {
            return (T)Enum.ToObject(typeof(T), PriorityList.Low);
        }
        else
        {
            throw new Exception($"Enum list has not been set up correctly");

        }
    }
    public static string Name(string type)
    {
        string input = "";
        int userTries = 3;
        do
        {
            Console.Clear();
            Console.WriteLine($"Please provide name of the {type}: ");
            input = Console.ReadLine();
            if (string.IsNullOrEmpty(input) || input.Length > 50)
            {
                --userTries;
                if (userTries > 0)
                {
                    Console.Clear();
                    Console.WriteLine($"Name of the {type} can't be empty string or longer than 50 signs. Please try again! ");
                    Console.ReadKey();
                }
            }
            else
            {
                return input;
            }
        } while (userTries != 0);
        return input;
    }

    internal static bool IsLetterBetweenAAndLastMenuItem(string? input, char lastMenuItem)
    {
        if (input.Length == 1) 
        { 
            char letter = char.ToUpper(input[0]); 
            return letter >= 'A' && letter <= lastMenuItem; 
        }
        return false;
    }

    internal static bool IsNumberFromTaskList(string? input, int taskCount)
    {
        if (int.TryParse(input, out int number)) 
        { 
            return number >= 1 && number <= taskCount; 
        }
        return false;
    }
}

