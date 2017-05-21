using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace YoutubeExtractor
{
    /// <summary>
    /// Provides a method to download a video from YouTube.
    /// </summary>
    public class VideoDownloader : Downloader
    {
       public VideoDownloader(VideoInfo video, string savePath, int? bytesToDownload = null)
            : base(video, savePath, bytesToDownload)
        { }

       public override void Execute()
        {
            //this.OnDownloadStarted(EventArgs.Empty);
            var request = (HttpWebRequest)WebRequest.Create(this.Video.DownloadUrl);
            
            // the following code is alternative, you may implement the function after your needs
            using (WebResponse response = request.GetResponse())
            {
                var Engine = new OctaneEngine();

                string[] s = this.Video.Title.Split(' ');
                string fileout = "";
                foreach(string sr in s)
                {
                    fileout += sr;
                }
                string my_String = Regex.Replace(fileout, @"[^0-9a-zA-Z]+", ",");

                Console.WriteLine(my_String);
                Engine.SplitDownloadArray(response.ResponseUri.ToString(), 4, (x) => {
                    File.WriteAllBytes(my_String + ".mp4", x);
                    Console.WriteLine("Done!");
                });
                Console.ReadLine();
            }
        }
    }
}