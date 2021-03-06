﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriTray_MG
{
    class Camera
    {

        public Vector3 Position = new Vector3(0, 0, 10);
        public Vector3 Direction = Vector3.Normalize(new Vector3(0, 0, -1));
        public Vector3 Up = Vector3.Up;
        public Matrix View => Matrix.CreateLookAt(Position, Position + Direction, Up);
        public Matrix Projection => Matrix.CreateOrthographic(Resolution.Width, Resolution.Height, 0.0f, 1.0f);
        //public Matrix Projection => Matrix.CreatePerspectiveFieldOfView(MathHelper.Pi/3, 1.0f, 0.5f, 100.0f);

        public Rectangle Resolution = new Rectangle(0, 0, 800, 600);
        public float CameraSensitivity = 0.02f;

        public static readonly Camera Main = new Camera();

        public Camera()
        {
            Console.WriteLine("Camera");
        }


        public void Move(Vector3 moveTo)
        {
            Console.WriteLine(Position.ToString());
            Position += moveTo * CameraSensitivity;
        }
    }
}
