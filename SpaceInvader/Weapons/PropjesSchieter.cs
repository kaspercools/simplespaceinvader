using SpaceInvader.Game;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SpaceInvader.Weapons
{
    public class PropjesSchieter : Weapon
    {
        public PropjesSchieter()
            : base(65, new TimeSpan(TimeSpan.TicksPerSecond))
        {

        }
        public override void Draw(DrawingContext ctx)
        {
            throw new System.NotImplementedException();
        }

        internal override List<Projectile> Fire(double shipX, double shipY)
        {
            List<Projectile> projectiles = new List<Projectile>();

            if (IsWeaponAvailable())
            {

                projectiles.Add(CreateProjectile(shipX + 20, shipY)); //shipX == middle of the ship
                projectiles.Add(CreateProjectile(shipX + 20, shipY)); //shipX == middle of the ship
                projectiles.Add(CreateProjectile(shipX + 20, shipY)); //shipX == middle of the ship
                projectiles.Add(CreateProjectile(shipX + 20, shipY)); //shipX == middle of the ship
            }

            return projectiles;
        }

        private Projectile CreateProjectile(double shipX, double shipY)
        {
            Projectile p = new Projectile(attack);
            p.SetDirection(SpaceDirection.Up);
            Random r = new Random(Guid.NewGuid().GetHashCode());
            p.SetVelocity(r.Next(3, 4));

            p.SetPosition(new Point(shipX + r.Next(-20, 20), shipY + r.Next(-10, 10)));

            BitmapSource image = CreateBitmap(4, 20, 96, drawingContext =>
            {
                drawingContext.DrawRectangle(
                    Brushes.Yellow, new Pen(Brushes.Yellow, 2D), new Rect(x, y, x + 2, y + 2));
            });
            image.Freeze();
            p.ImageSource = image;
            return p;
        }

        public override string ToString()
        {
            return "Propjesschieter";
        }
    }
}
