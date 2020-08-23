using System;

namespace YaacpShips
{
    abstract class Cannon
    {
        private bool working = true;
        private bool load = false;
        
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
    }
}
