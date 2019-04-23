using SpaceInvader.Game;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SpaceInvader.Weapons
{
    public class Laser : Weapon
    {
        public Laser()
            :base(25, new TimeSpan(TimeSpan.TicksPerMillisecond*100))
        {

        }
        public override void Draw(DrawingContext ctx)
        {
            throw new System.NotImplementedException();

        }

        internal override List<Projectile> Fire(double shipX, double shipY)
        {
            List<Projectile> projectiles = new List<Projectile>();
            if (IsWeaponAvailable()) { 
                projectiles.Add(CreateProjectile(shipX + 30, shipY + 10));
                projectiles.Add(CreateProjectile(shipX + 10, shipY + 10));
            }
            return projectiles;
        }

        

        private Projectile CreateProjectile(double shipX, double shipY)
        {
            Projectile p = new Projectile(attack);
            p.SetDirection(SpaceDirection.Up);
            p.SetVelocity(5);
            p.SetPosition(new Point(shipX, shipY));

            BitmapSource image = CreateBitmap(4, 20, 96, drawingContext =>
            {
                drawingContext.DrawRectangle(
                    Brushes.White, new Pen(Brushes.White, 3D), new Rect(x - 6, y, x + 6, y + 10));
            });
            image.Freeze();
            p.ImageSource = image;
            return p;
        }

        public override string ToString()
        {
            return "Laser";
        }
    }
}
