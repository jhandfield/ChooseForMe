using System.Diagnostics;

namespace Handfield.ChooseForMe {
    public class Program {
        static void Main(string[] args) {
            TextWriter errorWriter = Console.Error;
            
            if (args.Length < 2)
            {
                errorWriter.WriteLine("ERROR: Missing required arguments - you must specify a number of options and how many votes an option needs to win");
                return;
            }

            // First argument should be the number of options
            if (!Int32.TryParse(args[0], out int numOpts))
            {
                errorWriter.WriteLine("ERROR: You must specify a number of options");
                return;
            }

            if (!Int32.TryParse(args[1], out int numVotesToWin))
            {
                errorWriter.WriteLine("ERROR: You must specify a number of votes an option needs to win");
                return;
            }
            
            Console.WriteLine($"Voting across {numOpts} options, {numVotesToWin} votes for a single option needed to win");

            // Build the array to hold results
            int[] results = new int[numOpts];
            for(int i = 0; i <= numOpts - 1; i++)
                results[i] = 0;
            
            // Run the votes
            RunVoting(results, numVotesToWin);

            // Output the results
            OutputResults(results, numVotesToWin);
        }

        private static void RunVoting(int[] results, int votesNeeded)
        {
            var rand = new Random();
            
            // Calculate the maximum number of votes that should be needed
            /*
             * 1 option 3 votes to win = 3
             * 2 options 3 votes to win = 5
             * 3 options 3 votes to win = 7
             * 2 options 5 votes to win = 9
             *
             * Formula: [opts]x([VTW]-1)+1
             */
            int maxVotes = results.Length * (votesNeeded-1) + 1;
            
            Debug.WriteLine($"Casting maximum of {maxVotes} votes");
            Console.WriteLine("Casting votes...");

            for(int i = 1; i <= maxVotes; i++)
            {
                // Generate a random number
                int vote = rand.Next(results.Length);

                // Increment the option's vote count
                results[vote]++;

                Debug.WriteLine($"Vote cast for option {vote}; option now has {results[vote]} votes");

                // Check if we've hit the target
                if (results[vote] == votesNeeded)
                {
                    Debug.WriteLine("Winner determined");
                    return;
                }
            }
        }

        private static void OutputResults(int[] results, int votesNeeded)
        {
            Console.WriteLine("\nRESULTS\n=======");

            for(int i = 0; i <= results.Length - 1; i++)
            {
                string winnerText = (results[i] == votesNeeded) ? "WINNER" : "";
                Console.WriteLine($"Option {i+1}: {results[i]} votes {winnerText}");
            }
        }
    }
}