﻿using NamedPipeWrapper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WindowsFirewallCore.IPCommunication.Event
{
	public class Client
	{
		private NamedPipeClient<CommunicationObject> pipeClient;
		private int processID;

		private bool _messageRecievedByServer;

		public Client()
		{
			processID = Process.GetCurrentProcess().Id;

			pipeClient = new NamedPipeClient<CommunicationObject>(Server.CommunicationPipeName);
			pipeClient.Disconnected += delegate (NamedPipeConnection<CommunicationObject, CommunicationObject> connection)
			{
				Console.WriteLine("Client disconnected.");
			};
			pipeClient.Error += delegate (Exception ex)
			{
				Console.WriteLine("Exception was raised");
				Console.WriteLine(ex.Message);
			};
			pipeClient.ServerMessage += delegate (NamedPipeConnection<CommunicationObject, CommunicationObject> connection, CommunicationObject message)
			{
				Console.WriteLine($"Server says: {message.Message}");

				_messageRecievedByServer |= message.Status == CommunicationObject.CommunicationStatus.MessageRecieved;
			};
		}

		public List<string> OpenPipes()
		{
			return System.IO.Directory.GetFiles(@"\\.\pipe\").ToList();
		}

		public bool IsServerOpen()
		{
			return OpenPipes().Contains(Server.CommunicationPipeName);
		}

		public void Start()
		{
			pipeClient.Start();
			pipeClient.WaitForConnection(100);
		}

		public void Stop()
		{
			// waiting 2sec before closing
			var iteration = 0;
			while(!_messageRecievedByServer && iteration++ < 20)
			{
				Thread.Sleep(10);
			}
			pipeClient.Stop();
		}

		public void Send(CommunicationObject message)
		{
			message.Status = CommunicationObject.CommunicationStatus.None;
			pipeClient.PushMessage(message);
		}
	}
}
