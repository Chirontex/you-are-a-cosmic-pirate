using System;

namespace YaacpShips
{
    abstract class Cannon
    {
        private bool working = true;
        private bool load = false;
        private int size;
        private virtual int damageBase = 1;
        private virtual int damageMax;
        
        private bool Working
        {
            get
            {
                return working;
            }
            set
            {
                working = value;

                if (!working) load = false;
            }
        }

        public bool Load
        {
            get
            {
                return load;
            }
            set
            {
                if (working) load = value;
            }
        }

        public int Size
        {
            get
            {
                return size;
            }
            set
            {
                if (value < 1) size = 1;
                else if (value > 3) size = 3;
                else size = value;
            }
        }

        public int Fire(Random randomizer)
        {
            int result;

            if (working && load) result = randomizer.Next((size * damageBase), (size * damageMax));
            else result = 0;

            return result;
        }
    }
}
