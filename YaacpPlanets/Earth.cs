using YaacpShips.Vessels;

namespace YaacpPlanets
{
    public class Earth : Planet
    {
        public Earth() : base(3)
        {}

        public override void GiveQuest(Vessel ship)
        {
            ship.Status = "Earth quest taken";
        }

        public override void TakeQuest(Vessel ship)
        {
            ship.Status = "Nothing";
        }
    }
}
