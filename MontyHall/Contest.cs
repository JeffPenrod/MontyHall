using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MontyHall
{
    public class Contest
    {
        public Host Host { get; set; }
        public Doors Doors { get;  set; }
        public IContestant Contestant { get; set; }

        private Random _rnd;

        public Contest(Random rnd)
        {
            _rnd = rnd;
        }

        public void Setup(IContestant contestant)
        {
            const int nbrOfDoors = 3;

            Contestant = contestant;

            if (Doors == null)
            {
                Doors = new Doors(_rnd);
            }

            if (Host == null)
            {
                Host = new Host(_rnd);
            }

            Doors.Setup(nbrOfDoors);

        }

        public PrizeType Play() 
        {
            // Contestant makes initial choice.
            Contestant.Decide(Doors.Count);

            // Host shows a door (or doors)
            var shownDoors = Host.GetDoorsToShow(Doors, Contestant.DoorChoice);

            // Contest decides to switch or stay
            Contestant.Decide(Doors);

            // See what contestant won
            var door = Doors.GetDoorById(Contestant.DoorChoice);

            return door.Prize;

        }

    }
}
