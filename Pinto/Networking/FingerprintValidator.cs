using Mono.CSharp;
using Newtonsoft.Json;
using PintoNS.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using static PintoNS.Forms.FingerprintVerifierForm;

namespace PintoNS.Networking
{
    internal class FingerprintValidator
    {
        private static readonly string StorePath = Path.Combine(Program.DataFolder, "fingerprint_store.json");

        private struct StoreEntry 
        {
            public string Server;
            public string Fingerprint;
        }

        private static void WriteStore(List<StoreEntry> store) 
        {
            try
            {
                File.WriteAllText(StorePath, JsonConvert.SerializeObject(store));
            }
            catch (Exception ex)
            {
                Program.Console.WriteMessage($"[Networking] Failed to save the fingerprint store: {ex}");
            }
        }

        private static List<StoreEntry> ReadStore() 
        {
            List<StoreEntry> store = new List<StoreEntry>();

            if (!File.Exists(StorePath)) 
                WriteStore(store);
            else 
            {
                try
                {
                    string storeData = File.ReadAllText(StorePath);
                    store = JsonConvert.DeserializeObject<List<StoreEntry>>(storeData);
                }
                catch (Exception ex)
                {
                    Program.Console.WriteMessage($"[Networking] Failed to read the fingerprint store: {ex}");
                }
            }

            return store;
        }

        private static StoreEntry GetStoreEntry(string server, out bool failed) 
        {
            List<StoreEntry> store = ReadStore();
            
            foreach (StoreEntry entry in store) 
            {
                if (entry.Server == server) 
                {
                    failed = false;
                    return entry;
                }
            }

            failed = true;
            return new StoreEntry();
        }

        private static bool CheckServer(string server, string fingerprint, out bool failed) 
        {
            StoreEntry entry = GetStoreEntry(server, out failed);
            return entry.Fingerprint == fingerprint;
        }

        private static void StoreServer(string server, string fingerprint) 
        {
            StoreEntry entry = new StoreEntry();
            entry.Server = server;
            entry.Fingerprint = fingerprint;
            
            List<StoreEntry> store = ReadStore();
            store.Add(entry);
            WriteStore(store);
        }

        private static void RemoveServer(string server) 
        {
            List<StoreEntry> store = ReadStore();
            store.Remove(GetStoreEntry(server, out _));
            WriteStore(store);
        }

        public static bool Validate(byte[] rsa, NetClientHandler netHandler) 
        {
            string server = netHandler.NetManager.GetAddress().ToString();
            string fingerprint = Utils.GetSHA1Hash(rsa);
            Program.Console.WriteMessage($"[Networking] Server fingerprint: {fingerprint}");

            bool checkResult = CheckServer(server, fingerprint, out bool checkFailed);
            if (checkResult) 
                return true;

            bool mismatched = !checkFailed && !checkResult;
            FingerprintVerifierForm form = new FingerprintVerifierForm(server, fingerprint, mismatched);
            bool stallMagicException = false;
            VerifierResult result = VerifierResult.NONE;
            form.Callback += (VerifierResult r) => result = r;

            Thread thread = new Thread(new ThreadStart(() => 
            {
                form.ShowDialog();
            }));
            thread.Start();

            while (result == VerifierResult.NONE && !netHandler.ConnectionClosed) 
            {
                try 
                {
                    netHandler.NetManager.GetOutputStream().WriteBEInt(0x7FFFFFFF);
                    Thread.Sleep(100);
                }
                catch
                {
                    stallMagicException = true;
                    break;
                }
            }

            if (stallMagicException || netHandler.ConnectionClosed) 
            {
                form.Invoke(new Action(() => 
                {
                    form.Close();
                    form.Dispose();
                }));
                thread.Abort();
                throw new PintoConnectionException("Lost connection to the server whilst stalling");
            }

            switch (result) 
            {
                case VerifierResult.ACCEPT:
                    if (mismatched)
                        RemoveServer(server);
                    StoreServer(server, fingerprint);
                    return true;
                case VerifierResult.ONLY_ONCE:
                    return true;
                case VerifierResult.DISCONNECT:
                    return false;
                default:
                    // Something must've fucked up real bad for this to trigger
                    Program.Console.WriteMessage("SHIT HIT THE FAN !!!");
                    return false;
            }
        }
    }
}
