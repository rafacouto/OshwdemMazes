using System;
using Mono.Options;

namespace Treboada.Net.Ia
{
	public class ProgramOptions
	{

		public bool ShouldShowHelp { get; private set; }

		public float Straightness { get; private set; }

		private OptionSet Options;


		public ProgramOptions ()
		{
			ShouldShowHelp = false;

			Options = new OptionSet { 
				
				{ "s|straightness=", 
					"Probability to generate straightforward paths (0.0 - 1.0).", 
					s => Straightness = float.Parse(s) }, 
				
				{ "h|help", 
					"Show this message and exit", 
					h => ShouldShowHelp = (h != null) },
			};
		}


		public bool CommandLineArgs(string[] args)
		{
			bool abort = false;

			try 
			{
				Options.Parse (args);
			} 
			catch 
			{
				Console.WriteLine ("\nCommand line arguments error!");
				abort = true;
				ShouldShowHelp = true;
			}

			if (ShouldShowHelp) ShowHelp ();

			return abort;
		}


		public void ShowHelp()
		{
			Console.WriteLine ("\n-h --help");
			Console.WriteLine ("    Shows this help");

			Console.WriteLine ("\n-s --straightness");
			Console.WriteLine ("    Generates more straightness paths; float value (0.00 - 1.00), default is 0.00\n");
		}

	}
}

