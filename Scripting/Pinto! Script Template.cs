//css_reference Pinto.exe
using System;
using System.Windows.Forms;
using PintoNS;
using PintoNS.General;

/*
Template Pinto! script file
This is as a template for all script files that comes with some examples to get you started
*/
public class PintoScript : IPintoScript
{
	private MainForm mainForm;
	
	public PintoScript(MainForm mainForm)
	{
		this.mainForm = mainForm;
	}
	
	public void Log(string message)
	{
		// TODO: Change the name
		Program.Console.WriteMessage("[<name>] " + message);	
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