using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace SpaceInvader.Game
{
    public class SpaceRock : SpaceObject, IDestroyable, IDamageDealer
    {
        public SpaceRock(double x, double y, double width, double height, int health, int velocity)
        {
            this.x = x;
            this.y = y;
            this.Width = width;
            this.Height = height;
            this.Health = health;

            this.velocity = velocity;
        }



        public override void Draw(DrawingContext ctx)
        {
            ctx.DrawRectangle(Brushes.Green, new Pen(Brushes.Green, 1D), new System.Windows.Rect(x, y += velocity, Width, Height));
            ctx.DrawText(
                  new FormattedText($"H: {Health}",
                  CultureInfo.GetCultureInfo("en-us"),
                  FlowDirection.LeftToRight,
                  new Typeface("Verdana"),
                  14, System.Windows.Media.Brushes.White),
                  new System.Windows.Point(x+5, y+Height/2-10));
        }

        public bool HasBeenDestroyed()
        {
            return this.Health <= 0;
        }

        public void DealDamage(IDamageDealer dealer)
        {
            if (dealer is IDestroyable)
            {
                ((IDestroyable)dealer).DealDamage(this);
            }
            this.Health -= dealer.GetDamage();
        }

        public int GetDamage()
        {
            return Health;
        }
    }
}
