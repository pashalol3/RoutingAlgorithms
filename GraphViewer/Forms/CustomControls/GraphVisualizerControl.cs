using GraphViewer.Forms;
using RoutingAlgorithms.Models;
using RoutingAlgorithms.Routing;

using Timer = System.Windows.Forms.Timer;

public class GraphVisualizerControl : Control
{
    private readonly List<Node> _nodes = [];
    public List<Node> Nodes
    {
        get => _nodes;
        set
        {
            ClearPathFindingResults();
            _nodes.Clear();
            _startNode = null;
            _destinationNode = null;
            _nodes.AddRange(value);
        }
    }
    public event Action<Node, Node>? OnStartAndDestUpdate;
    private readonly List<PathFindingResult> _pathFindingResults = [];
    public List<PathFindingResult> PathFindingResults
    {
        get => _pathFindingResults;
        set
        {
            ClearPathFindingResults();
            foreach (var result in value)
            {
                if (!PathFindingResult.IsEmpty(result))
                {
                    _pathFindingResults.Add(result);
                }
            }

            _currentPathIndex = 0;
            _animationProgress = 0f;

            if (_pathFindingResults.Count > 0)
            {
                _animationTimer.Start();
            }

            this.Invalidate();
        }
    }
    private readonly Timer _animationTimer;
    private int _currentPathIndex = 0;
    private float _animationProgress = 0f;
    private const float AnimationSpeed = 0.02f;

    private Node? _startNode;
    private Node? _destinationNode;
    private Node? _selectedNode = null;
    private Node? _hoveredNode = null;
    private (Node from, Node to, MetricF metric)? _hoveredEdge = null;
    private Node? _draggedNode = null;
    private PointF _dragOffset;


    public int NodeRadius { get; set; } = 20;
    public int HitTestRadius { get; set; } = 20;

    private Font _nodeFont = new("Times New Roman", 14);

    private readonly Pen _edgePen = new(Color.Black, 2);
    private readonly Pen _hoverPen = new(Color.Red, 3);

    private readonly Pen _pathPen = new(Color.Blue, 4);
    private readonly Brush _pathBrush = new SolidBrush(Color.Blue);

    private readonly Pen _animatedPathSAMCRAPen = new(Color.LightGreen, 20);
    private readonly Pen _animatedPathDijekstraPen = new(Color.Gold, 20);

    private readonly Dictionary<NodeStatus, Brush> _brushes = new()
    {
        {NodeStatus.Start, new SolidBrush(Color.Green)},
        {NodeStatus.Destination, new SolidBrush(Color.Blue)},
        {NodeStatus.Hovered, new SolidBrush(Color.Red)},
        {NodeStatus.Default , new SolidBrush(Color.Orange)},
    };

    private static readonly Dictionary<RoutingMethod, string> _routingMethodName = new()
        {
            { RoutingMethod.SAMCRA, "Предлагаемый алгоритм" },
            { RoutingMethod.Dijkstra, "Алгоритм Дейкстры" },
            { RoutingMethod.BellmanFord, "BellmanFord" },
            { RoutingMethod.Invalid, "Invalid"},
        };
    private readonly Brush _textBrush = Brushes.White;

    public GraphVisualizerControl()
    {
        this.BackColor = Color.White;
        this.DoubleBuffered = true;

        _animationTimer = new Timer { Interval = 16 }; // ~60 FPS
        _animationTimer.Tick += (s, e) =>
        {
            _animationProgress += AnimationSpeed;
            if (_animationProgress >= 1f)
            {
                _animationProgress = 0f;
            }
            this.Invalidate();
        };

        this.KeyDown += GraphVisualizerControl_KeyDown;
        this.MouseWheel += GraphVisualizerControl_MouseWheel;
    }

    public void OnFormSizeChanged(int width, int height)
    {
        NodeRadius = (int)MathF.Sqrt(width * height) / 30;
        HitTestRadius = NodeRadius;
        _nodeFont = new Font("Times New Roman", MathF.Sqrt(width * height) / 45f);
        this.Invalidate();
    }
    private void GraphVisualizerControl_MouseWheel(object? sender, MouseEventArgs e)
    {
        if (PathFindingResults.Count == 0) return;
        if (e.Delta > 0) ShowPreviousPath();
        else ShowNextPath();
    }

