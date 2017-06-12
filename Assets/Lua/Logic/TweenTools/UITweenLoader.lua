require "System.Global"
require "Logic.Tool.UITools"
require "Logic.Tool.MathTools"
require "Logic.TweenTools.TweenGlobal"
require "Logic.TweenTools.UITweenPosition"
require "Logic.TweenTools.UITweenColor"
require "Logic.TweenTools.UITweenAlpha"
require "Logic.TweenTools.UITweenRotation"
require "Logic.TweenTools.UITweenScale"

class("UITweenLoader")

function UITweenLoader:Init(tbl, tweenName)
	-- body
	self.Pause = false
	self.IsDone = false
	self.OnUpdate = {}

	print("tweenName = " .. tweenName)

	if tweenName == "UITweenPosition" then
		self.tween = UITweenPosition.New()
	elseif tweenName == "UITweenColor" then
		self.tween = UITweenColor.New()
	elseif tweenName == "UITweenScale" then
		self.tween = UITweenScale.New()
	elseif tweenName == "UITweenRotation" then
		self.tween = UITweenRotation.New()
	elseif tweenName == "UITweenAlpha" then
		self.tween = UITweenAlpha.New()
	end
	assert(self.tween ~= nil, "no tween is selected!")
	self.tween:Init(tbl, self)
	self.tween:Begin()
end

--local mAmountPerDelta = 1000

