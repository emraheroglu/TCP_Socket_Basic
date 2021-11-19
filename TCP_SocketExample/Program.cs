using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TCP_SocketExample
{
    class Program
    {
        const int ServerPortNum = 50252;
        public static void Main(string[] args)
        {
            Console.WriteLine("Enter 's' for server, 'c' for client.");
            string input = Console.ReadLine();
            if (input.Equals('c'))
            {
                Client();
            }
            else if (input.Equals('s'))
            {
                Server();
            }
            else
            {
                Console.WriteLine("Unexpected input..");
            }
        }
        private static void Client()
        {
            //Create a Socket
            IPEndPoint clientEndPoint = new System.Net.IPEndPoint(System.Net.IPAddress.Loopback, 0);
            Socket clientSocket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            clientSocket.Bind(clientEndPoint);

            //Create a connection
            IPEndPoint serverEndPoint = new System.Net.IPEndPoint(System.Net.IPAddress.Loopback, ServerPortNum);
            clientSocket.Connect(serverEndPoint);

            //Send message
            string messageToSend = "Helloe";
            byte[] bytesToSend = Encoding.Default.GetBytes(messageToSend);
            clientSocket.Send(bytesToSend);
            Console.WriteLine("Client sent message.. " + messageToSend);

            //Display received message
            byte[] buffer = new byte[1024];
            int numberOfBytesReceived = clientSocket.Receive(buffer);
            byte[] receivedBytes = new byte[numberOfBytesReceived];
            Array.Copy(buffer, receivedBytes, numberOfBytesReceived);
            string receivedMessage = Encoding.Default.GetString(receivedBytes);
            Console.WriteLine("Client received.. " + receivedMessage);

        }

        private static void Server()
        {
            
            //Create a Socket
            IPEndPoint serverEndPoint = new System.Net.IPEndPoint(System.Net.IPAddress.Loopback, ServerPortNum);
            Socket welcomingSocket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            welcomingSocket.Bind(serverEndPoint);

            //Wait for connection
            welcomingSocket.Listen(ServerPortNum);
            Socket connectionSocket = welcomingSocket.Accept();

            //Display received message
            byte[] buffer = new byte[1024];
            int numberOfBytesReceived = connectionSocket.Receive(buffer);
            byte[] receivedBytes = new byte[numberOfBytesReceived];
            Array.Copy(buffer, receivedBytes, numberOfBytesReceived);
            string receivedMessage = Encoding.Default.GetString(receivedBytes);
            Console.WriteLine("Server received.. " + receivedMessage);

            //Send received message to the client
            connectionSocket.Send(receivedBytes);
            Console.ReadLine();
        }

    }
}
