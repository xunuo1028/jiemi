require "System.Global"

class("MsgManager")

--静态变量
--local oneParamCallBack = {} 	--元素为table，两个参数，第一个为目标func，第二个为参数param
MsgManager.callBackMap = {}         --元素为table，两个参数，消息ID，第二个参数为OnParamCallBack的table

MsgManager.AddListener = function(msgType, callBack, callBackSelf) 	--参数1：int类型，参数2：OneParamCallBack类型
	-- body
	if MsgManager.callBackMap.msgType ~= nil then
		local funcTbl = {msgType, callBack, callBackSelf}
		table.insert(MsgManager.callBackMap.msgType, funcTbl)
	else
		MsgManager.callBackMap.msgType = {}
		local funcTbl = {msgType, callBack, callBackSelf}
		table.insert(MsgManager.callBackMap.msgType, funcTbl)
	end
end

MsgManager.RemoveListener = function(msgType, callBack)
	-- body
	if MsgManager.callBackMap.msgType ~= nil then
		for i = #MsgManager.callBackMap.msgType, 1, -1 do
			if MsgManager.callBackMap.msgType[i][1] == callBack then
				table.remove(MsgManager.callBackMap.msgType, i)
			end
		end
	end

	if #MsgManager.callBackMap.msgType == 0 then
		MsgManager.callBackMap.msgType = nil
	end
end

MsgManager.BroadCast = function(msgType, param)
	-- body
	print("param = " .. param .. " " .. tostring(MsgManager.callBackMap.msgType))
	if MsgManager.callBackMap.msgType ~= nil then
		for i = #MsgManager.callBackMap.msgType, 1, -1 do
			if MsgManager.callBackMap.msgType[i] ~= nil and MsgManager.callBackMap.msgType[i][1] == msgType then
				MsgManager.callBackMap.msgType[i][2](MsgManager.callBackMap.msgType[i][3], param)
			end
		end
	end
end

MsgManager.Send = function(msgType, callBack, param)
	-- body
	if MsgManager.callBackMap.msgType ~= nil then
		for i = #MsgManager.callBackMap.msgType, 1, -1 do
			if MsgManager.callBackMap.msgType[i] ~= nil and MsgManager.callBackMap.msgType[i][2] == callBack then
				MsgManager.callBackMap.msgType[i][2](MsgManager.callBackMap.msgType[i][3], param)
			end
		end
	end	
end