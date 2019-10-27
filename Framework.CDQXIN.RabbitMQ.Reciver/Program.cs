using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.CDQXIN.RabbitMQ.Reciver
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
            //ConnectionFactory factory = new ConnectionFactory { HostName = "IMMQ", UserName = "cdqxin", Password = "cdqxin", VirtualHost = "IMMQ" };
            Console.WriteLine("开始了");
            using (IConnection conn = factory.CreateConnection())
            {
                using (IModel im = conn.CreateModel())
                {
                    while (true)
                    {
                        BasicGetResult res = im.BasicGet("rabbitmq_query", true);
                        if (res != null)
                        {
                            Console.WriteLine("receiver:" + UTF8Encoding.UTF8.GetString(res.Body));
                        }
                    }
                }
                _ = Console.ReadLine();
            }

        }
    }
}
