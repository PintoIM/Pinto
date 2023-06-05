--[[
	Pinto example extension:
	- This extension has been made to demonstrate how to use the Pinto! Lua API
	- It adds a new item to the help menu that shows the extensions currently loaded
	
	The Pinto! Lua API has the following methods and fields pre-defined:
	- PintoLib.MainForm
	- PintoLib.WriteDebug(msg)
--]]

-- import(assemblyName, namespace)
-- Check NLua for more information: https://github.com/NLua/NLua
import("Pinto", "PintoNS.General")
-- The namespace can be absent and will use the assembly name
import("System")
import("System.Windows.Forms")
import("System.Drawing")

-- Hook variables
toolStripSeparator = nil
toolStripMenuItem = nil
-- Form variables
form = nil
dgvExtensions = nil
name = nil
author = nil
version = nil

-- This can be absent
function OnFormLoad()
	-- Add a separator to the help menu
	toolStripSeparator = ToolStripSeparator()
	PintoLib.MainForm.tsddbMenuBarHelp.DropDownItems:Add(toolStripSeparator)
	
	-- Add our item to help menu
	toolStripMenuItem = PintoLib.MainForm.tsddbMenuBarHelp.DropDownItems:Add("Manage Extensions")
	toolStripMenuItem.Click:Add(item_Click)
	
	-- Say that we added the hook into the help menu
	-- By convention, the debug messages should contain a header that indicates the section
	PintoLib.WriteDebug("[ExtensionsViewer] Added hook into the help menu")
end

-- This can be absent
-- Called when the extension is unloaded, please make sure to fully clean-up anything that references this extension
function OnUnload()
	-- Remove the separator from the help menu
	PintoLib.MainForm.tsddbMenuBarHelp.DropDownItems:Remove(toolStripSeparator)
	-- Remove our item from the help menu
	PintoLib.MainForm.tsddbMenuBarHelp.DropDownItems:Remove(toolStripMenuItem)
end

function item_Click(sender, e)
	-- Designer code ported to Lua
	form = Form()
	dgvExtensions = DataGridView()
	name = DataGridViewTextBoxColumn()
	author = DataGridViewTextBoxColumn()
	version = DataGridViewTextBoxColumn()
	
	dgvExtensions.ReadOnly = true
	dgvExtensions.AllowUserToAddRows = false
	dgvExtensions.AllowUserToResizeColumns = false
	dgvExtensions.AllowUserToResizeRows = false
	dgvExtensions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
	dgvExtensions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
	dgvExtensions.Columns:Add(name)
	dgvExtensions.Columns:Add(author)
	dgvExtensions.Columns:Add(version)
	dgvExtensions.Dock = DockStyle.Fill
	dgvExtensions.Location = Point(0, 0)
	dgvExtensions.Name = "dgvExtensions"
	dgvExtensions.Size = Size(562, 345)
	dgvExtensions.TabIndex = 0

	name.HeaderText = "Name"
	name.Name = "name"
	author.HeaderText = "Author"
	author.Name = "author"
	version.HeaderText = "Version"
	version.Name = "version"

	form.AutoScaleDimensions = SizeF(6, 13)
	form.AutoScaleMode = AutoScaleMode.Font
	form.ClientSize = Size(562, 345)
	form.Controls:Add(dgvExtensions)
	form.Name = "ExtensionsViewer"
	form.ShowIcon = false
	form.Text = "Extensions Manager"
	form.Closing:Add(form_Closing)
	
	-- Put the currently loaded extensions into the form
	local extensions = PintoLib.MainForm.Extensions
	local extsEnumerator = extensions:GetEnumerator()
	
	while extsEnumerator:MoveNext() do
		local ext = extsEnumerator.Current
		local rowIndex = dgvExtensions.Rows:Add()
		dgvExtensions.Rows[rowIndex].Cells[0].Value = ext.Name
		dgvExtensions.Rows[rowIndex].Cells[1].Value = ext.Author
		dgvExtensions.Rows[rowIndex].Cells[2].Value = ext.Version
	end
	
	-- Show the form
	form:Show()
	
	-- Ignore this extension
	if extensions.Count > 1 then
		MsgBox.Show(nil, "You currently have extensions loaded!" .. Environment.NewLine .. "Beware that extensions can be made by anyone and have NO RESTRICTIONS!" .. Environment.NewLine .. "It is your responsibility to make sure that you install safe extensions", "Warning", MsgBoxIconType.WARNING)
	end
end

function form_Closing(sender, e)
	local extensions = PintoLib.MainForm.Extensions
	local rows = dgvExtensions.Rows

	for extIndex = 0, extensions.Count do
		if extIndex >= extensions.Count then break end
		local ext = extensions[extIndex]
		local foundRow = false
		
		for rowIndex = 0, rows.Count do
			if (rowIndex >= rows.Count) then break end
			local row = rows[rowIndex]
			
			if row.Cells[0].Value == ext.Name and 
				row.Cells[1].Value == ext.Author and 
				row.Cells[2].Value == ext.Version then
				foundRow = true
			end
		end
		
		if not foundRow then
			PintoLib.MainForm:UnloadExtension(ext)
		end
	end
end

-- This can be absent
function OnLogin()
end

-- This can be absent
-- Beware: This method is called on start-up too
function OnLogout()
end

-- This can be absent
function OnDisconnect()
end

-- This MUST be present otherwise your extension will fail to load
function ScriptInfo()
	return {
		name = "ExtensionsManager",
		author =  "vlOd",
		version = "1.0"
	}
end

-- This can be absent (assumes 0 if so)
--[[
	0 - Low
	1 - Medium
	2 - High
--]]
function ScriptPriority()
	return 2
end