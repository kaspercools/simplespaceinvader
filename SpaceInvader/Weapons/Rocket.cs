using SpaceInvader.Game;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SpaceInvader.Weapons
{
    public class Rocket : Weapon
    {
        public Rocket()
            : base(45, new TimeSpan(TimeSpan.TicksPerMillisecond*500))
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
                projectiles.Add(CreateProjectile(shipX + 20, shipY + 10));
            }

            return projectiles;
        }

        private Projectile CreateProjectile(double shipX, double shipY)
        {
            Projectile p = new Projectile(attack);
            p.SetDirection(SpaceDirection.Up);
            p.SetVelocity(7);
            p.SetPosition(new Point(shipX, shipY));

            BitmapSource image = CreateBitmap(4, 20, 96, drawingContext =>
            {
                drawingContext.DrawRectangle(
                    Brushes.Red, new Pen(Brushes.Red, 3D), new Rect(x - 2, y, x + 3, y + 15));
            });
            image.Freeze();
            p.ImageSource = image;
            return p;
        }

        public override string ToString()
        {
            return "Rocketlauncher";
        }

    }
}
