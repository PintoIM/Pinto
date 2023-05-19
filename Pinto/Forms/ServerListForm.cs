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
            dgvServersOfficial.Rows.Clear();
            dgvServersUnofficial.Rows.Clear();

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
                    string tags = server["tags"].Value<string>();

                    if (!tags.Split(',').Contains("official")) continue;
                    dgvServersOfficial.Rows.Add(name, ip, port, users, maxUsers, tags);
                }

                foreach (JObject server in response)
                {
                    string name = server["name"].Value<string>();
                    string ip = server["ip"].Value<string>();
                    int port = server["port"].Value<int>();
                    int users = server["users"].Value<int>();
                    int maxUsers = server["maxUsers"].Value<int>();
                    string tags = server["tags"].Value<string>();

                    if (tags.Split(',').Contains("official")) continue;
                    dgvServersUnofficial.Rows.Add(name, ip, port, users, maxUsers, tags);
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
            if (dgvServersOfficial.SelectedRows.Count > 0)
                btnUse.Enabled = true;
            else
                btnUse.Enabled = false;
        }

        private void dgvServersUnofficial_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvServersUnofficial.SelectedRows.Count > 0)
                btnUse.Enabled = true;
            else
                btnUse.Enabled = false;
        }

        private void btnUse_Click(object sender, EventArgs e)
        {
            string ip = "";
            int port = 0;

            if (tcServers.SelectedTab == tpServersOfficial) 
            {
                ip = (string)dgvServersOfficial.SelectedRows[0].Cells["ip"].Value;
                port = (int)dgvServersOfficial.SelectedRows[0].Cells["port"].Value;
            }
            else if (tcServers.SelectedTab == tpServersUnofficial)
            {
                ip = (string)dgvServersUnofficial.SelectedRows[0].Cells["ip2"].Value;
                port = (int)dgvServersUnofficial.SelectedRows[0].Cells["port2"].Value;
            }

            Close();
            if (ServerUse != null)
                ServerUse.Invoke(this, new ServerUseEventArgs(ip, port));
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