function UITweenLoader:DoUpdate(tbl)
	-- body
	--TODO delay不能是负数

	local mAmountPerDelta = 1000
	local mFactor = 0
	local time = os.time()
	local canLoop = true

	self.IsDone = false
	self.startTime = 0
	self.started = false
	self.method = tbl.method
	self.style = tbl.style
	self.ignoreTimeScale = tbl.ignoreTimeScale
	self.delay = tbl.delay
	self.duration = tbl.duration
	self.onTweenFinishDo = tbl.onTweenFinishDo
	self.steeperCurves = tbl.steeperCurves
	self.useFixedUpdate = tbl.useFixedUpdate
	self.animationCurve = tbl.animationCurve

	local delta = 0

	if self.ignoreTimeScale and not self.useFixedUpdate then
		delta = Time.unscaledDeltaTime
	else
		delta = Time.deltaTime
	end

	self.amountPerDelta = function(duration)
		-- body
		if duration == 0 then
			return 1000
		end

		mAmountPerDelta = math.abs(1 / duration) * MathTools.Sign(mAmountPerDelta)

		return mAmountPerDelta
	end

	local DoUpdateCoroutine = function()
		-- body
		while (self.started == false or time < self.startTime) do
			if self.started == false then
				self.started = true
				self.startTime = time + self.delay
			end
			time = time + delta
			coroutine.step()
		end

		local duration = self.duration
		while (canLoop) do

			if self.Pause or TweenGlobal.PauseAll then           --5.25增加的暂停功能
				
				coroutine.step()         --暂停则空跑一帧

			else                         --否则执行动画

				mFactor = mFactor + (self.duration == 0 and 1 or self.amountPerDelta(self.duration) * delta)
				if self.style == "Loop" then
					if mFactor > 1 then
						mFactor = mFactor - math.floor(mFactor)
					end
				elseif self.style == "PingPong" then
					if mFactor > 1 then
						mFactor = 1 - (mFactor - math.floor(mFactor))
						mAmountPerDelta = -mAmountPerDelta
					elseif mFactor < 0 then
						mFactor = -mFactor
						mFactor = mFactor - math.floor(mFactor)
						mAmountPerDelta = -mAmountPerDelta
					end
				end

				--print(self.duration .. " mFactor " .. mFactor)
				if self.style == "Once" and (duration == 0 or mFactor > 1 or mFactor < 0) then
					mFactor = MathTools.Clamp01(mFactor)
					self:Sample(mFactor, true)
					if #self.onTweenFinishDo > 0 then
						for i, v in ipairs(self.onTweenFinishDo) do
							v[1](v[2], v[3], v[4], v[5])
						end
					end
					canLoop = false
					self.IsDone = true
					print(tostring(self.tweenQueueCount) .. " AAAAAA " .. tostring(self.tweenQueue))
					if self.tweenQueueCount == nil or (self.tweenQueueCount == #self.tweenQueue) then	
						self.run = nil
						self:UnLoad(self.tween)
					end
				else
					self:Sample(mFactor, false)
					if self.style == "Once" then
						duration = duration - delta
					end
				end
				coroutine.step()

			end
		end
	end
	self.run = coroutine.start(DoUpdateCoroutine)
end

local BounceLogic = function(val)
	-- body
	if val < 0.363636 then
		val = 7.5685 * val * val
	
	elseif val < 0.727272 then
		val = 7.5625 * (val - 0.545454) * val + 0.75

	elseif val < 0.909090 then
		local val_temp = val - 0.818181
		val = 7.5625 * val_temp * val + 0.9375

	else
		local val_temp = val - 0.9545454
		val = 7.5625 * val_temp * val + 0.984375
	end

	return val
end

function UITweenLoader:Sample(factor, isFinished)
	-- body

	local val = MathTools.Clamp01(factor)
	if self.method == "EasyIn" then
		val = 1 - math.sin(0.5 * math.pi * (1 - val))
		if self.steeperCurves == true then
			val = val * val
		end

	elseif self.method == "EasyOut" then
		val = math.sin(0.5 * math.pi * val)
		if self.steeperCurves == true then
			val = 1 - val
			val = 1 - val * val
		end

	elseif self.method == "EasyInOut" then
		local pi2 = math.pi * 2
		val = val - math.sin(val * pi2) / pi2

		if self.steeperCurves == true then
			val = val * 2 -1
			local sign = MathTools.Sign(val)
			val = 1 - math.abs(val)
			val = 1 - val * val
			val = sign * val * 0.5 + 0.5
		end

	elseif self.method == "BounceIn" then
		val = BounceLogic(val)

	elseif self.method == "BounceOut" then
		val = 1 - BounceLogic(1 - val)

	else
		val = TweenGlobal.GetEaseProgress(self.method, val)
	end


	local a = self.OnUpdate[2]
	local b = self.OnUpdate[1]
	if self.animationCurve then
		b(a, self.animationCurve:Evaluate(val), isFinished)
	else
		b(a, val, isFinished)
	end
	
	--self.OnUpdate(val, isFinished)
end

function UITweenLoader:SetTweenQueue(tbl)
	-- body 
	for k, v in pairs(tbl) do
		if k ~= "tweenName" and k ~= "tbl" then
			error("table format is wrong! Please check your TweenQueue Table ！")
			return
		end
	end
	if not self.tweenQueue then
		self.tweenQueue = {}
	end

	table.insert(self.tweenQueue, tbl)
end

function UITweenLoader:PlayTweenQueue()
	-- body
	local DoPlayTweenQueue = function()
		-- body
		self.tweenQueueCount = 1
		while(self.tweenQueueCount <= #self.tweenQueue) do
			self:Init(self.tweenQueue[self.tweenQueueCount].tbl, self.tweenQueue[self.tweenQueueCount].tweenName)
			--print("22222222 " .. self.delay .. " " .. self.duration .. " " .. self.tweenQueueCount)
			coroutine.wait(self.delay + self.duration)
			if self.tweenQueueCount < #self.tweenQueue then
				self.tweenQueueCount = self.tweenQueueCount + 1
			end
		end
	end
	self.tweenQueueCor = coroutine.start(DoPlayTweenQueue)
end

function UITweenLoader:PauseAll(v)
	-- body
	TweenGlobal.PauseAll = v
end

function UITweenLoader:ResetToBeginning()
	-- body
	mStarted = false;
	mFactor = self.amountPerDelta(self.duration) < 0 and 1 or 0
	UITweenLoader.Sample(mFactor, false)
end
 
function UITweenLoader:UnLoad(tweener)
	-- body
	if self.run then
		coroutine.stop(self.run)
	end

	if self.tweenQueueCor then
		coroutine.stop(self.tweenQueueCor)
	end

	assert(tweener ~= nil, "Tween is Null")
	tweener:OnDestroy()
	self = nil
	print("UITweenLoader unload successfully !")
end