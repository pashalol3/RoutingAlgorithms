using GraphViewer.Forms;
using RoutingAlgorithms.Helpers;
using RoutingAlgorithms.Models;
using RoutingAlgorithms.Routing;
using System.Text;

namespace GraphViewer
{
    public partial class MainForm : Form
    {

        const int GRAPHS_SIZE = 7;
        private int _graphsIndex = 0;
        private readonly Dictionary<int, List<Node>> _graphs = new()
        {
            {0, GraphGenerator.MainGraph() },
            {1, GraphGenerator.Graph4()    },
            {2, GraphGenerator.Graph5()    },
            {3, GraphGenerator.Graph6()    },
            {4, GraphGenerator.Graph7()    },
            {5, GraphGenerator.Graph8()    },
            {6, GraphGenerator.Graph9()    },
        };
        private readonly Form _helloFormInstanse;
        private readonly GraphVisualizerControl _graphControl;

        private Node? _start;
        private Node? _destination;
        public int BandwidthLimit { get; set; } = 11;
        public int DelayLimit { get; set; } = 11;

        public MainForm(Form helloForm)
        {
            InitializeComponent();
            _graphControl = new GraphVisualizerControl
            {
                Dock = DockStyle.Fill,
            };
            this.Controls.Add(_graphControl);
            _helloFormInstanse = helloForm;
            _graphControl.Nodes = [.. _graphs[_graphsIndex]];

            _graphControl.BringToFront();
            _label1.BringToFront();
            _label2.BringToFront();
            _nudDelay.BringToFront();
            _nudBandwidth.BringToFront();
            _bNext.BringToFront();
            _bInputMatrix.BringToFront();

            _bNext.Click += BNext_Click;
            _bInputMatrix.Click += BInputMatrix_Click;

            _graphControl.OnStartAndDestUpdate += _graphControl_OnStartAndDestUpdate;
            this.Load += MainForm_Load;
            this.SizeChanged += MainForm_SizeChanged;
            this.FormClosing += MainForm_FormClosing;

        }

        private void BInputMatrix_Click(object? sender, EventArgs e)
        {
            var dialog = new MatrixInputDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var matrix = dialog.Matrix;

            }
        }


        private void BNext_Click(object? sender, EventArgs e)
        {
            _graphsIndex = (_graphsIndex + 1) % (GRAPHS_SIZE - 1);
            _graphControl.Nodes = [.. _graphs[_graphsIndex]];
        }

        private void MainForm_SizeChanged(object? sender, EventArgs e)
        {
            _graphControl?.OnFormSizeChanged(this.Width, this.Height);
        }

        private void _graphControl_OnStartAndDestUpdate(Node start, Node destination)
        {
            _start = start;
            _destination = destination;
            var res = TryToFindPath(start, destination);
            _graphControl.PathFindingResults = [.. res];
        }
        private void MainForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            _helloFormInstanse.Show();
        }
        private IList<PathFindingResult> TryToFindPath(Node start, Node dest)
        {
            IList<PathFindingResult> pathFindingResults = [];
            if (start is not null && dest is not null)
            {
                var dijkstraResult = Dijkstra.FindRoute(start, dest);
                var SAMCRAResult = SAMCRA.FindRoute(start, dest, BandwidthLimit, DelayLimit);

                var minDelayLimit = DelayLimit;
                var minBandwidthLimit = BandwidthLimit;
                do
                {
                    minDelayLimit--;
                    SAMCRAResult = SAMCRA.FindRoute(start, dest, BandwidthLimit, minDelayLimit);
                } while (!PathFindingResult.IsEmpty(SAMCRAResult));
                minDelayLimit++;
                do
                {
                    minBandwidthLimit--;
                    SAMCRAResult = SAMCRA.FindRoute(start, dest, minBandwidthLimit, minDelayLimit);
                } while (!PathFindingResult.IsEmpty(SAMCRAResult));
                minBandwidthLimit++;

                SAMCRAResult = SAMCRA.FindRoute(start, dest, minBandwidthLimit, minDelayLimit);
                pathFindingResults.Clear();
                pathFindingResults.Add(SAMCRAResult);
                pathFindingResults.Add(dijkstraResult);
                return pathFindingResults;
            }
            else
            {
                return Enumerable.Empty<PathFindingResult>().ToList();
            }
        }

        private void MainForm_Load(object? sender, EventArgs e)
        {
            _nudBandwidth.Value = BandwidthLimit;
            _nudDelay.Value = DelayLimit;
        }

        private void nudBandwidth_ValueChanged(object sender, EventArgs e)
        {
            var numericUpDown = sender as NumericUpDown;

            if (numericUpDown != null)
            {
                var bandwidthValue = numericUpDown.Value;
                BandwidthLimit = (int)bandwidthValue;
                if (_destination is not null && _start is not null)
                {
                    var res = TryToFindPath(_start, _destination);
                    _graphControl.PathFindingResults = [.. res];
                }
            }
        }

        private void nudDelay_ValueChanged(object sender, EventArgs e)
        {
            var numericUpDown = sender as NumericUpDown;

            if (numericUpDown != null)
            {
                var maxTimeValue = numericUpDown.Value;
                DelayLimit = (int)maxTimeValue;
                if (_destination is not null && _start is not null)
                {
                    var res = TryToFindPath(_start, _destination);
                    _graphControl.PathFindingResults = [.. res];
                }
            }
        }
    }
}
