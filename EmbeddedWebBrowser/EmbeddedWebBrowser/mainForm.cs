using CefSharp.WinForms;
using CefSharp;
using NAudio.Wave;
using System.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmbeddedWebBrowser
{
    public partial class mainForm : Form
    {
        ChromiumWebBrowser m_chromeBrowser = null;

        JsAudioRecording m_jsInteractionObj = null;

        public static string GetAppLocation()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }


        public mainForm()
        {
            InitializeComponent();
            //maximize box
            WindowState = FormWindowState.Maximized;
            this.MaximizeBox = false;
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            m_chromeBrowser = new ChromiumWebBrowser(string.Format("file:///{0}HTMLResources/html/BootstrapExample.html", GetAppLocation()));
            pnlMain.Controls.Add(m_chromeBrowser);

            m_jsInteractionObj = new JsAudioRecording();
            m_jsInteractionObj.SetChromeBrowser(m_chromeBrowser);

            // Register the JavaScriptInteractionObj class with JS
            m_chromeBrowser.RegisterJsObject("winformObj", m_jsInteractionObj);

            //m_chromeBrowser.ShowDevTools();
        }
    }
}
