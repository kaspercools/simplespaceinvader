using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SpaceInvader.Game
{
    public class Projectile : SpaceObject, IDamageDealer
    {
        public BitmapSource ImageSource { get; set; }
        private SpaceDirection direction;
        private int attack;
        public Projectile(int attack)
        {
            this.attack = attack;
            direction = SpaceDirection.Down;
        }

        public void SetDirection(SpaceDirection direction)
        {
            this.direction = direction;
        }
        
        public override void Draw(DrawingContext ctx)
        {
            ctx.DrawImage(ImageSource, new Rect(x, y, ImageSource.Width, ImageSource.Height));
            if (SpaceDirection.Up == direction)
            {
                y -= velocity;
            }

            if (direction == SpaceDirection.Down)
            {
                y += velocity;
            }
        }

        internal void SetPosition(Point point)
        {
            x = point.X;
            y = point.Y;
        }

        public int GetDamage()
        {
            return attack;
        }
    }
}
