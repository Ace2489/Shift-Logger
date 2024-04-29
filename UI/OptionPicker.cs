namespace UI;
using Spectre.Console;

public class OptionPicker : IOptionPicker
{
    public string GetOption()
    {
        Console.WriteLine("Welcome to the shift logger");
        string option = AnsiConsole.Prompt(new
        SelectionPrompt<string>()
        .Title("Select your [blue]desired[/] operation")
        .AddChoices(["Create a shift record", "View existing shift records", "Exit the app"])
        );
        return option.First().ToString().ToLower();
    }
}
