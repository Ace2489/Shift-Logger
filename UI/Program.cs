using Spectre.Console;
using ui;
using UI;
AnsiConsole.Clear();
UIApp ui = new("https://localhost:7011/api/shifts", new OptionPicker(), new DateTimeInput());


await ui.Run();