--[[
说明：

关于animationCurve每种type对应的曲线类型
type = 1：       *        type = 2：                                  *        type = 3:                                       *     type = 4:                                  * 
				*                                                    *                                                *                                                   *
			   *                                                    *                                          *                                                    *
			  *                                                   *                                      *                                                       * 
			 *                                                 *                                    *                                                           *
			*                                              *                                    *                                                              *
		   *                                          *                                      *                                                               *
		  *                                     *                                          *                                                              *
		 *                                *                                               *                                                          *
		*                          *                                                     *                                                    *


可能需要设置的地方：
1.需在CustomSettings.cs中加入_GT(typeof(AnimationCurve))及_GT(typeof(Keyframe))，然后重新wrap生成文件 (若没有用到AnimationCurve，可以不执行该操作)



关于需要设置的属性：（具体用法看例子）
1.method:              	提供Linear、EasyIn、EasyOut、EasyInOut，string类型， 默认值为Linear
2.style:               	提供Once、Loop、PingPong，string类型， 默认值为Once
3.ignoreTimeScale:     	bool类型， 默认值为false
4.delay:               	num类型， 默认值为0
5.duration:            	num类型， 默认值为1
6.useFixedUpdate:      	暂时没有作用
7.animationCurve:	   	提供1、2、3、4四种类型，对应曲线见上图， num类型，  默认值为类型1
8.onTweenFinishDo:		使用者自己提供若干table，每个table包含的元素依次为{方法体，方法体所在的lua(也就是方法的self)，参数1，参数2，参数3}，目前支持最多3个参数，再
						将每个table通过insert的方式加入onTweenFinishDo中， table类型， 默认为空{}


9.from:					详见2017.5.25改动说明
10.to:					根据不同的Tween会有不同的类型，会在每个具体的Tween脚本中进行说明，为必须给出的属性，脚本中会进行判空报错
11.mTrans:				需要指定执行动画的物体，该物体会挂载对应的tween脚本，该项不能为空。

扩展的Tween运动曲线：
增加了40中运动曲线，具体请打开TweenTools文件夹中的tween.png图片，查看每种曲线的图例


额外实现的math方法：（文件路径Logic/Tool/MathTools.lua）
Math.Clamp01
Math.Sign
Math.Lerp
Math.LerpUnclamped
Math.LerpAngle
Math.Repeat

*****************
* 2017.5.25改动 *
*****************

1.增加单个Tween的暂停功能，这个功能独立于TimeScale存在
	使用方法：调用当前Tween实例的Pause字段，并设置为true

2.讲EaseFunction文件改名为TweenGlobal，并增加全局暂停功能
	使用方法：调用TweenGlobal文件中的TweenGlobal.PauseAll字段，改变值为true

3.修改用法，现在只需要调用UITweenLoader实例中的方法

4.增加Tween队列功能，需要调用UITweenLoader实例中的SetTweenQueue方法，传入一个tbl，此tbl包括{{TweenName1（string），TweenTbl1（table）}, {TweenName2（string），TweenTbl2（table）}, 
										{TweenName3（string），TweenTbl3（table）}, {TweenName4（string），TweenTbl4（table）}, ... }
  然后调用UITweenLoader实例中的PlayTweenQueue方法播放Tween动画

5.from字段现在可以不赋值，默认将取需要执行Tween动画对象的当前状态值


*****************
* 2017.5.31改动 *
*****************

1.为UITweenPosition中添加一个worldSpace的选项，用于选择position和localPosition


*****************
* 2017.6.5改动  *
*****************

1.在CustomSetting.cs中添加_GT(typeof(TextEditor))，用以将制定文本复制到剪切板上


*****************
* 2017.6.14改动 *
*****************

1.UITweenLoader中添加EaseGenerate方法，用于快速生成Tween方法，参数顺序为
	参数名称				类型
	mTrans 			--- 	Transform
	from 			---		Vector3
	to 				--- 	Vector3
	delay   		--- 	number
	duration 		--- 	number
	style 			--- 	string
	method 			--- 	string	
	onFinishDo  	--- 	table    注：table内部的结构请参考30行，onTweenFinishDo的说明
	tweenType   	---     string
	isWorldSpace	--- 	bool     注：该字段为UITweenPosition单独使用，用于区分position、localPosition

请严格按照该顺序填入参数，其中mTrans，to为必须填写的参数，其余参数可以为空，填nil，默认值请参考22行 “ 关于需要设置的属性 ” 的说明

]]