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
import("System.Windows.Forms")
import("System.Drawing")

-- This can be absent
function onLoad()
	-- Add the separator to the help menu
	PintoLib.MainForm.tsddbMenuBarHelp.DropDownItems:Add(ToolStripSeparator())
	
	-- Add our item to help menu
	PintoLib.MainForm.tsddbMenuBarHelp.DropDownItems:Add("Manage Extensions").Click:Add(item_Click)
	
	-- Say that we added the hook into the help menu
	-- By convention, the debug messages should contain a header that indicates the section
	PintoLib.WriteDebug("[ExtensionsManager] Added hook into the help menu")
end

function item_Click(sender, e)
	-- Designer code ported to Lua
	local form = Form()
	local dgvExtensions = DataGridView()
	local name = DataGridViewTextBoxColumn()
	local author = DataGridViewTextBoxColumn()
	local version = DataGridViewTextBoxColumn()
	
	dgvExtensions.ReadOnly = true
	dgvExtensions.AllowUserToAddRows = false
	dgvExtensions.AllowUserToDeleteRows = false
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
	dgvExtensions.RowHeadersVisible = false
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
	form.Name = "ExtensionsManager"
	form.ShowIcon = false
	form.Text = "ExtensionsManager"
	
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
	form:ShowDialog()
end

-- This can be absent
function onDisconnect()
end

-- This can be absent
function onExit()
end

-- This MUST be present
function getScriptInfo()
	return {
		name = "ExtensionsManager",
		author =  "vlOd",
		version = "1.0"
	}
end