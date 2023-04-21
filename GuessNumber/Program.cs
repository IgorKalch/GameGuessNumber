using GuessNumber.Properties;
using GuessNumberLibrary;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using System.Resources;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(app =>
    {
        app.SetBasePath(Directory.GetCurrentDirectory());
        app.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        app.AddEnvironmentVariables();
    })
    .ConfigureServices((context, services) =>
    {
        Settings settings = new Settings();
        ResourceManager rm = new ResourceManager("GuessNumber.Properties.Resources", Assembly.GetExecutingAssembly());
        context.Configuration.GetSection(Settings.SettingsKey).Bind(settings);
        services.AddSingleton(settings);
        services.AddScoped<IRandomGenerator,RandomGenerator>();
        services.AddScoped<Game>(
            x => new Game(x.GetRequiredService<Settings>(), x.GetRequiredService<IRandomGenerator>(), rm)
        );
    }).Build();
       

Console.WriteLine(Resources.Greeting);
Game game = host.Services.GetService<Game>() ?? throw new ArgumentNullException(nameof(game));
game.OnNotify += Notify; 
do
{
    game.Play();
    Console.WriteLine(string.Format(Resources.MessageTryAgain));

} while (Console.ReadKey().Key == ConsoleKey.Y);

void Notify(Game sender)
{
    switch (sender.Step)
    {
        case GameStep.Start:
            Console.WriteLine(string.Format(Resources.MessageStart));
            break;

        case GameStep.Turn:
            Console.WriteLine(string.Format(Resources.MessageTryToGuess, sender.AttemptsLeft));           
            sender.SetUserGuess(Console.ReadLine());
            break;

        case GameStep.AnswerWrongData:
            Console.WriteLine(string.Format(Resources.MessageError));
            break;

        case GameStep.AnswerSmoller:
            Console.WriteLine(string.Format(Resources.MessageSmaller));
            break;

        case GameStep.AnswerBigger:
            Console.WriteLine(string.Format(Resources.MessageBigger));
            break;

        case GameStep.Win:
            Console.WriteLine(string.Format(Resources.MessageCongrat));
            break;

        case GameStep.Lose:
            Console.WriteLine(string.Format(Resources.MessageLose));
            break;

        case GameStep.End:
            break;
    }
}