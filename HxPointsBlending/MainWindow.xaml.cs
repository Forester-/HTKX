using HelixToolkit.Wpf.SharpDX;
using hx = HelixToolkit.Wpf.SharpDX;
using SharpDX;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Media3D;

namespace HxPointsBlending
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = this;

            InitializeComponent();

            #region Create planes
            var builder = new MeshBuilder(true, false, false);
            builder.AddRectangularMesh(BoxFaces.Front, 10, 10, 100, 100);
            PlaneGeometry = builder.ToMesh();

            RedDiffuseMaterial = new hx.DiffuseMaterial()
            {
                DiffuseColor = new Color4(1f, 0f, 0f, 0.5f),
            };
            BlueDiffuseMaterial = new hx.DiffuseMaterial()
            {
                DiffuseColor = new Color4(0f, 0f, 1f, 0.5f),
            };
            RedPhongMaterial = new PhongMaterial()
            {
                AmbientColor = new Color4(1f, 0f, 0f, 0.5f),
            };
            BluePhongMaterial = new PhongMaterial()
            {
                AmbientColor = new Color4(0f, 0f, 1f, 0.5f),
            };

            var matrix = new Matrix3D();
            matrix.Translate(new Vector3D(-125, -50, 0));
            RedDiffusePlaneTransform = new MatrixTransform3D(matrix);
            matrix = new Matrix3D();
            matrix.Translate(new Vector3D(-125, -50, 50));
            BlueDiffusePlaneTransform = new MatrixTransform3D(matrix);

            matrix = new Matrix3D();
            matrix.Translate(new Vector3D(25, -50, 0));
            RedPhongPlaneTransform = new MatrixTransform3D(matrix);
            matrix = new Matrix3D();
            matrix.Translate(new Vector3D(25, -50, 50));
            BluePhongPlaneTransform = new MatrixTransform3D(matrix);
            #endregion

            #region Create points
            PointsGeometry = new PointGeometry3D()
            {
                Positions = new Vector3Collection(new Vector3[] {
                    new Vector3(0, 0, 5),
                }),
            };
            PointsInstances = new List<Matrix>();
            for (var x = -50; x <= 50; x += 10)
            {
                var matrix2 = Matrix.Translation(x, x / 5f, x / 5f);
                PointsInstances.Add(matrix2);
            }

            matrix = new Matrix3D();
            matrix.Translate(new Vector3D(-75, 0, 0));
            DiffusePointsTransform = new MatrixTransform3D(matrix);
            matrix = new Matrix3D();
            matrix.Translate(new Vector3D(75, 0, 0));
            PhongPointsTransform = new MatrixTransform3D(matrix);
            #endregion

            if (viewport.Camera is hx.OrthographicCamera camera)
            {
                camera.NearPlaneDistance = -5000;
                camera.FarPlaneDistance = 5000;
                camera.LookDirection = new Vector3D(0, 0, -10);
                camera.Position = new Point3D(0, 0, 11);
                camera.UpDirection = new Vector3D(0, 1, 0);
                camera.Width = 300;
            }
        }

        public hx.MeshGeometry3D PlaneGeometry { get; }
        public PointGeometry3D PointsGeometry { get; }
        public List<Matrix> PointsInstances { get; }

        public hx.Material RedDiffuseMaterial { get; }
        public hx.Material BlueDiffuseMaterial { get; }
        public hx.Material RedPhongMaterial { get; }
        public hx.Material BluePhongMaterial { get; }

        public Transform3D RedDiffusePlaneTransform { get; }
        public Transform3D BlueDiffusePlaneTransform { get; }
        public Transform3D DiffusePointsTransform { get; }
        public Transform3D RedPhongPlaneTransform { get; }
        public Transform3D BluePhongPlaneTransform { get; }
        public Transform3D PhongPointsTransform { get; }
    }
}
