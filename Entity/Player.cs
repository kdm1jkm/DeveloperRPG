using System;
using SFML.Graphics;
using SFML.System;
using static DeveloperRPG.Player.PlayerState;

namespace DeveloperRPG
{
    public class Player : Entity
    {
        public enum PlayerState
        {
            StateIdle,
            StateMoving
        }
        public const float Speed = 50;

        public float Direction = 0;
        public float Velocity = Speed;

        public Player() : base(CHARACTER_PATH)
        {
            
        }

        public PlayerState State { get; set; }



        public override void Update(Time elapsed)
        {
            base.Update(elapsed);
            switch (State)
            {
                case StateMoving:
                    CurrentAnimation = Animations["dash-right"];
                    if (Velocity > Speed) Velocity -= (Velocity - Speed) * Math.Min(10.0f * elapsed.AsSeconds(), 1.0f);

                    Coordinate += new Vector2f((float) Math.Cos(Direction), (float) Math.Sin(Direction)) * Velocity *
                                  elapsed.AsSeconds();

                    break;

                case StateIdle:
                    CurrentAnimation = Animations["idle"];
                    Velocity = Speed;
                    break;
            }
        }
    }
}