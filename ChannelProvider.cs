using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Channels;
using System.IO;
using System.Threading;


namespace Producer_Consumer
{
    class ChannelProvider
    {
        //任意の時間にchannelに書き込むproducerは1つしかないことを設定し、
        //同時書き込みの制御を利用しないことで、負荷を減らす。
        private  Channel<string> channel = Channel.CreateUnbounded<string>
            (
                new UnboundedChannelOptions
                {
                    SingleWriter = true
                }
             );

       


        //channelのwriterを返す
        public ChannelWriter<string> getWriter()
        {
            return channel.Writer;
        }

        //consumerのobject宣言
        private List<Consumer> consumers;
        private List<Task> consumerTasks;

        //2つのconsumerを起動する。
        public void startConsumers(string destFolder, int consumerCount)
        {
            Console.WriteLine("Channelの中身はstring");
            consumers = new List<Consumer>(consumerCount);
            consumerTasks = new List<Task>(consumerCount);
            for (int i = 0; i < consumerCount; i++)
            {
                consumers.Add(new Consumer(channel.Reader, i.ToString()));//Create a consumer

                //For Console program:
                //This works in Console program, but freezes UI in a UI program.
                //consumerTasks.Add(consumers[i].ConsumeData(destFolder));//Begin consuming content from the Channel

                //For UI program(Windows Form, WPF):
                int index = i;
                Task copyTask = Task.Run(() => consumers[index].ConsumeData(destFolder));
                //Why using Task.Run() here can avoid freezing UI 
                //Reference: https://stackoverflow.com/questions/69565851/using-await-task-run-somemethodasync-vs-await-somemethodasync-in-a-ui
                consumerTasks.Add(copyTask);
            }
        }



        internal class Consumer
        {
            private readonly ChannelReader<string> _reader;
            
            private readonly string _identifier;
            //private readonly int _delay;

            public Consumer(ChannelReader<string> reader, string identifier)
            {
                _reader = reader;
                _identifier = identifier;
                
            }

            public async Task ConsumeData(string destFolder)
            {
                //Console.WriteLine($"CONSUMER ({_identifier}): Starting");
                Console.WriteLine($"ViDi AI処理 ({_identifier}): Starting");
                //Console.WriteLine($"Before WaitToReadAsync CONSUMER ({_identifier}) ThreadID ({Thread.CurrentThread.ManagedThreadId})");
                //Console.WriteLine("   Background: {0}\n", Thread.CurrentThread.IsBackground);
                //Console.WriteLine("   Thread Pool: {0}\n", Thread.CurrentThread.IsThreadPoolThread);
                Directory.CreateDirectory(destFolder);
                while (await _reader.WaitToReadAsync()) //ここでChannel内に内容が追加されるのを待っている
                {
                    string oriPath= "";
                    
                    //Consume the data in the queue
                    if (_reader.TryRead(out oriPath))
                    {

                        //Process the data received from the queue---------------
                        try
                        {
                            FileIO.waitUntilFileIsReady(oriPath);
                            File.Move(oriPath, $"{destFolder}/{Path.GetFileName(oriPath)}");
                            //Console.WriteLine($"CONSUMER ({_identifier}) ThreadID ({Thread.CurrentThread.ManagedThreadId}): Processed {Path.GetFileName(oriPath)} from the channel.");
                            Console.WriteLine($"ViDi AI処理 ({_identifier}): Processed {Path.GetFileName(oriPath)} from the channel.");
                            //Console.WriteLine("   Background: {0}\n", Thread.CurrentThread.IsBackground);
                            //Console.WriteLine("   Thread Pool: {0}\n", Thread.CurrentThread.IsThreadPoolThread);
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine(e.ToString());
                        }

                        //Process the data received from the queue---------------
                    }
                }

                //Channelがcompleteとマークされたら、WaitToReadAsyncはfalseを返し、ここにくる。
                Console.WriteLine($"CONSUMER ({_identifier}): Completed");
                
            }
        }




        internal class Producer
        {
            private readonly ChannelWriter<string> writer;
            private readonly string identifier;
            

            public Producer(ChannelWriter<string> _writer, string _identifier)
            {
                writer = _writer;
                identifier = _identifier;
                
            }

            public void writeData(string data)
            {
                //Synchronously write to the channel
                if (writer.TryWrite(data))
                {


                    //string msg = $"PRODUCER ({identifier}) ThreadID ({Thread.CurrentThread.ManagedThreadId}): Wrote {Path.GetFileName(data)} to the channel";
                    string msg = $"カメラ ({identifier}): Wrote {Path.GetFileName(data)} to the channel";

                    Console.WriteLine(msg);
                }
            }


            public async void writeDataAsync(string data)
            {
                await writer.WriteAsync(data);


                //string msg = $"PRODUCER ({identifier}) ThreadID ({Thread.CurrentThread.ManagedThreadId}): Wrote {Path.GetFileName(data)} to the channel";
                string msg = $"カメラ ({identifier}): Wrote {Path.GetFileName(data)} to the channel";

                Console.WriteLine(msg);
            }
        }

    }
}
