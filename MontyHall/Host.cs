using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MontyHall
{
    public class Host
    {

        private Random _rnd;

        public Host(Random rnd)
        {
            _rnd = rnd;
        }

        public int[] GetDoorsToShow(Doors doors, int contestantChoice)
        {

            var list = new List<int>();

            var choosableDoorId = new List<int>();

            foreach (Door door in doors)
            {

                if (door.Prize == PrizeType.Good) continue;

                if (door.Id == contestantChoice) continue;

                // Choose the doors without the prize and 
                // not chosen by contestant.
                list.Add(door.Id);
                door.State = DoorState.Open;

                if (list.Count > doors.Count - 3) break;

            }

            return list.ToArray();

        }

    }
}
