--FileName:   NewLuaScript
--Author:     JackalLiu
--CreateTime: 4/11/2017 7:10:04 PM
--Description:

require "System.Global"

class("NewLuaScript")

function NewLuaScript:Awake(this)
	self.this = this
	
	--insert your lua code here,good luck :)
end

function NewLuaScript:OnDestroy()
	self.this = nil
	
	--insert your release code here,good luck :)
	
	self = nil
end