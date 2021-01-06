using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace GameServer
{
    class ServerSend
    {
        /// <summary>Sends a packet to a client via TCP.</summary>
        /// <param name="_toClient">The client to send the packet the packet to.</param>
        /// <param name="_packet">The packet to send to the client.</param>
        private static void SendTCPData(int _toClient, Packet _packet)
        {
            _packet.WriteLength();
            Server.clients[_toClient].tcp.SendData(_packet);
        }

        /// <summary>Sends a packet to a client via UDP.</summary>
        /// <param name="_toClient">The client to send the packet the packet to.</param>
        /// <param name="_packet">The packet to send to the client.</param>
        private static void SendUDPData(int _toClient, Packet _packet)
        {
            _packet.WriteLength();
            Server.clients[_toClient].udp.SendData(_packet);
        }

        /// <summary>Sends a packet to all clients via TCP.</summary>
        /// <param name="_packet">The packet to send.</param>
        private static void SendTCPDataToAll(Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                Server.clients[i].tcp.SendData(_packet);
            }
        }
        /// <summary>Sends a packet to all clients except one via TCP.</summary>
        /// <param name="_exceptClient">The client to NOT send the data to.</param>
        /// <param name="_packet">The packet to send.</param>
        private static void SendTCPDataToAll(int _exceptClient, Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                if (i != _exceptClient)
                {
                    Server.clients[i].tcp.SendData(_packet);
                }
            }
        }

        /// <summary>Sends a packet to all clients via UDP.</summary>
        /// <param name="_packet">The packet to send.</param>
        private static void SendUDPDataToAll(Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                Server.clients[i].udp.SendData(_packet);
            }
        }
        /// <summary>Sends a packet to all clients except one via UDP.</summary>
        /// <param name="_exceptClient">The client to NOT send the data to.</param>
        /// <param name="_packet">The packet to send.</param>
        private static void SendUDPDataToAll(int _exceptClient, Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                if (i != _exceptClient)
                {
                    Server.clients[i].udp.SendData(_packet);
                }
            }
        }

        #region Packets
        /// <summary>Sends a welcome message to the given client.</summary>
        /// <param name="_toClient">The client to send the packet to.</param>
        /// <param name="_msg">The message to send.</param>
        public static void Welcome(int _toClient, string _msg)
        {
            int type=0;
            //Server.clients2=null;
            if(Server.teamCnum>Server.teamInum){
                type=1;
                Server.teamInum+=1;
            }else{
                type=2;
                Server.teamCnum+=1;
            }
            using (Packet _packet = new Packet((int)ServerPackets.welcome))
            {
                
                _packet.Write(_msg);
                _packet.Write(_toClient);
                

                _packet.Write(type);

                SendTCPData(_toClient, _packet);
            }
        }

        /// <summary>Tells a client to spawn a player.</summary>
        /// <param name="_toClient">The client that should spawn the player.</param>
        /// <param name="_player">The player to spawn.</param>
        public static void SpawnPlayer(int _toClient, Player _player)
        {
            using (Packet _packet = new Packet((int)ServerPackets.spawnPlayer))
            {
                _packet.Write(_player.id);
                _packet.Write(_player.username);
                _packet.Write(_player.position);
                _packet.Write(_player.rotation);

                SendTCPData(_toClient, _packet);
            }
        }
        public static void SpawnProjectile(int _toClient, Vector3 pos,Vector3 rot){
            using (Packet _packet = new Packet((int)ServerPackets.spawnProjectile))
            {
                _packet.Write(pos);
                _packet.Write(rot);
                //_packet.Write(_player.position);
               // _packet.Write(_player.rotation);
                Console.WriteLine("send shoot");
                SendTCPData(_toClient, _packet);
                
            }
        }
        public static void SpawnGun(int _toClient, bool hasgun,int from){
            using (Packet _packet = new Packet((int)ServerPackets.playerGun))
            {
                _packet.Write(from);
                _packet.Write(hasgun);
               // _packet.Write(rot);
                //_packet.Write(_player.position);
               // _packet.Write(_player.rotation);
                Console.WriteLine("send gun");
                SendTCPData(_toClient, _packet);
                
            }
        }

        /// <summary>Sends a player's updated position to all clients.</summary>
        /// <param name="_player">The player whose position to update.</param>
        public static void PlayerPosition(Player _player)
        {
            using (Packet _packet = new Packet((int)ServerPackets.playerPosition))
            {
                _packet.Write(_player.id);
                _packet.Write(_player.position);
                _packet.Write(_player.facing);
                _packet.Write(_player.walk);
                _packet.Write(_player.isbombed);
                _packet.Write(_player.coal);
                _packet.Write(_player.metal);
                _packet.Write(_player.water);
                _packet.Write(_player.develop);
                _packet.Write(_player.weaponrot);
                /*
                public bool isbombed;
        public bool coal;
        public bool metal ;
        public bool water ;*/


                SendUDPDataToAll(_packet);
            }
        }

        /// <summary>Sends a player's updated rotation to all clients except to himself (to avoid overwriting the local player's rotation).</summary>
        /// <param name="_player">The player whose rotation to update.</param>
        public static void PlayerRotation(Player _player)
        {
            using (Packet _packet = new Packet((int)ServerPackets.playerRotation))
            {
                _packet.Write(_player.id);
                _packet.Write(_player.rotation);

                SendUDPDataToAll(_player.id, _packet);
            }
        }
        #endregion
    }
}
