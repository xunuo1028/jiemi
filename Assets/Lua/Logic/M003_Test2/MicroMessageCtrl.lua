require "System.Global"
require "Logic.Tool.UITools"

class ("MicroMessageCtrl")

function MicroMessageCtrl:Awake(this)
	self.this = this
	self.root = self.this.transform
	self.controller_SelfMes = self.root:Find("SelfMes")
	self.controller_OtherMes = self.root:Find("OtherMes")
	self.controller_Content = self.root:Find("Content")
	self.controller_TitleBar = self.root:Find("TitleBar")
	self.controller_Bg = self.root:Find("Bg")
	self.selfMes_Words = self.controller_SelfMes:Find("Words")
	self.selfMes_SelfTimeBg = self.controller_SelfMes:Find("SelfTimeBg")
	self.selfTimeBg_Time = self.selfMes_SelfTimeBg:Find("Time")
	self.otherMes_Words = self.controller_OtherMes:Find("Words")
	self.otherMes_OtherTimeBg = self.controller_OtherMes:Find("OtherTimeBg")
	self.otherTimeBg_Time = self.otherMes_OtherTimeBg:Find("Time")
	self.content_Grid = self.controller_Content:Find("Grid")
	self.titleBar_MessageOwner = self.controller_TitleBar:Find("MessageOwner")
	self.titleBar_Return = self.controller_TitleBar:Find("Return")

end
 
function MicroMessageCtrl:Start()

end
 
function MicroMessageCtrl:OnDestroy()
	self.this = nil
	self = nil
end