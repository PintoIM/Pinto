//css_reference Pinto.exe
//css_reference Newtonsoft.Json.dll
using System;
using System.IO;
using System.Windows.Forms;
using PintoNS;
using PintoNS.General;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

/*
	Template Pinto! script file - Using settings
*/
public class PintoScript : IPintoScript
{
	// TODO: Change this to the file name you want
	private const string SETTINGS_FILE_NAME = "TemplateScriptUsingSettings.json";
	private static readonly string SETTINGS_FILE_PATH = Path.Combine(Program.DataFolder, "scripts", "settings", SETTINGS_FILE_NAME);
	private MainForm mainForm;
	private PintoScriptInfo scriptInfo;
	private bool test;
	
	public PintoScript(MainForm mainForm)
	{
		this.mainForm = mainForm;
		scriptInfo = new PintoScriptInfo("TemplateScriptUsingSettings", "PintoIM", "1.0", "b1.2");
	}
	
	public void Log(string message)
	{
		// TODO: Change the name
		Program.Console.WriteMessage("[<name>] " + message);
	}
	
	public PintoScriptInfo GetScriptInfo()
	{
		return scriptInfo; 
	}
	
	public void OnLoad()
	{
		Log("Script initialized");
	}
	
	public void OnPintoInit()
	{
		LoadSettings();
		MsgBox.Show(null, "The value of the test variable is " + test, 
			"TemplateScriptUsingSettings", MsgBoxIconType.INFORMATION, true);
		// TODO: Implement functionality
	}
	
	private void SaveSettings()
	{
		Log("Saving settings");
		JObject settings = new JObject();
		#region Settings
		settings["test"] = test;
		#endregion
		File.WriteAllText(SETTINGS_FILE_PATH, settings.ToString(Formatting.Indented));
	}
	
	private void LoadSettings()
	{
		Log("Loading settings");
		if (!File.Exists(SETTINGS_FILE_PATH)) SaveSettings();
		JObject settings = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(SETTINGS_FILE_PATH));
		#region Settings
		test = settings.GetValueOrDefault("test", false).ToObject<bool>();
		#endregion
		SaveSettings();
	}
}