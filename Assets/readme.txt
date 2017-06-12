In [MOBA] project folders setted up by these rules:

1.Lua - customs lua files, main region we worked,only contain in client project.In art project ,this folder will be empty
	1.1 logic - mvc lua design coding style:
		1.1.1 An example: 
			  1.Lua/logic/m1001 means login module because designer named 1001 as login :)
			  2.Lua/logic/m1001/LoginAPI.lua lua file that generate by "LUA Build TOOL" ,in this file always get components ,set navigations, bind buttons and so on.It's a displayer API.
			  3.Lua/logic/m1001/LoginController.lua real logic file that calls API to play.
	1.2 net - lua net core
	1.3 system - contains sproto, ulua globle and so on

	(Editor Action To This Folder:editor will call LUAJIT converter to build lua files to bytecode files)

2.Res - ui original resources.
	2.1 atlas_common - shared textures that every one use them.
	2.2 atlas_icon - icons that every one use them,if there are huge mount of icons needed,you can use "revert search api" to get what you want.
	2.3 audios - only ui audios(button press audio effects)under this folder.
	2.4 effects - only ui effects(fx) under this folder. maybe partical system effects which has changed shader for ui show.
	2.5 fonts - dynamic fonts like 文鼎中隶 
	2.6 shaders - customs shaders just like 3dmodel show shader that used in uipanel.
	2.7 An example:
		1.Res/atlas_m10001 means login module's clipped textures because designer named 1001 as login :)
		
3.Logic - ui prefabs are here
	3.1 An example:
		1.Logic/prefab_ui_m10001.prefab means login module's uiprefab.
			  
	(Editor Action To This Folder:editor will build all uiprefabs into assetbundles,auto analyze font and atlas reuse)

4.Scripts - c# scripts
	4.1 ApplicationKernel - framework kernel scritps
		4.1.1 BaseInstallationAPI - many base installations api here such as debugutils,resource controller,net.
		4.1.2 CoroutineOperations - yield-returnable operations
		4.1.3 LockedStep - locked step core codes
	4.2 Editor - all editor scripts.
	
	(Editor Action To This Folder:editor will build dlls and apply dlls to designer's project)
	
5.Plugins - tolua&other 3rd plugins

6.ToLua - tolua plugins and its generated files.
			  