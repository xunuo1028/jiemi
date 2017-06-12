require "System.Global"
require "Logic.Tool.UITools"
require "Logic.TweenTools.UITweenLoader"

class("UITweenTool")

function UITweenTool:Awake(this)
	-- body
	self.this = this
	self.toolBar = self.this.transform:Find("ToolBar")
	self.flyBar = self.this.transform:Find("Scroll View/Viewport/Grid/FlyBar")

	--self.tweenSelector = self.toolBar:Find("Dropdown")
	self.initButton = self.toolBar:Find("Init")
	self.clearButton = self.toolBar:Find("Clear")
	self.genButton = self.toolBar:Find("Generate")
	self.beginButton = self.toolBar:Find("Begin")
	self.isTweenToggle = self.toolBar:Find("IsTweenGroup")
	self.confirmTweenGroupButton = self.isTweenToggle:Find("Confirm")

	self.isSingleCheck = true
	self.mTrans = {}
	self.flyBarTbl = {}
	self.groupNum = {}

	self.tweenTbl = {
		"UITweenPosition",
		"UITweenRotation",
		"UITweenAlpha",
		"UITweenColor",
		"UITweenScale",
	}

	local listener

	--初始化按钮，用于将LuaScript上的transforms的对象读取到lua脚本中
	listener = EventTriggerProxy.Get(self.initButton.gameObject)
	local callback_onSelect = function(self, e)
		self:Init()
	end
	listener.onPointerClick = EventTriggerProxy.PointerEventDelegate(callback_onSelect, self)

	--开始按钮，用于开始全局Tween动画
	listener = EventTriggerProxy.Get(self.beginButton.gameObject)
	local callback_begin = function(self, e)
		if self.isSingleCheck then
			for k, v in pairs(self.flyBarTbl) do
				self:DoTween(v.dropValueCheck, v.flyBar, true)
			end
		else
			for k, v in pairs(self.flyBarTbl) do
				print(v.dropValueCheck .. " " .. v.flyBar.name .. " 11111111111 ")
				self:DoTween(v.dropValueCheck, v.flyBar, false)
			end			
		end
	end
	listener.onPointerClick = EventTriggerProxy.PointerEventDelegate(callback_begin, self)

	listener = EventTriggerProxy.Get(self.clearButton.gameObject)
	local callback_clear = function(self, e)

	end
	listener.onPointerClick = EventTriggerProxy.PointerEventDelegate(callback_clear, self)

	listener = EventTriggerProxy.Get(self.isTweenToggle.gameObject)
	local callback_isTweenToggle = function(self, e)
		--print(tostring(self.isTweenToggle:GetComponent("UnityEngine.UI.Toggle").isOn))
		if self.isTweenToggle:GetComponent("UnityEngine.UI.Toggle").isOn == true then
			local childNum = self.flyBar.parent.childCount
			for i = 1, childNum - 1 do
				GameObject.Destroy(self.flyBar.parent:GetChild(i).gameObject)
			end 
			self.isTweenToggle:Find("InputField").gameObject:SetActive(true)
			self.isTweenToggle:Find("Confirm").gameObject:SetActive(true)
			self.isSingleCheck = false
		else
			self.isTweenToggle:Find("InputField").gameObject:SetActive(false)
			self.isTweenToggle:Find("Confirm").gameObject:SetActive(false)
			self:DoIsTweenGroup(1)
			self.isSingleCheck = true
		end
	end
	listener.onPointerClick = EventTriggerProxy.PointerEventDelegate(callback_isTweenToggle, self)

	listener = EventTriggerProxy.Get(self.confirmTweenGroupButton.gameObject)
	local callback_confirmTweenGroup = function(self, e)
		local num = tonumber(self.isTweenToggle:Find("InputField"):GetComponent("UnityEngine.UI.InputField").text)
		assert(num ~= 0, "TweenGroup Number can't be 0 !")
		self:DoIsTweenGroup(num)
	end
	listener.onPointerClick = EventTriggerProxy.PointerEventDelegate(callback_confirmTweenGroup, self)

end

function UITweenTool:Start()
	-- body
	self:DoIsTweenGroup(1)
end

function UITweenTool:SelectTween(tweenName, flyBar)
	-- body
	for i = 0, flyBar.childCount-1 do
		if flyBar:GetChild(i).name ~= "TitleBar" and flyBar:GetChild(i).name ~= "ButtonZone" then
			flyBar:GetChild(i).gameObject:SetActive(false)
		end
	end
	if tweenName ~= "None" then
		flyBar:Find(tweenName).gameObject:SetActive(true)
	end
