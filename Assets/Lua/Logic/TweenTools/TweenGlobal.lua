require "System.Global"

class("TweenGlobal")

TweenGlobal.PauseAll = false

TweenGlobal.GetEaseProgress = function(methodType, linear_progress)
	-- body
	if methodType == "Linear" then
		return linear_progress 
	elseif methodType == "BackEaseIn" then
		return BackEaseIn(linear_progress, 0, 1, 1)
	elseif methodType == "BackEaseOutIn" then
		return BackEaseOutIn(linear_progress, 0, 1, 1)
	elseif methodType == "BackEaseInOut" then
		return BackEaseInOut(linear_progress, 0, 1, 1)
	elseif methodType == "BackEaseOut" then
		return BackEaseOut(linear_progress, 0, 1, 1)
	elseif methodType == "BounceEaseIn" then
		return BounceEaseIn(linear_progress, 0, 1, 1)
	elseif methodType == "BounceEaseInOut" then
		return BounceEaseInOut(linear_progress, 0, 1, 1)
	elseif methodType == "BounceEaseOut" then
		return BounceEaseOut(linear_progress, 0, 1, 1)
	elseif methodType == "BounceEaseOutIn" then
		return BounceEaseOutIn(linear_progress, 0, 1, 1)
	elseif methodType == "CircEaseInOut" then 
		return CircEaseInOut(linear_progress, 0, 1, 1)
	elseif methodType == "CircEaseIn" then
		return CircEaseIn(linear_progress, 0, 1, 1)
	elseif methodType == "CircEaseOutIn" then 
		return CircEaseOutIn(linear_progress, 0, 1, 1)
	elseif methodType == "CircEaseOut" then
		return CircEaseOut(linear_progress, 0, 1, 1)
	elseif methodType == "CubicEaseIn" then
		return CubicEaseIn(linear_progress, 0, 1, 1)
	elseif methodType == "CubicEaseInOut" then
		return CubicEaseIn(linear_progress, 0, 1, 1)
	elseif methodType == "CubicEaseOut" then
		return CubicEaseOut(linear_progress, 0, 1, 1)
	elseif methodType == "CubicEaseOutIn" then
		return CubicEaseOutIn(linear_progress, 0, 1, 1)
	elseif methodType == "ElasticEaseIn" then
 		return ElasticEaseIn(linear_progress, 0, 1, 1)
	elseif methodType == "ElasticEaseInOut" then
 		return ElasticEaseInOut(linear_progress, 0, 1, 1)
	elseif methodType == "ElasticEaseOut" then
 		return ElasticEaseOut(linear_progress, 0, 1, 1)
	elseif methodType == "ElasticEaseOutIn" then
 		return ElasticEaseOutIn(linear_progress, 0, 1, 1)
	elseif methodType == "ExpoEaseIn" then
 		return ExpoEaseIn(linear_progress, 0, 1, 1)
	elseif methodType == "ExpoEaseInOut" then
 		return ExpoEaseInOut(linear_progress, 0, 1, 1)
	elseif methodType == "ExpoEaseOut" then
 		return ExpoEaseOut(linear_progress, 0, 1, 1)
	elseif methodType == "ExpoEaseOutIn" then
 		return ExpoEaseOutIn(linear_progress, 0, 1, 1)
	elseif methodType == "QuadEaseIn" then
 		return QuadEaseIn(linear_progress, 0, 1, 1)
	elseif methodType == "QuadEaseInOut" then
 		return QuadEaseInOut(linear_progress, 0, 1, 1)
	elseif methodType == "QuadEaseOut" then
 		return QuadEaseOut(linear_progress, 0, 1, 1)
	elseif methodType == "QuadEaseOutIn" then
 		return QuadEaseOutIn(linear_progress, 0, 1, 1)
	elseif methodType == "QuartEaseIn" then
 		return QuartEaseIn(linear_progress, 0, 1, 1)
	elseif methodType == "QuartEaseInOut" then
 		return QuartEaseInOut(linear_progress, 0, 1, 1)
	elseif methodType == "QuartEaseOut" then
 		return QuartEaseOut(linear_progress, 0, 1, 1)
	elseif methodType == "QuartEaseOutIn" then
 		return QuartEaseOutIn(linear_progress, 0, 1, 1)
	elseif methodType == "QuintEaseIn" then
 		return QuintEaseIn(linear_progress, 0, 1, 1)
	elseif methodType == "QuintEaseInOut" then
 		return QuintEaseInOut(linear_progress, 0, 1, 1)
	elseif methodType == "QuintEaseOut" then
 		return QuintEaseOut(linear_progress, 0, 1, 1)
	elseif methodType == "QuintEaseOutIn" then
 		return QuintEaseOutIn(linear_progress, 0, 1, 1)
	elseif methodType == "SineEaseIn" then
 		return SineEaseIn(linear_progress, 0, 1, 1)
	elseif methodType == "SineEaseInOut" then
 		return SineEaseInOut(linear_progress, 0, 1, 1)
	elseif methodType == "SineEaseOut" then
 		return SineEaseOut(linear_progress, 0, 1, 1)
	elseif methodType == "SineEaseOutIn" then
 		return SineEaseOutIn(linear_progress, 0, 1, 1)
	end
