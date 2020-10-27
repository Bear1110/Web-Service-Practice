# Cpp client 

Release / x64

## nuget nlohmann json

### libcurl setting
Please read this [reference](https://adc.github.trendmicro.com/william-hsiung/http-server-client-practice/tree/master/Client)

$(SolutionDir)packages\curl-vc141-dynamic-x86_64.7.59.0\build\native\lib\x64\static\libcurl.lib

## signalR - CPP

https://www.nuget.org/packages/Microsoft.AspNet.SignalR.Client.Cpp.v140.WinDesktop/1.0.0-beta2

Application Dependencies. Please be attation on platphrom and debug/release config. (If necessary, change the Release to Debug in github readme. )

Please unzip the `Release.7z` , place `x64` and `signalrclient.lib` in this root folder. (This is x64 Debug)

//I replace it with my lib which is builded by the readme.md from [offical github](https://github.com/aspnet/SignalR-Client-Cpp)

## include folder

There are signalR header file in here.

## Not fully function same as C# Client.