--jit.off()
--jit.opt(3)

require "Logic.PanelNum"
require "Logic.Tool.UITools"
require "Logic.Tool.UILocalDataOperator"

class("GameManager")

GameData = {}

function GameManager:Awake(this) 
  self.this = this  
  self:Init()
  --self.this.gameObject:AddComponent(NTGLuaScript.GetType("NTGLuaScript")):Load("Logic.UTGData.UTGDataOperator")
  GameManager.Instance = self
  if GameManager.PanelRoot ~= nil then
    --GameManager.CreatePanelAsync("M000_Bg", "M000", true)
    GameManager.CreatePanel("M002_Test1", nil)
    --GameManager.CreatePanel("UpdateResource", nil)
    --GameManager.CreatePanel("Promote", nil)
    --GameManager.CreatePanel("Matching") 
  end
end

function GameManager:Start()
  -- body
  self.panelLoadDic = {}
  self.preloadList = {}
  self.currentPreloadModelList = {}
  --self:InitLocalRecord()
end

function GameManager:Init()
  GameManager.NetDispatcherHost = self.this    
  GameManager.PanelRoot = self.this.transforms[0]
  GameManager.UIAudioListener = self.this.transforms[1]:GetComponent("AudioListener")
  
  self.this.gameObject:AddComponent(LuaScript.GetType("LuaScript")):Load("Logic.GameGuard")
end

function GameManager:OnDestroy()
  self.this = nil
  self = nil
end

--同步加载(唯一)
function GameManager.CreatePanel(name)
  local assetName = name .. "Panel"
  --print("Creating Panel: " .. assetName) 
  local prefab = ResourceController.Instance:LoadAsset(name, assetName)
  
  local go
  --print("assetName " .. assetName) 
  if GameManager.PanelRoot:FindChild(assetName) == nil and prefab ~= nil then
    go = GameObject.Instantiate(prefab)
    go.name = assetName
    go.transform:SetParent(GameManager.PanelRoot)
    UITools.ResetPanelScale(go.transform)
    go.transform.localScale = Vector3.one
    go.transform.localPosition = Vector3.zero
    go:SetActive(true)
    --NTGResourceController.Instance:UnloadAssetBundle(name, false);
  end
  if GameManager.PanelRoot:FindChild(assetName) ~= nil then go = GameManager.PanelRoot:FindChild(assetName) end
  return go.transform
end

--同步加载提示框（可复用）
function GameManager.CreateDialog(name)
  local assetName = name .. "Dialog"
  --print("Creating Dialog: " .. assetName) 
  local prefab = ResourceController.Instance:LoadAsset(name, assetName)
  
  local go  
  if prefab ~= nil then
    go = GameObject.Instantiate(prefab)
    go.name = assetName
    go.transform:SetParent(GameManager.PanelRoot)
    UITools.ResetPanelScale(go.transform)
    go.transform.localScale = Vector3.one
    go.transform.localPosition = Vector3.zero
    go:SetActive(true)       
    --NTGResourceController.Instance:UnloadAssetBundle(name, false);       
  end
  return go.transform
end

function GameManager.CreatePanelAsync(name, parentName, needScript)
  local result = {Done = false}     
  
  coroutine.start(GameManager.doCreatePanelAsync, GameManager.Instance, name, result, parentName, needScript)
  return result
end

function GameManager:doCreatePanelAsync(name, result, parentName, needScript)
  local assetLoader = nil
  local prefab = nil
  local assetName = name .. "Panel"
  if not parentName then
    local strTemp = UITools.StringSplit(name, "_")
    parentName = strTemp[1]
  end

  assert(parentName ~= nil and string.find(parentName, "M") and string.len(parentName) == 4 , "parentName's format is not M00X or 'name' format is not M00X_PanelName")
  --将面板放置到制定的空物体下
  
  local parentPanel = GameObject.Find(GameManager.PanelRoot.name .. "/" .. parentName)
  print(GameManager.PanelRoot.name .. "/" .. parentName)
  if not parentPanel then
    parentPanel = GameObject.New(parentName)
    parentPanel:AddComponent(LuaScript.GetType("UnityEngine.RectTransform"))
    parentPanel.transform.name = parentName
    parentPanel.transform:SetParent(GameManager.PanelRoot)
    UITools.ResetPanelScale(parentPanel.transform)
    parentPanel.transform.localScale = Vector3.one
    parentPanel.transform.localPosition = Vector3.zero
  end
