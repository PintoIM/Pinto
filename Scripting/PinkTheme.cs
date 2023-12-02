//css_reference Pinto.exe
using System;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using System.Collections.Generic;
using PintoNS;
using PintoNS.General;
using PintoNS.Forms;
using PintoNS.Controls;

/*
	PinkTheme - Gives Pinto! a glorious pink theme
	! This is a template script, meant as a base for people that want to make themes
	!!! Contains repetitions and hacks
	!!! Not thoroughly tested, could contain some bugs or theme forms incorrectly 
*/
public class PintoScript : IPintoScript
{
	private MainForm mainForm;
	private Thread formMonitor;
	private PintoScriptInfo scriptInfo;
	
	public PintoScript(MainForm mainForm)
	{
		this.mainForm = mainForm;
		scriptInfo = new PintoScriptInfo("PinkTheme", "PintoIM", "1.0", "b1.2");
		formMonitor = new Thread(new ThreadStart(FormMonitor_Func));
	}
	
	// Thread that monitors for newly opened forms and styles them
	private void FormMonitor_Func()
	{
		List<Form> prevOpenForms = new List<Form>();
		
		while (!mainForm.IsDisposed)
		{
			try 
			{
				for (int i = 0; i < Application.OpenForms.Count; i++)
				{
					Form form = Application.OpenForms[i];
					
					if (!prevOpenForms.Contains(form))
					{
						prevOpenForms.Add(form);
						mainForm.Invoke(new Action(() => 
						{
							FormOpened(form);
						}));
					}
				}	
			}
			// This is just in-case somehow a form is opened/closed right as the list is being looked-up
			catch {}
		}
	}
	
	public void Log(string message)
	{
		Program.Console.WriteMessage("[PinkTheme] " + message);	
	}
	
	public PintoScriptInfo GetScriptInfo()
	{
		return scriptInfo; 
	}
	
	public void OnLoad()
	{
	}

	public void OnPintoInit()
	{
		Log("Starting theming thread");
		formMonitor.Start();
		
		// Color the message format
		MessageForm.MsgSelfSenderColor = Color.DeepPink;
		MessageForm.MsgOtherSenderColor = Color.DarkViolet;
		MessageForm.MsgSeparatorColor = Color.White;
		MessageForm.MsgContentColor = Color.White;
		MessageForm.MsgTimeColor = Color.Violet;
	}
	
	private void ColorControl(Control control)
	{
		control.BackColor = Color.Pink;
		control.ForeColor = Color.White;
		
		foreach (Control control2 in control.Controls)
		{
			ColorControl(control2);
		}
	}
	
	private void FormOpened(Form form)
	{
		//Log("Theming the newly opened form: " + form.GetType().Name);
		
		// No theming to these
		if (form is AboutForm || form is BrowserForm)
		{
			return;
		}

		if (form is MainForm)
		{
			ColorControl(form);

			// mainForm is the instance from the script
			// XXX: Should be fine
			ColorControl(mainForm.tpLogin);
			ColorControl(mainForm.tpConnecting);
			ColorControl(mainForm.tpStart);
			ColorControl(mainForm.tpContacts);
			ColorControl(mainForm.tpCall);
			mainForm.dgvContacts.BackgroundColor = Color.Pink;
			mainForm.dgvContacts.DefaultCellStyle.BackColor = Color.Pink;
			mainForm.dgvContacts.DefaultCellStyle.ForeColor = Color.White;
			mainForm.dgvContacts.DefaultCellStyle.SelectionBackColor = Color.DeepPink;
			mainForm.dgvContacts.DefaultCellStyle.SelectionForeColor = Color.White;
			
			return;
		}
		
		if (form is MessageForm)
		{
			form.BackColor = Color.Pink;
			form.ForeColor = Color.White;
			MessageForm form2 = (MessageForm) form;
			
			foreach (Control control in form.Controls)
			{
				if (control == form2.rtxtMessages) 
				{
					control.BackColor = Color.Pink;
					continue;
				}
				ColorControl(control);
			}
			
			return;
		}

		// Generic coloring (works for most)
		ColorControl(form);
	}
}