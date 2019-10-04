using System;
using System.CommandLine;
using System.CommandLine.Invocation;

namespace span_string
{

    class Program
    {

        static void Main(string[] args)
        {

            var rootCommand = new RootCommand
            {
                new Option(
                    "--span",
                    "use span")
                {
                    Argument = new Argument<bool>()
                },
            };

            rootCommand.Description = "10 Million iterations on Lorem Ipsum";
            rootCommand.Handler = CommandHandler.Create<bool>((span) =>
            {
                var input = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum";

                Action t = null;

                if(span)
                {
                    Console.WriteLine("Using span");
                    t = () => IsUpperSpan(input);
                }
                else
                {
                    Console.WriteLine("Using Foreach");
                    t = () => IsUpperForeach(input);
                }

                for (int i = 0; i < 1000 * 1000 * 10; i++)
                {
                    t();
                }
            });


            rootCommand.Invoke(args);
        }

        static void IsUpperSpan(string intput)
        {
            var s = intput.AsSpan();
            for (int i = 0; i < s.Length; i++)
            {
                char.IsUpper(s[i]);
            }
        }

        static void IsUpperForeach(string input)
        {
            foreach (char c in input)
            {
                char.IsUpper(c);
            }

        }
    }
}
