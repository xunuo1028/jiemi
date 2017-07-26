require "System.Global"
require "Logic.Tool.UITools"
require "Logic.TweenTools.UITweenLoader"

class("LockScreenCtrl")

function LockScreenCtrl:Awake(this)
	-- body
	self.this = this
	self.unLockButton = self.this.transform:Find("Action/BottomArrow")
	self.messageZone = self.this.transform:Find("Action/MessageZone")
	self.actionPanel = self.this.transform:Find("Action")
	self.passwordPanel = self.this.transform:Find("PasswordPanel")
	self.password = {}

	local dis = self.actionPanel.position.y - self.unLockButton.position.y
	local oPos = self.actionPanel.position
	local passwordToUnlockButton = self.passwordPanel.position.y - self.unLockButton.position.y
	local passwordPos = self.passwordPanel.position

	local listener
	listener = EventTriggerProxy.Get(self.unLockButton.gameObject)
	local callback_unLock = function(self, e)
		local pos = self.unLockButton.position.y
	end
	listener.onBeginDrag = EventTriggerProxy.PointerEventDelegate(callback_unLock, self)

	listener = EventTriggerProxy.Get(self.unLockButton.gameObject)
	local callback_drag = function(self, e)
		--self.cor = coroutine.start(self.DoUnlock, self, pos)
		self.unLockButton.position = Vector3.New(self.unLockButton.position.x, 
							UITools.GetUIPosition(e.position, GameObject.Find("UIRoot").transform).y, 
							self.unLockButton.position.z)
		self:DoUnlock(self.actionPanel, dis, self.unLockButton.position.y, passwordToUnlockButton)
	end
	listener.onDrag = EventTriggerProxy.PointerEventDelegate(callback_drag, self)

	listener = EventTriggerProxy.Get(self.unLockButton.gameObject)
	local callback_unLock = function(self, e)
		if self.actionPanel.position.y < 3 then

			local tweener = UITweenLoader.New()
			tweener:EaseGenerate(self.actionPanel, nil, oPos, 0, 0.3, nil, nil, nil, "UITweenPosition", true)

			local tweener1 = UITweenLoader.New()
			tweener1:EaseGenerate(self.passwordPanel, nil, passwordPos, 0, 0.3, nil, nil, nil, "UITweenPosition", true)
		else

			local tweener = UITweenLoader.New()
			tweener:EaseGenerate(self.actionPanel, nil, Vector3.New(oPos.x, 9.4, oPos.z), 0, 0.3, nil, nil, nil, "UITweenPosition", true)

			local tweener1 = UITweenLoader.New()
			tweener1:EaseGenerate(self.passwordPanel, nil, oPos, 0, 0.3, nil, nil, nil, "UITweenPosition", true)
			self:PressNum()
		end
	end
	listener.onEndDrag = EventTriggerProxy.PointerEventDelegate(callback_unLock, self)

end

function LockScreenCtrl:Start()
	-- body
	local tbl = {one = "one", two = "two", three = "three", four = "four"}
end

function LockScreenCtrl:DoUnlock(trans, dis, posy, oPos)
	-- body
	local x = trans.position.x
	local z = trans.position.z
	local y = trans.position.y
	trans.position = Vector3.New(x, posy + dis, z)

	--position
	self.passwordPanel.position = Vector3.New(self.passwordPanel.position.x,
										posy + oPos, 
										self.passwordPanel.position.z)
end

function LockScreenCtrl:PressNum()
	-- body
	--clear block
	local blockTrans = self.passwordPanel:Find("Block")
	for i = 1, blockTrans.childCount do
		blockTrans:GetChild(i - 1):Find("Full").gameObject:SetActive(false)
	end
	self.password = {}

	local numTrans = self.passwordPanel:Find("Number")
	for i = 1, numTrans.childCount do
		local listener
		listener = EventTriggerProxy.Get(numTrans:GetChild(i - 1).gameObject)
			local callback_unLock = function(self, e)
				table.insert(self.password, numTrans:GetChild(i - 1).name)
				blockTrans:GetChild(#self.password - 1):Find("Full").gameObject:SetActive(true)
				if #self.password == 4 then
					self:CheckPassword(self.password)
				end
			end
		listener.onPointerClick = EventTriggerProxy.PointerEventDelegate(callback_unLock, self)		
	end
end

function LockScreenCtrl:CheckPassword(tbl)
	-- body
	local password = ""
	for i = 1, #self.password do
		password = password .. self.password[i]
	end
	password = tonumber(password)
	if password == ConfigData.Instance().password_UnlockMainPanel then
		--ShowMainPanel
	else
		--Shock
		coroutine.start(self.ScreenShock, self)
		self.password = {}
	end

end

function LockScreenCtrl:ScreenShock()
	-- body
	local animator = self.passwordPanel:GetComponent("Animator")
	animator.enabled = true
	animator:Play("Shock", 0, 0)
	coroutine.wait(1)
	self:PressNum()
	animator.enabled = false
end

function LockScreenCtrl:OnDestroy()
	-- body
	self.this = nil
	self = nil
end