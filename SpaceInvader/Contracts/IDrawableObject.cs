using System.Windows.Media;

namespace SpaceInvader.Contracts
{
    public interface IDrawableObject
    {
        void Draw(DrawingContext ctx);
    }
}