Imports System
Imports System.Collections.Generic
Imports System.Windows
Imports DevExpress.Xpf.Map

Namespace LoadPrjData
    Partial Public Class MainWindow
        Inherits Window


        Private data_Renamed As New List(Of MapData)()
        Public ReadOnly Property Data() As List(Of MapData)
            Get
                Return data_Renamed
            End Get
        End Property

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub Window_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim baseUri As New Uri(System.Reflection.Assembly.GetEntryAssembly().Location)
'            #Region "#AutomaticallyLoaded"
            data_Renamed.Add(New MapData() With {.Name = "Automatically loaded data", .FileUri = New Uri(baseUri, "../../Shapefiles/Albers/switzerland.shp")})
'            #End Region ' #AutomaticallyLoaded
'            #Region "#LoadPrjFile"
            data_Renamed.Add(New MapData() With {.Name = "LoadPrjFile( ) loaded coordinate system", .FileUri = New Uri(baseUri, "../../Shapefiles/Lambert/Belize.shp"), .CoordinateSystem = ShapefileDataAdapter.LoadPrjFile(New Uri(baseUri, "../../Shapefiles/Lambert/Projection.prj"))})
'            #End Region ' #LoadPrjFile
'            #Region "#ManuallyCreated"
            data_Renamed.Add(New MapData() With { _
                .Name = "Manually created coordinate system", .FileUri = New Uri(baseUri, "../../Shapefiles/TransverseMercator/israel.shp"), .CoordinateSystem = New CartesianSourceCoordinateSystem() With { _
                    .CoordinateConverter = New UTMCartesianToGeoConverter() With {.Hemisphere = Hemisphere.Northern, .UtmZone = 36} _
                } _
            })
'            #End Region ' #ManuallyCreated

            Me.DataContext = Me
        End Sub

        Private Sub Button_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            map.ZoomToFitLayerItems(0.4)
        End Sub
    End Class

    Public Class MapData
        Public Property Name() As String
        Public Property FileUri() As Uri
        Public Property CoordinateSystem() As SourceCoordinateSystem
    End Class
End Namespace
