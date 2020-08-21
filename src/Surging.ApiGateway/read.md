启动
cmd 命令窗口执行：consul agent -dev

consul 自带 UI 界面，打开网址：http://localhost:8500 ，可以看到当前注册的服务界面

cmd 命令窗口执行:consul.exe agent -server ui -bootstrap -client 0.0.0.0 -data-dir="E:\consul" -bind X.X.X.X

其中X.X.X.X为服务器ip,即可使用http://X.X.X.X:8500 访问ui而不是只能使用localhost连接