    private void GraphVisualizerControl_KeyDown(object? sender, KeyEventArgs e)
    {
        if (PathFindingResults.Count == 0) return;

        switch (e.KeyCode)
        {
            case Keys.Left:
            case Keys.Up:
                ShowPreviousPath();
                break;
            case Keys.Right:
            case Keys.Down:
                ShowNextPath();
                break;
        }
    }

    private void ShowNextPath()
    {
        if (_currentPathIndex < PathFindingResults.Count - 1)
        {
            _currentPathIndex++;
            _animationProgress = 0f;
            _animationTimer.Stop();
            _animationTimer.Start();
            this.Invalidate();
        }
    }

    private void ShowPreviousPath()
    {
        if (_currentPathIndex > 0)
        {
            _currentPathIndex--;
            _animationProgress = 0f;
            _animationTimer.Stop();
            _animationTimer.Start();
            this.Invalidate();
        }
    }

    public void ClearPathFindingResults()
    {
        PathFindingResults.Clear();
        _currentPathIndex = 0;
        _animationProgress = 0f;
        _animationTimer.Stop();
        Nodes.ForEach(node =>
        {
            if (node != _startNode && node != _destinationNode)
            {
                node.Status = NodeStatus.Default;
            }
        });
        this.Invalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        var g = e.Graphics;
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

        if (PathFindingResults.Count > 0)
        {
            DrawPathFindingResults(g);
        }

        DrawAllEdges(g);

        DrawAllNodes(g);


    }

    private void DrawPathFindingResults(Graphics g)
    {
        var currentResult = PathFindingResults[_currentPathIndex];

        for (int i = 0; i < currentResult.Path.Count - 1; i++)
        {
            var start = NormalizedToPixel(currentResult.Path[i].PositionN);
            var end = NormalizedToPixel(currentResult.Path[i + 1].PositionN);
            g.DrawLine(_pathPen, start, end);
        }

        if (_animationProgress > 0)
        {
            int animatedSegment = (int)((currentResult.Path.Count - 1) * _animationProgress);
            animatedSegment = Math.Min(animatedSegment, currentResult.Path.Count - 2);

            for (int i = 0; i <= animatedSegment; i++)
            {
                var start = NormalizedToPixel(currentResult.Path[i].PositionN);
                var end = NormalizedToPixel(currentResult.Path[i + 1].PositionN);

                // Последний сегмент рисуем частично
                if (i == animatedSegment)
                {
                    float partialProgress = (_animationProgress * (currentResult.Path.Count - 1)) - i;
                    end = new(
                        start.X + (end.X - start.X) * partialProgress,
                        start.Y + (end.Y - start.Y) * partialProgress);
                }
                var pen = currentResult.RoutingMethod == RoutingMethod.Dijkstra ? _animatedPathDijekstraPen : _animatedPathSAMCRAPen;
                g.DrawLine(pen, start, end);
            }
        }

        string pathInfo = $"Метод поиска: {_routingMethodName[currentResult.RoutingMethod]}\n" +
                          $"Пропускная способность: {currentResult.TotalBandwidth:F1}\n" +
                          $"Задержка: {currentResult.TotalDelay:F1}\n" +
                          $"Путь: [{string.Join(' ', currentResult.Path.Select(s => s.NodeName))}]";
            

        var infoSize = g.MeasureString(pathInfo, _nodeFont);
        var infoRect = new RectangleF(10, 10, infoSize.Width + 10, infoSize.Height + 10);

        g.FillRectangle(Brushes.White, infoRect);
        g.DrawString(pathInfo, _nodeFont, Brushes.Black, 15, 15);

        if (PathFindingResults.Count > 1)
        {
            string indexInfo = $"{_currentPathIndex + 1} из {PathFindingResults.Count}";
            var indexSize = g.MeasureString(indexInfo, _nodeFont);
            var indexRect = new RectangleF(
                this.Width - indexSize.Width - 20,
                10,
                indexSize.Width + 10,
                indexSize.Height + 10);

            g.FillRectangle(Brushes.White, indexRect);
            g.DrawString(indexInfo, _nodeFont, Brushes.Black,
                this.Width - indexSize.Width - 15, 15);
        }
    }

