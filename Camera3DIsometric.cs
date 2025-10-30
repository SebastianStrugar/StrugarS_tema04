using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;

namespace ConsoleApp3
{
    class Camera3DIsometric
    {
        private Vector3 eye;
        private Vector3 target;
        private Vector3 up_vector;

        private const int MOVEMENT_UNIT = 1;
        private const float ROTATION_ANGLE = 0.05f; // radiani (~3°)

        public Camera3DIsometric()
        {
            eye = new Vector3(200, 175, 25);
            target = new Vector3(0, 25, 0);
            up_vector = new Vector3(0, 1, 0);
        }

        public Camera3DIsometric(int _eyeX, int _eyeY, int _eyeZ, int _targetX, int _targetY, int _targetZ, int _upX, int _upY, int _upZ)
        {
            eye = new Vector3(_eyeX, _eyeY, _eyeZ);
            target = new Vector3(_targetX, _targetY, _targetZ);
            up_vector = new Vector3(_upX, _upY, _upZ);
        }

        public Camera3DIsometric(Vector3 _eye, Vector3 _target, Vector3 _up)
        {
            eye = _eye;
            target = _target;
            up_vector = _up;
        }

        public void SetCamera()
        {
            Matrix4 camera = Matrix4.LookAt(eye, target, up_vector);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref camera);
        }

        private void RotateAroundTarget(float angle)
        {
            Vector3 direction = eye - target;

            float cos = (float)Math.Cos(angle);
            float sin = (float)Math.Sin(angle);

            float x = direction.X * cos - direction.Z * sin;
            float z = direction.X * sin + direction.Z * cos;

            direction.X = x;
            direction.Z = z;

            eye = target + direction;
            SetCamera();
        }

        public void RotateLeft()
        {
            RotateAroundTarget(ROTATION_ANGLE);
        }

        public void RotateRight()
        {
            RotateAroundTarget(-ROTATION_ANGLE);
        }

        public void MoveRight()
        {
            eye.Z -= MOVEMENT_UNIT;
            target.Z -= MOVEMENT_UNIT;
            SetCamera();
        }

        public void MoveLeft()
        {
            eye.Z += MOVEMENT_UNIT;
            target.Z += MOVEMENT_UNIT;
            SetCamera();
        }

        public void MoveForward()
        {
            eye.X -= MOVEMENT_UNIT;
            target.X -= MOVEMENT_UNIT;
            SetCamera();
        }

        public void MoveBackward()
        {
            eye.X += MOVEMENT_UNIT;
            target.X += MOVEMENT_UNIT;
            SetCamera();
        }

        public void MoveUp()
        {
            eye.Y += MOVEMENT_UNIT;
            target.Y += MOVEMENT_UNIT;
            SetCamera();
        }

        public void MoveDown()
        {
            eye.Y -= MOVEMENT_UNIT;
            target.Y -= MOVEMENT_UNIT;
            SetCamera();
        }
    }
}
