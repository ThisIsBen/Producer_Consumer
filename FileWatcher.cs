using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Producer_Consumer
{
    class FileWatcher
    {

        private FileSystemWatcher watcher;

        //ChannelProviderのデータ追加機能を利用するために、宣言する
        private readonly ChannelProvider.Producer producerObj;

        public FileWatcher(ChannelProvider.Producer _producerObj, string folderPath)
        {
            watcher = new FileSystemWatcher(folderPath);

            watcher.Created += OnCreated;
        
            watcher.Filter = "*.*";
            watcher.IncludeSubdirectories = true;

            //Consumerが起動されたChannelProviderのobjectを取得
            producerObj = _producerObj;


        }




        public void start()
        {
            watcher.EnableRaisingEvents = true;
        }

        
        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            ////Process the file by Consumer via Channel
            ////Write to the channel
            //producerObj.writeData(e.FullPath);


            //Process the file by FileSystemWatcher Threadpool thread
            string oriPath = e.FullPath;
            FileIO.waitUntilFileIsReady(oriPath);
            File.Move(oriPath, $"{MainProgram.destFolder}/{Path.GetFileName(oriPath)}");
            //Console.WriteLine($"CONSUMER ({_identifier}) ThreadID ({Thread.CurrentThread.ManagedThreadId}): Processed {Path.GetFileName(oriPath)} from the channel.");
            Console.WriteLine($"ViDi AI処理 : Processed {Path.GetFileName(oriPath)} by FileSystemWatcher Threadpool thread.");

        }

    }
}
