require "System.Global"
require "Logic.Tool.UITools"

class ("MicroMessageCtrl")

function MicroMessageCtrl:Awake(this)
	self.this = this
	self.selfMes = self.root:Find("SelfMes")
	self.otherMes = self.root:Find("OtherMes")
	self.content = self.root:Find("Content")
	self.titleBar = self.root:Find("TitleBar")
	self.bg = self.root:Find("Bg")
	self.s_Words = self.selfMes:Find("S_Words")
	self.selfTimeBg = self.selfMes:Find("SelfTimeBg")
	self.s_Time = self.selfTimeBg:Find("S_Time")
	self.o_Words = self.otherMes:Find("O_Words")
	self.otherTimeBg = self.otherMes:Find("OtherTimeBg")
	self.o_Time = self.otherTimeBg:Find("O_Time")
	self.grid = self.content:Find("Grid")
	self.messageOwner = self.titleBar:Find("MessageOwner")
	self.returnButton = self.titleBar:Find("Return")
	self.root = self.this.transform

	local listener
	listener = EventTriggerProxy.Get(self.returnButton.gameObject)
		local callback_Return = function(self, e)
			
		end
	listener.onPointerClick = EventTriggerProxy.PointerEventDelegate(callback_Return, self)


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