require "System.Global"
require "Logic.Tool.UITools"

function Content:Awake(this)
	self.this = this

end
 
function Content:Start(this)

end
 
function Content:OnDestroy()
	self.this = nil
	self = nil
end
 