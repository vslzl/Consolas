using CommandLine;
namespace Consolas;

public class CommandLineOptions
{
    [Option(shortName: 'l', longName: "length", Required = false, HelpText = "Length of the array", Default = 50)]
    public int ArrayLength { get; set; }

    [Option(shortName: 'm', longName: "max-value", Required = false, HelpText = "Maximum value of array.", Default = 50)]
    public int MaxValue { get; set; }


    [Option(shortName: 'd', longName: "delay-value", Required = false, HelpText = "Delay between visualizations", Default = 50)]
    public int DelayMs { get; set; }

}