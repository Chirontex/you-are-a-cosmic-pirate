using YaacpShips.Vessels;

namespace YaacpPlanets
{
    public class Earth : Planet
    {
        public Earth() : base(3)
        {}

        public override void GiveQuest(Vessel ship)
        {
            if (ship.Status == "Nothing") ship.Status = "Quest taken: Earth";
        }

        public override void TakeQuest(Vessel ship)
        {
            if (ship.Status != "Nothing") ship.Status = "Nothing";
        }
    }
}
