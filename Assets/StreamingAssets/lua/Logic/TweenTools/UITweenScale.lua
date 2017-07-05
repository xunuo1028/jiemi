require "System.Global"

class("UITweenScale")

function UITweenScale:Awake(this)
	-- body
	self.this = this
	self.onTweenFinishDo = {}
end

function UITweenScale:Start()
	-- body
end

function UITweenScale:Init(tbl, uiTweener)
	-- body
	self.uiTweener = uiTweener
	self.uiTweener:Init()

	if tbl then
		assert(tbl.from, "from data can't be null")
		assert(tbl.to, "to data can't be null")
		assert(tbl.mTrans, "target gameObject can't be null")

		local isVector3_from = (type(tbl.from) ~= "table" or UITools.GetPairsLength(tbl.from) < 3 or UITools.CheckTableType(tbl.from, "num"))
		assert(isVector3_from == false, "param 'From' is not a Vector3 value")

		local isVector3_to = (type(tbl.to) ~= "table" or UITools.GetPairsLength(tbl.to) < 3 or UITools.CheckTableType(tbl.to, "num"))
		assert(isVector3_to == false, "param 'To' is not a Vector3 value")	

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

function UITweenScale:SetAnimationCurve(type)
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

function UITweenScale:Begin()
	-- body
	local comp = {}
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
	return comp
end

function UITweenScale:OnUpdate(val, isFinished)
	-- body
	local value = self.from * (1 - val) + self.to * val
	--value = value - self.mTrans.localPosition
	self.mTrans.localScale = value
end

function UITweenScale:OnDestroy()
	-- body
	self.this = nil
	self = nil
end