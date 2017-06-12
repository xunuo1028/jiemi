require "System.Global"
require "Logic.Tool.UITools"
require "Logic.GameConfig.ConfigData"

class("BgCtrl")

function BgCtrl:Awake(this)
	-- body
	self.this = this
	self.mainButton = self.this.transform:Find("Action/MainButton")

	self.root = GameObject.Find("UIRoot")

	local listener
	--返回按钮事件
	listener = EventTriggerProxy.Get(self.mainButton.gameObject)
	local callback_main = function(self, e)
		UITool.ClearUIRoot("M003_MainPanel")
	end
	listener.onPointerClick = EventTriggerProxy.PointerEventDelegate(callback_main, self)


	--将自己置于StaticPanel节点下
	self.this.transform.parent:SetParent(GameObject.Find("UIRoot/M000").transform)

	ConfigData.Instance():InitData()

end

function BgCtrl:Start()
	-- body
	print(GameObject.Find("UIRoot/Component/UICamera").transform.name .. " " .. "asdfasdf")
	self:GoToPanel("M001_LockScreen", "M000")
end

function BgCtrl:GoToPanel(stringPanel, parentName)  --panel名称，是否销毁当前界面
  coroutine.start(self.WaitForCreatePanel, self, stringPanel, parentName)
end
function BgCtrl:WaitForCreatePanel(stringPanel, parentName)
  local async = GameManager.CreatePanelAsync (stringPanel, parentName)
  while async.Done == false do
    coroutine.step()
  end
end

function BgCtrl:OnDestroy()
	-- body
	self.this = nil
	self = nil
end