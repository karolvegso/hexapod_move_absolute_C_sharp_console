using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace hexapod_move_absolute_console
{
    class Program
    {
        static void Main(string[] args)
        {
            // this program move absolute all 6 axes: X, Y, Z. U, V, W
            // ask for IP address of hexapod
            Console.WriteLine("Insert IP address of hexapod: ");
            string server_hexapod = Console.ReadLine();
            // ask for port of hexapod
            Console.WriteLine("Insert port number of hexapod (e.g. 50000): ");
            string port_hexapod_str = Console.ReadLine();
            Int32 port_hexapod = Int32.Parse(port_hexapod_str);
            // create session with hexapod
            TcpClient session_hexapod = new TcpClient(server_hexapod, port_hexapod);
            NetworkStream stream_hexapod = session_hexapod.GetStream();
            // declare data as byte array in method and initialize it to null value
            Byte[] data = null;
            // ask if you want to continue to move absolute single axis
            Console.WriteLine("Do you want to move absolute (Y/N)?");
            string response_str = Console.ReadLine();
            while (response_str == "Y" || response_str == "y")
            {
                Console.WriteLine("Select axis; X/Y/Z/U/V/W: ");
                string axis = Console.ReadLine();
                Console.WriteLine("Set absolute movement value in mm: ");
                string mov_abs_value = Console.ReadLine();
                string cmd_mov_abs = "MOV " + axis + " " + mov_abs_value + "\n";
                data = System.Text.Encoding.ASCII.GetBytes(cmd_mov_abs);
                stream_hexapod.Write(data, 0, data.Length);
                Thread.Sleep(250);
                data = null;
                Console.WriteLine("Do you want to read position (Y/N)?");
                string response_pos_str = Console.ReadLine();
                while (response_pos_str == "Y" || response_pos_str == "y")
                {
                    // query for absolute position
                    string cmd_pos_qm = "POS?" + "\n";
                    data = System.Text.Encoding.ASCII.GetBytes(cmd_pos_qm);
                    stream_hexapod.Write(data, 0, data.Length);
                    data = null;
                    // read absolute position
                    data = new Byte[256];
                    string response_pos_qm = String.Empty;
                    Int32 bytes = stream_hexapod.Read(data, 0, data.Length);
                    response_pos_qm = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                    data = null;
                    Thread.Sleep(250);
                    Console.Write(response_pos_qm);
                    // ask again if you want to read position
                    Console.WriteLine("Do you want to read position (Y/N)?");
                    response_pos_str = Console.ReadLine();
                }
                // ask again if you want to move absolute
                Console.WriteLine("Do you want to move absolute (Y/N)?");
                response_str = Console.ReadLine();
            }
            // close stream and session, close all
            stream_hexapod.Flush();
            stream_hexapod.Close();
            session_hexapod.Close();
            // end of program
        }
    }
}
