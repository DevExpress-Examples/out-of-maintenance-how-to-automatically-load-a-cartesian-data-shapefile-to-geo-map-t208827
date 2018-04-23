using System;
using System.Collections.Generic;
using System.Windows;
using DevExpress.Xpf.Map;

namespace LoadPrjData {
    public partial class MainWindow : Window {
        List<MapData> data = new List<MapData>();
        public List<MapData> Data { get { return data; } }

        public MainWindow() {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            Uri baseUri = new Uri(System.Reflection.Assembly.GetEntryAssembly().Location);
            #region #AutomaticallyLoaded
            data.Add(new MapData() {
                Name = "Automatically loaded data",
                FileUri = new Uri(baseUri, "../../Shapefiles/Albers/switzerland.shp")
            });
            #endregion #AutomaticallyLoaded
            #region #LoadPrjFile
            data.Add(new MapData() {
                Name = "LoadPrjFile( ) loaded coordinate system",
                FileUri = new Uri(baseUri, "../../Shapefiles/Lambert/Belize.shp"),
                CoordinateSystem = ShapefileDataAdapter.LoadPrjFile(new Uri (
                    baseUri, "../../Shapefiles/Lambert/Projection.prj"))
            });
            #endregion #LoadPrjFile
            #region #ManuallyCreated
            data.Add(new MapData() {
                Name = "Manually created coordinate system",
                FileUri = new Uri(baseUri, "../../Shapefiles/TransverseMercator/israel.shp"),
                CoordinateSystem = new CartesianSourceCoordinateSystem() {
                    CoordinateConverter = new UTMCartesianToGeoConverter() {
                        Hemisphere = Hemisphere.Northern,
                        UtmZone = 36
                    }
                }
            });
            #endregion #ManuallyCreated

            this.DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            map.ZoomToFitLayerItems(0.4);
        }
    }

    public class MapData {
        public string Name { get; set; }
        public Uri FileUri { get; set; }
        public SourceCoordinateSystem CoordinateSystem { get; set; }
    }
}
