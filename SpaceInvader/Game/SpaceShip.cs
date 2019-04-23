using SpaceInvader.Weapons;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace SpaceInvader.Game
{
    public delegate void ProjectileFired(List<Projectile> p);
    public class SpaceShip : SpaceObject, ISpaceInputHandler, IDestroyable, IDamageDealer
    {
        private Weapon weapon;
        public event ProjectileFired projectileFired;

        public SpaceShip()
        {
           x = 100;
            y = 100;
            Health = 100;
            velocity = 10;

        }

        public SpaceShip(double x, double y, int size, Weapon weapon)
            : this()
        {
            this.x = x;
            this.y = y;
            Height = size;
            Width = size;
            this.weapon = weapon;
        }

        public override void Draw(DrawingContext ctx)
        {
            Pen pen = new Pen(Brushes.White, 1.0);
            double refX = x + Width/2;

            ctx.DrawLine(pen, new Point(refX, y), new Point(refX + Width * .5, y + Width * .6));
            ctx.DrawLine(pen, new Point(refX, y), new Point(refX - Width * .5, y + Width * .6));

            ctx.DrawLine(pen, new Point(refX + Width * .5, y + Height * .6), new Point(refX + Width * .3, y + Height * .6));
            ctx.DrawLine(pen, new Point(refX - Width * .5, y + Height * .6), new Point(refX - Width * .3, y + Height * .6));

            ctx.DrawLine(pen, new Point(refX + Width * .3, y + Height * .6), new Point(refX + Width * .10, y + Height * .3));
            ctx.DrawLine(pen, new Point(refX - Width * .3, y + Height * .6), new Point(refX - Width * .10, y + Height * .3));

            ctx.DrawLine(pen, new Point(refX - Width * .10, y + Height * .3), new Point(refX - Width * .12, y + Height * .5));
            ctx.DrawLine(pen, new Point(refX + Width * .10, y + Height * .3), new Point(refX + Width * .12, y + Height * .5));

            ctx.DrawLine(pen, new Point(refX - Width * .16, y + Height * .5), new Point(refX - Width * .12, y + Height * .5));
            ctx.DrawLine(pen, new Point(refX + Width * .16, y + Height * .5), new Point(refX + Width * .12, y + Height * .5));

            ctx.DrawText(
                  new FormattedText($"H: {Health} / W: {weapon}",
                  CultureInfo.GetCultureInfo("en-us"),
                  FlowDirection.LeftToRight,
                  new Typeface("Verdana"),
                  14, System.Windows.Media.Brushes.LightGray),
                  new System.Windows.Point(x-Width, y + Height-10));
        }

        internal void MoveUp()
        {
            y += velocity;
        }

        internal void MoveDown()
        {
            y -= velocity;
        }

        internal void MoveRight()
        {
            x += velocity;
        }

        internal void MoveLeft()
        {
            x -= velocity;
        }

        internal void Shoot()
        {
            List<Projectile> p = weapon.Fire(x, y);
            projectileFired?.Invoke(p);
        }

        internal void Move(double x, double y)
        {
            this.x += x;
            this.y = y;
        }

        public void KeyPressed(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.W))
            {
                y -= velocity;
            }

            if (Keyboard.IsKeyDown(Key.S))
            {
                y += velocity;
            }

            if (Keyboard.IsKeyDown(Key.D))
            {
                x += velocity;
            }

            if (Keyboard.IsKeyDown(Key.A))
            {
                x -= velocity;
            }

            if (e.Key == Key.Space)
            {
                Shoot();
            }
        }

        public bool HasBeenDestroyed()
        {
            return this.Health > 0;
        }

        public int GetDamage()
        {
            return Health;
        }

        public void DealDamage(IDamageDealer dealer)
        {
            this.Health -= dealer.GetDamage();
        }

        internal void SetWeapon(Weapon weapon)
        {
            this.weapon = weapon;
        }
    }
}