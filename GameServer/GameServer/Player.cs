using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace GameServer
{
    class Player
    {
        public int id;
        public string username;
        public string facing="right";
        public bool walk;
        public bool isbombed;
        public bool coal;
        public bool metal ;
        public bool water ;
        public bool develop ;
        public Vector3 position;
        public Quaternion rotation;

        private float moveSpeed = 5f / Constants.TICKS_PER_SEC;
        private bool[] inputs;

        public Player(int _id, string _username, Vector3 _spawnPosition)
        {
            id = _id;
            username = _username;
            position = new Vector3(0,0,-26);
            rotation = Quaternion.Identity;

            inputs = new bool[4];
        }

        /// <summary>Processes player input and moves the player.</summary>
        public void Update()
        {
            /*
            Vector2 _inputDirection = Vector2.Zero;
            if (inputs[0])
            {
                _inputDirection.Y += 3;
            }
            if (inputs[1])
            {
                _inputDirection.Y -= 3;
            }
            if (inputs[2])
            {
                _inputDirection.X += 3;
            }
            if (inputs[3])
            {
                _inputDirection.X -= 3;
            }*/

            Move();
        }

        /// <summary>Calculates the player's desired movement direction and moves him.</summary>
        /// <param name="_inputDirection"></param>
        private void Move()
        {
            /*
            Vector3 _forward = Vector3.Transform(new Vector3(0, 0, 1), rotation);
            Vector3 _right = Vector3.Normalize(Vector3.Cross(_forward, new Vector3(0, 1, 0)));

            Vector3 _moveDirection = _right * _inputDirection.X + _forward * _inputDirection.Y;
            position += _moveDirection * moveSpeed;
*/
            ServerSend.PlayerPosition(this);
            ServerSend.PlayerRotation(this);
        }

        /// <summary>Updates the player input with newly received input.</summary>
        /// <param name="_inputs">The new key inputs.</param>
        /// <param name="_rotation">The new rotation.</param>
        public void SetInput(bool[] _inputs, Quaternion _rotation)
        {
            inputs = _inputs;
            rotation = _rotation;
        }
        public void SetPOS(Vector3 p)
        {
            position=p;

            //ServerSend.PlayerPosition(this);
        }
        public void SetFace(string face){this.facing=face;}
        public void SetWalk(bool w){this.walk=w;}
        
    }
}
