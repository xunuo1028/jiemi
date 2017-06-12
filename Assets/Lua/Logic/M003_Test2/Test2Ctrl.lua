require "System.Global"

class("Test2Ctrl")

function Test2Ctrl:Awake(this)
	-- body
	self.this = this
	self.testButton = self.this.transform:Find("Button")
end

function Test2Ctrl:Start()
	-- body
	local listener
	--返回按钮事件
	listener = EventTriggerProxy.Get(self.testButton.gameObject)
	local callback = function(self, e)
		self:TestBroadCast()
	end
	listener.onPointerClick = EventTriggerProxy.PointerEventDelegate(callback, self)	
end

function Test2Ctrl:TestBroadCast()
	-- body
	MsgManager.BroadCast(MsgPool.Msg.MSG_TestUI, 200)
end

function Test2Ctrl:OnDestroy()
	-- body
	self.this = nil 
	self = nil
end