using Days.Game.Combat.Infra;

namespace Days.Game.Object.Script
{
    public enum State
    {
        Ready,
        Wait,
    }
    public class CombatHandler
    {
        
        public State State { get; set; } 
        public ActionReport Action()
        {
            var ar = new ActionReport();


            return ar;
        }
    }
}