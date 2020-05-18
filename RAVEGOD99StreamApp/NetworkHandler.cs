using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace StreamApp
{
    class NetworkHandler
    {

        private UdpClient client;

        public NetworkHandler()
        {
            client = new UdpClient(Dashboard.WorkingProfile.NetworkProfile.HOST_IP,Dashboard.WorkingProfile.NetworkProfile.HOST_PORT);
        }

        public int TransmitData(byte[] data)
        {
            byte[] payload;
            int len;

            if (data[0] != Dashboard.WorkingProfile.NetworkProfile._KEY_)
            {
                Console.WriteLine("No key");
                len = data.Length + 1; //new length of data
                payload = new byte[len]; //init payload
                payload[0] = Dashboard.WorkingProfile.NetworkProfile._KEY_; //add key to start

                for (int i = 1; i < len; ++i) payload[i] = data[i - 1];
            } else
            {
                len = data.Length;
                payload = data;
            }    

            return client.Send(payload, len);
        }

        public string PacketAsString(byte[] packet)
        {
            string result = "";

            for(int i = 0; i < packet.Length; ++ i)
            {
                result += "[";
                result += packet[i];
                result += "]";
            }

            return result;
        }

    }
}
