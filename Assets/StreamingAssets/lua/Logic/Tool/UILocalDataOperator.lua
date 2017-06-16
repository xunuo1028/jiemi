require "System.Global"
require "Logic.Tool.UITools"

class("UILocalDataOperator")

UILocalDataOperator.SetNumber = function(key, data)
	-- body
	if type(data) ~= "number" then
		error(string.format("number expected, got %s", type(data)))
		return
	end

	local str = tostring(data)
	PlayerPrefs.SetString(key, data)
	return "Save Successfully"
end

UILocalDataOperator.GetNumber = function(key)
	-- body
	if type(key) ~= "string" then
		error(string.format("string expected for Key, got %s", type(key)))
		return nil
	end

	if PlayerPrefs.HasKey(key) then
		local result = PlayerPrefs.GetString(key)
		result = tonumber(result)
		return result
	else
		error(string.format("no key is named %s", key))
		return nil
	end
end

UILocalDataOperator.SetString = function(key, data)
	-- body
	if type(data) ~= "string" then
		error(string.format("string expected, got %s", type(data)))
		return
	end

	PlayerPrefs.SetString(key, data)
	return "Save Successfully"	
end

UILocalDataOperator.GetString = function(key)
	-- body
	if type(key) ~= "string" then
		error(string.format("string expected for Key, got %s", type(key)))
		return nil
	end

	if PlayerPrefs.HasKey(key) then
		local result = PlayerPrefs.GetString(key)
		return result
	else
		error(string.format("no key is named %s", key))
		return nil
	end
end

UILocalDataOperator.SetBool = function(key, data)
	-- body
	if type(data) ~= "boolean" then
		error(string.format("string expected, got %s", type(data)))
		return
	end

	PlayerPrefs.SetBool(key, data)
	return "Save Successfully"	
end

UILocalDataOperator.GetBool = function(key)
	-- body
	if type(key) ~= "string" then
		error(string.format("string expected for Key, got %s", type(key)))
		return nil
	end

	local result = PlayerPrefs.GetBool(key)
	return result	
end

UILocalDataOperator.SetTable = function(key, data)
	-- body
	if type(data) ~= "table" then
		error(string.format("table expected, got %s", type(data)))
		return
	else
		for k, v in pairs(data) do
			if type(v) ~= "string" or type(v) ~= "number" or type(v) ~= "boolean" then
				error(string.format("%s type is unexpected", type(v)))
			end
		end
	end
	
	local count = 0
	for k, v in pairs(data) do
		count = count + 1
		PlayerPrefs.SetString(key .. "_key_" .. count, tostring(k))
		PlayerPrefs.SetString(key .. "_value_" .. type(v) .. "_" .. count, tostring(v))
	end
	return "Save Successfully"	
end

UILocalDataOperator.GetTable = function(key)
	-- body
	if type(key) ~= "string" then
		error(string.format("string expected for Key, got %s", type(key)))
		return nil
	end

	local typeTemp = {"string", "number", "boolean"}

	local result = {}
	local count = 0
	local hasValue = false
	while(true) do
		hasValue = false
		count = count + 1
		for i = 1, 4 do
			if PlayerPrefs.HasKey(key .. "_key_" .. count) then
				local strTbl = UITools.StringSplit(key .. "_key_" .. count, "_")
				if PlayerPrefs.GetString(key .. "_key_" .. count) == tostring(count) then
					local value = PlayerPrefs.GetString(key .. "_value_" .. typeTemp[i] .. "_" .. count)
					if i == 2 then
						value = tonumber(value)
					elseif i == 3 then
						if value == "true" then
							value = true
						elseif value == "false" then
							value = false
						end
					end
					table.insert(result, value)
				else
					local tblKey = PlayerPrefs.GetString(key .. "_key_" .. count)
					local value = PlayerPrefs.GetString(key .. "_value_" .. typeTemp[i] .. "_" .. count)
					if i == 2 then
						value = tonumber(value)
					elseif i == 3 then
						if value == "true" then
							value = true
						elseif value == "false" then
							value = false
						end
					end		
					result[tblKey] = value			
				end
				hasValue = true
			end
		end
		if not hasValue then
			if count == 1 then
				error(string.format("table %s not found", key))
			else
				print(string.format("table %s load done", key)) 
			end
			return result
		end
	end
end