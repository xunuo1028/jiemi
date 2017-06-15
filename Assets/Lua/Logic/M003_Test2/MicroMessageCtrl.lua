require "System.Global"
require "Logic.Tool.UITools"

class ("MicroMessageCtrl")

function MicroMessageCtrl:Awake(this)
	self.this = this
	self.otherMsgTemplate = self.this.transform:Find("Template/otherMsg")
	self.selfMsgTemplate = self.this.transform:Find("Template/selfMsg")
end
 
function MicroMessageCtrl:Start()

end

function MicroMessageCtrl:InitTotalList()
	-- body
	
end
 
function MicroMessageCtrl:OnDestroy()
	self.this = nil
	self = nil
end