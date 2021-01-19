using System;
using System.Collections.Generic;
using System.Text;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
//1/19 mosquitto那邊已經可以看到subscribe 但列印不出publish的東西 
namespace MQTTCMD
{
    public class Program
    {
        static List<string> TopicsHome = new List<string>();
        public static MqttClient client; 
        static string clientid;
        
               
      public static void Main(string[] args)
        {
            
           
                try
                {
                    client = new MqttClient("localhost");

                    clientid = Guid.NewGuid().ToString();
                    client.Connect(clientid);
                }
                catch (SystemException)
                {
                    Console.WriteLine("Connection Failed");
                }
            client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
           
            client.ConnectionClosed += close;
            Console.WriteLine("Please write a topic you want to subscribe.");
            TopicsHome.Add( Console.ReadLine());
            client.Subscribe(new string[] { TopicsHome[0] }, new byte[] { 0 });


        }
            public static void subscribe(object sender, MqttMsgSubscribedEventArgs e)
        {
             if (Console.ReadLine() != "")
            {
           
            client.Subscribe(new string[] { TopicsHome[0] }, new byte[] { 0 });
            }
            else
            {
               Console.WriteLine("請輸入要訂閱的主題!!");
            }
        }
           static void  mqttconnecting()
            {
            try
            {
                client = new MqttClient("localhost");
               
                clientid = Guid.NewGuid().ToString();
                client.Connect(clientid);           
            }
            catch (SystemException)
            {
                Console.WriteLine("Connection Failed");
            }
        }
       static void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            String ReceiveMsg = Encoding.UTF8.GetString(e.Message);
            setText(ReceiveMsg);
        }
      static void close(object sender,EventArgs e)
        { 
            client.Disconnect();
        }
        public static void setText(string text)
        {
            while (true)
            {
                Console.WriteLine(text);
            }
        }
    }
}
