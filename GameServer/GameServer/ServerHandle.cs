﻿using System;
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
             
             Server.clients[_fromClient].player.SetFace(face);
             Server.clients[_fromClient].player.SetWalk(walk);
             Server.clients[_fromClient].player.SetPOS(_position);
             //Console.WriteLine("getposition");

        }
        public static void PlayerShoot(int _fromClient, Packet _packet){
            Vector3 _position = _packet.ReadVector3();
            Vector3 _rotation = _packet.ReadVector3();
            Console.WriteLine("shoot");
            Console.WriteLine(_position);
            Server.clients[_fromClient].bulletShoot(_position,_rotation);

        }
    }
}
