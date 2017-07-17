require "System.Global"
require "Logic.Tool.UITools"
require "Logic.Tool.UILocalDataOperator"

class("OptionCtrl")

function OptionCtrl:Awake(this)
	-- body
	self.this = this
	self.quiet = self.this.transform:Find("")
	self.shake = self.this.transform:Find("")
	self.gps = self.this.transform:Find("")
	self.language = self.this.transform:Find("")
	self.resetButton = self.this.transform:Find("")
	self.back = self.this.transform:Find("")
	self.audioSource = GameManager.Instance.PanelRoot:Find("Component/UICamera"):GetComponent("AudioSource")

	self.isQuiet = false
	self.isShake = true
	self.isOpenGPS = false
	self.defaultLanguage = 1
	self.lastSelectLanguage = 0

	local listener
	listener = EventTriggerProxy.Get(self.quiet.gameObject)
	local callbackquiet = function(self, e)
		self:DoSwitchQuiet()
	end
	listener.onPointerClick = EventTriggerProxy.PointerEventDelegate(callbackquiet, self)

	listener = EventTriggerProxy.Get(self.shake.gameObject)
	local callbackShake = function(self, e)
		self:DoSwitchShake()
	end
	listener.onPointerClick = EventTriggerProxy.PointerEventDelegate(callbackShake, self)	

	listener = EventTriggerProxy.Get(self.gps.gameObject)
	local callbackgps = function(self, e)
		self:DoSwitchGPS()
	end
	listener.onPointerClick = EventTriggerProxy.PointerEventDelegate(callbackgps, self)	

	listener = EventTriggerProxy.Get(self.language.gameObject)
	local callbacklanguage = function(self, e)
		self:DoChangeLanguage()
	end
	listener.onPointerClick = EventTriggerProxy.PointerEventDelegate(callbacklanguage, self)	

	listener = EventTriggerProxy.Get(self.back.gameObject)
	local callbackback = function(self, e)
		Object.Destroy(self.this.transform.gameObject)
	end
	listener.onPointerClick = EventTriggerProxy.PointerEventDelegate(callbackback, self)			

	for i = #ConfigData.instance.languages, 1, -1 do
		self.languagePanel:Find("List/" .. i .. "/text"):GetComponent("Text").text = ConfigData.instance.languages[i]
		if ConfigData.instance.languages[i] == UILocalDataOperator.GetString("Language") then
			self.languagePanel:Find("List/" .. i .. "/flag").gameObject:SetActive(true)
			self.lastSelectLanguage = i
		end

		UITools.PointerClick(self.languagePanel:Find("List/" .. i), OptionCtrl.DoChangeLanguage, self, i)
	end

end

function OptionCtrl:Start()
	-- body
end

function OptionCtrl:DoSwitchQuiet()
	-- body
	self.isQuiet = UITools.SwapBool(self.isQuiet)
	local size = self.quiet:Find("Point"):GetComponent("RectTransform").sizeDelta
	self.audioSource.enabled = self.isQuiet
	if self.isQuiet then
		self.quiet:Find("Point").localPosition = Vector3.New(-size.x, 0, 0)
	else
		self.quiet:Find("Point").localPosition = Vector3.New(size.x, 0, 0)
	end
end

function OptionCtrl:DoSwitchShake()
	-- body
	self.isShake = UITools.SwapBool(self.isShake)
	UILocalDataOperator.SetBool("Shake", self.isShake)
	if self.isQuiet then
		UnityEngine.Handheld.Vibrate()
	end	
end

function OptionCtrl:DoGPS()
	-- body
	self.isOpenGPS = UITools.SwapBool(self.isOpenGPS)
	UILocalDataOperator.SetBool("GPS", self.isOpenGPS)
end

function OptionCtrl:DoChangeLanguage(index)
	-- body
	self.languagePanel:Find("List/" .. self.lastSelectLanguage .. "/flag").gameObject:SetActive(false)
	self.languagePanel:Find("List/" .. index .. "/flag").gameObject:SetActive(true)
	UILocalDataOperator.SetString("Language", ConfigData.instance.languages[index])
end

function OptionCtrl:OnDestroy()
	-- body
	self.this = nil
	self = nil
end