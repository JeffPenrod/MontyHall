using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MontyHall
{
    class Program
    {
        static void Main(string[] args)
        {
            const int nbrOfRuns = 1000;
            const int nbrOfDoors = 3;

            Random rnd = new Random((int)DateTime.Now.Ticks);

            RunContests(rnd, nbrOfRuns, nbrOfDoors, typeof(SteadfastContestant));
            RunContests(rnd, nbrOfRuns, nbrOfDoors, typeof(SwitchyContestant));

            Console.WriteLine();
            Console.WriteLine("Hit ENTER to exit");
            Console.ReadLine();


        }

        private static void RunContests(Random rnd, int nbrOfRuns, int nbrOfDoors, Type contestantType)
        {
            var host = new Host(rnd);

            var wins = 0;
            var losses = 0;

            const string fmtSummary = "{0} Wins {1:N0}, Losses {2:N0}";
            // const string fmtResult = "Round {0:D3} InitialChoice: {1:D2} OpenDoor: {2:D2} FinalChoice: {3:D2}  PrizeDoor: {4:D2} ";
            
            string contestantDescription = null;

            for (int i = 0; i < nbrOfRuns; i++)
            {
                var doors = new Doors(rnd);

                IContestant contestant = null;
                
                if (contestantType == typeof(SteadfastContestant))
                {
                    contestant = new SteadfastContestant(rnd);
                    contestantDescription = "Steadfast Contestant";
                }
                else if (contestantType == typeof(SwitchyContestant))
                {
                    contestant = new SwitchyContestant(rnd);
                    contestantDescription = "Switchy Contestant";
                }

                var contest = new Contest(rnd) { Doors = doors, Host = host };
                contest.Setup(contestant);

                var prize = contest.Play();

                if (prize == PrizeType.Good)
                {
                    wins++;
                }
                else
                {
                    losses++;
                }

                //Console.WriteLine(fmtResult, i + 1, contestant.InitialDoorChoice, doors.ShownDoor.Id, contestant.DoorChoice, doors.WinnerDoor.Id);
            }

            Console.WriteLine();
            Console.WriteLine(fmtSummary, contestantDescription, wins, losses);
            Console.WriteLine();
            Console.WriteLine();


        }



    }
}
