using RoutingAlgorithms.Models;
using SkiaSharp;

namespace RoutingAlgorithms
{
    internal class Render
    {
        private static readonly int _fontSize = 20;
        internal static void Graph(List<Node> nodes, Node source, Node dest, PathFindingResult result)
        {
            int width = 1920;
            int height = 1080;
            using (var surface = SKSurface.Create(new SKImageInfo(width, height)))
            {
                var canvas = surface.Canvas;
                canvas.Clear(SKColors.White);

                float scaleX = width / 2f;
                float scaleY = height / 2f;
                float offsetX = width / 2f;
                float offsetY = height / 2f;

                // Рисуем ребра
                using (var paint = new SKPaint())
                {
                    paint.Color = SKColors.Black;
                    paint.StrokeWidth = 2;

                    foreach (var node in nodes)
                    {
                        foreach (var neighbor in node.Neighbors)
                        {
                            var startX = (node.X * scaleX) + offsetX;
                            var startY = (node.Y * scaleY) + offsetY;
                            var endX = (neighbor.Key.X * scaleX) + offsetX;
                            var endY = (neighbor.Key.Y * scaleY) + offsetY;
                            canvas.DrawLine(startX, startY, endX, endY, paint);

                            var metric = neighbor.Value.ToString();
                            var midX = (startX + endX) / 2;
                            var midY = (startY + endY) / 2;

                            var typeface = SKTypeface.Default;
                            var font = new SKFont(typeface, _fontSize);
                            paint.Color = SKColors.Black;
                            canvas.DrawText(metric, midX, midY, SKTextAlign.Center, font, paint);

                        }
                    }
                }

                // Рисуем узлы
                using (var paint = new SKPaint())
                {
                    paint.Style = SKPaintStyle.Fill;

                    foreach (var node in nodes)
                    {
                        if (node == source) paint.Color = SKColors.Green;
                        else if (node == dest) paint.Color = SKColors.Red;
                        else paint.Color = SKColors.Blue;

                        var translatedX = (node.X * scaleX) + offsetX;
                        var translatedY = (node.Y * scaleY) + offsetY;

                        var typeface = SKTypeface.Default;
                        var font = new SKFont(typeface, _fontSize);

                        canvas.DrawCircle(translatedX, translatedY, 10, paint);
                        paint.Color = SKColors.White;
                        canvas.DrawText(node.ToString(), translatedX, translatedY + _fontSize / 3, SKTextAlign.Center, font, paint);
                        paint.Color = SKColors.Black;
                        canvas.DrawText($"[{node.Neighbors.Count}]", translatedX, translatedY - _fontSize, SKTextAlign.Center, font, paint);
                    }
                }
                // Рисуем путь
                if (!PathFindingResult.IsEmpty(result))
                {
                    using var paint = new SKPaint();
                    paint.Color = SKColors.Orange; // Цвет для пути
                    paint.StrokeWidth = 4; // Ширина линии пути

                    for (int i = 0; i < result.Path.Count - 1; i++)
                    {
                        var startNode = result.Path[i];
                        var endNode = result.Path[i + 1];

                        var startX = (startNode.X * scaleX) + offsetX;
                        var startY = (startNode.Y * scaleY) + offsetY;
                        var endX = (endNode.X * scaleX) + offsetX;
                        var endY = (endNode.Y * scaleY) + offsetY;

                        canvas.DrawLine(startX, startY, endX, endY, paint);
                    }
                }

                using var image = surface.Snapshot();
                using var data = image.Encode(SKEncodedImageFormat.Jpeg, 100);
                using var stream = File.OpenWrite($"{result.TotalCost} {result.TotalTime}.jpg");
                data.SaveTo(stream);
            }
        }
    }
}