end

function UITweenTool:Init()
	-- body
	if self.this.transforms ~= nil and self.this.transforms.Length >= 1 then
		for i = 1, self.this.transforms.Length do
			self.mTrans[i] = self.this.transforms[i - 1]
		end
	end
end

function UITweenTool:DoIsTweenGroup(maxGroup)
	-- body

	local childNum = self.flyBar.parent.childCount
	for i = 1, childNum - 1 do
		GameObject.Destroy(self.flyBar.parent:GetChild(i).gameObject)
	end 


	local go
	local listener
	self.tweenGroup = {}
	for i = 1, maxGroup do
		go = GameObject.Instantiate(self.flyBar)
		go.name = i
		go.transform:SetParent(self.flyBar.parent)
		go.transform.localPosition = Vector3.zero
		go.transform.localScale = Vector3.one
		go.gameObject:SetActive(true)
		table.insert(self.tweenGroup, go.gameObject.transform)
		listener = EventTriggerProxy.Get(go.gameObject)
		local callback_everyWindow = function(self, e)
			for k = 1, #self.tweenGroup do
				self.tweenGroup[k]:Find("Light").gameObject:SetActive(true)
			end
			self.tweenGroup[i]:Find("Light").gameObject:SetActive(true)



		end
		listener.onPointerClick = EventTriggerProxy.PointerEventDelegate(callback_everyWindow, self)

		local listener
		listener = EventTriggerProxy.Get(self.tweenGroup[i]:Find("TitleBar/Dropdown").gameObject)
		local callback_dropdown = function(self, e)
			if self.tweenGroup[i]:Find("TitleBar/Dropdown"):GetComponent("UnityEngine.UI.Dropdown").value ~= self.dropValueCheck then
				local dropValueCheck = self.tweenGroup[i]:Find("TitleBar/Dropdown"):GetComponent("UnityEngine.UI.Dropdown").value
				local dropNameCheck = self.tweenGroup[i]:Find("TitleBar/Dropdown"):GetComponent("UnityEngine.UI.Dropdown").options[dropValueCheck].text
				self:SelectTween(dropNameCheck, self.tweenGroup[i])
				self.flyBarTbl[self.tweenGroup[i].name] = {
					dropValueCheck = dropValueCheck,
					flyBar = self.tweenGroup[i],
				}
			end
		end
		listener.onSelect = EventTriggerProxy.BaseEventDelegate(callback_dropdown, self)	

		listener = EventTriggerProxy.Get(self.tweenGroup[i]:Find("ButtonZone/Generate").gameObject)
		local callback_generate = function(self, e)
			--print("Click GenerateButton")
			local dropValueCheck = self.tweenGroup[i]:Find("TitleBar/Dropdown"):GetComponent("UnityEngine.UI.Dropdown").value
			self:GenerateTweenCode(dropValueCheck, self.tweenGroup[i])
		end
		listener.onPointerClick = EventTriggerProxy.PointerEventDelegate(callback_generate, self)

		listener = EventTriggerProxy.Get(self.tweenGroup[i]:Find("ButtonZone/SingleStart").gameObject)
		local callback_generate = function(self, e)
			local dropValueCheck = self.tweenGroup[i]:Find("TitleBar/Dropdown"):GetComponent("UnityEngine.UI.Dropdown").value
			self:DoTween(dropValueCheck, self.tweenGroup[i], true)
		end
		listener.onPointerClick = EventTriggerProxy.PointerEventDelegate(callback_generate, self)

	end
end

