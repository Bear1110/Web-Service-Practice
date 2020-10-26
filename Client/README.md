# Client HttpClient C# .net core

## Command

|Command| Description| 
| - | - |
|listplayer| List all players.|
|listroom| List all rooms.|
|login [playerName]| Register a player to server.|
|logout| Logout.|
|info| Show player information.|
|join [roomID]| Join a room with roomID.|
|create| Create a room. |
|setup | Read the txt `BattleShipMap.txt` next to the program as a map. |
|attack [x] [y]| Attack a ship for example `attack 5 7` |

## SingalR

[Reference](https://docs.microsoft.com/zh-tw/aspnet/core/signalr/dotnet-client)

use `/chathub` as connect channel.

## Map Format

Next to the `exe` and named `BattleShipMap.txt`

10 x 10
```
1 0 0 0 0 0 0 0 0 1
0 0 0 0 0 0 0 0 0 0
0 0 0 0 0 0 0 0 0 0
0 0 0 0 0 0 0 0 0 0
0 0 0 0 0 0 0 0 0 0
0 0 0 0 0 0 0 0 0 0
0 0 0 0 0 0 0 0 0 0
0 0 0 0 0 0 0 0 0 0
0 0 0 0 0 0 0 0 0 0
1 0 0 0 0 0 0 0 0 1
```