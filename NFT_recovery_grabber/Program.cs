// See https://aka.ms/new-console-template for more information
using NFT_recovery_grabber;
using Microsoft.Extensions.Configuration;


//string[] cmdArgs = Environment.GetCommandLineArgs();

bool launchHidden = false;

if(args.Contains("-h") || args.Contains("--hidden"))
{
    launchHidden= true;
}

int delayBeforeClose = 0;


FileLogger.Log("Nft recovery grabber started");

IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
Settings settings = config.GetRequiredSection("Settings").Get<Settings>();

int.TryParse(settings.DelayBeforeCloseSeconds, out delayBeforeClose);

Grabber grabber = new Grabber(settings.Url, settings.LauncherID);
grabber.GetCoins(delayBeforeClose, launchHidden);
