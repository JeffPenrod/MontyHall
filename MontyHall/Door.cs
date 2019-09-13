using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MontyHall
{
    public class Door
    {

        public Door()
        { }

        public Door(int id, PrizeType prize)
        {
            Id = id;
            Prize = prize;
        }

        public int Id { get; set; }
        public PrizeType Prize { get; set; }

        public DoorState State { get; set; }

        public override string ToString()
        {
            var prizeDescr = Enum.GetName(typeof(PrizeType), Prize);
            var stateDescr = Enum.GetName(typeof(DoorState), State);
            return string.Format("Door {0:D2} is {1}: {2}", Id, stateDescr, prizeDescr);
        }
    }

    public class Doors : List<Door>
    {

        public Door WinnerDoor
        {
            get
            {
                var qry = from Door d in this
                          where d.Prize == PrizeType.Good 
                          select d;

                var door = qry.FirstOrDefault();

                return door;
            }
        }
        public Door ShownDoor
        {
            get
            {
                var qry = from Door d in this
                          where d.State ==  DoorState.Open
                          select d;

                var door = qry.FirstOrDefault();

                return door;
            }
        }

        private Random _rnd;

        public Doors(Random rnd)
        {
            _rnd = rnd;
        }

        public int Setup(int nbrOfDoors)
        {
            Clear();

            // Add doors to collection.
            for (int i = 0; i < nbrOfDoors; i++)
            {
                Add(new Door(i + 1, PrizeType.Consolation));
            }

            var winnerIdx = _rnd.Next(nbrOfDoors);
            var goatIdx = winnerIdx;

            while (goatIdx == winnerIdx)
            {
                goatIdx = _rnd.Next(nbrOfDoors);
            }

            this[winnerIdx].Prize = PrizeType.Good;
            this[goatIdx].Prize = PrizeType.Goat;

            return this[winnerIdx].Id;
        }

        public Door GetDoorById(int doorId)
        {
            var qry = from Door d in this
                      where d.Id == doorId
                      select d;

            var door = qry.FirstOrDefault();

            return door;

        }


    }
    }