--[[
  if UTGDataOperator.Instance.assetLoader ~= nil then 
      while prefab == nil do 
        if UTGDataOperator.Instance.currentAssetName == name then
          while UTGDataOperator.Instance.assetLoader.Done ~= true do
            coroutine.step()
          end
          prefab = UTGDataOperator.Instance.assetLoader.Asset
        else
          if UTGDataOperator.Instance.panelList[1] ~= name then 
            local isIn = false
            for i,v in ipairs(UTGDataOperator.Instance.panelList) do
              if v == name then 
                table.insert(UTGDataOperator.Instance.panelList,1,name)
                table.remove(UTGDataOperator.Instance.panelList,i)
                isIn = true
              end
            end
            if isIn == false then table.insert(UTGDataOperator.Instance.panelList,1,name) end
          end
        end
        coroutine.step()
      end
  else
    assetLoader = NTGResourceController.AssetLoader.New()
    assetLoader:LoadAsset(name, assetName)
    while assetLoader.Done ~= true do
      coroutine.step()
    end
    prefab = assetLoader.Asset
    assetLoader:Close()
    assetLoader = nil
  end
  ]]
  assetLoader = ResourceController.AssetLoader.New()
  assetLoader:LoadAsset(name, assetName)
  while assetLoader.Done ~= true do
    coroutine.step()
  end
  prefab = assetLoader.Asset
  assetLoader:Close()
  assetLoader = nil

  local go
  if GameManager.PanelRoot:FindChild(parentName .. "/" .. assetName) == nil and prefab ~= nil then
    go = GameObject.Instantiate(prefab)
    go.name = assetName
    if parentPanel then 
      --print(parentPanel.transform.name .. " " .. parentPanel.transform.parent.name)
      go.transform:SetParent(parentPanel.transform)
    else 
      go.transform:SetParent(GameManager.PanelRoot) 
    end
    UITools.ResetPanelScale(go.transform)
    go.transform.localScale = Vector3.one
    go.transform.localPosition = Vector3.zero
    go:SetActive(true)

    --以上部分之后由c#代码提供，此处仅作测试
    --以下部分将和该面板相关的Lua脚本自动贴到该面板上

    if needScript == nil then
      needScript = true
    end

    if needScript then  
      local tbl = UITools.StringSplit(name, "_")
      assert(go.transform:Find("Controller"), "Panel's first child-panel's name must be 'Controller'...")
      go.transform:Find("Controller").gameObject:AddComponent(LuaScript.GetType("LuaScript")):Load("Logic." .. name .. "." .. tbl[2] .. "Ctrl")     --添加Ctrl
      --go.transform.gameObject:AddComponent(LuaScript.GetType("LuaScript")):Load("Logic.".. name .. "." .. tbl[2] .."API")        --添加API
    end
  end  
  
  if go ~= nil then
    result.Panel = go.transform
  end
  result.Done = true  
  
end




--**************************记录面板加载次数
function GameManager:InitLocalRecord()
  -- body
  --取出本地数据中面板加载情况的相关数据
  self.panelLoadDic = UILocalDataOperator.GetTable("PanelLoad")
end

--面板加载计数
function GameManager:DoPanelRecord(panelName)
  -- body
  if self.panelLoadDic[panelName] ~= nil then
    self.panelLoadDic[panelName] = self.panelLoadDic[panelName] + 1
  else
    self.panelLoadDic[panelName] = 1
  end
end

--存储面板加载情况的相关数据到本地
function GameManager:SavePanelRecordToLocal()
  -- body
  UILocalDataOperator.SetTable("PanelLoad", self.panelLoadDic)
end

--处理取出的面板加载数据
function GameManager:PanelRecordAction()
  -- body
  self.preloadList = {}
  local preLoadTemp = {}
  local tbl = {}
  for k, v in pairs(self.panelLoadDic) do
    if v >= 15 then
      tbl = {panelName = k, totalLoadedNum = v}
      table.insert(preLoadTemp, tbl)
    end
  end

  if #preLoadTemp > 3 then
    table.sort(preLoadTemp, function(a, b)
      -- body
      return a.totalLoadedNum > b.totalLoadedNum
    end)
    table.insert(self.preloadList, preLoadTemp[1])
    table.insert(self.preloadList, preLoadTemp[2])
    table.insert(self.preloadList, preLoadTemp[3])

    return self.preloadList
  elseif #preLoadTemp > 0 and #preLoadTemp <= 3 then
    self.preloadList = preLoadTemp
    return self.preloadList
  else
    return
  end
end

