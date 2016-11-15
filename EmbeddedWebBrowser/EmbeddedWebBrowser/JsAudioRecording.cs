using CefSharp;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CefSharp.WinForms;

namespace EmbeddedWebBrowser
{
    public class JsAudioRecording
    {
        public JsAudioRecording()
        {
            obj = Guid.NewGuid();
        }

        [JavascriptIgnore]
        public ChromiumWebBrowser m_chromeBrowser { get; set; }

        [JavascriptIgnore]
        public WaveInEvent waveSource { get; set; }

        [JavascriptIgnore]
        public WaveFileWriter waveFile { get; set; }

        [JavascriptIgnore]
        public Guid obj { get; set; }

        [JavascriptIgnore]
        public void SetChromeBrowser(ChromiumWebBrowser b)
        {
            m_chromeBrowser = b;
        }

        public void StartRecording()
        {
            string path = @"C:\VoiceScripts";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string newPath = path + @"\" + obj.ToString();
            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }

            waveSource = new WaveInEvent();
            waveSource.WaveFormat = new WaveFormat(44100, 1);

            waveSource.DataAvailable += new EventHandler<WaveInEventArgs>(waveSource_DataAvailable);
            waveSource.RecordingStopped += new EventHandler<StoppedEventArgs>(waveSource_RecordingStopped);

            waveFile = new WaveFileWriter(newPath + @"\Test0001.wav", waveSource.WaveFormat);

            waveSource.StartRecording();

        }

        public void StopRecording()
        {
            waveSource.StopRecording();
        }

        [JavascriptIgnore]
        void waveSource_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (waveFile != null)
            {
                waveFile.Write(e.Buffer, 0, e.BytesRecorded);
                waveFile.Flush();
            }
        }

        [JavascriptIgnore]
        void waveSource_RecordingStopped(object sender, StoppedEventArgs e)
        {
            if (waveSource != null)
            {
                waveSource.Dispose();
                waveSource = null;
            }

            if (waveFile != null)
            {
                waveFile.Dispose();
                waveFile = null;
            }

        }

    }
}
