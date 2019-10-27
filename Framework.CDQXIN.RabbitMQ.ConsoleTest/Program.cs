using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Framework.CDQXIN.RabbitMQ.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            factory.UserName = "cdqxin"; //用户名，对应Management工具的admin-->user
            factory.Password = "cdqxin"; //密码，对应Management工具的admin-->密码
            factory.HostName = "127.0.0.1"; //本地部署服务直接用hostname即可
            factory.Port = AmqpTcpEndpoint.UseDefaultPort;
            factory.VirtualHost = "/"; //使用默认值： "/"
            factory.Protocol = Protocols.DefaultProtocol;
            //ConnectionFactory factory = new ConnectionFactory { HostName = "IMMQ", UserName = "cdqxin", Password = "cdqxin",  };
            using (IConnection conn = factory.CreateConnection())
            {
                using (IModel im = conn.CreateModel())
                {
                    im.ExchangeDeclare("rabbitmq_route", ExchangeType.Direct);
                    im.QueueDeclare("rabbitmq_query", false, false, false, null);
                    im.QueueBind("rabbitmq_query", "rabbitmq_route", ExchangeType.Direct, null);
                    for (int i = 0; i < 100000000; i++)
                    {
                        byte[] message = Encoding.UTF8.GetBytes("Hello Lv"+i.ToString());
                        im.BasicPublish("rabbitmq_route", ExchangeType.Direct, null, message);
                        Console.WriteLine("send:" + i);
                    }
                }
            }
            Console.ReadLine();
        }

        
    }
}
