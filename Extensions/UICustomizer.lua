--[[
	UICustomizer: UI customization extension
	THIS EXTENSION IS PROVIDED UNDER NO WARRANTY "AS IS"
	⚠️ This extension makes Pinto! significantly more resource intesnsive and slow
--]]

import("Pinto", "PintoNS")
import("Pinto", "PintoNS.General")
import("System")
import("System.Windows.Forms")
import("System.Drawing")

formOpenedEvent = nil

function OnLoad()
	formOpenedEvent = Program.FormOpened:Add(Program_FormOpened)

	-- Tabs that are not always active
	PintoLib.MainForm.tpLogin.BackColor = GetBackColor()
	PintoLib.MainForm.tpConnecting.BackColor = GetBackColor()
	PintoLib.MainForm.tpStart.BackColor = GetBackColor()
	PintoLib.MainForm.tpContacts.BackColor = GetBackColor()
	PintoLib.MainForm.dgvContacts.BackgroundColor = GetBackColor()
	PintoLib.MainForm.dgvContacts.ForeColor = GetForeColor()
	PintoLib.MainForm.dgvContacts.DefaultCellStyle.BackColor = GetBackColor()

	-- Tray
	PintoLib.MainForm.cmsTray.BackColor = Color.Transparent
	PintoLib.MainForm.tsmiTrayChangeStatus.BackColor = GetBackColor()
	PintoLib.MainForm.tsmiTrayChangeStatus.ForeColor = GetForeColor()
	PintoLib.MainForm.tsmiTrayExit.BackColor = GetBackColor()
	PintoLib.MainForm.tsmiTrayExit.ForeColor = GetForeColor()
	PintoLib.MainForm.tsmiTrayChangeStatusOnline.BackColor = GetBackColor()
	PintoLib.MainForm.tsmiTrayChangeStatusOnline.ForeColor = GetForeColor()
	PintoLib.MainForm.tsmiTrayChangeStatusAway.BackColor = GetBackColor()
	PintoLib.MainForm.tsmiTrayChangeStatusAway.ForeColor = GetForeColor()
	PintoLib.MainForm.tsmiTrayChangeStatusBusy.BackColor = GetBackColor()
	PintoLib.MainForm.tsmiTrayChangeStatusBusy.ForeColor = GetForeColor()
	PintoLib.MainForm.tsmiTrayChangeStatusInvisible.BackColor = GetBackColor()
	PintoLib.MainForm.tsmiTrayChangeStatusInvisible.ForeColor = GetForeColor()
end

function OnUnload()
	Program.FormOpened:Remove(formOpenedEvent)
end

function Program_FormOpened(sender, form)
	if form.Name == "AboutForm" then return end

	PintoLib.ControlInvoke(form, function() 
		StyleControl(form)
	end)
end

function GetBackColor()
	return Color.Pink
end

function GetForeColor()
	return Color.Black
end

function GetLinkColor()
	return Color.Blue
end

function GetActiveLinkColor()
	return Color.DeepPink
end

function StyleControlNoSub(control)
	if control.BackColor ~= nil then
		control.BackColor = GetBackColor()
	end

	if control.ForeColor ~= nil then
		control.ForeColor = GetForeColor()
	end

	if control.TabPages ~= nil then
		for tabPageIndex = 0, control.TabPages.Count do
			if tabPageIndex >= control.TabPages.Count then break end
			local tabPage = control.TabPages[tabPageIndex]
			StyleControl(tabPage)
		end
	end
	
	if control.Items ~= nil then
		for itemIndex = 0, control.Items.Count do
			if itemIndex >= control.Items.Count then break end
			local item = control.Items[itemIndex]
			StyleControlNoSub(item)
		end
	end
	
	if control.PlaceholderTextForeColor ~= nil then
		control.PlaceholderTextForeColor = GetForeColor()
	end
	
	if control.LinkColor ~= nil then
		control.LinkColor = GetLinkColor()
	end
	
	if control.ActiveLinkColor ~= nil then
		control.ActiveLinkColor = GetActiveLinkColor()
	end
	
	if control.DropDown ~= nil then
		if control.DropDown.BackColor ~= nil then 
			control.DropDown.BackColor = GetBackColor()
		end
		
		if control.DropDown.ForeColor ~= nil then 
			control.DropDown.ForeColor = GetForeColor()
		end
	end
end

function StyleControl(control)
	StyleControlNoSub(control)

	for controlIndex = 0, control.Controls.Count do
		if controlIndex >= control.Controls.Count then break end
		local subControl = control.Controls[controlIndex]
		StyleControl(subControl)
	end
end

function ScriptInfo()
	return {
		name = "UICustomizer",
		author =  "vlOd, jonnyprogamer",
		version = "1.0"
	}
end