function UITweenTool:DoTween(dropValueCheck, flyBar, isSingle)
	-- body
	local style = flyBar:Find(self.tweenTbl[dropValueCheck] .. "/Style/Dropdown"):GetComponent("UnityEngine.UI.Dropdown").options[flyBar:Find("UITweenPosition/Style/Dropdown"):GetComponent("UnityEngine.UI.Dropdown").value].text
	local delay = tonumber(flyBar:Find(self.tweenTbl[dropValueCheck] .. "/Delay/InputField"):GetComponent("UnityEngine.UI.InputField").text)
	local duration = tonumber(flyBar:Find(self.tweenTbl[dropValueCheck] .. "/Duration/InputField"):GetComponent("UnityEngine.UI.InputField").text)
	local method = flyBar:Find(self.tweenTbl[dropValueCheck] .. "/Method/Dropdown"):GetComponent("UnityEngine.UI.Dropdown").options[flyBar:Find(self.tweenTbl[dropValueCheck] .. "/Style/Dropdown"):GetComponent("UnityEngine.UI.Dropdown").value].text
	local from
	if dropValueCheck == 1 or dropValueCheck == 2 or dropValueCheck == 5 then
		from = Vector3.New(tonumber(flyBar:Find(self.tweenTbl[dropValueCheck] .. "/From/X"):GetComponent("UnityEngine.UI.InputField").text), 
					tonumber(flyBar:Find(self.tweenTbl[dropValueCheck] .. "/From/Y"):GetComponent("UnityEngine.UI.InputField").text),
					tonumber(flyBar:Find(self.tweenTbl[dropValueCheck] .. "/From/Z"):GetComponent("UnityEngine.UI.InputField").text) )
	elseif dropValueCheck == 3 then
		from = tonumber(flyBar:Find(self.tweenTbl[dropValueCheck] .. "/From/InputField"):GetComponent("UnityEngine.UI.InputField").text)
	elseif dropValueCheck == 4 then
		from = Color.New(tonumber(flyBar:Find(self.tweenTbl[dropValueCheck] .. "/From/R"):GetComponent("UnityEngine.UI.InputField").text), 
					tonumber(flyBar:Find(self.tweenTbl[dropValueCheck] .. "/From/G"):GetComponent("UnityEngine.UI.InputField").text),
					tonumber(flyBar:Find(self.tweenTbl[dropValueCheck] .. "/From/B"):GetComponent("UnityEngine.UI.InputField").text), 
					tonumber(flyBar:Find(self.tweenTbl[dropValueCheck] .. "/From/A"):GetComponent("UnityEngine.UI.InputField").text))
	end

	local to 
	if dropValueCheck == 1 or dropValueCheck == 2 or dropValueCheck == 5 then
		to = Vector3.New(tonumber(flyBar:Find(self.tweenTbl[dropValueCheck] .. "/To/X"):GetComponent("UnityEngine.UI.InputField").text), 
					tonumber(flyBar:Find(self.tweenTbl[dropValueCheck] .. "/To/Y"):GetComponent("UnityEngine.UI.InputField").text),
					tonumber(flyBar:Find(self.tweenTbl[dropValueCheck] .. "/To/Z"):GetComponent("UnityEngine.UI.InputField").text))
	elseif dropValueCheck == 3 then
		to = tonumber(flyBar:Find(self.tweenTbl[dropValueCheck] .. "/To/InputField"):GetComponent("UnityEngine.UI.InputField").text)
	elseif dropValueCheck == 4 then
		to = Color.New(tonumber(flyBar:Find(self.tweenTbl[dropValueCheck] .. "/To/R"):GetComponent("UnityEngine.UI.InputField").text), 
					tonumber(flyBar:Find(self.tweenTbl[dropValueCheck] .. "/To/G"):GetComponent("UnityEngine.UI.InputField").text),
					tonumber(flyBar:Find(self.tweenTbl[dropValueCheck] .. "/To/B"):GetComponent("UnityEngine.UI.InputField").text), 
					tonumber(flyBar:Find(self.tweenTbl[dropValueCheck] .. "/To/A"):GetComponent("UnityEngine.UI.InputField").text))		
	end

	local addition = flyBar:Find(self.tweenTbl[dropValueCheck] .. "/Addition/Toggle"):GetComponent("UnityEngine.UI.Toggle").isOn
	local groupNum = tonumber(flyBar:Find(self.tweenTbl[dropValueCheck] .. "/Group/InputField"):GetComponent("UnityEngine.UI.InputField").text)
	local pos = tonumber(flyBar:Find(self.tweenTbl[dropValueCheck] .. "/Pos/InputField"):GetComponent("UnityEngine.UI.InputField").text)
	local tweenObjectNum = tonumber(flyBar:Find(self.tweenTbl[dropValueCheck] .. "/TweenObject/InputField"):GetComponent("UnityEngine.UI.InputField").text)

	local newTweener
	if self.groupNum[groupNum] == nil and not isSingle then     --group相同的，表示要按照先后顺序在group编号相同的tweener中进行；group不同的则会各自播放互不干扰
		newTweener = UITweenLoader.New()
		self.groupNum[groupNum] = newTweener
	elseif self.groupNum[groupNum] ~= nil and not isSingle then
		newTweener = self.groupNum[groupNum]
	end

	local tbl = {
		from = from,
		to = to,
		mTrans = self.mTrans[tweenObjectNum],
		delay = delay,
		duration = duration,
		style = style,
		method = method,

		onTweenFinishDo = {},
	}

	if isSingle then
		newTweener = UITweenLoader.New()
		newTweener:Init(tbl, self.tweenTbl[dropValueCheck])
	else
		local groupTweenTbl = {
								tweenName = self.tweenTbl[dropValueCheck], 
								tbl = tbl
								}
		newTweener:SetTweenQueue(groupTweenTbl)
	end

	if not isSingle and tonumber(flyBar.name) == #self.tweenGroup then
		for k, v in pairs(self.groupNum) do
			v:PlayTweenQueue()
		end
	end	
