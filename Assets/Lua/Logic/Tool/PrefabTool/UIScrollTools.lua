require "System.Global"

class("UIScrollTools")

function UIScrollTools:Awake(this)
	-- body
	self.this = this
	self.scroll = self.this.transforms[0]
	self.grid = self.scroll:Find("Content/Grid")
	self.scrollBar = self.scroll:Find("ScrollBar")
	self.sample = self.scroll:Find("Sample")
	if self.sample.childCount > 0 then
		self.item = self.sample:GetChild(0)
	end
	self.itemCount = 0
	self.current = 0
	self.max = 0
	self.currentIndexLow = 0
	self.currentIndexHigh = 0


end

function UIScrollTools:Start()
	-- body
end

function UIScrollTools:GetItem(sample)
	-- body
	self.item = sample
end

function UIScrollTools:Init(isVertical, rc, toTop, toBottom, toLeft, toRight, spaceX, spaceY)
	-- body
	if self.item ~= nil then
		local width = self.item:GetComponent("RectTransform").rect.width
		local height = self.item:GetComponent("RectTransform").rect.height
		local scrollWidth = self.scroll:GetComponent("RectTransform").rect.width
		local scrollHeight = self.scroll:GetComponent("RectTransform").rect.height
		
		assert(rc ~= nil and rc > 0, "column or raw is nil or smaller than 0")

		if toTop == nil then
			toTop = 0
		end

		if toBottom == nil then
			toBottom = 0
		end

		if toLeft == nil then
			toLeft = 0
		end

		if toRight == nil then
			toRight = 0
		end

		if spaceX == nil then
			if rc > 1 then
				spaceX = math.floor((scrollWidth - width * rc - toLeft - toRight) / (rc - 1))
			else
				spaceX = 0
			end
		end

		if spaceY == nil then
			spaceY = spaceX
		end

		self.grid:GetComponent("GridLayoutGroup").padding.left = toLeft
		self.grid:GetComponent("GridLayoutGroup").padding.right = toRight
		self.grid:GetComponent("GridLayoutGroup").padding.top = toTop
		self.grid:GetComponent("GridLayoutGroup").padding.bottom = toBottom
		self.grid:GetComponent("GridLayoutGroup").spacing = Vector2.New(spaceX, spaceY)
		self.containRaw = math.floor(scrollHeight / (height + spaceY)) + 1
		self.column = rc
		self.firstLoad = (self.containRaw + 1) * rc


		local trans = {}
		local listener
		listener = EventTriggerProxy.Get(self.scroll.gameObject)
		local callback_drag = function(self, e)
			local cal = math.floor(math.floor(self.grid.localPosition.y - self.beginPosY) / math.floor(height + spaceY / 2))
			trans = {}
			if cal > self.current and rc * self.current < maxNum then
				for i = 0, 3 do
					table.insert(trans, self.grid:GetChild(i))
				end
				for i = 1, #trans do
					trans[i]:SetAsLastSibling()
					self.currentIndexLow = self.currentIndexLow + 1
					self.doItemShowFunc(self.doItemShowFuncSelf, trans, self.itemDataList[self.currentIndexLow])
				end
				self.grid:GetComponent("GridLayoutGroup").padding.top = self.grid:GetComponent("GridLayoutGroup").padding.top + height + spaceY
				self.current = cal

			elseif cal < self.current and cal > 0 then
				for i = 1, 4 do
					table.insert(trans, self.grid:GetChild(i-1))
				end
				for i = 1, #trans do
					trans[i]:SetAsFirstSibling()
				end
				self.grid:GetComponent("GridLayoutGroup").padding.top = self.grid:GetComponent("GridLayoutGroup").padding.top - height - spaceY
				self.current = cal				
			end
		end
		listener.onDrag = EventTriggerProxy.PointerEventDelegate(callback_drag, self)

	end
end

function UIScrollTools:DoScroll(idTbl)
	-- body
	self.itemDataList = idTbl
	local go
	for i = 1, #idTbl do
		if i < self.firstLoad then
			go = GameObject.Instantiate(self.item.gameObject).transform
			go.name = self.item.name .. index
			go:SetParent(self.grid)
			go.localPosition = Vector3.zero
			go.localScale = Vector3.one
			local transTemp = go

			self.doItemShowFunc(self.doItemShowFuncSelf, transTemp, idTbl[i])
		end
	end
	self.currentIndexLow = (self.containRaw - 1) * self.column + 1
--[[
	local tbl = {}
	tbl.trans = go
	local imageList = go:GetComponentsInChildren(LuaScript.GetType("UnityEngine.UI.Image"), true)
	for i = 0, imageList.Length - 1 do
		if tbl[imageList[i].name] == nil then
			tbl[imageList[i].name] = {}
		end
		tbl[imageList[i].name]["trans"] = imageList[i]:GetComponent(LuaScript.GetType("Transform"))
		tbl[imageList[i].name]["image"] = imageList[i]:GetComponent("Image")
	end

	local buttonList = go:GetComponentsInChildren(LuaScript.GetType("UnityEngine.UI.Button"), true)
	for i = 0, buttonList.Length - 1 do
		if tbl[buttonList[i].name] == nil then
			tbl[buttonList[i].name] = {}
		end
		tbl[buttonList[i].name]["button"] = buttonList[i]:GetComponent("Button")
	end

	local textList = go:GetComponentsInChildren(LuaScript.GetType("UnityEngine.UI.Text"), true)
	for i = 0, textList.Length - 1 do
		if tbl[textList[i].name] == nil then
			tbl[textList[i].name] = {}
		end
		tbl[textList[i].name]["trans"] = textList[i]:GetComponent(LuaScript.GetType("Transform"))
		tbl[textList[i].name]["text"] = textList[i]:GetComponent("Text")
	end	
]]

	


	self.beginPosY = self.grid.localPosition.y

	return tbl
end

function UIScrollTool:DoItemShow(func, funcSelf, clickFunc, clickFuncSelf)
	-- body
	self.doItemShowFunc = func
	self.doItemShowFuncSelf = funcSelf
end

function UIScrollTools:OnClickEvent(buttonObj, func, funcSelf, ...)
	-- body
	local param = {...}
	local function Temp()
		-- body
		func(funcSelf, unpack(param))
	end
	local action = UnityEngine.Events.UnityAction(Temp, self)
	buttonObj.onClick:AddListener(action)
end

function UIScrollTools:OnDestroy()
	-- body
	self.this = nil
	self = nil
end