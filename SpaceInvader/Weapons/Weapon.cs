using SpaceInvader.Game;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SpaceInvader.Weapons
{
    public abstract class Weapon : SpaceObject, IDamageDealer
    {
        protected int attack;
        private long previousTicks;
        protected bool weaponDisabled = false;
        protected TimeSpan weaponUnrisponsiveTimespan;

        internal abstract List<Projectile> Fire(double shipX, double shipY);

        public Weapon(int attack, TimeSpan weaponUnrisponsiveTimespan)
        {
            this.attack = attack;
            this.weaponUnrisponsiveTimespan = weaponUnrisponsiveTimespan;
        }


        public static BitmapSource CreateBitmap(int width, int height, double dpi, Action<DrawingContext> render)
        {
            DrawingVisual drawingVisual = new DrawingVisual();
            using (DrawingContext drawingContext = drawingVisual.RenderOpen())
            {
                render(drawingContext);
            }
            RenderTargetBitmap bitmap = new RenderTargetBitmap(
                width, height, dpi, dpi, PixelFormats.Default);
            bitmap.Render(drawingVisual);

            return bitmap;
        }

        public int GetDamage()
        {
            return attack;
        }

        protected bool IsWeaponAvailable()
        {
            bool available = false;
            long difference = (DateTime.UtcNow.Ticks -previousTicks);
            if (difference > weaponUnrisponsiveTimespan.Ticks)
            {
                Debug.WriteLine($"diff: {difference}");
                 previousTicks = DateTime.UtcNow.Ticks;
                available = true;
            }

             available &= !weaponDisabled;

            return available;
        }
    }
}