--预加载行为
function GameManager:ActionForecast(resType, fLevel)
  -- body
  if ConfigData.Instance().currentStatus == 1 then
    --预加载主流程界面
    self:PanelRecordAction()
    if #self.preloadList == 0 then
      --加载主流程界面1
      --加载英雄列表
      --加载商店1
      --将上述三个界面加入self.preloadList中
    else
      local haveMainStream1 = false
      local haveHeroList = false
      local haveShop = false
      for i, v in ipairs(self.preloadList) do
        --依次加载列表中的界面
        if v.panelName == "" then
          haveMainStream1 = true
        elseif v.panelName == "" then
          haveHeroList = true
        elseif v.panelName == "" then
          haveShop = true
        end
      end

      if haveMainStream1 == false then
        --加载主流程界面1
        --添加主流程界面进入self.preloadList
        if #self.preloadList == 3 then
          return
        end
      end

      if haveHeroList == false then
        --加载英雄列表界面1
        --添加英雄列表界面1进入self.preloadList
        if #self.preloadList == 3 then
          return
        end
      end

      if haveShop == false then
        --加载商店界面1
        --添加商店界面1进入self.preloadList
        if #self.preloadList == 3 then
          return
        end
      end
    end
  elseif ConfigData.Instance().currentStatus == 2 then
    local tbl = UILocalDataOperator.GetTable("MainStream")
    if tbl ~= nil then
      --卸载self.preloadList中的面板，此处指从内存中清理掉，同时清空self.preloadList
      --加载tbl中存在的面板
      --将预加载的面板加入到self.preloadList中
    else
      --加载默认的MainStream面板
    end
  elseif ConfigData.Instance().currentStatus == 3 then
    local tbl = UILocalDataOperator.GetTable("HeroAbout")
    if tbl ~= nil then
      --卸载self.preloadList中的面板，此处指从内存中清理掉，同时清空self.preloadList
      --加载tbl中存在的面板
      --将预加载的面板加入到self.preloadList中
      self:PreLoadModel()
    else
      --加载默认的MainStream面板
    end
  elseif ConfigData.Instance().currentStatus == 4 then
    local tbl = UILocalDataOperator.GetTable("LoadShopAbout")
    if tbl ~= nil then
      --卸载self.preloadList中的面板，此处指从内存中清理掉，同时清空self.preloadList
      --加载tbl中存在的面板
      --将预加载的面板加入到self.preloadList中
    else
      --加载默认的ShopAbout面板
    end    
  end
end

function GameManager:PreLoadModel(modelIdList, currentModelId)
  -- body
  if modelIdList == nil then            --第一次进入英雄列表
    --加载玩家拥有的第一个英雄的模型
    table.insert(self.currentPreloadModelList, 15000130)
  else
    if #self.currentPreloadModelList > 20 then
      for i = #self.currentPreloadModelList - 10, 1, -1  do
        --将列表中从头开始的前10个模型从内存中清除
        table.remove(self.currentPreloadModelList, i)
      end
    end
    if currentModelId == nil then                       --玩家选择了一种英雄排列，没有选择英雄
      local idx = table.GetTableIndex(self.currentPreloadModelList, currentModelId[1])
      if idx == -1 then       --预加载过的模型中没有该模型
        --加载列表中的第一个模型
        table.insert(self.currentPreloadModelList, currentModelId[1])
      end
    else                                                 --玩家选择了一种英雄排列，并点击了该排列中的某个英雄
      local idx = table.GetTableIndex(self.currentPreloadModelList, currentModelId)      --如果已经预加载了该英雄的模型
      if idx == -1 then    --预加载过的模型中没有该模型
        table.insert(self.currentPreloadModelList, currentModelId)
      end

      local preIdx = table.GetTableIndex(modelIdList, currentModelId)
      if preIdx == -1 then           --该列表的元素不在该列表内，情况不可能出现，先留空

      else
        if preIdx + 1 > #modelIdList then
          local tempIdx = table.GetTableIndex(self.currentPreloadModelList, currentModelId[1])     --当前模型是列表最后一个，则预加载第一个
          if tempIdx == -1 then
            --加载该模型
            table.insert(self.currentPreloadModelList, currentModelId[1])
          end
        else
          local tempIdx = table.GetTableIndex(self.currentPreloadModelList, currentModelId[preIdx + 1])
          if tempIdx == -1 then
            --加载该模型
            table.insert(self.currentPreloadModelList, currentModelId[preIdx + 1])
          end
        end
      end 
    end
  end
end

