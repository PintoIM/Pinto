-- Imports, check ExtensionsManager for a more indept explanation
import("Pinto", "PintoNS")
import("Pinto", "PintoNS.General")
import("System")
import("System.Windows.Forms")
import("System.Drawing")

formOpenedEvent = nil

function OnLoad()
	-- Event called when a form is opened
	formOpenedEvent = Program.FormOpened:Add(Program_FormOpened)
end

function OnUnload()
	Program.FormOpened:Remove(formOpenedEvent)
end

function Program_FormOpened(sender, form)
	-- Check if this is not a message form, if so, return
	if form.Name ~= "MessageForm" then return end
	
	-- Event for when we received a message
	form.MessageWritting:Add(function(msg, color, newLine)
		-- Check if the color is not red, if so, cancel the message and make it red
		if color ~= Color.Red then form:WriteMessage(msg, Color.Red, newLine) return false end
		-- If the color is red, allow the message
		return true
	end)
end

function ScriptInfo()
	return {
		name = "RedMessages",
		author =  "vlOd",
		version = "1.0"
	}
end