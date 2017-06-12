require "System.Global"
require "Logic.Tool.MathTools"

class("UITweenColor")

function UITweenColor:Awake(this)
	-- body
	self.this = this
	self.onTweenFinishDo = {}
end

function UITweenColor:Start()
	-- body
end

function UITweenColor:Init(tbl, uiTweener)
	-- body
	self.uiTweener = uiTweener
	self.uiTweener:Init()

	if tbl then
		assert(tbl.from, "from data can't be null")
		assert(tbl.to, "to data can't be null")
		assert(tbl.mTrans, "target gameObject can't be null")

		print(tostring(UITools.CheckTableType(tbl.from, "number")))
		print(type(tbl.from) ~= "table")
		print(UITools.GetPairsLength(tbl.from) < 4)
		local isColor_from = (type(tbl.from) ~= "table" or UITools.GetPairsLength(tbl.from) < 4 or UITools.CheckTableType(tbl.from, "number") == false)
		assert(isColor_from == false, "param 'From' is not a Color value")

		local isColor_to = (type(tbl.to) ~= "table" or UITools.GetPairsLength(tbl.to) < 4 or UITools.CheckTableType(tbl.to, "number") == false)
		assert(isColor_to == false, "param 'To' is not a Color value")	

		self.from = tbl.from
		self.to = tbl.to
		self.mTrans = tbl.mTrans
		if tbl.ignoreTimeScale then
			self.ignoreTimeScale = tbl.ignoreTimeScale
		else
			self.ignoreTimeScale = false
		end

		if tbl.delay then
			self.delay = tbl.delay
		else
			self.delay = 0
		end

		if tbl.duration then
			self.duration = tbl.duration
		else
			self.duration = 1
		end

		if tbl.method then
			self.method = tbl.method
		else
			self.method = "Linear"
		end

		if tbl.style then
			self.style = tbl.style
		else
			self.style = "Once"
		end

		if tbl.steeperCurves then
			self.steeperCurves = tbl.steeperCurves
		else
			self.steeperCurves = false
		end

		if tbl.useFixedUpdate then
			self.useFixedUpdate = tbl.useFixedUpdate
		else
			self.useFixedUpdate = false
		end

		if tbl.animationCurve then
			self.animationCurve = tbl.animationCurve
		else
			self.animationCurve = self:SetAnimationCurve(1)
		end

		if tbl.onTweenFinishDo then
			self.onTweenFinishDo = tbl.onTweenFinishDo		--onTweenFinishDo为一个table，保存{Function, FunctionSelf}
		else
			self.onTweenFinishDo = {}
		end
	end
end

function UITweenColor:SetAnimationCurve(type)
	-- body
	local curve = {}
	if type == 1 then
		curve = {{0, 0, 1, 1}, {1, 1, 1, 1}}
	elseif type == 2 then
		curve = {{0, 0, 0, 0}, {1, 1, 2, 2}}
	elseif type == 3 then 
		curve = {{0, 0, 2, 2}, {1, 1, 0, 0}}
	elseif type == 4 then
		curve = {{0, 0, 0, 0}, {1, 1, 0, 0}}
	end
	return curve
end

function UITweenColor:Begin(worldSpace)
	-- body
	local comp = {}
	if worldSpace then
		comp.ignoreTimeScale = self.ignoreTimeScale
		comp.delay = self.delay
		comp.duration = self.duration
		comp.onTweenFinishDo = self.onTweenFinishDo
		comp.style = self.style
		comp.method = self.method
		comp.steeperCurves = self.steeperCurves
		comp.useFixedUpdate = self.useFixedUpdate
		comp.animationCurve = AnimationCurve.New(KeyFrame.New(self.animationCurve[1][1], self.animationCurve[1][2], self.animationCurve[1][3], self.animationCurve[1][4]),
											KeyFrame.New(self.animationCurve[2][1], self.animationCurve[2][2], self.animationCurve[2][3], self.animationCurve[2][4]))
		--self.uiTweener:DoUpdate(comp)
	
		table.insert(self.uiTweener.OnUpdate, self.OnUpdate)
		table.insert(self.uiTweener.OnUpdate, self)

		self.uiTweener:DoUpdate(comp)
	end
	return comp
end

function UITweenColor:OnUpdate(val, isFinished)
	-- body
	local value = MathTools.Lerp(self.from, self.to, val)
	--value = value - self.mTrans.localPosition
	self.mTrans:GetComponent("UnityEngine.UI.Image").color = value
end

function UITweenColor:OnDestroy()
	-- body
	self.this = nil
	self = nil
end