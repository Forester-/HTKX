using HelixToolkit.Wpf.SharpDX;
using SharpDX;
using System.Windows;

namespace HxRflc
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = this;

            InitializeComponent();

            var builder = new MeshBuilder(false, false, false);
            builder.AddRectangularMesh(BoxFaces.Front, 10, 10, 100, 100);
            Plane1Geometry = builder.ToMesh();

            builder = new MeshBuilder(true, false, false);
            builder.AddRectangularMesh(BoxFaces.Front, 10, 10, 100, 100);
            Plane2Geometry = builder.ToMesh();

            var dx = -50f;
            var dy = -50f;
            var dz = 10f;
            for (var i = 0; i < Plane1Geometry.Positions.Count; i++)
            {
                var position = Plane1Geometry.Positions[i];
                position.X += dx;
                position.Y += dy;
                Plane1Geometry.Positions[i] = position;
            }
            for (var i = 0; i < Plane2Geometry.Positions.Count; i++)
            {
                var position = Plane2Geometry.Positions[i];
                position.X += dx;
                position.Y += dy;
                position.Z += dz;
                Plane2Geometry.Positions[i] = position;
            }

            Plane1Material = new DiffuseMaterial()
            {
                DiffuseColor = new Color4(1f, 0f, 0f, 0.5f),
            };
            Plane2Material = new DiffuseMaterial()
            {
                DiffuseColor = new Color4(0f, 0f, 1f, 0.5f),
            };

            var camera = new OrthographicCamera()
            {
                LookDirection = new System.Windows.Media.Media3D.Vector3D(0, 0, -11),
                Position = new System.Windows.Media.Media3D.Point3D(0, 0, 11),
                UpDirection = new System.Windows.Media.Media3D.Vector3D(0, 1, 0),
                FarPlaneDistance = 5000,
                Width = 200,
            };
            viewport.Camera = camera;
		}

		public MeshGeometry3D Plane1Geometry { get; }
        public MeshGeometry3D Plane2Geometry { get; }

        public Material Plane1Material { get; }
        public Material Plane2Material { get; }
	}
}