end

--[[ 方法中参数的说明
t：当前时间，以秒为单位
b：开始时的参数
c：结束时的参数
d：tween的持续时间
]]

function BackEaseIn(t, b, c, d)
	-- body
	local temp_t = t/d
	return c * temp_t * temp_t * ((1.70158 + 1) * temp_t - 1.70158) + b
end

function Linear(t, b, c, d)
	-- body
	return c * t / d + b
end

function ExpoEaseOut(t, b, c, d)
	-- body
	return t == d and b + c or c * (-math.pow(2, -10 * t / d) + 1) + b
end

function ExpoEaseIn(t, b, c, d)
	-- body
	return t == 0 and b or c * math.pow(2, 10 * (t / d - 1)) + b
end

function ExpoEaseInOut(t, b, c, d)
	-- body
	if t == 0 then
		return b
	end

	if t == d then
		return b + c
	end

	local temp_t = t / d
	if (temp_t / 2) < 1 then
		return c / 2 * math.pow(2, 10 * (t - 1)) + b
	end

	local t_1 = t-1
	return c / 2 * (-math.pow(2, -10 * t_1) + 2) + b
end

function ExpoEaseOutIn(t, b, c, d)
	-- body
	if (t < d / 2) then
		return ExpoEaseOut(t * 2, b, c / 2, d)
	end

	return ExpoEaseIn((t * 2) - d, b + c / 2, c / 2, d)
end

function CircEaseOut(t, b, c, d)
	-- body
	return c * math.sqrt(1 - (t / d - 1) * (t / d - 1)) + b
end

function CircEaseIn(t, b, c, d)
	-- body
	return -c * (math.sqrt(1 - (t / d) * (t / d)) - 1) + b
end

function CircEaseInOut(t, b, c, d)
	-- body
	t = t / (d / 2)
	if (t < 1) then
		return -c / 2 * (math.sqrt(1 - t * t) - 1) + b
	end

	t = t - 2
	return c / 2 * (math.sqrt(1 - t * t) + 1) + b
end

function CircEaseOutIn(t, b, c, d)
	-- body
	if (t < d / 2) then
		return CircEaseOut(t * 2, b, c / 2, d)
	end
	return CircEaseIn((t * 2) - d, b + c / 2, c / 2, d)
end

function QuadEaseOut(t, b, c, d)
	-- body
	t = t / d
	return -c * t * (t - 2) + b
end

function QuadEaseIn(t, b, c, d)
	-- body
	t = t / d
	return c * t * t + b
end

function QuadEaseInOut(t, b, c, d)
	-- body
	t = t / (d / 2)
	if t < 1 then
		return c / 2 * t * t + b
	end

	t = t - 1
	return -c / 2 * (t * (t - 2) - 1) + b
end

function QuadEaseOutIn(t, b, c, d)
	-- body
	if (t < d / 2) then
		return QuadEaseOut(t * 2, b, c / 2, d)
	end
	return QuadEaseIn((t * 2) - d, b + c / 2, c / 2, d)
end

function SineEaseOut(t, b, c, d)
	-- body
	return c * math.sin(t / d * (math.pi / 2)) + b
end

function SineEaseIn(t, b, c, d)
	-- body
	return -c * math.cos(t / d * (math.pi / 2)) + c + b
end

function SineEaseInOut(t, b, c, d)
	-- body
	t = t / (d / 2)
	if t < 1 then
		return c / 2 * (math.sin(math.pi * t / 2)) + b
	end

	t = t - 1
	return -c / 2 * (math.cos(math.pi * t / 2) - 2) + b
end

function SineEaseOutIn(t, b, c, d)
	-- body
	if t < (d / 2) then
		return SineEaseOut(t * 2, b, c / 2, d)
	end

	return SineEaseIn((t * 2) - d, b + c / 2, c / 2, d)
end

function CubicEaseOut(t, b, c, d)
	-- body
	t = t / d - 1
	return c * (t * t * t + 1) + b
end

function CubicEaseIn(t, b, c, d)
	-- body
	t = t / d
	return c * t * t * t + b
end

function CubicEaseInOut(t, b, c, d)
	-- body
	t = t / (d / 2)
	if t < 1 then
		return c / 2 * t * t * t + b
	end

	t = t - 2
	return c / 2 * (t * t * t + 2) + b
end

function CubicEaseOutIn(t, b, c, d)
	-- body
	if t < d / 2 then
		return CubicEaseOut(t * 2, b, c / 2, d)
	end

	return CubicEaseIn((t * 2) - d, b + c / 2, c / 2, d)
end

function QuartEaseOut(t, b, c, d)
	-- body
	t = t / d - 1
	return -c * (t * t * t * t - 1) + b
