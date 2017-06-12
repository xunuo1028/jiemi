--jit.off()
--jit.opt(3)

require "Logic.PanelNum"
require "Logic.Tool.UITools"

class("GameManager")

GameData = {}

function GameManager:Awake(this) 
  self.this = this  
  self:Init()
  --self.this.gameObject:AddComponent(NTGLuaScript.GetType("NTGLuaScript")):Load("Logic.UTGData.UTGDataOperator")
  GameManager.Instance = self
  if GameManager.PanelRoot ~= nil then
    --GameManager.CreatePanelAsync("M000_Bg", "M000", true)
    --GameManager.CreatePanel("Login", nil)
    --GameManager.CreatePanel("UpdateResource", nil)
    --GameManager.CreatePanel("Promote", nil)
    --GameManager.CreatePanel("Matching") 
  end
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
  print("aaaaa " .. parentName)
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




