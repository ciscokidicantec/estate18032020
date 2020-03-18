using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;
using System.Net;
using System.Text;
using System.Drawing;
using System.Collections.Specialized;
using System.ComponentModel;

namespace estate18032020
{
    public partial class Initform : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnProcess_Click(object sender, EventArgs e)
        {
            try
            {
                Uri videoUri = new Uri(txtYouTubeURL.Text);
                string videoID = HttpUtility.ParseQueryString(videoUri.Query).Get("v");
                string videoInfoUrl = "http://www.youtube.com/get_video_info?video_id=" + videoID; //https://www.youtube.com/watch?v=-9b8NRqjUFM



                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(videoInfoUrl);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();



                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));



                string videoInfo = HttpUtility.HtmlDecode(reader.ReadToEnd());



                NameValueCollection videoParams = HttpUtility.ParseQueryString(videoInfo);



                if (videoParams["reason"] != null)
                {
                    lblMessage.Text = videoParams["reason"];
                    return;
                }



                string[] videoURLs = videoParams["url_encoded_fmt_stream_map"].Split(',');



                foreach (string vURL in videoURLs)
                {
                    string sURL = HttpUtility.HtmlDecode(vURL);



                    NameValueCollection urlParams = HttpUtility.ParseQueryString(sURL);
                    string videoFormat = HttpUtility.HtmlDecode(urlParams["type"]);



                    sURL = HttpUtility.HtmlDecode(urlParams["url"]);
                    sURL += "&signature=" + HttpUtility.HtmlDecode(urlParams["sig"]);
                    sURL += "&type=" + videoFormat;
                    sURL += "&title=" + HttpUtility.HtmlDecode(videoParams["title"]);



                    videoFormat = urlParams["quality"] + " - " + videoFormat.Split(';')[0].Split('/')[1];



                    ddlVideoFormats.Items.Add(new ListItem(videoFormat, sURL));
                }

                BtnProcess.Enabled = false;
                ddlVideoFormats.Visible = true;
                BtnDownload.Visible = true;
                lblMessage.Text = string.Empty;
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = Color.Red;
                return;
            }
        }

        protected void BtnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                string sURL = ddlVideoFormats.SelectedValue;



                if (string.IsNullOrEmpty(sURL))
                {
                    lblMessage.Text = "Unable to locate the Video.";
                    return;
                }



                NameValueCollection urlParams = HttpUtility.ParseQueryString(sURL);



                string videoTitle = urlParams["title"] + " " + ddlVideoFormats.SelectedItem.Text;
                string videoFormt = HttpUtility.HtmlDecode(urlParams["type"]);
                videoFormt = videoFormt.Split(';')[0].Split('/')[1];



                string downloadPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string sFilePath = string.Format(Path.Combine(downloadPath, "Downloads\\{0}.{1}"), videoTitle, videoFormt);

                WebClient webClient = new WebClient();
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                webClient.DownloadFileAsync(new Uri(sURL), sFilePath);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = Color.Red;
                return;
            }

        }

        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            lblMessage.Text = "Download completed!";
        }
    }
}
