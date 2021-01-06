using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace GameServer
{
    class ServerHandle
    {
        public static void WelcomeReceived(int _fromClient, Packet _packet)
        {
            int _clientIdCheck = _packet.ReadInt();
            string _username = _packet.ReadString();

            Console.WriteLine($"{Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} connected successfully and is now player {_fromClient}.");
            if (_fromClient != _clientIdCheck)
            {
                Console.WriteLine($"Player \"{_username}\" (ID: {_fromClient}) has assumed the wrong client ID ({_clientIdCheck})!");
            }
            Server.clients[_fromClient].SendIntoGame(_username);
        }

        public static void PlayerMovement(int _fromClient, Packet _packet)
        {
            bool[] _inputs = new bool[_packet.ReadInt()];
            for (int i = 0; i < _inputs.Length; i++)
            {
                _inputs[i] = _packet.ReadBool();
            }
            Quaternion _rotation = _packet.ReadQuaternion();

            Server.clients[_fromClient].player.SetInput(_inputs, _rotation);
        }
        public static void PlayerPosi (int _fromClient, Packet _packet){
             Vector3 _position = _packet.ReadVector3();
             Vector3 weaponrot = _packet.ReadVector3();
             string face=_packet.ReadString();
             bool walk = _packet.ReadBool();
             bool isbombed=_packet.ReadBool();
             bool coal=_packet.ReadBool();
             bool metal =_packet.ReadBool();
             bool water =_packet.ReadBool();
             bool develop =_packet.ReadBool();
             
             Server.clients[_fromClient].player.SetFace(face);
             Server.clients[_fromClient].player.SetWalk(walk);
             Server.clients[_fromClient].player.SetPOS(_position);
             Server.clients[_fromClient].player.weaponrot=weaponrot;
             Console.WriteLine(weaponrot);
             Server.clients[_fromClient].player.isbombed=isbombed;
             Server.clients[_fromClient].player.coal=coal;
             Server.clients[_fromClient].player.metal=metal;
             Server.clients[_fromClient].player.water=water;
             Server.clients[_fromClient].player.develop=develop;
             //Console.WriteLine("getposition");
             /*
             ClientSend.PlayerPos(gameObject.transform.position
      ,gameObject.GetComponent<facingDirection>().facing
      ,gameObject.GetComponent<Animator>().GetBool("walk")
      ,gameObject.GetComponent<Animator>().GetBool("isBombed")
      ,gameObject.GetComponent<Animator>().GetBool("coal")
      ,gameObject.GetComponent<Animator>().GetBool("metal")
      ,gameObject.GetComponent<Animator>().GetBool("water"));*/

        }
        public static void PlayerShoot(int _fromClient, Packet _packet){
            Vector3 _position = _packet.ReadVector3();
            Vector3 _rotation = _packet.ReadVector3();
            Console.WriteLine("shoot");
            Console.WriteLine(_position);
            Server.clients[_fromClient].bulletShoot(_position,_rotation);

        }//playerGun
        public static void playerGun(int _fromClient, Packet _packet){
            bool  hasgun = _packet.ReadBool();
            //Vector3 _rotation = _packet.ReadVector3();
            Console.WriteLine("take gun");
            //Console.WriteLine(_position);
            Server.clients[_fromClient].takegun(_fromClient,hasgun);

        }//playerGun

    }
}
