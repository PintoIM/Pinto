--[[
	Pinto example extension:
	- This extension has been made to demonstrate how to use the Pinto! Lua API
	- It adds a new item to the help menu that shows the extensions currently loaded
	
	The Pinto! Lua API has the following methods and fields pre-defined:
	- pintoLib.mainForm
	- pintoLib.writeDebug(msg)
	- pintoLib.castType(object, target type name)
	- pintoLib.getStaticClass(type name)
	- pintoLib.getNewClass(type name, type constructor params)
	- luaNumber(number, is short, is int, is float) -- This is done as Lua numbers are doubles
--]]

-- This can be absent
function onLoad()
	-- Add the separator to the help menu
	pintoLib.mainForm.tsddbMenuBarHelp.dropDownItems.add(pintoLib.getNewClass("System.Windows.Forms.ToolStripSeparator"))
	
	-- Add our item to help menu
	pintoLib.mainForm.tsddbMenuBarHelp.dropDownItems.add("Manage Extensions").click.add(item_Click)
	
	-- Say that we added the hook into the help menu
	-- By convention, the debug messages should contain a header that indicates the section
	pintoLib.writeDebug("[ExtensionsManager] Added hook into the help menu")
end

function item_Click(sender, e)
	-- Designer code ported to Lua
	local form = pintoLib.getNewClass("System.Windows.Forms.Form")
	local dgvExtensions = pintoLib.getNewClass("System.Windows.Forms.DataGridView")
	local name = pintoLib.getNewClass("System.Windows.Forms.DataGridViewTextBoxColumn")
	local author = pintoLib.getNewClass("System.Windows.Forms.DataGridViewTextBoxColumn")
	local version = pintoLib.getNewClass("System.Windows.Forms.DataGridViewTextBoxColumn")
	
	dgvExtensions.readOnly = true
	dgvExtensions.allowUserToAddRows = false
	dgvExtensions.allowUserToDeleteRows = false
	dgvExtensions.allowUserToResizeColumns = false
	dgvExtensions.allowUserToResizeRows = false
	dgvExtensions.autoSizeColumnsMode = pintoLib.getStaticClass("System.Windows.Forms.DataGridViewAutoSizeColumnsMode").Fill
	dgvExtensions.columnHeadersHeightSizeMode = pintoLib.getStaticClass("System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode").AutoSize
	dgvExtensions.columns.add(name)
	dgvExtensions.columns.add(author)
	dgvExtensions.columns.add(version)
	dgvExtensions.dock = pintoLib.getStaticClass("System.Windows.Forms.DockStyle").Fill
	dgvExtensions.location = pintoLib.getNewClass("System.Drawing.Point", luaNumber(0, false, true, false), luaNumber(0, false, true, false))
	dgvExtensions.name = "dgvExtensions"
	dgvExtensions.rowHeadersVisible = false
	dgvExtensions.size = pintoLib.getNewClass("System.Drawing.Size", luaNumber(562, false, true, false), luaNumber(345, false, true, false))
	dgvExtensions.tabIndex = 0

	name.headerText = "Name"
	name.name = "name"
	author.headerText = "Author"
	author.name = "author"
	version.headerText = "Version"
	version.name = "version"

	form.autoScaleDimensions = pintoLib.getNewClass("System.Drawing.SizeF", luaNumber(6, false, false, true), luaNumber(13, false, false, true))
	form.autoScaleMode = pintoLib.getStaticClass("System.Windows.Forms.AutoScaleMode").Font
	form.clientSize = pintoLib.getNewClass("System.Drawing.Size", luaNumber(562, false, true, false), luaNumber(345, false, true, false))
	form.controls.Add(dgvExtensions)
	form.name = "ExtensionsManager"
	form.showIcon = false
	form.text = "ExtensionsManager"
	
	-- Put the currently loaded extensions into the form
	
	local extensions = pintoLib.mainForm.extensions
	local extsEnumerator = extensions.getEnumerator()
	
	while extsEnumerator.moveNext() do
		local ext = extsEnumerator.current
		local rowIndex = dgvExtensions.rows.add()
		dgvExtensions.rows[rowIndex].cells[0].value = ext.name
		dgvExtensions.rows[rowIndex].cells[1].value = ext.author
		dgvExtensions.rows[rowIndex].cells[2].value = ext.version
	end
	
	-- Show the form
	form.ShowDialog()
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