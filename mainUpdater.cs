using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MagiX.AutoUpdate
{
    /// <summary>
    /// Settings for Auto Update
    /// </summary>
    public class Settings
    {

    }

    /// <summary>
    /// Main Update Class
    /// </summary>
    public class Updater
    {
        public class UpdateFiles
        {
            public List<UpdateFile> files = new List<UpdateFile>();

            public class UpdateFile
            {
                public string Name { get; set; }
                public string Extension { get; set; }
                public string Path { get; set; }
                public string DownloadURL { get; set; }
            }
        }

        private string _url { get; set; }

        public void SetURL(string url) { _url = url; }

        /// <summary>
        /// installs the update
        /// </summary>
        /// <param name="force"></param>
        public void InstallUpdate(bool? force = false)
        {

        }

        /// <summary>
        /// Checks the URL if an update is available
        /// </summary>
        public async Task<bool> GetIsUpdateAvailable()
        {
            bool isAv = false;
            using (WebClient wc = new WebClient())
            {
                try
                {
                    string result = await wc.DownloadStringTaskAsync(_url);
                    isAv = !string.IsNullOrWhiteSpace(result);
                    //frm.Load += (g, s) => {
                    //    Console.WriteLine("ez");
                    //    testUpdateForm(frm);
                    //};
                }
                catch
                {
                    System.Diagnostics.Debug.WriteLine("[Auto Updater] >> URL may be wrong or not accessible!");
                    isAv = false;
                }
            }
            return isAv;
        }


        private void testUpdateForm(Form frm)
        {
            Thread th = new Thread(() =>
            {
               for (int i = frm.PB_Progress.Value; i < frm.PB_Progress.Maximum; i++)
                {
                    Console.WriteLine("test");
                    Thread.Sleep(1000);
                    frm.PB_Progress.Value = i;
                }
            });

            th.Start();
        }

        private async Task<HttpStatusCode> GetWebsiteStatus()
        {
            HttpStatusCode code;

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(_url);
            HttpWebResponse response =  await req.GetResponseAsync() as HttpWebResponse;
            code = response.StatusCode;
            response.Close();
            return code;
        }
    }
}
