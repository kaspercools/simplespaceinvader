using SpaceInvader.Contracts;
using System.Windows;
using System.Windows.Media;

namespace SpaceInvader.Game
{
    public abstract class SpaceObject : IDrawableObject, IColidable
    {
        protected double x;
        protected double y;
        public double Width { get; protected set; }
        public double  Height { get; protected set; }
        protected int Health { get; set; }
        protected int velocity;

        public SpaceObject()
        {

        }

        public SpaceObject(Point position)
        {
            this.x = position.X;
            this.y = position.Y;
        }

        public bool DidColide(SpaceObject target)
        {
            Point targetPosition = target.GetPosition();
            return ((x + Width >= targetPosition.X) 
                && (x <= targetPosition.X + target.Width) 
                && (y + Height >= targetPosition.Y) && (y <= targetPosition.Y + target.Height));
        }

        public abstract void Draw(DrawingContext ctx);

        public Point GetPosition()
        {
            return new Point(x, y);
        }

        public void SetVelocity(int velo)
        {
            velocity = velo;
        }
    }
}