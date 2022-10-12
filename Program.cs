using CommandLine;
using Consolas;

internal class Program
{

    private static int Main(string[] args)
    {
        return Parser.Default
            .ParseArguments<CommandLineOptions>(args)
            .MapResult<CommandLineOptions, int>(opts =>
            {
                try
                {
                    // We have the parsed arguments, so let's just pass them down
                    Consolas.ConsoleGraph.Test(opts.ArrayLength, opts.MaxValue, opts.DelayMs);
                    return 0;
                }
                catch(Exception e)
                {
                    Console.WriteLine("Error!");
                    Console.WriteLine(e.ToString());
                    return -3; // Unhandled error
                }
            },
            errs => -1
            ); // Invalid arguments

    }
}