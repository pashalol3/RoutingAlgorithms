# SDK:
[���������� .NET 8+ SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

# �����������:
- SkiaSharp (������ ��������)

# ��� �������: 
1. ���������� SDK.
2. 
   ```cmd
   cd ../RoutingAlgorithms	
   dotnet restore
   dotnet run


```
Program.cs
var nodes = GraphGenerator.RandomTree(numNodes:6, connectionProbability: 0.3f);
var source = nodes.First();
var dest = nodes.Last();
var result = SAMCRA.FindRoute(  source: source, 
                                destination: dest, 
                                routeMaxCost:2f, 
                                routeMaxTime:2f
                             );
```

�� ������ ���������� ����������� �����:
![example](example.jpg)
�� ��������� ����� ������� [���� ��������:����� ��������]
������� - ������
������� - �����
��� ������ [N] ��������� ����
������ - ����