end

function UITweenTool:GetValueFillInputField(inputfileType, flyBar, fromOrTo)	--inputfieldType：1.UITweenPosition; 2.UITweenRotation; 3.UITweenAlpha; 4.UITweenColor; 5.UITweenScale
															--
	-- body

	local input = flyBar:Find(self.tweenTbl[inputfileType] .. "/TweenObject/InputField"):GetComponent("UnityEngine.UI.InputField").text
	assert(input ~= "", "Need to appoint a GameObject ! ")
	
	local tweenObjectNum = tonumber(input)
	local mTrans = self.mTrans[tweenObjectNum]

	if inputfileType == 1 then
		local worldSpace = flyBar:Find(self.tweenTbl[inputfileType] .. "/WorldSpace/Toggle"):GetComponent("UnityEngine.UI.InputField").isOn
		if worldSpace then
			flyBar:Find(self.tweenTbl[inputfileType] .. "/" .. fromOrTo .. "/X"):GetComponent("UnityEngine.UI.InputField").text = mTrans.position.x
			flyBar:Find(self.tweenTbl[inputfileType] .. "/" .. fromOrTo .. "/Y"):GetComponent("UnityEngine.UI.InputField").text = mTrans.position.y
			flyBar:Find(self.tweenTbl[inputfileType] .. "/" .. fromOrTo .. "/Z"):GetComponent("UnityEngine.UI.InputField").text = mTrans.position.z
		else
			flyBar:Find(self.tweenTbl[inputfileType] .. "/" .. fromOrTo .. "/X"):GetComponent("UnityEngine.UI.InputField").text = mTrans.localPosition.x
			flyBar:Find(self.tweenTbl[inputfileType] .. "/" .. fromOrTo .. "/Y"):GetComponent("UnityEngine.UI.InputField").text = mTrans.localPosition.y
			flyBar:Find(self.tweenTbl[inputfileType] .. "/" .. fromOrTo .. "/Z"):GetComponent("UnityEngine.UI.InputField").text = mTrans.localPosition.z
		end
	elseif inputfileType == 2 then
		flyBar:Find(self.tweenTbl[inputfileType] .. "/" .. fromOrTo .. "/X"):GetComponent("UnityEngine.UI.InputField").text = mTrans.rotation.x
		flyBar:Find(self.tweenTbl[inputfileType] .. "/" .. fromOrTo .. "/Y"):GetComponent("UnityEngine.UI.InputField").text = mTrans.rotation.y
		flyBar:Find(self.tweenTbl[inputfileType] .. "/" .. fromOrTo .. "/Z"):GetComponent("UnityEngine.UI.InputField").text = mTrans.rotation.z	
	elseif inputfileType == 3 then
		flyBar:Find(self.tweenTbl[inputfileType] .. "/" .. fromOrTo .. "/Alpha"):GetComponent("UnityEngine.UI.InputField").text = mTrans:GetComponent("UnityEngine.UI.Image").color.a
	elseif inputfileType == 4 then
		flyBar:Find(self.tweenTbl[inputfileType] .. "/" .. fromOrTo .. "/R"):GetComponent("UnityEngine.UI.InputField").text = mTrans:GetComponent("UnityEngine.UI.Image").color.r
		flyBar:Find(self.tweenTbl[inputfileType] .. "/" .. fromOrTo .. "/G"):GetComponent("UnityEngine.UI.InputField").text = mTrans:GetComponent("UnityEngine.UI.Image").color.g
		flyBar:Find(self.tweenTbl[inputfileType] .. "/" .. fromOrTo .. "/B"):GetComponent("UnityEngine.UI.InputField").text = mTrans:GetComponent("UnityEngine.UI.Image").color.b
		flyBar:Find(self.tweenTbl[inputfileType] .. "/" .. fromOrTo .. "/A"):GetComponent("UnityEngine.UI.InputField").text = mTrans:GetComponent("UnityEngine.UI.Image").color.a
	end
