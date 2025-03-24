using DurakGame.Classes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace DurakGame.Classes
{
    public class ComputerPlayer : Player
    {
        public ComputerPlayer(string name) : base(name) { }

        public Card MakeMove()
        {
            return Hand.Count > 0 ? Hand[0] : null;
        }
    }
}
