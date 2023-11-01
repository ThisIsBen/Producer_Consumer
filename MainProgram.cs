using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Producer_Consumer
{
    class MainProgram
    {

        public static string destFolder = @"..\..\Channel_string画像移動先";
        static string oriFolder = @"C:\Users\MM15697\source\repos\ImgCopier\ImgCopier\bin\画像監視先\監視フォルダ1";

        static void Main(string[] args)
        {


            #region Channelの中身はstring（画像名）
            //Channelの宣言
            ChannelProvider channelObj;
            //FileWatcherの宣言
            FileWatcher filewatcherObj;



            //Create channel object
            channelObj = new ChannelProvider();
            //Start N consumers
            int consumerCount = 2;
            channelObj.startConsumers(destFolder,consumerCount);
            //Start 1 producer
            ChannelProvider.Producer producer = new ChannelProvider.Producer(channelObj.getWriter(), "監視フォルダ1");
            filewatcherObj = new FileWatcher(producer, oriFolder);
            filewatcherObj.start();
            #endregion





            //#region Channelの中身はBitmap
            ////Channelの宣言
            //ChannelProvider_Img channelObj_Img;
            ////FileWatcherの宣言
            //FileWatcher_Img filewatcherObj_Img;



            ////Create channel object
            //channelObj_Img = new ChannelProvider_Img();
            ////Start 2 consumers
            //string destFolder_Img = @"..\..\Channel_Bitmap画像移動先";
            //channelObj_Img.start2Consumers(destFolder_Img);
            ////Start 1 producer
            //string oriFolder_Img = @"..\..\画像監視先\監視フォルダ1";
            //ChannelProvider_Img.Producer producer_Img = new ChannelProvider_Img.Producer(channelObj_Img.getWriter(), "監視フォルダ1");
            //filewatcherObj_Img = new FileWatcher_Img(producer_Img, oriFolder_Img);
            //filewatcherObj_Img.start();
            //#endregion


            //ChannelはObjectに対してpointerだけを保存することを確認できた
            #region Channelの中身はObject
            ////Channelの宣言
            //ChannelProvider_Obj channelObj_Obj;
            ////FileWatcherの宣言
            //FileWatcher_Obj filewatcherObj_Obj;



            ////Create channel object
            //channelObj_Obj = new ChannelProvider_Obj();
            ////Start 2 consumers
            //string destFolder_Obj = @"..\..\Channel_Bitmap画像移動先";
            //channelObj_Obj.start2Consumers(destFolder_Obj);
            ////Start 1 producer
            //string oriFolder_Obj = @"..\..\画像監視先\監視フォルダ1";
            //ChannelProvider_Obj.Producer producer_Obj = new ChannelProvider_Obj.Producer(channelObj_Obj.getWriter(), "監視フォルダ1");
            //filewatcherObj_Obj = new FileWatcher_Obj(producer_Obj, oriFolder_Obj);
            //filewatcherObj_Obj.start();
            #endregion


            Console.Read();
        }
    }
}