    private void DrawAllEdges(Graphics g)
    {
        foreach (var node in Nodes)
        {
            var startPos = NormalizedToPixel(node.PositionN);

            foreach (var neighbor in node.Neighbors)
            {
                var endPos = NormalizedToPixel(neighbor.Key.PositionN);

                bool isHovered = _hoveredEdge.HasValue &&
                    ((node == _hoveredEdge.Value.from && neighbor.Key == _hoveredEdge.Value.to) ||
                     (node == _hoveredEdge.Value.to && neighbor.Key == _hoveredEdge.Value.from));

                g.DrawLine(isHovered ? _hoverPen : _edgePen, startPos, endPos);
                DrawEdgeMetric(g, node, neighbor.Key, neighbor.Value);
            }
        }
    }

    private void DrawEdgeMetric(Graphics g, Node from, Node to, MetricF metric)
    {
        var start = NormalizedToPixel(from.PositionN);
        var end = NormalizedToPixel(to.PositionN);
        var center = new PointF((start.X + end.X) / 2, (start.Y + end.Y) / 2);

        var metricText = metric.ToString();
        var textSize = g.MeasureString(metricText, _nodeFont);
        var textRect = new RectangleF(
            center.X - textSize.Width / 2,
            center.Y - textSize.Height / 2,
            textSize.Width,
            textSize.Height);

        g.FillRectangle(Brushes.White, textRect);
        g.DrawString(metricText, _nodeFont, Brushes.Black, textRect);
    }

    private PointF NormalizedToPixel(PointF normalized)
    {
        return new PointF(
            normalized.X * this.Width,
            normalized.Y * this.Height);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
        base.OnMouseDown(e);

        var clickedNode = GetNodeAtPosition(e.Location);
        _selectedNode = clickedNode;

        if (clickedNode != null)
        {
            if (e.Button == MouseButtons.Left)
            {
                HandleNodeSelection(clickedNode);
            }
            else if (e.Button == MouseButtons.Right)
            {
                _draggedNode = clickedNode;
                var nodePos = NormalizedToPixel(clickedNode.PositionN);
                _dragOffset = new PointF(e.X - nodePos.X, e.Y - nodePos.Y);
            }
        }

        this.Invalidate();
    }

    private void HandleNodeSelection(Node clickedNode)
    {
        bool startOrDestChanged = false;

        if (clickedNode == _startNode)
        {
            _startNode.Status = NodeStatus.Default;
            _startNode = null;
            startOrDestChanged = true;
        }
        else if (clickedNode == _destinationNode)
        {
            _destinationNode.Status = NodeStatus.Default;
            _destinationNode = null;
            startOrDestChanged = true;
        }
        else
        {
            if (_startNode == null)
            {
                _startNode = clickedNode;
                _startNode.Status = NodeStatus.Start;
                startOrDestChanged = true;
            }
            else if (_destinationNode == null)
            {
                _destinationNode = clickedNode;
                _destinationNode.Status = NodeStatus.Destination;
                startOrDestChanged = true;
            }
            else
            {
                _destinationNode.Status = NodeStatus.Default;
                _destinationNode = clickedNode;
                _destinationNode.Status = NodeStatus.Destination;
                startOrDestChanged = true;
            }
        }

        if (startOrDestChanged)
            ClearPathFindingResults();


        if (_startNode != null && _destinationNode != null)
            OnStartAndDestUpdate?.Invoke(_startNode, _destinationNode);

    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);

        foreach (var node in Nodes)
        {
            if (node.Status == NodeStatus.Hovered && node != _startNode && node != _destinationNode)
                node.Status = NodeStatus.Default;
        }

        _hoveredNode = GetNodeAtPosition(e.Location);
        if (_hoveredNode != null && _hoveredNode != _startNode && _hoveredNode != _destinationNode)
        {
            _hoveredNode.Status = NodeStatus.Hovered;
        }

        _hoveredEdge = FindEdgeAtPosition(e.Location);

        if (_draggedNode != null && e.Button == MouseButtons.Right)
        {
            var newX = (e.X - _dragOffset.X) / this.Width;
            var newY = (e.Y - _dragOffset.Y) / this.Height;

            newX = Math.Max(0, Math.Min(1, newX));
            newY = Math.Max(0, Math.Min(1, newY));

            _draggedNode.PositionN = new(newX, newY);
        }

