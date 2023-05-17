using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Cache;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PintoNS.Forms
{
    public partial class ServerListForm : Form
    {
        public const string SERVERS_URL = "http://api.fieme.net:8880/pinto-server-list/servers.php";
        public event EventHandler<ServerUseEventArgs> ServerUse;

        public ServerListForm()
        {
            InitializeComponent();
        }

        private async void ServerListForm_Load(object sender, EventArgs e)
        {
            tcSections.Appearance = TabAppearance.FlatButtons;
            tcSections.ItemSize = new Size(0, 1);
            tcSections.SizeMode = TabSizeMode.Fixed;
            await LoadServers();
        }

        public async Task LoadServers() 
        {
            btnRefresh.Enabled = false;
            btnUse.Enabled = false;
            lError.Visible = false;
            lError.Text = "Error: null";
            tcSections.SelectedTab = tpLoading;
            dgvServers.Rows.Clear();

            try 
            {
                HttpClient httpClient = new HttpClient(new WebRequestHandler()
                {
                    CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore)
                });
                httpClient.DefaultRequestHeaders.Add("User-Agent", "PintoClient");

                string responseRaw = await httpClient.GetStringAsync(SERVERS_URL);
                JArray response = JsonConvert.DeserializeObject<JArray>(responseRaw);

                foreach (JObject server in response)
                {
                    string name = server["name"].Value<string>();
                    string ip = server["ip"].Value<string>();
                    int port = server["port"].Value<int>();
                    int users = server["users"].Value<int>();
                    int maxUsers = server["maxUsers"].Value<int>();
                    dgvServers.Rows.Add(name, ip, port, users, maxUsers);
                }
            }
            catch (Exception ex) 
            {
                lError.Visible = true;
                lError.Text = $"Error: {ex.Message}";
            }

            btnRefresh.Enabled = true;
            tcSections.SelectedTab = tpServers;
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadServers();
        }

        private void dgvServers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvServers.SelectedRows.Count > 0)
                btnUse.Enabled = true;
            else
                btnUse.Enabled = false;
        }

        private void btnUse_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dgvServers.SelectedRows[0];
            Close();
            if (ServerUse != null)
                ServerUse.Invoke(this, new ServerUseEventArgs(
                    (string)row.Cells["ip"].Value, (int)row.Cells["port"].Value));
        }
    }
    
    public class ServerUseEventArgs : EventArgs
    {
        public string IP { get; protected set; }
        public int Port { get; protected set; }

        public ServerUseEventArgs(string ip, int port)
        {
            IP = ip;
            Port = port;
        }
    }
}
