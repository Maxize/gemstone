# 我的unity 3d之旅
## 前篇
### unity 3d 
>[unity 3d](http://unity3d.com/)和[cocos2dx](http://cn.cocos2d-x.org/?v=CN)是目前市场上比较火热的两款游戏引擎。unity 3d由_Unity Technologies_开发的一个让玩家轻松创建诸如三维视频游戏、建筑可视化、实时三维动画等类型互动内容的多平台的综合型游戏开发工具，是一个全面整合的专业游戏引擎。Unity类似于Director,Blender game engine, Virtools 或 Torque Game Builder等利用交互的图型化开发环境为首要方式的软件其编辑器运行在Windows 和Mac OS X下，可发布游戏至Windows、Mac、Wii、iPhone、Windows phone 8和Android平台。它的网页播放器也被Mac widgets所支持。
#### 自学者常驻地:
[游戏蛮牛](http://www.unitymanual.com/)
[学习文档](http://edu.china.unity3d.com/learning_document)
[unity圣典](http://game.ceeger.com/)
### MonoDevelop IDE 工具
>**MonoDevelop** 是个Linux平台上的开放源代码集成开发环境，主要用来开发Mono与.NET Framework软件。MonoDevelop 整合了很多Eclipse与Microsoft Visual Studio的特性，像是 Intellisense、版本控制还有 GUI 与 Web 设计工具。另外还整合了GTK# GUI设计工具(叫做Stetic)。目前支援的语言有C#、Java、BOO、Nemerle、Visual Basic .NET、CIL、C与C++ 。

### [C#](https://msdn.microsoft.com/zh-cn/library/618ayhy6.aspx)

## 中篇
>飞快掠过前面简介，首先拿个视频教程来练练手

### 创建project
![create_project](./images/create_project.png)

### 导入资源
![import_package](./images/import_package_1.png)

![import_package](./images/import_package_2.png)
> 路径中不要有中文，否则会引入失败

### 添加背景
![add_sprite](./images/add_sprite_1.png)

### 创建目录结构
![create_res_folder](./images/create_res_folder.png)

### 添加脚本
![add_scripts](./images/add_scripts.png)

### 添加预设(意志体)
![add_prefabs](./images/add_prefabs.png)
> 相当于有了一个模，以后哪里需要就盖一个就好

### 添加初始化一个宝石
> 编写脚本
![add_gemstone_1](./images/add_gemstone_1.png)

![add_gemstone_2](./images/add_gemstone_2.png)

### 生成一系列宝石
![add_gemstone_list_1](./images/add_gemstone_list_1.png)

![add_gemstone_list_2](./images/add_gemstone_list_2.png)
> 添加方法调用

> c.GetComponent<Gemstone>().UpdatePostion(rowIndex, columnIndex);

![add_gemstone_list_3](./images/add_gemstone_list_3.png)
> 添加意志体

![add_gemstone_list_4](./images/add_gemstone_list_4.png)
> 添加到数组中

![add_gemstone_list_5](./images/add_gemstone_list_5.png)
> 添加方法调用

> c.GetComponent<Gemstone>().RandomCreateGemstoneBg();

![add_gemstone_list_6](./images/add_gemstone_list_6.png)
> 兔死狗烹

![add_gemstone_list_7](./images/add_gemstone_list_7.png)
> 刷新查看效果

![add_gemstone_list_8](./images/add_gemstone_list_8.png)

### 实现点击和交换

> 重构代码

![onclick_switch_1](./images/onclick_switch_1.png)
> 添加事件响应

![onclick_switch_2](./images/onclick_switch_2.png)
> 实现事件响应方法

![onclick_switch_3](./images/onclick_switch_3.png)

> public void Select (Gemstone gemstone){
> 	 Destroy (gemstone.gameObject);  // 测试
> 
> }
> 
> **ps 曾经因为方法名字写错搞了一晚上，罪过罪过**
>
> **（c#中方法 大写字母开头[OnMouseDown]）**
> 
> 瞧瞧

> 交换宝石
![onclick_switch_4](./images/onclick_switch_4.png)
> 实现点击效果

![onclick_switch_5](./images/onclick_switch_5.png)
> 添加调用代码 

> _curGemstone.isSelected = true;_

> _curGemstone.isSelected =false;_

> 完善交换(只有附近的才能交换)
>
>
>	if (Mathf.Abs(curGemstone.rowIndex - gemstone.rowIndex)+ Mathf.Abs(curGemstone.columnIndex - gemstone.columnIndex) == 1){
>
>
>		Exchange(curGemstone, gemstone);
>
>	}
>

### 检测并消除相同的宝石

> ExchangeAndMatch(Gemstone c1, Gemstone c2)

> MatchHorizontal () or MatchVertical ()

> RemoveMatches()
>

![check_delete_1](./images/check_delete_1.png)

![check_delete_2](./images/check_delete_2.png)

![check_delete_3](./images/check_delete_3.png)

### 使用协程 消除相同的宝石

> IEnumerator ExchangeAndMatch(Gemstone c1, Gemstone c2)

> yield return new WaitForSeconds (0.5f);

> StartCoroutine(ExchangeAndMatch(curGemstone, gemstone));

> 命中之后继续检测 RemoveMatches

> StartCoroutine (WaitForCheckMatchAgain());

> IEnumerator WaitForCheckMatchAgain() {

>		yield return new WaitForSeconds (0.5f);

>		if (MatchHorizontal() || MatchVertical()) {

>			RemoveMatches();

>		}

>	}

> 开始之前也添加检测 start()

>   if (MatchHorizontal() || MatchVertical()) {

>			RemoveMatches();

>	}

>

### 使用[ITween](http://itween.pixelplacement.com/documentation.php)实现滑动

![add_itween_1](./images/add_itween_1.png)

> 安装之后,在Gemstone中添加移动方法

>	public void TweenToPostion(int _rowIndex, int _columnIndex){

>		rowIndex = _rowIndex;

>		columnIndex = _columnIndex;

>		iTween.MoveTo(this.gameObject,iTween.Hash("x",columnIndex * 1.2f + xOffset,"y", >rowIndex * 1.2f + yOffset, "time", 0.3f));

>	}

> 替换GameController 中的移动方法

### 加入背景音乐和音效
![add_music_1](./images/add_music_1.png)

![add_music_2](./images/add_music_2.png)

> **添加各个音效**

>	public AudioClip matchClip;

>	public AudioClip errorClip;

>	public AudioClip swapClip;

>  _分别在Exchange, RemoveGemstone, Select 添加对应的音效_
>  此种添加音效的方式在5.x失效了

![add_music_3](./images/add_music_3.png)

### 发布到android
![build_to_android](./images/build_to_android.png)

### 其他功能后续再添加

## 后篇
### NGUI学习
### 未完待续...