using System.Windows.Input;

namespace SpaceInvader.Game
{
    public interface ISpaceInputHandler
    {
        void KeyPressed(object sender, KeyEventArgs e);
    }
}