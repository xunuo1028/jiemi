require "System.Global"

class("UIScrollTools")

function UIScrollTools:Awake(this)
	-- body
	self.this = this
	self.scroll = self.this.transforms[0]
	self.grid = self.scroll:Find("Content/Grid")
	self.vScrollBar = self.scroll:Find("VerticalScrollBar")
	self.hScrollBar = self.scroll:Find("HorizontalScrollBar")
	self.sample = self.scroll:Find("Sample")
	if self.sample.childCount > 0 then
		self.item = self.sample:GetChild(0)
	end
	self.itemCount = 0
	self.current = 0
	self.max = 0
	self.currentIndexLow = 0
	self.currentIndexHigh = 0
	self.isVertical = true


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

		self.isVertical = isVertical

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
		if isVertical then
			self.containRaw = math.floor(scrollHeight / (height + spaceY)) + 1
			self.scroll:GetComponent("ScrollRect").vertical = true
			self.scroll:GetComponent("ScrollRect").horizontal = false
			self.grid:GetComponent("ContentSizeFitter").horizontalFit = UnityEngine.UI.ContentSizeFitter.FitMode.Unconstrained
			self.grid:GetComponent("ContentSizeFitter").verticalFit = UnityEngine.UI.ContentSizeFitter.FitMode.PreferredSize
			self.vScrollBar:GetComponent("Scrollbar").direction = UnityEngine.UI.Scrollbar.Direction.BottomToTop
			self.grid:GetComponent("GridLayoutGroup").startAxis = UnityEngine.UI.GridLayoutGroup.Axis.Horizontal
		else
			self.containRaw = math.floor(scrollWidth / (width + spaceX)) + 1
			self.scroll:GetComponent("ScrollRect").vertical = false
			self.scroll:GetComponent("ScrollRect").horizontal = true
			self.grid:GetComponent("ContentSizeFitter").horizontalFit = UnityEngine.UI.ContentSizeFitter.FitMode.PreferredSize
			self.grid:GetComponent("ContentSizeFitter").verticalFit = UnityEngine.UI.ContentSizeFitter.FitMode.Unconstrained
			self.vScrollBar:GetComponent("Scrollbar").direction = UnityEngine.UI.Scrollbar.Direction.RightToLeft
			self.grid:GetComponent("GridLayoutGroup").startAxis = UnityEngine.UI.GridLayoutGroup.Axis.Vertical
		end
		self.column = rc
		self.firstLoad = (self.containRaw + 1) * rc


		local trans = {}
		local isFinish = true
		self.activeCount = 0

		local function DoMoreItems()
			-- body
			local cal 
			if isVertical then
				cal = math.floor(math.floor(self.grid.localPosition.y - self.beginPosY) / math.floor(height + spaceY))
			else
				cal = math.floor(math.floor(self.beginPosX - self.grid.localPosition.x) / math.floor(width + spaceX))
			end
			
			local function DoDown(tempCal, isLast)
				-- body
				trans = {}
				if isFinish and rc * (self.current + self.containRaw + 1) < self.max then
					isFinish = false
					for i = 0, rc - 1 do
						table.insert(trans, self.grid:GetChild(i))
					end
					for i = 1, #trans do
						trans[i]:SetAsLastSibling()
						if self.currentIndexLow < self.max then
							self.currentIndexLow = self.currentIndexLow + 1
						else
							trans[i].gameObject:SetActive(false)
							self.activeCount = self.activeCount + 1
						end
						--print(#trans .. " " .. self.currentIndexLow)	
					end
					self.currentIndexHigh = self.currentIndexHigh + rc

					if isLast  then
						for i = self.currentIndexHigh, self.currentIndexLow do
							local idx = i - self.currentIndexHigh
							self.doItemShowFunc(self.doItemShowFuncSelf, self.grid:GetChild(idx), self.itemDataList[i], i)
							if self.doItemClickEventFunc ~= nil then
								self.doItemClickEventFunc(self.doItemClickEventFuncSelf, self.grid:GetChild(idx), self.itemDataList[i], i)
							end
						end
					end

					self.grid:GetComponent("GridLayoutGroup").padding.top = self.grid:GetComponent("GridLayoutGroup").padding.top + height + spaceY
					self.grid:GetComponent("GridLayoutGroup").padding.bottom = self.grid:GetComponent("GridLayoutGroup").padding.bottom - height - spaceY
					self.current = tempCal
					isFinish = true
				end
			end

			local function DoUp(isLast)
				-- body
				trans = {}
				if isFinish then
					isFinish = false
					self.currentIndexLow = self.currentIndexLow - rc + self.activeCount
					for i = 1, rc do
						table.insert(trans, self.grid:GetChild(self.grid.childCount - i))
						if self.activeCount > 0 then
							self.grid:GetChild(self.grid.childCount - i).gameObject:SetActive(true)
							self.activeCount = self.activeCount - 1
						end
					end
					for i = 1, #trans do
						trans[i]:SetAsFirstSibling()
						self.currentIndexHigh = self.currentIndexHigh - 1
					end
					

					if isLast then
						for i = self.currentIndexHigh, self.currentIndexLow do
							local idx = i - self.currentIndexHigh
							self.doItemShowFunc(self.doItemShowFuncSelf, self.grid:GetChild(idx), self.itemDataList[i], i)
							if self.doItemClickEventFunc ~= nil then
								self.doItemClickEventFunc(self.doItemClickEventFuncSelf, self.grid:GetChild(idx), self.itemDataList[i], i)
							end
						end
					end
					self.grid:GetComponent("GridLayoutGroup").padding.top = self.grid:GetComponent("GridLayoutGroup").padding.top - height - spaceY
					self.grid:GetComponent("GridLayoutGroup").padding.bottom = self.grid:GetComponent("GridLayoutGroup").padding.bottom + height + spaceY
					self.current = cal
					isFinish = true
				end					
			end

			local function DoRight(tempCal, isLast)
				-- body
				trans = {}
				if isFinish and rc * (self.current + self.containRaw + 1) < self.max then
					isFinish = false
					for i = 0, rc - 1 do
						table.insert(trans, self.grid:GetChild(i))
					end
					for i = 1, #trans do
						trans[i]:SetAsLastSibling()
						if self.currentIndexLow < self.max then
							self.currentIndexLow = self.currentIndexLow + 1
						else
							trans[i].gameObject:SetActive(false)
							self.activeCount = self.activeCount + 1
						end
						--print(#trans .. " " .. self.currentIndexLow)
					end
					self.currentIndexHigh = self.currentIndexHigh + rc

					if isLast then
						for i = self.currentIndexHigh, self.currentIndexLow do
							local idx = i - self.currentIndexHigh
							self.doItemShowFunc(self.doItemShowFuncSelf, self.grid:GetChild(idx), self.itemDataList[i], i)
							if self.doItemClickEventFunc ~= nil then
								self.doItemClickEventFunc(self.doItemClickEventFuncSelf, self.grid:GetChild(idx), self.itemDataList[i], i)
							end
						end
					end


					self.grid:GetComponent("GridLayoutGroup").padding.left = self.grid:GetComponent("GridLayoutGroup").padding.left + width + spaceX
					self.grid:GetComponent("GridLayoutGroup").padding.right = self.grid:GetComponent("GridLayoutGroup").padding.right - width - spaceX
					self.current = tempCal
					isFinish = true
				end
			end

			local function DoLeft(isLast)
				-- body
				trans = {}
				if isFinish then
					isFinish = false
					self.currentIndexLow = self.currentIndexLow - rc + self.activeCount
					for i = 1, rc do
						table.insert(trans, self.grid:GetChild(self.grid.childCount - i))
						if self.activeCount > 0 then
							self.grid:GetChild(self.grid.childCount - i).gameObject:SetActive(true)
							self.activeCount = self.activeCount - 1							
						end
					end
					for i = 1, #trans do
						trans[i]:SetAsFirstSibling()
						self.currentIndexHigh = self.currentIndexHigh - 1
					end

					if isLast then
						for i = self.currentIndexHigh, self.currentIndexLow do
							local idx = i - self.currentIndexHigh
							self.doItemShowFunc(self.doItemShowFuncSelf, self.grid:GetChild(idx), self.itemDataList[i], i)
							if self.doItemClickEventFunc ~= nil then							
								self.doItemClickEventFunc(self.doItemClickEventFuncSelf, self.grid:GetChild(idx), self.itemDataList[i], i)
							end
						end
					end



					self.grid:GetComponent("GridLayoutGroup").padding.left = self.grid:GetComponent("GridLayoutGroup").padding.left - width - spaceX
					self.grid:GetComponent("GridLayoutGroup").padding.right = self.grid:GetComponent("GridLayoutGroup").padding.right + width + spaceX
					self.current = cal
					isFinish = true
				end					
			end

			if isVertical then
				if cal < 0 and self.current > 0 then
					cal = 0
				end

				if cal > self.current then
					for i = self.current + 1, cal do
						if i ~= cal then
							DoDown(i, false)
						else
							DoDown(i, true)
						end
					end
				elseif cal < self.current and cal >= 0 then
					local currentTemp = self.current
					for i = currentTemp, cal + 1, -1 do
						if i ~= cal + 1 then
							DoUp(false)
						else
							DoUp(true)
						end
					end
				end
			else
				if cal < 0 and self.current > 0 then
					cal = 0
				end


				--print("sdfsdfd " .. cal .. " " .. self.current)
				if cal > self.current then
					for i = self.current + 1, cal do
						if i ~= cal then
							DoRight(i, false)
						else
							DoRight(i, true)
						end
					end
				elseif cal < self.current and cal >= 0 then
					local currentTemp = self.current
					for i = currentTemp, cal + 1, -1 do
						if i ~= cal + 1 then
							DoLeft(false)
						else
							DoLeft(true)
						end
					end
				end				
			end			
		end

		local listener
		listener = EventTriggerProxy.Get(self.scroll.gameObject)
		local callback_drag = function(self, e)
			DoMoreItems()
		end
		listener.onDrag = EventTriggerProxy.PointerEventDelegate(callback_drag, self)

		if isVertical then
			local scroll_Component = self.vScrollBar:GetComponent("Scrollbar")
			local action = UnityEngine.Events.UnityAction_float(DoMoreItems, self)
			scroll_Component.onValueChanged:AddListener(action)
		else
			local scroll_Component = self.hScrollBar:GetComponent("Scrollbar")
			local action = UnityEngine.Events.UnityAction_float(DoMoreItems, self)
			scroll_Component.onValueChanged:AddListener(action)			
		end

	end
end

function UIScrollTools:DoScroll(idTbl)
	-- body
	local reset = false
	for i = 1, self.grid.childCount do
		GameObject.Destroy(self.grid:GetChild(i - 1).gameObject)
		reset = true
	end
	if reset then
		self.grid:GetComponent("GridLayoutGroup").padding.left = 0
		self.grid:GetComponent("GridLayoutGroup").padding.right = 0
		self.grid:GetComponent("GridLayoutGroup").padding.top = 0
		self.grid:GetComponent("GridLayoutGroup").padding.bottom = 0
		self.grid.localPosition = Vector3.New(self.grid.localPosition.x, self.beginPosY, self.grid.localPosition.z)
		self.current = 0
		print("sdfsdfsdfsdfsdfsdfsdf")
	end
	self.itemDataList = idTbl
	self.max = #idTbl
	local go
	for i = 1, #idTbl do
		if i <= self.firstLoad then
			go = GameObject.Instantiate(self.item.gameObject).transform
			go:SetParent(self.grid)
			go.localPosition = Vector3.zero
			go.localScale = Vector3.one
			local transTemp = go

			self.doItemShowFunc(self.doItemShowFuncSelf, transTemp, idTbl[i], i)
			self.currentIndexLow = i
		end
	end
	self.currentIndexHigh = 1
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

	if self.isVertical then
		local height = self.item:GetComponent("RectTransform").rect.height
		local spaceY = self.grid:GetComponent("GridLayoutGroup").spacing.y
		local leftRawNum = 0
		if math.floor(#idTbl / self.column) == (#idTbl / self.column) then
			leftRawNum = #idTbl / self.column - self.containRaw - 1
		else
			leftRawNum = math.floor(#idTbl / self.column) - self.containRaw
		end
		self.grid:GetComponent("GridLayoutGroup").padding.bottom = self.grid:GetComponent("GridLayoutGroup").padding.bottom + (height + spaceY)*leftRawNum
		self.beginPosY = self.grid.localPosition.y
		self.beginPosX = self.grid.localPosition.x
	else
		local width = self.item:GetComponent("RectTransform").rect.width
		local spaceX = self.grid:GetComponent("GridLayoutGroup").spacing.x
		local leftRawNum = 0
		if math.floor(#idTbl / self.column) == (#idTbl / self.column) then
			leftRawNum = #idTbl / self.column - self.containRaw - 1
		else
			leftRawNum = math.floor(#idTbl / self.column) - self.containRaw
		end
		self.grid:GetComponent("GridLayoutGroup").padding.right = self.grid:GetComponent("GridLayoutGroup").padding.right + (width + spaceX)*leftRawNum
		self.beginPosY = self.grid.localPosition.y
		self.beginPosX = self.grid.localPosition.x
	end

	return tbl
end

function UIScrollTools:DoItemShow(func, funcSelf)
	-- body
	self.doItemShowFunc = func
	self.doItemShowFuncSelf = funcSelf
end

function UIScrollTools:OnClickEvent(buttonObj, func, funcSelf, ...)
	-- body
	buttonObj.onClick:RemoveAllListeners()
	local param = {...}
	local function Temp()
		-- body
		func(funcSelf, unpack(param))
	end
	local action = UnityEngine.Events.UnityAction(Temp, self)
	buttonObj.onClick:AddListener(action)
end

function UIScrollTools:ClickEvent(func, funcSelf)
	-- body
	self.doItemClickEventFunc = func
	self.doItemClickEventFuncSelf = funcSelf
end

function UIScrollTools:RemoveNode(index)
	-- body
	for i = #self.itemDataList, 1, -1 do
		if i == index then
			table.remove(self.itemDataList, i)
		end
	end

	self.max = #self.itemDataList

	if index >= self.currentIndexHigh and index <= self.currentIndexLow then
		local node = self:GetNodeByIndex(index)
		node:SetAsLastSibling()
		self.max = #self.itemDataList
		if self.itemDataList[self.currentIndexLow] then
			self.doItemShowFunc(self.doItemShowFuncSelf, self:GetNodeByIndex(self.currentIndexLow), self.itemDataList[self.currentIndexLow], self.currentIndexLow)
		else
			node.gameObject:SetActive(false)
			self.activeCount = self.activeCount + 1
			self.currentIndexLow = self.currentIndexLow - 1
		end
	else

	end

	local height = self.item:GetComponent("RectTransform").rect.height
	local spaceY = self.grid:GetComponent("GridLayoutGroup").spacing.y
	local width = self.item:GetComponent("RectTransform").rect.width
	local spaceX = self.grid:GetComponent("GridLayoutGroup").spacing.x

	if #self.itemDataList % self.column == 0 and self.activeCount ~= self.column then
		if self.isVertical then
			self.grid:GetComponent("GridLayoutGroup").padding.bottom = self.grid:GetComponent("GridLayoutGroup").padding.bottom - height - spaceY
		else
			self.grid:GetComponent("GridLayoutGroup").padding.right = self.grid:GetComponent("GridLayoutGroup").padding.right - width - spaceX
		end
	end


	if self.activeCount == self.column then
		local trans = {}
		for i = 1, self.column do
			table.insert(trans, self.grid:GetChild(self.grid.childCount - i))
		end

		for i = 1, #trans do
			trans[i]:SetAsFirstSibling()
			trans[i].gameObject:SetActive(true)
			self.doItemShowFunc(self.doItemShowFuncSelf, trans[i], self.itemDataList[self.currentIndexHigh - i], self.currentIndexHigh - i)
		end
		if self.isVertical then
			self.grid:GetComponent("GridLayoutGroup").padding.top = self.grid:GetComponent("GridLayoutGroup").padding.top - height - spaceY
		else
			self.grid:GetComponent("GridLayoutGroup").padding.left = self.grid:GetComponent("GridLayoutGroup").padding.left - width - spaceX
		end
		self.current = self.current - 1
		self.currentIndexHigh = self.currentIndexHigh - self.column
		self.activeCount = 0
	end
end

function UIScrollTools:AddNode(index, data)
	-- body
	assert(index - #self.itemDataList <= 1, "index has a little problem")

	if index < #self.itemDataList then
		for i = #self.itemDataList, index, -1 do
			self.itemDataList[i + 1] = self.itemDataList[i]
		end
	end

	self.itemDataList[index] = data

	self.max = #self.itemDataList

	if index < self.currentIndexHigh then
		for i = self.currentIndexHigh, self.currentIndexLow do
			self.doItemShowFunc(self.doItemShowFuncSelf, self.grid:GetChild(i - self.currentIndexHigh), self.itemDataList[i], i)
		end
	elseif index >= self.currentIndexHigh and index <= self.currentIndexLow then
		for i = index, self.currentIndexLow do
			self.doItemShowFunc(self.doItemShowFuncSelf, self.grid:GetChild(i - self.currentIndexHigh), self.itemDataList[i], i)
		end
	end

	local height = self.item:GetComponent("RectTransform").rect.height
	local spaceY = self.grid:GetComponent("GridLayoutGroup").spacing.y
	local width = self.item:GetComponent("RectTransform").rect.width
	local spaceX = self.grid:GetComponent("GridLayoutGroup").spacing.x

	if (self.max - 1) % self.column == 0 then
		if self.isVertical then
			self.grid:GetComponent("GridLayoutGroup").padding.bottom = self.grid:GetComponent("GridLayoutGroup").padding.bottom + height + spaceY
			self.vScrollBar:GetComponent("Scrollbar").value = 0.01
		else
			self.grid:GetComponent("GridLayoutGroup").padding.right = self.grid:GetComponent("GridLayoutGroup").padding.right + width + spaceX
			self.hScrollBar:GetComponent("Scrollbar").value = 0.99
		end
	else
		local trans = self.grid:GetChild(self.grid.childCount - self.activeCount)
		trans.gameObject:SetActive(true)
		self.doItemShowFunc(self.doItemShowFuncSelf, trans, self.itemDataList[self.currentIndexLow + 1], self.currentIndexLow + 1)
		self.currentIndexLow = self.currentIndexLow + 1
		self.activeCount = self.activeCount - 1
	end
end

function UIScrollTools:GetNodeByIndex(index)
	-- body
	return self.grid:GetChild(index - self.currentIndexHigh) 
end

function UIScrollTools:CheckActive()
	-- body
	if self.activeCount == 4 then
		for i = self.grid.childCount, self.grid.childCount - 4, -1 do
			self.grid:GetChild(i - 1).gameObject:SetActive(true)
		end
		self.activeCount = 0
	end
end

function UIScrollTools:OnDestroy()
	-- body
	self.this = nil
	self = nil
end