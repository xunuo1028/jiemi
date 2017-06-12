require "System.Global"

class("MathTools")

MathTools.Sign = function(num)
	-- body
	local result = 0
	if num >= 0 then
		result = 1
	elseif num < 0 then
		result = -1
	end

	return result
end

MathTools.Clamp01 = function(num)
	-- body
	local result = 0
	if num < 0 then
		result = 0
	elseif num > 1 then
		result = 1
	else
		result = num
	end

	return result
end

MathTools.Lerp =  function(a, b, t)
	-- body
	return (a + ((b - a) * MathTools.Clamp01(t)))
end

MathTools.LerpUnclamped = function(a, b, t)
	-- body
	return (a + ((b - a) * t))
end

MathTools.LerpAngle = function(a, b, t)
	-- body
	local num = MathTools.Repeat(b - a, 360)
	if num > 180 then
		num = num - 360
	end
	return (a + (num * Clamp01(t)))
end

MathTools.Repeat = function(t, length)
	-- body
	return (t - (math.floor(t / length) * length))
end