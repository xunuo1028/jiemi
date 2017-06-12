require "System.Global"

class("ConfigData")

function ConfigData.Instance()
  if ConfigData.instance == nil then
    ConfigData.instance = ConfigData.New()
    
  end
  return ConfigData.instance
end

function ConfigData:InitData()
	-- body
	self.password_UnlockMainPanel = 0000
end