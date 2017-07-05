require "System.Global"
require "Logic.Tool.UITools"

class("M002_Test1Ctrl")

function M002_Test1Ctrl:Awake(this)
	-- body
	self.this = this
	self.testButton = self.this.transforms[0]
--[[
	local str = 
		"\nself.newTweener = UITweenLoader.New()" .. "\n" .. 
		string.format("local tbl = {\n" .. 
						"\tfrom = Vector3.New(%.1f, %.1f, %.1f),\n" ..
						"\tfrom = Vector3.New(%.1f, %.1f, %.1f),\n" .. 
						"\tmTrans = \"\",\n" ..
						"\tstyle = \"%s\",\n" .. 
						"\tmethod = \"%s\",\n" .. 
						"\tdelay = %.1f,\n" .. 
						"\tduration = %.1f,\n" .. 
						"\tonTweenFinishDo = {},\n" .. "}\n", 
						1, 1, 1, 0, 0, 0, "Once", "Linear", 1, 1) .. 
		"--table.insert(tbl.onTweenFinishDo,        )\n" ..
		string.format("self.newTween:Init(tbl, \"%s\") \n\n", "UITweenPosition")

	print(str)

	local copyText = UnityEngine.TextEditor.New()
	copyText.text = str
	copyText:SelectAll()
	copyText:Copy()
]]





end

function M002_Test1Ctrl:Start()
	-- body

	local listener
	--返回按钮事件
	listener = EventTriggerProxy.Get(self.testButton.gameObject)
	local callback = function(self, e)
		self:GoToPanel("M001_Test")
	end
	listener.onPointerClick = EventTriggerProxy.PointerEventDelegate(callback, self)	 	
end

function M002_Test1Ctrl:GoToPanel(stringPanel, parentName)  --panel名称，是否销毁当前界面
  coroutine.start(self.WaitForCreatePanel,self,stringPanel)
end
function M002_Test1Ctrl:WaitForCreatePanel(stringPanel, parentName)
  local async = GameManager.CreatePanelAsync (stringPanel, parentName)
  while async.Done == false do
    coroutine.step()
  end
end


function M002_Test1Ctrl:OnDestroy()
	-- body
	self.this = nil
	self = nil
end