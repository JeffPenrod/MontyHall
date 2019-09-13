using System;
using System.Collections.Generic;
using System.Linq;

namespace MontyHall
{
    public interface IContestant
    {
        int InitialDoorChoice { get; set; }
        int DoorChoice { get; set; }
        void Decide(int nbrOfDoors);
        void Decide(Doors doors);

    }

    public class SteadfastContestant : IContestant
    {
        private Random _rnd;

        public SteadfastContestant(Random rnd)
        {
            _rnd = rnd;
        }
        public int InitialDoorChoice { get; set; }
        public int DoorChoice { get; set; }

        public SteadfastContestant()
        { }

        public SteadfastContestant(int doorChoice)
        {
            DoorChoice = DoorChoice;
        }

        public void Decide(int nbrOfDoors = 3)
        {
            DoorChoice = _rnd.Next(nbrOfDoors) + 1;
        }


        public void Decide(Doors doors)
        {
            InitialDoorChoice = DoorChoice;
            
            // Nothing to do here folks. Player is standing fast.
        }


    }

    public class SwitchyContestant : IContestant
    {
        private Random _rnd;

        public SwitchyContestant(Random rnd)
        {
            _rnd = rnd;
        }

        public int InitialDoorChoice { get; set; }

        public int DoorChoice { get; set; }

        public SwitchyContestant()
        { }

        public SwitchyContestant(int doorChoice)
        {
            DoorChoice = DoorChoice;
        }

        public void Decide(int nbrOfDoors)
        {
            DoorChoice = _rnd.Next(nbrOfDoors) + 1;
        }


        public void Decide(Doors doors)
        {

            InitialDoorChoice = DoorChoice;

            foreach (Door d in doors)
            {
                // We are switchy! We don't want to stay on our original choice.
                if (d.Id == InitialDoorChoice) continue;

                // We don't want the loser prize in the door the host has shown us.
                if (d.State == DoorState.Open) continue;

                DoorChoice = d.Id;
            }

        }

    }


}