        this.Invalidate();
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
        base.OnMouseUp(e);
        _draggedNode = null;
    }

    private void DrawAllNodes(Graphics g)
    {
        foreach (var node in Nodes)
        {
            var pos = NormalizedToPixel(node.PositionN);

            var brush = _brushes[node.Status];

            if (PathFindingResults.Count > 0 &&
                PathFindingResults[_currentPathIndex].Path.Contains(node) &&
                node != _startNode &&
                node != _destinationNode)
            {

                brush = _pathBrush;
            }

            g.FillEllipse(brush,
                pos.X - NodeRadius,
                pos.Y - NodeRadius,
                NodeRadius * 2,
                NodeRadius * 2);

            var nameSize = g.MeasureString(node.NodeName, _nodeFont);
            g.DrawString(node.NodeName, _nodeFont, _textBrush,
                pos.X - nameSize.Width / 2,
                pos.Y - nameSize.Height / 2);
#if DEBUG
            g.DrawString(node.PositionN.ToString(), _nodeFont, new SolidBrush(Color.Black),
                pos.X - 40 - nameSize.Width / 2,
                pos.Y - 40 - nameSize.Height / 2);
#endif
        }
    }

    protected override void OnMouseDoubleClick(MouseEventArgs e)
    {
        base.OnMouseDoubleClick(e);

        if (e.Button == MouseButtons.Left)
        {
            var edge = FindEdgeAtPosition(e.Location);
            if (edge.HasValue)
            {
                using var form = new MetricEditForm(edge.Value.metric);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    if (edge.Value.from.Neighbors.ContainsKey(edge.Value.to))
                        edge.Value.from.Neighbors[edge.Value.to] = form.Metric;

                    if (edge.Value.to.Neighbors.ContainsKey(edge.Value.from))
                        edge.Value.to.Neighbors[edge.Value.from] = form.Metric;

                    ClearPathFindingResults();

                    if (_startNode is not null && _destinationNode is not null)
                        OnStartAndDestUpdate?.Invoke(_startNode, _destinationNode);

                    this.Invalidate();
                }
                return;
            }
        }
    }

    private (Node from, Node to, MetricF metric)? FindEdgeAtPosition(Point position)
    {
        const float edgeHitTestDistance = 8f;
        float minDistance = float.MaxValue;
        (Node from, Node to, MetricF metric)? closestEdge = null;

        foreach (var node in Nodes)
        {
            var startPos = NormalizedToPixel(node.PositionN);

            foreach (var neighbor in node.Neighbors)
            {
                var endPos = NormalizedToPixel(neighbor.Key.PositionN);

                float distance = DistanceToLineSegment(position, startPos, endPos);

                if (distance < edgeHitTestDistance && distance < minDistance)
                {
                    minDistance = distance;
                    closestEdge = (node, neighbor.Key, neighbor.Value);
                }
            }
        }

        return closestEdge;
    }

    private float DistanceToLineSegment(Point p, PointF a, PointF b)
    {
        // Вектор AB
        var ab = new PointF(b.X - a.X, b.Y - a.Y);
        // Вектор AP
        var ap = new PointF(p.X - a.X, p.Y - a.Y);

        float abLengthSquared = ab.X * ab.X + ab.Y * ab.Y;
        float dotProduct = ap.X * ab.X + ap.Y * ab.Y;
        float t = Math.Max(0, Math.Min(1, dotProduct / abLengthSquared));

        // Ближайшая точка на отрезке
        PointF closest = new(
            a.X + t * ab.X,
            a.Y + t * ab.Y);

        // Расстояние до ближайшей точки
        return (float)Math.Sqrt(
            Math.Pow(p.X - closest.X, 2) +
            Math.Pow(p.Y - closest.Y, 2));
    }

    private Node? GetNodeAtPosition(Point location)
    {
        foreach (var node in Nodes)
        {
            var nodePos = NormalizedToPixel(node.PositionN);
            var distance = Math.Sqrt(
                Math.Pow(location.X - nodePos.X, 2) +
                Math.Pow(location.Y - nodePos.Y, 2));

            if (distance <= HitTestRadius)
                return node;
        }
        return null;
    }
}