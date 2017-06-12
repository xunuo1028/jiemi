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
		if scriptsTbl[i-1].luaScript == luaName then
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