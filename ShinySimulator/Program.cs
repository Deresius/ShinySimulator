using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShinySimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            programStart();
        }

        static private void publish(string input, int delay)
        {            
            foreach(var chaR in input)
            {                
                System.Threading.Thread.Sleep(delay);
                Console.Write(chaR);
            }
            Console.WriteLine("");
        }

        static private void publishInLine(string input, int delay)
        {
            foreach (var chaR in input)
            {
                System.Threading.Thread.Sleep(delay);
                Console.Write(chaR);
                
            }
        }

        static private void programStart()
        {
            writeMainMenu();
           
            ConsoleKey response = Console.ReadKey().Key;

            while (response != ConsoleKey.D1 && response != ConsoleKey.D2 && response != ConsoleKey.D3 && response != ConsoleKey.Q)
            {
                Console.Clear();
                writeMainMenu();
                response = Console.ReadKey().Key;
            }

            Random rng = new Random();
            Console.Clear();

            switch (response)
            {
                case ConsoleKey.D1:
                    getSingleEncountersTilShiny(rng);
                    break;
                case ConsoleKey.D2:
                    SimulateShinyHunt(true, 0);
                    break;
                case ConsoleKey.D3:
                    bunch(true, 0);
                    break;
            }
            
        }

        static private void writeMainMenu()
        {
            Console.Clear();
            publishInLine("Welcome to Shiny Simulator", 40);
            publishInLine(".", 250);
            publishInLine(".", 250);
            publishInLine(".", 250);
            publish("", 250);
            addSomeSpace();
            publish("Press 1 for a random estimate of remaining encounters until a shiny spawn.", 1);
            publish("Press 2 to simulate shiny odds given a number of encounters.", 1);
            publish("Press 3 to simulate a number of shiny hunts.", 1);
            publish("Press Q to quit.", 1);
        }

        static private void bunch(bool setCount, int simulationCount)
        {
            Console.Clear();

            int shinyHuntSimulationCount;

            if (setCount == true)
            {
                Console.Clear();
                publish("How many simulations would you like to run?", 20);
                string theNumber = Console.ReadLine();
                shinyHuntSimulationCount = Int32.Parse(theNumber);
            }
            else
            {
                shinyHuntSimulationCount = simulationCount;
            }
            Console.Clear();

            Random rng = new Random();
            var totalEncounters = 0;

            var maxLength = Int32.MinValue;
            var minLength = Int32.MaxValue;

            for (int i = 0; i < shinyHuntSimulationCount; i++)
            {
                var thisCount = getEncountersTilShiny(rng);
                totalEncounters += thisCount;
                if (thisCount > maxLength) maxLength = thisCount;
                if (thisCount < minLength) minLength = thisCount;
            }

            var stringOne = "Out of " + shinyHuntSimulationCount + " simulations, the average shiny hunt took " + (totalEncounters / (double)shinyHuntSimulationCount) + " encounters.";
            var stringTwo = "The longest hunt took " + maxLength + " encounters.";
            var stringThree = "The shortest hunt took " + minLength + " encounters.";
            Console.WriteLine(stringOne);
            Console.WriteLine(stringTwo);
            Console.WriteLine(stringThree);

            addSomeSpace();

            publish("Rerun?  Y/N,", 0);
            publish("To change simulation count, press C.", 0);
            ConsoleKey theKey = Console.ReadKey().Key;
            while (theKey != ConsoleKey.Y && theKey != ConsoleKey.N && theKey != ConsoleKey.C)
            {
                Console.Clear();
                publish("Rerun?  Y/N,", 150);
                publish("To change simulation count, press C.", 100);
                theKey = Console.ReadKey().Key;
            }
            Console.WriteLine("");
            if (theKey == ConsoleKey.Y) bunch(false, shinyHuntSimulationCount);
            if (theKey == ConsoleKey.N) programStart();
            if (theKey == ConsoleKey.C) bunch(true, 0);
        }


        static private void getSingleEncountersTilShiny(Random rngP)
        {
            Console.Clear();
            var rng = rngP;
            Console.WriteLine("The shiny hunt took " + getEncountersTilShiny(rng) + " encounters.");
            addSomeSpace();
            Console.WriteLine("Rerun?  Y/N");
            ConsoleKey theKey = Console.ReadKey().Key;
            while (theKey != ConsoleKey.Y && theKey != ConsoleKey.N)
            {
                Console.Clear();
                publishInLine("Rerun?   ", 120);
                publishInLine("Y/N", 1000);
                Console.WriteLine("");
                theKey = Console.ReadKey().Key;
            }
            Console.WriteLine("");
            if (theKey == ConsoleKey.Y) getSingleEncountersTilShiny(rngP);
            if (theKey == ConsoleKey.N) programStart();
        }

        static private int getEncountersTilShiny(Random rng)
        {
            var encounters = 0;
            bool noShinyFound = true;

            while (noShinyFound)
            {
                encounters++;
                bool isShiny = false;                
                int tryMultiplier = 15;

                for (int j = 0; j < tryMultiplier; j++)
                {
                    int attempt = rng.Next(1, 4097);
                    if (attempt == (int)4096) isShiny = true;
                }
                if (isShiny) noShinyFound = false;
            }

            return encounters;
        }


        static private void addSomeSpace()
        {
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
        }

        static private void SimulateShinyHunt(bool setCount, int simulationCount)
        {
            int shiniesFound = 0;
            int spawnsToSimulate = 0;
            var toSimulate = 0;

            if (setCount == true)
            {
                Console.Clear();
                publish("How many simulations would you like to run?", 20);
                string theNumber = Console.ReadLine();
                toSimulate = Int32.Parse(theNumber);
                spawnsToSimulate = toSimulate;
            }
            else
            {
                spawnsToSimulate = simulationCount;
            }            

            int tryMultiplier = 15;
            Random rng = new Random();

            for (int i = 0; i < spawnsToSimulate; i++)
            {
                bool isShiny = false;

                for (int j = 0; j < tryMultiplier; j++)
                {
                    int attempt = rng.Next(1, 4097);
                    if (attempt == (int)4096) isShiny = true;
                }

                if (isShiny) shiniesFound++;
            }

            Console.WriteLine("Simulated " + spawnsToSimulate + " encounters");
            Console.WriteLine(shiniesFound + " shinies found.");

            addSomeSpace();
            Console.WriteLine("Rerun?  Y/N,");
            Console.WriteLine("To change simulation count, press C.");
            ConsoleKey theKey = Console.ReadKey().Key;
            while (theKey != ConsoleKey.Y && theKey != ConsoleKey.N && theKey != ConsoleKey.C)
            {
                Console.Clear();
                publishInLine("Rerun?   ", 120);
                publishInLine("Y/N,", 1000);
                Console.WriteLine("");
                publishInLine("To change simulation count, press ", 50);
                publishInLine("C.", 600);
                theKey = Console.ReadKey().Key;
            }
            Console.Clear();
            if (theKey == ConsoleKey.Y) SimulateShinyHunt(false, spawnsToSimulate);
            if (theKey == ConsoleKey.N) programStart();
            if (theKey == ConsoleKey.C) SimulateShinyHunt(true, 0);
        }
    }
}