end

function UITweenTool:GenerateTweenCode(dropValueCheck, flyBar)
	-- body

	local style = flyBar:Find(self.tweenTbl[dropValueCheck] .. "/Style/Dropdown"):GetComponent("UnityEngine.UI.Dropdown").options[flyBar:Find("UITweenPosition/Style/Dropdown"):GetComponent("UnityEngine.UI.Dropdown").value].text
	--assert(style ~= "None", "Style must not be None, please chose another style Option")

	local delay = flyBar:Find(self.tweenTbl[dropValueCheck] .. "/Delay/InputField"):GetComponent("UnityEngine.UI.InputField").text
	assert(delay ~= "", "Delay is null or not a int value")
	delay = tonumber(delay)

	local duration = flyBar:Find(self.tweenTbl[dropValueCheck] .. "/Duration/InputField"):GetComponent("UnityEngine.UI.InputField").text
	assert(duration ~= "", "Duration is null or not a int value")
	duration = tonumber(duration)

	local method = flyBar:Find(self.tweenTbl[dropValueCheck] .. "/Method/Dropdown"):GetComponent("UnityEngine.UI.Dropdown").options[flyBar:Find("UITweenPosition/Style/Dropdown"):GetComponent("UnityEngine.UI.Dropdown").value].text
	--assert(method ~= "None", "Method must not be None, please chose another method Option")

	local from_1, from_2, from_2, from_4, to_1, to_2, to_3, to_4, from, to, str
	if dropValueCheck == 1 or dropValueCheck == 2 or dropValueCheck == 5 then    --Vector3

		from_1 = flyBar:Find(self.tweenTbl[dropValueCheck] .. "/From/X"):GetComponent("UnityEngine.UI.InputField").text
		assert(from_1 ~= "", "From : x , is null or not a number value")
		from_1 = tonumber(from_1)

		from_2 = flyBar:Find(self.tweenTbl[dropValueCheck] .. "/From/Y"):GetComponent("UnityEngine.UI.InputField").text
		assert(from_2 ~= "", "From : y , is null or not a number value")
		from_2 = tonumber(from_2)

		from_3 = flyBar:Find(self.tweenTbl[dropValueCheck] .. "/From/Z"):GetComponent("UnityEngine.UI.InputField").text
		assert(from_3 ~= "", "From : z , is null or not a number value")
		from_3 = tonumber(from_3)

		to_1 = flyBar:Find(self.tweenTbl[dropValueCheck] .. "/To/X"):GetComponent("UnityEngine.UI.InputField").text
		assert(to_1 ~= "", "To : x , is null or not a number value")
		to_1 = tonumber(to_1)

		to_2 = flyBar:Find(self.tweenTbl[dropValueCheck] .. "/To/Y"):GetComponent("UnityEngine.UI.InputField").text
		assert(to_2 ~= "", "To : y , is null or not a number value")
		to_2 = tonumber(to_2)

		to_3 = flyBar:Find(self.tweenTbl[dropValueCheck] .. "/To/Z"):GetComponent("UnityEngine.UI.InputField").text
		assert(to_3 ~= "", "To : z , is null or not a number value")
		to_3 = tonumber(to_3)

		str = 
			"\nself.newTweener = UITweenLoader.New()" .. "\n" .. 
			string.format("local tbl = {\n" .. 
							"\tfrom = Vector3.New(%.1f, %.1f, %.1f),\n" ..
							"\tto = Vector3.New(%.1f, %.1f, %.1f),\n" .. 
							"\tmTrans = \"\",\n" ..
							"\tstyle = \"%s\",\n" .. 
							"\tmethod = \"%s\",\n" .. 
							"\tdelay = %.1f,\n" .. 
							"\tduration = %.1f,\n" .. 
							"\tonTweenFinishDo = {},\n" .. "}\n", 
							from_1, from_2, from_3, to_1, to_2, to_3, "Once", "Linear", delay, duration) .. 
			"--table.insert(tbl.onTweenFinishDo,        )\n" ..
			string.format("self.newTween:Init(tbl, \"%s\") \n\n", "UITweenPosition")

	elseif dropValueCheck == 3 then				--Alpha

		from_1 = flyBar:Find(self.tweenTbl[dropValueCheck] .. "/From/InputField"):GetComponent("UnityEngine.UI.InputField").text
		assert(from_1 ~= "", "From : alpha is null or not a number value")
		from_1 = tonumber(from_1)

		to_1 = flyBar:Find(self.tweenTbl[dropValueCheck] .. "/From/InputField"):GetComponent("UnityEngine.UI.InputField").text
		assert(to_1 ~= "", "To : alpha is null or not a number value")
		to_1 = tonumber(to_1)

		str = 
			"\nself.newTweener = UITweenLoader.New()" .. "\n" .. 
			string.format("local tbl = {\n" .. 
							"\tfrom = %.1f,\n" ..
							"\tto = %.1f,\n" .. 
							"\tmTrans = \"\",\n" ..
							"\tstyle = \"%s\",\n" .. 
							"\tmethod = \"%s\",\n" .. 
							"\tdelay = %.1f,\n" .. 
							"\tduration = %.1f,\n" .. 
							"\tonTweenFinishDo = {},\n" .. "}\n", 
							from_1, to_1, "Once", "Linear", delay, duration) .. 
			"--table.insert(tbl.onTweenFinishDo,        )\n" ..
			string.format("self.newTween:Init(tbl, \"%s\") \n\n", "UITweenPosition")

	elseif dropValueCheck == 4 then             --Color

		from_1 = flyBar:Find(self.tweenTbl[dropValueCheck] .. "/From/R"):GetComponent("UnityEngine.UI.InputField").text
		assert(from_1 ~= "", "From : r , is null or not a number value")
		from_1 = tonumber(from_1)

		from_2 = flyBar:Find(self.tweenTbl[dropValueCheck] .. "/From/G"):GetComponent("UnityEngine.UI.InputField").text
		assert(from_2 ~= "", "From : g , is null or not a number value")
		from_2 = tonumber(from_2)

		from_3 = flyBar:Find(self.tweenTbl[dropValueCheck] .. "/From/B"):GetComponent("UnityEngine.UI.InputField").text
		assert(from_3 ~= "", "From : b , is null or not a number value")
		from_3 = tonumber(from_3)

		from_4 = flyBar:Find(self.tweenTbl[dropValueCheck] .. "/From/A"):GetComponent("UnityEngine.UI.InputField").text
		assert(from_4 ~= "", "From : a , is null or not a number value")
		from_4 = tonumber(from_4)

		to_1 = flyBar:Find(self.tweenTbl[dropValueCheck] .. "/To/R"):GetComponent("UnityEngine.UI.InputField").text
		assert(to_1 ~= "", "To : r , is null or not a number value")
		to_1 = tonumber(to_1)

		to_2 = flyBar:Find(self.tweenTbl[dropValueCheck] .. "/To/G"):GetComponent("UnityEngine.UI.InputField").text
		assert(to_2 ~= "", "To : g , is null or not a number value")
		to_2 = tonumber(to_2)

		to_3 = flyBar:Find(self.tweenTbl[dropValueCheck] .. "/To/B"):GetComponent("UnityEngine.UI.InputField").text
		assert(to_3 ~= "", "To : b , is null or not a number value")
		to_3 = tonumber(to_3)

		to_4 = flyBar:Find(self.tweenTbl[dropValueCheck] .. "/To/A"):GetComponent("UnityEngine.UI.InputField").text
		assert(to_4 ~= "", "To : a , is null or not a number value")
		to_4 = tonumber(to_4)

		str = 
			"\nself.newTweener = UITweenLoader.New()" .. "\n" .. 
			string.format("local tbl = {\n" .. 
							"\tfrom = Color.New(%.1f, %.1f, %.1f, %.1f),\n" ..
							"\tto = Color.New(%.1f, %.1f, %.1f, %.1f),\n" .. 
							"\tmTrans = \"\",\n" ..
							"\tstyle = \"%s\",\n" .. 
							"\tmethod = \"%s\",\n" .. 
							"\tdelay = %.1f,\n" .. 
							"\tduration = %.1f,\n" .. 
							"\tonTweenFinishDo = {},\n" .. "}\n", 
							from_1, from_2, from_3, from_4, to_1, to_2, to_3, to_4, "Once", "Linear", delay, duration) .. 
			"--table.insert(tbl.onTweenFinishDo,        )\n" ..
			string.format("self.newTween:Init(tbl, \"%s\") \n\n", "UITweenPosition")

	end

	print(str)

	local copyText = UnityEngine.TextEditor.New()
	copyText.text = str
	copyText:SelectAll()
	copyText:Copy()



end

function UITweenTool:OnDestroy()
	-- body
	self.this = nil
	self = nil
end