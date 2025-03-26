using RoutingAlgorithms.Models;
using RoutingAlgorithms.Routing;
using SkiaSharp;

namespace RoutingAlgorithms
{
    internal class Render
    {
        private static readonly int _fontSize = 30;
        private static readonly int _width = 1920;
        private static readonly int _height = 1080;

        private static readonly Dictionary<RoutingMethod, string> _routingMethodName = new()
        {
            { RoutingMethod.SAMCRA, "SAMCRA" },
            { RoutingMethod.Dijkstra, "Dijkstra" },
            { RoutingMethod.BellmanFord, "BellmanFord" },
            { RoutingMethod.Invalid, "Invalid"},

        };

        private static readonly Dictionary<RoutingMethod, SKColor> _colors = new()
        {
            { RoutingMethod.SAMCRA, SKColors.Red },
            { RoutingMethod.Dijkstra, SKColors.Blue },
            { RoutingMethod.BellmanFord, SKColors.Green },
            { RoutingMethod.Invalid, SKColors.Black},

        };
        private static readonly Dictionary<(Node start, Node end), int> _edgeCount = [];

        internal static void Graph(IList<Node> nodes, IList<PathFindingResult> result, Node source, Node dest)
        {
            using var surface = SKSurface.Create(new SKImageInfo(_width, _height));
            var canvas = surface.Canvas;
            canvas.Clear(SKColors.White);

            float scaleX = _width / 2f;
            float scaleY = _height / 2f;
            float offsetX = _width / 2f;
            float offsetY = _height / 2f;

            // Рисуем ребра
            using (var paint = new SKPaint())
            {
                paint.Color = SKColors.Black;
                paint.StrokeWidth = 2;

                var drawnEdges = new HashSet<(Node, Node)>();

                foreach (var node in nodes)
                {
                    foreach (var neighbor in node.Neighbors)
                    {
                        // Проверяем, было ли это ребро уже нарисовано
                        var edge = (node, neighbor.Key);
                        var reverseEdge = (neighbor.Key, node);

                        if (drawnEdges.Contains(edge) || drawnEdges.Contains(reverseEdge))
                        {
                            continue; // Пропускаем, если ребро уже нарисовано
                        }

                        drawnEdges.Add(edge); // Добавляем ребро в множество

                        var startX = (node.PositionN.X * scaleX) + offsetX;
                        var startY = (node.PositionN.Y * scaleY) + offsetY;
                        var endX = (neighbor.Key.PositionN.X * scaleX) + offsetX;
                        var endY = (neighbor.Key.PositionN.Y * scaleY) + offsetY;
                        canvas.DrawLine(startX, startY, endX, endY, paint);

                        var metric = neighbor.Value.ToString();
                        var midX = (startX + endX) / 2;
                        var midY = (startY + endY) / 2;

                        // Рассчитываем угол наклона
                        var deltaX = endX - startX;
                        var deltaY = endY - startY;
                        var angle = MathF.Atan2(deltaY, deltaX) * (180f / MathF.PI);
                        angle = angle > 90 ? angle - 180 : angle < -90 ? angle + 180 : angle;

                        // Сохраняем текущее состояние канваса
                        canvas.Save();
                        canvas.Translate(midX, midY);
                        canvas.RotateDegrees(angle);

                        // Рисуем текст
                        var typeface = SKTypeface.Default;
                        var font = new SKFont(typeface, _fontSize);
                        paint.Color = SKColors.Black;

                        canvas.DrawText(metric, 0, 0, SKTextAlign.Center, font, paint);

                        // Восстанавливаем состояние канваса
                        canvas.Restore();
                    }

                }
            }


            // Рисуем узлы
            using (var paint = new SKPaint())
            {
                var fontScaleFactor = 0.5f;
                paint.Style = SKPaintStyle.Fill;

                foreach (var node in nodes)
                {
                    if (node == source) paint.Color = SKColors.Green;
                    else if (node == dest) paint.Color = SKColors.Red;
                    else paint.Color = SKColors.Blue;

                    var translatedX = (node.PositionN.X * scaleX) + offsetX;
                    var translatedY = (node.PositionN.Y * scaleY) + offsetY;

                    var typeface = SKTypeface.Default;
                    var font = new SKFont(typeface, _fontSize * fontScaleFactor);

                    canvas.DrawCircle(translatedX, translatedY, 10, paint);
                    paint.Color = SKColors.White;
                    canvas.DrawText(node.ToString(), translatedX, translatedY + _fontSize / 3 * fontScaleFactor, SKTextAlign.Center, font, paint);
                    paint.Color = SKColors.Black;
                    canvas.DrawText($"[{node.Neighbors.Count}]", translatedX, translatedY - _fontSize * fontScaleFactor, SKTextAlign.Center, font, paint);
                }
            }
            foreach (var route in result)
            {
                if (PathFindingResult.IsEmpty(route))
                {
                    continue;
                }

                using var paint = new SKPaint();
                paint.Color = _colors[route.RoutingMethod];
                paint.StrokeWidth = 4;

                for (int i = 0; i < route.Path.Count - 1; i++)
                {
                    var startNode = route.Path[i];
                    var endNode = route.Path[i + 1];

                    var startX = (startNode.PositionN.X * scaleX) + offsetX;
                    var startY = (startNode.PositionN.Y * scaleY) + offsetY;
                    var endX = (endNode.PositionN.X * scaleX) + offsetX;
                    var endY = (endNode.PositionN.Y * scaleY) + offsetY;

                    // Определяем ребро
                    var edge = (startNode, endNode);

                    // Увеличиваем счетчик для этого ребра
                    if (!_edgeCount.TryGetValue(edge, out int value))
                    {
                        value = 0;
                        _edgeCount[edge] = value;
                    }
                    _edgeCount[edge] = ++value;

                    // Смещение для текущего маршрута
                    float offset = (value - 1) * 3.0f;

                    // Применяем смещение к Y координатам
                    startY += offset;
                    endY += offset;

                    // Рисуем линию
                    canvas.DrawLine(startX, startY, endX, endY, paint);
                }

                var typeface = SKTypeface.Default;
                var font = new SKFont(typeface, _fontSize);
                canvas.DrawText($"{_routingMethodName[route.RoutingMethod]} Cost: {route.TotalDelay:F4} Time: {route.TotalBandwidth:F4}",
                    200f, (int)route.RoutingMethod * _fontSize,
                    SKTextAlign.Left, font, paint);
            }
            _edgeCount.Clear();

            using var image = surface.Snapshot();
            using var data = image.Encode(SKEncodedImageFormat.Jpeg, 100);
            using var stream = File.OpenWrite($"output.jpg");
            data.SaveTo(stream);
        }
    }
}