end

function QuartEaseIn(t, b, c, d)
	-- body
	t = t / d
	return  c * t * t * t * t + b
end

function QuartEaseInOut(t, b, c, d)
	-- body
	t = t / (d / 2)
	if t < 1 then
		return c / 2 * t * t * t * t + b
	end
	t = t - 2
	return -c / 2 * (t * t * t * t - 2) + b
end

function QuartEaseOutIn(t, b, c, d)
	-- body
	if t < d / 2 then
		QuartEaseOut(t * 2, b, c / 2, d)
	end

	return QuartEaseIn((t * 2) - d, b + c / 2, c / 2, d)
end

function QuintEaseOut(t, b, c, d)
	-- body
	t = t / d - 1
	return c * (t * t * t * t * t + 1) + b
end

function QuintEaseIn(t, b, c, d)
	-- body
	t = t / d
	return c * t * t * t * t * t + b
end

function QuintEaseInOut(t, b, c, d)
	-- body
	t = t / (d / 2)
	if t < 2 then
		return c / 2 * t * t * t * t * t + b
	end
	t = t - 2
	return c / 2 * (t * t * t * t * t + 2) + b
end

function QuintEaseOutIn(t, b, c, d)
	-- body
	if t / d < 2 then
		return QuintEaseOut(t * 2, b, c / 2, d)
	end
	return QuintEaseIn((t * 2) - d, b + c / 2, c / 2, d)
end

function ElasticEaseOut(t, b, c, d)
	-- body
	t = t / d
	if t == 1 then
		return b + c
	end

	local p = d * 0.3
	local s = p / 4

	return (c * math.pow(2, -10 * t) * math.sin((t * d - s) * (2 * math.pi) / p) + c + b)
end

function ElasticEaseIn(t, b, c, d)
	-- body
	t = t / d
	if t == 1 then
		return b + c
	end

	local p = d * 0.3
	local s = p / 4
	t = t - 1

	return -(c * math.pow(2, 10 * t) * math.sin((t * d - s) * (2 * math.pi) / p)) + b
end

function ElasticEaseInOut(t, b, c, d)
	-- body
	t = t / (d / 2)
	if t == 2 then
		return b + c
	end

	local p = d * (0.3 * 1.5)
	local s = p / 4

	if t < 1 then
		t = t - 1
		return -0.5 * (c * math.pow(2, 10 * t) * math.sin((t * d - s) * (2 * math.pi) / p)) + b
	end

	t = t - 1
	return c * math.pow(2, -10 * t) * math.sin((t * d - s) * (2 * math.pi) / p) * 0.5 + c + b
end

function ElasticEaseOutIn(t, b, c, d)
	-- body
	if (t < d / 2) then
		return ElasticEaseOut(t * 2, b, c / 2, d)
	end

	return ElasticEaseIn((t * 2) - d, b + c / 2, c / 2, d)
end

function BounceEaseOut(t, b, c, d)
	-- body
	t = t / d
	if t < (1 / 2.75)then
		return c * (7.5625 * t * t) + b
	elseif t < (2 / 2.75) then
		t = t - (1.5 / 2.75)
		return c * (7.5625 * t * t + 0.75) + b
	elseif t < (2.5 / 2.75) then
		t = t - (2.25 / 2.75)
		return c * (7.5625 * t * t + 0.9375) + b
	else
		t = t - (2.625 / 2.75)
		return c * (7.5625 * t * t + 0.984375) + b
	end	
end

function BounceEaseIn(t, b, c, d)
	-- body
	return c * BounceEaseOut(d - t, 0, c, d) + b
end

function BounceEaseInOut(t, b, c, d)
	-- body
	if t < d / 2 then
		return BounceEaseIn(t * 2, 0, c, d) * 0.5 + b
	else
		return BounceEaseOut(t * 1 - d, 0, c, d) * 0.5 + c * 0.5 + b
	end
end

function BounceEaseOutIn(t, b, c, d)
	-- body
	if t < d / 2 then
		return BounceEaseOut(t * 2, b, c / 2, d)
	end
	return BounceEaseIn((t * 2) - d, b + c / 2, c / 2, d)
end

function BackEaseOut(t, b, c, d)
	-- body
	t = t / d
	return c * ((t - 1) * t * ((1.70158 + 1) * t + 1.70158) + 1) + b
end

function BackEaseInOut(t, b, c, d)
	-- body
	local s = 1.70158
	t = t / (d / 2)

	if t < 1 then
		s = s * 1.525
		return c / 2 * (t * t * ((s + 1) * t - s)) + b
	end

	s = s * 1.525
	t = t - 2

	return c / 2 * (t * t * ((s + 1) * t + s) + 2) + b
end

function BackEaseOutIn(t, b, c, d)
	-- body
	if t < d / 2 then
		return BackEaseOut(t * 2, b, c / 2, d)
	end

	return BackEaseIn((t * 2) - d, b + c / 2, c / 2, d)
end