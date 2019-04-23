using SpaceInvader.Contracts;
using SpaceInvader.Weapons;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace SpaceInvader.Game
{
    internal class Level : SpaceObject, IDrawableObject, ISpaceInputHandler
    {
        private SpaceShip ship;
        private List<Projectile> projectiles;
        private List<SpaceRock> spacerocks;
        private Timer rockTimer;
        private double rockInterval = 2000;
        private double previousInterval;
        private LinkedListNode<Weapon> referenceWeapon;
        private long objectsDestroyed;
        private long objectsMissed;

        public Level()
        {
            spacerocks = new List<SpaceRock>();
            projectiles = new List<Projectile>();
            rockTimer = new Timer(500);
            rockTimer.Elapsed += RockTimer_Elapsed;

            //Create weapon list
            LinkedList<Weapon> referenceList = new LinkedList<Weapon>();
            referenceList.AddFirst(new Laser());
            referenceList.AddLast(new Rocket());
            referenceList.AddLast(new PropjesSchieter());
            referenceWeapon = referenceList.First;
        }

        private void RockTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (previousInterval > rockInterval)
            {
                previousInterval = 0;

                Random rand = new Random((int)System.DateTime.Now.Ticks);
                double x = rand.NextDouble() * Width - 100;
                double y = 0;
                int width = rand.Next(20, 50) + 10;
                int height = rand.Next(20, 50) + 10;

                AddRock(x, y, width, height, width + height, rand.Next(1, 2));
            }
            previousInterval += rockTimer.Interval;

        }

        public Level(double width, double height)
            : this()
        {
            Width = width;
            Height = height;
            ship = new SpaceShip(width / 2 - 20, height - 100, 40, new Laser());
            ship.projectileFired += Ship_projectileFired;
            rockTimer.Start();

        }

        private void Ship_projectileFired(List<Projectile> p)
        {
            projectiles.AddRange(p);
        }

        public override void Draw(DrawingContext ctx)
        {
            //clean canvas
            ctx.DrawRectangle(Brushes.Black, new Pen(Brushes.Black, 1.0), new Rect(x, y, Width, Height));
            // print stats
            ctx.DrawText(
                  new FormattedText($"Destroyed: {objectsDestroyed}",
                  CultureInfo.GetCultureInfo("en-us"),
                  FlowDirection.LeftToRight,
                  new Typeface("Verdana"),
                  14, System.Windows.Media.Brushes.LightBlue),
                  new System.Windows.Point(5, 20));

            ctx.DrawText(
                  new FormattedText($"Missed: {objectsMissed}",
                  CultureInfo.GetCultureInfo("en-us"),
                  FlowDirection.LeftToRight,
                  new Typeface("Verdana"),
                  14, System.Windows.Media.Brushes.LightBlue),
                  new System.Windows.Point(5, 35));

            ship.Draw(ctx);

            foreach (var projectile in projectiles)
            {
                projectile.Draw(ctx);
            }

            for (int i = spacerocks.Count - 1; i >= 0; i--)
            {
                SpaceRock rock = spacerocks[i];
                rock.Draw(ctx);
            }

            CheckColisions();
            CheckRemovableItems();
        }

        private void CheckRemovableItems()
        {


            for (int i = projectiles.Count - 1; i >= 0; i--)
            {
                if (projectiles[i].GetPosition().Y - projectiles[i].Height < 0 || projectiles[i].GetPosition().Y > Height)
                {
                    projectiles.Remove(projectiles[i]);

                }
            }

            for (int i = spacerocks.Count - 1; i >= 0; i--)
            {
                SpaceRock spaceRock = spacerocks[i];

                if (spaceRock.GetPosition().Y > Height)
                {
                    objectsMissed++;
                    spacerocks.Remove(spacerocks[i]);
                }

                if (spaceRock.HasBeenDestroyed())
                {
                    objectsDestroyed++;
                    spacerocks.Remove(spaceRock);
                }
            }
        }

        private void CheckColisions()
        {
            for (int i = projectiles.Count - 1; i >= 0; i--)
            {
                Projectile projectile = projectiles[i];
                for (int j = 0; j < spacerocks.Count; j++)
                {
                    SpaceRock rock = spacerocks[j];
                    if (projectile.DidColide(rock))
                    {
                        Debug.WriteLine("HIT!!");
                        rock.DealDamage(projectile);
                        projectiles.Remove(projectile);
                    }
                }
            }

            for (int i = 0; i < spacerocks.Count; i++)
            {
                if (ship.DidColide(spacerocks[i]))
                {
                    spacerocks[i].DealDamage(ship);
                    if (ship.HasBeenDestroyed())
                    {
                        Console.WriteLine("You lost!");
                    }
                }
            }
        }

        private void AddRock(double x, double y, int width, int height, int health, int velocity)
        {
            SpaceRock sp = new SpaceRock(x, y, width, height, health, velocity);

            spacerocks.Add(sp);
        }

        public SpaceShip GetShip()
        {
            return ship;
        }

        public void KeyPressed(object sender, KeyEventArgs e)
        {
            ship.KeyPressed(sender, e);

            if (e.Key == Key.Z)
            {
                referenceWeapon = referenceWeapon.Next ?? referenceWeapon.List.First;
                ship.SetWeapon(referenceWeapon.Value);
            }
        }
    }
}
