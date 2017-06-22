require "System.Global"
require "Logic.MsgManager"
require "Logic.MsgPool"
require "Logic.TweenTools.UITweenLoader"

class("TestCtrl")

function TestCtrl:Awake(this)
	-- body
	self.this = this
	self.testButton = self.this.transform:Find("Button")
	self.test1Button = self.this.transform:Find("Button1")
	self.scroll = self.this.transform:Find("Scroll")
	self.newTweener = nil
end

function TestCtrl:Start()
	-- body
	--MsgManager.AddListener(0x1001, TestCtrl.Test, self)
	--MsgManager.BroadCast(0x1001, 100)
	--MsgManager.AddListener(0x1001, TestCtrl.Test2, self)



	local listener
	--返回按钮事件
	listener = EventTriggerProxy.Get(self.testButton.gameObject)
	local callback = function(self, e)
		--self:GoToPanel("M003_Test2", "M003")
		--self:TestTween()
		self.newTweener = UITweenLoader.New()
		local tbl = {
			to = Vector3.New(0, 0, 0),
			mTrans = self.this.transform,
			onTweenFinishDo = {},
		}
		table.insert(tbl.onTweenFinishDo, {TestCtrl.Test, self, 10})
		self.newTweener:Init(tbl, "UITweenPosition")

		--coroutine.start(TestCtrl.TestTweenPause, self, self.newTweener)	
	end
	listener.onPointerClick = EventTriggerProxy.PointerEventDelegate(callback, self)

	listener = EventTriggerProxy.Get(self.test1Button.gameObject)
	local callback1 = function(self, e)
		print("abcabcabca " .. e.clickCount)
	end
	listener.onPointerClick = EventTriggerProxy.PointerEventDelegate(callback1, self)		

	self:ScrollTest()
end

function TestCtrl:Test(num)
	-- body
	print("Test num = " .. num)
end

function TestCtrl:Test2(num)
	-- body
	self.testButton:GetComponent("UnityEngine.UI.Image").color = Color.New(1, 1, 0, 1)
end

function TestCtrl:GoToPanel(stringPanel, parentName)  --panel名称，是否销毁当前界面
  coroutine.start(self.WaitForCreatePanel,self,stringPanel)
end

function TestCtrl:WaitForCreatePanel(stringPanel, parentName)
  local async = GameManager.CreatePanelAsync (stringPanel, parentName)
  while async.Done == false do
    coroutine.step()
  end
end

function TestCtrl:TestTweenPause(tweener)
	-- body
	coroutine.wait(1)
	tweener.Pause = true
	coroutine.wait(1)
	tweener.Pause = false

end

function TestCtrl:ScrollTest()
	-- body
	local s = UITools.GetLuaScript("UIScrollTools", self.scroll)
	s.self:Init(true, 28, 4)
	for i = 1, 28 do
		local itemTbl = s.self:DoScroll(nil, i)
		--[[
		local listener
		listener = EventTriggerProxy.Get(itemTbl.trans.gameObject)
		local callback1 = function(self, e)
			print("abcabcabca " .. itemTbl.trans.name)
		end
		listener.onPointerClick = EventTriggerProxy.PointerEventDelegate(callback1, self)	
		]]
		itemTbl.Text.text.text = i
		s.self:OnClickEvent(itemTbl.trans:GetComponent("Button"), self.ClickTest, self, itemTbl.trans.name)	
	end
end

function TestCtrl:ScrollEvent(trans, id)
	-- body
	trans:Find("Text"):GetComponent("Text").text = "Click" .. id
end

function TestCtrl:ClickTest(str)
	-- body
	print("Click Test " .. str)
end

function TestCtrl:OnDestroy()
	-- body
	self.this = nil
	self = nil
end