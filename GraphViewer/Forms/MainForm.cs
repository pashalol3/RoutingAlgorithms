using GraphViewer.Forms;
using RoutingAlgorithms.Helpers;
using RoutingAlgorithms.Models;
using RoutingAlgorithms.Routing;
using SkiaSharp;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace GraphViewer
{
    public partial class MainForm : Form
    {

        private static readonly Dictionary<RoutingMethod, SKColor> _colors = new()
        {
            { RoutingMethod.SAMCRA, SKColors.Red },
            { RoutingMethod.Dijkstra, SKColors.Blue },
            { RoutingMethod.BellmanFord, SKColors.Green },
            { RoutingMethod.Invalid, SKColors.Black},
        };
        private static readonly Dictionary<RoutingMethod, string> _routingMethodName = new()
        {
            { RoutingMethod.SAMCRA, "Предлагаемый алгоритм" },
            { RoutingMethod.Dijkstra, "Алгоритм дейкстры" },
            { RoutingMethod.BellmanFord, "BellmanFord" },
            { RoutingMethod.Invalid, "Invalid"},
        };

        private readonly IList<Node> _graph = GraphGenerator.StaticGraph();

        private (Node start, Node end)? _hoveredEdge = null;
        private Node? _hoveredNode = null;
        private Node? _clickedNode = null;
        private int _formWidth;
        private int _formHeight;
        private readonly IList<PathFindingResult> _pathFindingResults = [];

        public void RerenderCallback()
        {
            TryToFindPath();
            RenderImage(_graph, _pathFindingResults);
        }
        public int BandwidthLimit { get; set; } = 8;
        public int DelayLimit { get; set; } = 11;
        public (Node start, Node end)? HoveredEdge
        {
            get => _hoveredEdge;
            set
            {
                _hoveredEdge = value;
            }
        }
        public Node? StartNode
        {
            get => _graph.FirstOrDefault(n => n!.Status == NodeStatus.Start, null);
        }
        public Node? DestinationNode
        {
            get => _graph.FirstOrDefault(n => n!.Status == NodeStatus.Destination, null);
        }

        public MainForm(Form helloForm)
        {
            InitializeComponent();


            _helloFormInstanse = helloForm;
            this.Load += MainForm_Load;
            this.Resize += MainForm_Resize;
            this.pbGraphViewer.MouseMove += pb_MouseMove;
            this.pbGraphViewer.DoubleClick += PbGraphViewer_DoubleClick;
            this.pbGraphViewer.MouseClick += pb_MouseClick;
            this.FormClosing += MainForm_FormClosing;
        }

        private void MainForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            _helloFormInstanse.Show();
        }

        private void PbGraphViewer_DoubleClick(object? sender, EventArgs e)
        {
            if (HoveredEdge.Equals(null)) return;
            var form = new InputWeight(HoveredEdge.Value, RerenderCallback);
            form.Show();

        }

        private void pb_MouseMove(object? sender, MouseEventArgs e)
        {
            float mouseX = e.X;
            float mouseY = e.Y;
            var isStateChanged = false;

            var hovered = _graph.Where(n => n.Status == NodeStatus.Hovered);
            isStateChanged = hovered.Any();
            foreach (var node in hovered)
            {
                node.Status = NodeStatus.Default;
            }

            _hoveredNode = null;
            HoveredEdge = null;

            foreach (var node in _graph)
            {
                var translatedX = TranslateXCoords(node.PositionN.X);
                var translatedY = TranslateYCoords(node.PositionN.Y);
                float radius = MathF.Sqrt(_formWidth * _formHeight) / 30f;

                if (MathF.Pow(mouseX - translatedX, 2) + MathF.Pow(mouseY - translatedY, 2) <= MathF.Pow(radius, 2))
                {
                    _hoveredNode = node;

                    if (_hoveredNode.Status == NodeStatus.Default)
                    {
                        _hoveredNode.Status = NodeStatus.Hovered;
                        isStateChanged = true;
                    }
                    break;
                }
            }
            foreach (var node in _graph)
            {
                foreach (var neighbor in node.Neighbors)
                {
                    var startX = TranslateXCoords(node.PositionN.X);
                    var startY = TranslateYCoords(node.PositionN.Y);
                    var endX = TranslateXCoords(neighbor.Key.PositionN.X);
                    var endY = TranslateYCoords(neighbor.Key.PositionN.Y);

                    if (IsPointNearLine(mouseX, mouseY, startX, startY, endX, endY))
                    {
                        HoveredEdge = (node, neighbor.Key);
                        isStateChanged = true;
                        break;
                    }
                }
                if (HoveredEdge != null) break;
            }


            if (isStateChanged)
            {
                RenderImage(_graph, _pathFindingResults);
            }
        }

        private bool IsPointNearLine(float px, float py, float x1, float y1, float x2, float y2, float tolerance = 5f)
        {
            if (MathF.Abs(x1 - x2) < 0.01f)
            {
                return MathF.Abs(px - x1) <= tolerance && py >= MathF.Min(y1, y2) && py <= MathF.Max(y1, y2);
            }

            if (MathF.Abs(y1 - y2) < 0.01f)
            {
                return MathF.Abs(py - y1) <= tolerance && px >= MathF.Min(x1, x2) && px <= MathF.Max(x1, x2);
            }

            float A = py - y1;
            float B = x1 - px;
            float C = x2 - x1;
            float D = y2 - y1;

            float distance = MathF.Abs(A * C + B * D) / MathF.Sqrt(C * C + D * D);
            return distance <= tolerance &&
                   (px >= MathF.Min(x1, x2) && px <= MathF.Max(x1, x2)) &&
                   (py >= MathF.Min(y1, y2) && py <= MathF.Max(y1, y2));
        }


        private void pb_MouseClick(object? sender, MouseEventArgs e)
        {
            float mouseX = e.X;
            float mouseY = e.Y;

            _clickedNode = null;
            foreach (var node in _graph)
            {
                var translatedX = TranslateXCoords(node.PositionN.X);
                var translatedY = TranslateYCoords(node.PositionN.Y);
                float radius = MathF.Sqrt(_formWidth * _formHeight) / 30f;

                if (MathF.Pow(mouseX - translatedX, 2) + MathF.Pow(mouseY - translatedY, 2) <= MathF.Pow(radius, 2))
                {
                    _clickedNode = node;
                    break;
                }
            }

            if (_clickedNode != null)
            {
                var newValue = ((int)_clickedNode.Status + 1) % (int)NodeStatus.Size;
                _clickedNode.Status = (NodeStatus)newValue;
            }

            TryToFindPath();
            RenderImage(_graph, _pathFindingResults);
        }

        private bool TryToFindPath()
        {
            if (StartNode is not null && DestinationNode is not null)
            {
                var dijkstraResult = Dijkstra.FindRoute(StartNode, DestinationNode);
                var dTotalBandwidth = dijkstraResult.TotalBandwidth;
                var dTotalDelay = dijkstraResult.TotalDelay;

                var SAMCRAResult = SAMCRA.FindRoute(StartNode, DestinationNode, BandwidthLimit, DelayLimit);

                _pathFindingResults.Clear();
                _pathFindingResults.Add(dijkstraResult);
                _pathFindingResults.Add(SAMCRAResult);
                return true;
            }
            else
            {
                _pathFindingResults.Clear();
                return false;
            }
        }

        private SKSurface? _surface = null;
        private Bitmap? _cachedBitmap = null;

        private int _currentWidth = 0;
        private int _currentHeight = 0;
        private Form _helloFormInstanse { get; init; }
#if DEBUG
        private Stopwatch _stopwatch = new();
#endif
        private void RenderImage(IList<Node> nodes, IList<PathFindingResult> routes, [CallerMemberName] string methodName = "")
        {
#if DEBUG
            _stopwatch.Start();
#endif
            if (_currentWidth != _formWidth || _currentHeight != _formHeight)
            {
                _surface?.Dispose();
                _surface = SKSurface.Create(new SKImageInfo(_formWidth, _formHeight));

                _currentWidth = _formWidth;
                _currentHeight = _formHeight;
            }

            var surface = _surface;
            if (surface is null) return;

            var canvas = surface.Canvas;
            canvas.Clear(SKColors.White);

            var fontSize = MathF.Sqrt(_formWidth * _formHeight) / 40f;
            DrawEdges(nodes, canvas, fontSize);
            DrawNodes(nodes, canvas, fontSize);
            DrawRoutes(routes, canvas, fontSize);

            using (SKImage image = surface.Snapshot())
            {
                if (_cachedBitmap == null || _cachedBitmap.Width != _formWidth || _cachedBitmap.Height != _formHeight)
                {
                    _cachedBitmap?.Dispose();
                    _cachedBitmap = new Bitmap(_formWidth, _formHeight);
                }

                using (SKData data = image.Encode())
                using (MemoryStream mStream = new(data.ToArray()))
                {
                    pbGraphViewer.Image?.Dispose();
                    pbGraphViewer.Image = new Bitmap(mStream, false);
                }
            }
#if DEBUG
            _stopwatch.Stop();
            this.Text = $"{_stopwatch.Elapsed.TotalMilliseconds.ToString()} ms {methodName}";
            _stopwatch.Reset();
#endif
        }

        private float TranslateXCoords(float nValue) => nValue * _formWidth;
        private float TranslateYCoords(float nValue) => nValue * _formHeight;


        private void DrawRoutes(IList<PathFindingResult> routes, SKCanvas canvas, float fontSize)
        {
            Dictionary<(Node start, Node end), int> edgeCount = [];
            foreach (var route in routes)
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

                    var startX = TranslateXCoords(startNode.PositionN.X);
                    var startY = TranslateYCoords(startNode.PositionN.Y);
                    var endX = TranslateXCoords(endNode.PositionN.X);
                    var endY = TranslateYCoords(endNode.PositionN.Y);

                    var edge = (startNode, endNode);

                    if (!edgeCount.TryGetValue(edge, out int value))
                    {
                        value = 0;
                        edgeCount[edge] = value;
                    }
                    edgeCount[edge] = ++value;

                    float offset = (value - 1) * 3.0f;

                    startY += offset;
                    endY += offset;

                    canvas.DrawLine(startX, startY, endX, endY, paint);
                }

                var typeface = SKTypeface.Default;
                var font = new SKFont(typeface, fontSize);


                const int firstColumnWidth = 50;

                string algorithmName = _routingMethodName[route.RoutingMethod];
                string paddedAlgorithmName = algorithmName.PadRight(firstColumnWidth - algorithmName.Length,' ');
                string formattedText = $"{paddedAlgorithmName} Пропускная способность: {route.Path.SelectMany(s => s.Neighbors.Values).MinBy(s => s.BandWidth).BandWidth:F4} Задержка: {route.TotalDelay:F4}";

                canvas.DrawText(formattedText, 5, (int)route.RoutingMethod * fontSize, SKTextAlign.Left, font, paint);
            }
            edgeCount.Clear();
        }

        private void DrawNodes(IList<Node> nodes, SKCanvas canvas, float fontSize)
        {
            // Рисуем узлы
            using var paint = new SKPaint();
            var fontScaleFactor = 1f;
            paint.Style = SKPaintStyle.Fill;

            foreach (var node in nodes)
            {
                paint.Color = node.NodeColor;

                var translatedX = TranslateXCoords(node.PositionN.X);
                var translatedY = TranslateYCoords(node.PositionN.Y);

                var typeface = SKTypeface.Default;
                var font = new SKFont(typeface, fontSize * fontScaleFactor);
                float radius = MathF.Sqrt(_formWidth * _formHeight) / 30f;
                canvas.DrawCircle(translatedX, translatedY, radius, paint);
                paint.Color = SKColors.White;
                canvas.DrawText(node.ToString(), translatedX, translatedY + fontSize / 3 * fontScaleFactor, SKTextAlign.Center, font, paint);
            }
        }

        private void DrawEdges(IList<Node> nodes, SKCanvas canvas, float fontSize)
        {
            using var paint = new SKPaint();
            paint.Color = SKColors.Black;
            paint.StrokeWidth = 3;

            var drawnEdges = new HashSet<(Node, Node)>();

            foreach (var node in nodes)
            {
                foreach (var neighbor in node.Neighbors)
                {
                    var edge = (node, neighbor.Key);
                    var reverseEdge = (neighbor.Key, node);

                    if (drawnEdges.Contains(edge) || drawnEdges.Contains(reverseEdge))
                    {
                        continue;
                    }

                    drawnEdges.Add(edge);

                    var startX = TranslateXCoords(node.PositionN.X);
                    var startY = TranslateYCoords(node.PositionN.Y);
                    var endX = TranslateXCoords(neighbor.Key.PositionN.X);
                    var endY = TranslateYCoords(neighbor.Key.PositionN.Y);

                    if (HoveredEdge.HasValue &&
                        (HoveredEdge.Value.start == node &&
                        HoveredEdge.Value.end == neighbor.Key ||
                        HoveredEdge.Value.start == neighbor.Key &&
                        HoveredEdge.Value.end == node))
                    {
                        paint.Color = SKColors.Red; // Цвет при наведении
                    }
                    else
                    {
                        paint.Color = SKColors.Black; // Цвет по умолчанию
                    }

                    canvas.DrawLine(startX, startY, endX, endY, paint);

                    var metric = neighbor.Value.ToString();
                    var midX = (startX + endX) / 2;
                    var midY = (startY + endY) / 2;

                    var deltaX = endX - startX;
                    var deltaY = endY - startY;
                    var angle = MathF.Atan2(deltaY, deltaX) * (180f / MathF.PI);
                    angle = angle > 90 ? angle - 180 : angle < -90 ? angle + 180 : angle;

                    canvas.Save();
                    canvas.Translate(midX, midY);
                    canvas.RotateDegrees(angle);

                    var typeface = SKTypeface.Default;
                    var font = new SKFont(typeface, fontSize);
                    paint.Color = SKColors.Black;

                    canvas.DrawText(metric, 0, -5, SKTextAlign.Center, font, paint);

                    canvas.Restore();
                }

            }
        }

        private void MainForm_Resize(object? sender, EventArgs e)
        {
            _formHeight = this.pbGraphViewer.Height;
            _formWidth = this.pbGraphViewer.Width;
            RenderImage(_graph, _pathFindingResults);
        }

        private void MainForm_Load(object? sender, EventArgs e)
        {
            _formHeight = this.pbGraphViewer.Height;
            _formWidth = this.pbGraphViewer.Width;

            nudBandwidth.Value = BandwidthLimit;
            nudDelay.Value = DelayLimit;

            RenderImage(_graph, _pathFindingResults);
        }

        private void nudBandwidth_ValueChanged(object sender, EventArgs e)
        {
            var numericUpDown = sender as NumericUpDown;

            if (numericUpDown != null)
            {
                var bandwidthValue = numericUpDown.Value;
                BandwidthLimit = (int)bandwidthValue;

                var isPathFound = TryToFindPath();
                if (isPathFound)
                    RenderImage(_graph, _pathFindingResults);
            }
        }

        private void nudDelay_ValueChanged(object sender, EventArgs e)
        {
            var numericUpDown = sender as NumericUpDown;

            if (numericUpDown != null)
            {
                var maxTimeValue = numericUpDown.Value;
                DelayLimit = (int)maxTimeValue;
                var isPathFound = TryToFindPath();
                if (isPathFound)
                    RenderImage(_graph, _pathFindingResults);

            }
        }
    }
}
