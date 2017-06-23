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
	self.exampleList = {
		15000001,
		15000002,
		15000003,
		15000004,
		15000005,
		15000006,
		15000007,
		15000008,
		15000009,
		15000010,
		15000011,
		15000012,
		15000013,
		15000014,
		15000015,
		15000016,
		15000017,
		15000018,
		15000019,
		15000020,
		15000021,
		15000022,
		15000023,
		15000024,
		15000025,
		15000026,
		15000027,
		15000028,
		15000029,
		15000030,
		15000031,
		15000032,
		15000033,
		15000034,
		15000035,
		15000036,
		15000037,
		15000038,
		15000039,
		15000040,
		15000041,
		15000042,
		15000043,
		15000044,
		15000045,
		15000046,
		15000047,
		15000048,
		15000049,
		15000050,
		15000051,
		15000052,
		15000053,
		15000054,
		15000055,
		15000056,
	}
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
	s.self:Init(true, 4)
	local function DoEveryItem(luaSelf, trans, id, idx)
		-- body
		trans:Find("Text"):GetComponent("Text").text = id
		trans:Find("Text"):GetComponent("Text").fontSize = 20
		trans.name = id .. "_" .. idx
		local function DoClickEvent(luaSelf, str)
			-- body
			print("Click Test " .. str .. " " .. self.scroll.name)
		end
		s.self:OnClickEvent(trans:GetComponent("Button"), DoClickEvent, self, trans.name)
	end
	s.self:DoItemShow(DoEveryItem, self)
	s.self:DoScroll(self.exampleList)
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