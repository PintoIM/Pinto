//css_reference Pinto.exe
using System;
using System.Windows.Forms;
using PintoNS;
using PintoNS.General;

/*
	Template Pinto! script file
*/
public class PintoScript : IPintoScript
{
	private MainForm mainForm;
	private PintoScriptInfo scriptInfo;
	
	public PintoScript(MainForm mainForm)
	{
		this.mainForm = mainForm;
		scriptInfo = new PintoScriptInfo("TemplateScript", "PintoIM", "1.0", "b1.2");
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
		// Adds a new item in the File menu
		#region Example code (to remove)
		ToolStripMenuItem item = new ToolStripMenuItem();
		item.Text = "Test";
		item.Click += (object sender, EventArgs e) => 
		{
			MsgBox.Show(null, "Hello, world!", "This is a test", MsgBoxIconType.INFORMATION, true);
		};
		mainForm.tsddbMenuBarFile.DropDownItems.Add(item);
		#endregion
		
		// TODO: Implement functionality
	}
}