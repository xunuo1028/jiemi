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
	self.currentMicroMessageIndex = 0
	self.localDataList = {}
	self.depthestPanelList = {
		"M003_TestPanel"
	}
	self.lastAction = ""
	self.currentStatus = 1      --0:Idle	1:NewLogin or ReLogin	2:LoadMainStream  	3:LoadHeroAbout		4:LoadShopAbout	
end

function ConfigData:SetDataToLocal()
	-- body
	
end
















































function ConfigData:MicroMessage()
	-- body
	self.mm1 = {
		{"父亲", "我没你这儿子，没用的东西，因为你生不出孩子，我们刘家要断后"},
		{"我", "我又什么办法？！你想要孙子可我现在也没辙啊"},
	}
	

	self.mm2 = {
		{"赵", "孩子到手了，我后天给你送过去"},
		{"我", "谢了赵哥"},
		{"赵", "怎么说也是帮兄弟，帮个忙理所当然"},
	}
end