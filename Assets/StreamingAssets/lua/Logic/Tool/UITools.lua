require "System.Global"
require "Logic.Tool.MathTools"

class("UITools")

UITools.StringSplit = function(str, separator)
	-- body
	local index = 1
	local strLength = string.len(str)
	local strTbl = {}
	for i = 1, strLength do
		if string.sub(str, i, i) == separator then
			if i ~= index then
				table.insert(strTbl, string.sub(str, index, i - 1))
				index = i + 1
			else
				index = i + 1
			end
		end
	end
	if index-1 ~= strLength then
		table.insert(strTbl, string.sub(str, index, strLength))
	elseif index == 1 then
		table.insert(strTbl, str)
	end
	return strTbl	
end

UITools.GetLuaScript = function(luaName, obj)
	-- body
	local scriptsTbl = obj.transform.gameObject:GetComponents(LuaScript.GetType("LuaScript"))
	for i = 1, scriptsTbl.Length do
		local strTbl = UITools.StringSplit(scriptsTbl[i-1].luaScript, ".")
		local scriptName = strTbl[#strTbl]
		if scriptName == luaName then
			return scriptsTbl[i-1]
		end
	end
	return nil
end

UITools.ResetPanelScale = function(panel)
  -- body
  local rt = panel:GetComponent("RectTransform")
  rt.anchorMin = Vector2.New(0, 0)
  rt.anchorMax = Vector2.New(1, 1)
  rt.sizeDelta = Vector2.New(0, 0)
end

UITools.GetPairsLength = function(tbl)
	-- body
	local count = 0
	for k, v in pairs(tbl) do
		count = count + 1
	end
	return count
end

UITools.CheckTableType = function(tbl, typeName)
	-- body
	for k, v in pairs(tbl) do
		if type(v) ~= typeName then
			return false
		end
	end
	return true
end

UITools.GetVector3Tbl = function(x, y, z)
	-- body
	local tbl = {}
	tbl.x = x
	tbl.y = y
	tbl.z = z
	return tbl
end

UITools.GetColorTbl = function(r, g, b, a)
	-- body
	local tbl = {}
	tbl.r = MathTools.Clamp01(r)
	tbl.g = MathTools.Clamp01(g)
	tbl.b = MathTools.Clamp01(b)
	tbl.a = MathTools.Clamp01(a)
	return tbl
end

UITools.ResetShaderName = function()
	-- body
end

UITools.ClearUIRoot = function(savePanel)
	-- body
	local root = GameObject:Find("UIRoot")
	local destroyCount = 0
	local needDestroy = root.childCount
	for i = 0, needDestroy - 1 do
		if root:GetChild(i).name ~= "Component" then
			if savePanel == nil or root:GetChild(i).name ~= savePanel then
				GameObject:Destroy(root:GetChild(i).gameObject)
				destroyCount = destroyCount + 1
			end
		end
	end

	if savePanel ~= nil then
		if needDestroy - destroyCount == 1 then
			return "No Panel was saved"
		end 
	end
end

UITools.GetUIPosition = function(clickPos, parentObj)
	-- body
	local uiCamera = GameObject.Find("UIRoot/Component/UICamera").transform:GetComponent("Camera")

	local v_v3 = uiCamera:ScreenToWorldPoint(clickPos)

	return v_v3
end

UITools.GetTableIndex = function(tbl, element)
	-- body
	if #tbl == 0 then
		error("Your table is nil or a dictionary")
		return nil
	end

	for i = 1, #tbl do
		if tbl[i] == element then
			return i
		end
	end

	error("The element is not present in the table")
	return -1
end

UITools.SwapTable = function(tbl, element1, element2)
	-- body
	if #tbl == 0 then
		error("Your table is nil or a dictionary")
		return nil
	end

	local haveElement1 = false
	local haveElement2 = false

	for i = 1, #tbl do
		if tbl[i] == element1 then
			tbl[i] = element2
			haveElement1 = true
		elseif tbl[i] == element2 then
			tbl[i] = element1
			haveElement2 = true
		end
	end

	if haveElement1 == false then
		error("Table does'n contain element1")
		return nil
	end

	if haveElement2 == false then
		error("Table does'n contain element2")
		return nil
	end

	return tbl	
end

UITools.SwapTableByIndex = function(tbl, index1, index2)
	-- body
	if #tbl == 0 then
		error("Your table is nil or a dictionary")
		return nil
	end

	if #tbl < index1 then
		error("Table length is smaller than index1")
		return nil
	end

	if #tbl < index2 then
		error("Table length is smaller than index2")
		return nil
	end

	local temp
	for i = 1, #tbl do
		if i == index1 then
			if temp == nil then
				temp = tbl[i]
				tbl[i] = tbl[index2]
			else
				tbl[i] = temp
			end
		elseif i == index2 then
			if temp == nil then
				temp = tbl[i]
				table[i] = tbl[index1]
			else
				tbl[i] = temp
			end
		end
	end

	return tbl
end

UITools.PointerClick = function(targetObj, funcTable, funcSelfTable, paramTbl)
	-- body
	local listener
	listener = EventTriggerProxy.Get(targetObj.gameObject)
		local callback = function(self, e)
			if type(funcTable) == "table" and type(funcSelfTable) == "table" and #funcTable == #funcSelfTable then
				local funcNum = #funcTable
				for i = 1, funcNum do
					funcTable[i](funcSelfTable[i], unpack(paramTbl[i]))
				end
			elseif type(funcTable) == "function" then
				funcTable(funcSelfTable, unpack(paramTbl))
			end
		end
	listener.onPointerClick = EventTriggerProxy.PointerEventDelegate(callback, self)
end

UITools.PointerDown = function(targetObj, funcTable, funcSelfTable, paramTbl)
	-- body
	local listener
	listener = EventTriggerProxy.Get(targetObj.gameObject)
		local callback = function(self, e)
			if type(funcTable) == "table" and type(funcSelfTable) == "table" and #funcTable == #funcSelfTable then
				local funcNum = #funcTable
				for i = 1, funcNum do
					funcTable[i](funcSelfTable[i], unpack(paramTbl[i]))
				end
			elseif type(funcTable) == "function" then
				funcTable(funcSelfTable, unpack(paramTbl))
			end
		end
	listener.onPointerDown = EventTriggerProxy.PointerEventDelegate(callback, self)
end

UITools.PointerUp = function(targetObj, funcTable, funcSelfTable, paramTbl)
	-- body
	local listener
	listener = EventTriggerProxy.Get(targetObj.gameObject)
		local callback = function(self, e)
			if type(funcTable) == "table" and type(funcSelfTable) == "table" and #funcTable == #funcSelfTable then
				local funcNum = #funcTable
				for i = 1, funcNum do
					funcTable[i](funcSelfTable[i], unpack(paramTbl[i]))
				end
			elseif type(funcTable) == "function" then
				funcTable(funcSelfTable, unpack(paramTbl))
			end
		end
	listener.onPointerUp = EventTriggerProxy.PointerEventDelegate(callback, self)
end

UITools.BeginDrag = function(targetObj, funcTable, funcSelfTable, paramTbl)
	-- body
	local listener
	listener = EventTriggerProxy.Get(targetObj.gameObject)
		local callback = function(self, e)
			if type(funcTable) == "table" and type(funcSelfTable) == "table" and #funcTable == #funcSelfTable then
				local funcNum = #funcTable
				for i = 1, funcNum do
					funcTable[i](funcSelfTable[i], unpack(paramTbl[i]))
				end
			elseif type(funcTable) == "function" then
				funcTable(funcSelfTable, unpack(paramTbl))
			end
		end
	listener.onBeginDrag = EventTriggerProxy.PointerEventDelegate(callback, self)
end

UITools.EndDrag = function(targetObj, funcTable, funcSelfTable, paramTbl)
	-- body
	local listener
	listener = EventTriggerProxy.Get(targetObj.gameObject)
		local callback = function(self, e)
			if type(funcTable) == "table" and type(funcSelfTable) == "table" and #funcTable == #funcSelfTable then
				local funcNum = #funcTable
				for i = 1, funcNum do
					funcTable[i](funcSelfTable[i], unpack(paramTbl[i]))
				end
			elseif type(funcTable) == "function" then
				funcTable(funcSelfTable, unpack(paramTbl))
			end
		end
	listener.onEndDrag = EventTriggerProxy.PointerEventDelegate(callback, self)
end

UITools.Drag = function(targetObj, funcTable, funcSelfTable, paramTbl)
	-- body
	local listener
	listener = EventTriggerProxy.Get(targetObj.gameObject)
		local callback = function(self, e)
			if type(funcTable) == "table" and type(funcSelfTable) == "table" and #funcTable == #funcSelfTable then
				local funcNum = #funcTable
				for i = 1, funcNum do
					funcTable[i](funcSelfTable[i], unpack(paramTbl[i]))
				end
			elseif type(funcTable) == "function" then
				funcTable(funcSelfTable, unpack(paramTbl))
			end
		end
	listener.onDrag = EventTriggerProxy.PointerEventDelegate(callback, self)
end

UITools.SwapBool = function(boolValue)
	-- body
	local result = boolValue
	if result == true then
		result = false
	else
		result = true
	end

	return result
end

