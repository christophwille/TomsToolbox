﻿#pragma warning disable CCRSI_CreateContractInvariantMethod // Missing Contract Invariant Method.

namespace SampleApp.Samples;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Composition;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

using PropertyChanged;

using SampleApp.Map;

using TomsToolbox.Wpf;
using TomsToolbox.Wpf.Composition.AttributedModel;
using TomsToolbox.Wpf.Controls;

[VisualCompositionExport(RegionId.Main, Sequence = 1)]
[AddINotifyPropertyChangedInterface]
public class MapViewModel
{
    private static readonly string _configurationFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory!, "Map", "MapSources.xml");
    private readonly MapSourceFile _mapSourceFile;

    [ImportingConstructor]
    // ReSharper disable once UnusedParameter.Local
    public MapViewModel(ChessViewModel dummyToTestInjection)
    {
        try
        {
            _mapSourceFile = MapSourceFile.Load(_configurationFileName);
            ImageProvider = _mapSourceFile.MapSources?.FirstOrDefault();
        }
        catch (IOException ex)
        {
            MessageBox.Show(ex.Message);
            throw;
        }
    }

    public IList<MapSource>? MapSources => _mapSourceFile.MapSources;

    public IImageProvider? ImageProvider { get; set; }

    public Coordinates Center { get; set; } = new(52.5075419, 13.4251364);

    public Coordinates MousePosition { get; set; }

    public Poi? SelectedPoi { get; set; }

#pragma warning disable IDE0051 // Remove unused private members
    private void OnSelectedPoiChanged()
#pragma warning restore IDE0051 // Remove unused private members
    {
        if (SelectedPoi != null)
        {
            Center = SelectedPoi.Coordinates;

        }
    }

    public IList<Poi> Pois { get; } = new ObservableCollection<Poi>
    {
        new() {Coordinates = new Coordinates(52.3747158, 4.8986142), Description = "Amsterdam"},
        new() {Coordinates = new Coordinates(52.5075419, 13.4251364), Description = "Berlin"},
        new() {Coordinates = new Coordinates(55.749792, 37.632495), Description = "Moscow"},
        new() {Coordinates = new Coordinates(40.7033127, -73.979681), Description = "New York"},
        new() {Coordinates = new Coordinates(41.9100711, 12.5359979), Description = "Rome"},
    };

    public Rect Bounds { get; set; }

    [Description(nameof(OnSelectionChanged))]
    public Rect Selection { get; set; } = Rect.Empty;

    private void OnSelectionChanged()
    {
        var value = Selection;
        if (value.IsEmpty)
            return;

        // Sample: Transform to WGS-84:
        // ReSharper disable UnusedVariable => just show how to use this..
#pragma warning disable IDE0059 // Value assigned to symbol is never used
        var topLeft = (Coordinates)value.TopLeft;
        var bottomRight = (Coordinates)value.BottomRight;
#pragma warning restore IDE0059 // Value assigned to symbol is never used
        // ReSharper restore UnusedVariable
    }

    public ICommand ClearSelectionCommand => new DelegateCommand(() => !Selection.IsEmpty, () => Selection = Rect.Empty);

    public ICommand MouseDoubleClickCommand => new DelegateCommand<Point>(p => Pois.Add(new Poi { Coordinates = p, Description = "New Poi" }));

    public IList<Coordinates> Track { get; } = new []
    {
        new Coordinates(52.550048, 13.295667),
        new Coordinates(52.549874, 13.295785),
        new Coordinates(52.549612, 13.295961),
        new Coordinates(52.549524, 13.29594),
        new Coordinates(52.549441, 13.295949),
        new Coordinates(52.549284, 13.295988),
        new Coordinates(52.549072, 13.296136),
        new Coordinates(52.548669, 13.296441),
        new Coordinates(52.548267, 13.296747),
        new Coordinates(52.54822, 13.296781),
        new Coordinates(52.548121, 13.29685),
        new Coordinates(52.548001, 13.29694),
        new Coordinates(52.547998, 13.297083),
        new Coordinates(52.547991, 13.297359),
        new Coordinates(52.547987, 13.29759),
        new Coordinates(52.547972, 13.298302),
        new Coordinates(52.547957, 13.299015),
        new Coordinates(52.547944, 13.299618),
        new Coordinates(52.547932, 13.30022),
        new Coordinates(52.547919, 13.300823),
        new Coordinates(52.547858, 13.300773),
        new Coordinates(52.547679, 13.300628),
        new Coordinates(52.5477, 13.300597),
        new Coordinates(52.547812, 13.300682),
        new Coordinates(52.54812, 13.300924),
        new Coordinates(52.548429, 13.301166),
        new Coordinates(52.548737, 13.301407),
        new Coordinates(52.548874, 13.301524),
        new Coordinates(52.548819, 13.30169),
        new Coordinates(52.54879, 13.301749),
        new Coordinates(52.548764, 13.301764),
        new Coordinates(52.548769, 13.301869),
        new Coordinates(52.548764, 13.302086),
        new Coordinates(52.548746, 13.302616),
        new Coordinates(52.548734, 13.303156),
        new Coordinates(52.548723, 13.303696),
        new Coordinates(52.548708, 13.304294),
        new Coordinates(52.548694, 13.304892),
        new Coordinates(52.54868, 13.305491),
        new Coordinates(52.548658, 13.306048),
        new Coordinates(52.548636, 13.306606),
        new Coordinates(52.548614, 13.307163),
        new Coordinates(52.548592, 13.307721),
        new Coordinates(52.548572, 13.308429),
        new Coordinates(52.548552, 13.309137),
        new Coordinates(52.548532, 13.309845),
        new Coordinates(52.548595, 13.31021),
        new Coordinates(52.548622, 13.31037),
        new Coordinates(52.548724, 13.31051),
        new Coordinates(52.548737, 13.310591),
        new Coordinates(52.548729, 13.311115),
        new Coordinates(52.548721, 13.311639),
        new Coordinates(52.548759, 13.311831),
        new Coordinates(52.54883, 13.311984),
        new Coordinates(52.548914, 13.312045),
        new Coordinates(52.549308, 13.312178),
        new Coordinates(52.549375, 13.312265),
        new Coordinates(52.549422, 13.312352),
        new Coordinates(52.549739, 13.312377),
        new Coordinates(52.550056, 13.312403),
        new Coordinates(52.550373, 13.312429),
        new Coordinates(52.550593, 13.312825),
        new Coordinates(52.550668, 13.312857),
        new Coordinates(52.550836, 13.313107),
        new Coordinates(52.550805, 13.313204),
        new Coordinates(52.550768, 13.313316),
        new Coordinates(52.550656, 13.31388),
        new Coordinates(52.550541, 13.314457),
        new Coordinates(52.550418, 13.315058),
        new Coordinates(52.550383, 13.315248),
        new Coordinates(52.550302, 13.315645),
        new Coordinates(52.550179, 13.31627),
        new Coordinates(52.550086, 13.316743),
        new Coordinates(52.550021, 13.316754),
        new Coordinates(52.549932, 13.316777),
        new Coordinates(52.549877, 13.316814),
        new Coordinates(52.549778, 13.316508),
        new Coordinates(52.549666, 13.31639),
        new Coordinates(52.549458, 13.315853),
        new Coordinates(52.549251, 13.315317),
        new Coordinates(52.549043, 13.31478),
        new Coordinates(52.548835, 13.314243),
        new Coordinates(52.548516, 13.314573),
        new Coordinates(52.548371, 13.314996),
        new Coordinates(52.548264, 13.315201),
        new Coordinates(52.548239, 13.315716),
        new Coordinates(52.548237, 13.315786),
        new Coordinates(52.548213, 13.316396),
        new Coordinates(52.548189, 13.317006),
        new Coordinates(52.548177, 13.317312),
        new Coordinates(52.548132, 13.317652),
        new Coordinates(52.548096, 13.317837),
        new Coordinates(52.548067, 13.317918),
        new Coordinates(52.548021, 13.31802),
        new Coordinates(52.547841, 13.318352),
        new Coordinates(52.547784, 13.318432),
        new Coordinates(52.547489, 13.318816),
        new Coordinates(52.547194, 13.3192),
        new Coordinates(52.546861, 13.319643),
        new Coordinates(52.546528, 13.320086),
        new Coordinates(52.546195, 13.320529),
        new Coordinates(52.545862, 13.320972),
        new Coordinates(52.545529, 13.321415),
        new Coordinates(52.545409, 13.321604),
        new Coordinates(52.545204, 13.321871),
        new Coordinates(52.544999, 13.322138),
        new Coordinates(52.545121, 13.322381),
        new Coordinates(52.54492, 13.322647),
        new Coordinates(52.544712, 13.322965),
        new Coordinates(52.544503, 13.323284),
        new Coordinates(52.544305, 13.323602),
        new Coordinates(52.544092, 13.324004),
        new Coordinates(52.543974, 13.324335),
        new Coordinates(52.543907, 13.324676),
        new Coordinates(52.543918, 13.324788),
        new Coordinates(52.543849, 13.324889),
        new Coordinates(52.543834, 13.324989),
        new Coordinates(52.543847, 13.325157),
        new Coordinates(52.543668, 13.325388),
        new Coordinates(52.54349, 13.325618),
        new Coordinates(52.543445, 13.32567),
        new Coordinates(52.543418, 13.325686),
        new Coordinates(52.54339, 13.325675),
        new Coordinates(52.543364, 13.325635),
        new Coordinates(52.543228, 13.325376),
        new Coordinates(52.543199, 13.325342),
        new Coordinates(52.543166, 13.325326),
        new Coordinates(52.543132, 13.325327),
        new Coordinates(52.543099, 13.325357),
        new Coordinates(52.542795, 13.32576),
        new Coordinates(52.54249, 13.326164),
        new Coordinates(52.542289, 13.326429),
        new Coordinates(52.542089, 13.326695),
        new Coordinates(52.541851, 13.32701),
        new Coordinates(52.541612, 13.327326),
        new Coordinates(52.541431, 13.327587),
        new Coordinates(52.541335, 13.327775),
        new Coordinates(52.541273, 13.328008),
        new Coordinates(52.541222, 13.32839),
        new Coordinates(52.541202, 13.328805),
        new Coordinates(52.541208, 13.329299),
        new Coordinates(52.541264, 13.330028),
        new Coordinates(52.541304, 13.330536),
        new Coordinates(52.541343, 13.331043),
        new Coordinates(52.541382, 13.33155),
        new Coordinates(52.541409, 13.332128),
        new Coordinates(52.541407, 13.332624),
        new Coordinates(52.541405, 13.333119),
        new Coordinates(52.541404, 13.333615),
        new Coordinates(52.541403, 13.333696),
        new Coordinates(52.541402, 13.334002),
        new Coordinates(52.541301, 13.334197),
        new Coordinates(52.541404, 13.334392),
        new Coordinates(52.541243, 13.334594),
        new Coordinates(52.541188, 13.334661),
        new Coordinates(52.541039, 13.334835),
        new Coordinates(52.540908, 13.335296),
        new Coordinates(52.540777, 13.335757),
        new Coordinates(52.540645, 13.336218),
        new Coordinates(52.540488, 13.336794),
        new Coordinates(52.540382, 13.337147),
        new Coordinates(52.540276, 13.3375),
        new Coordinates(52.540135, 13.337997),
        new Coordinates(52.54006, 13.33832),
        new Coordinates(52.539977, 13.338626),
        new Coordinates(52.539911, 13.338872),
        new Coordinates(52.53985, 13.339109),
        new Coordinates(52.539803, 13.339329),
        new Coordinates(52.539746, 13.339612),
        new Coordinates(52.539711, 13.33986),
        new Coordinates(52.539783, 13.339998),
        new Coordinates(52.53975, 13.340163),
        new Coordinates(52.539665, 13.340809),
        new Coordinates(52.53958, 13.341456),
        new Coordinates(52.539494, 13.342102),
        new Coordinates(52.539409, 13.342748),
        new Coordinates(52.539324, 13.343395),
        new Coordinates(52.539237, 13.344039),
        new Coordinates(52.539151, 13.344682),
        new Coordinates(52.539134, 13.344812),
        new Coordinates(52.539113, 13.344975),
        new Coordinates(52.539092, 13.345143),
        new Coordinates(52.539196, 13.345276),
        new Coordinates(52.539366, 13.34549),
        new Coordinates(52.539619, 13.345835),
        new Coordinates(52.539949, 13.346293),
        new Coordinates(52.540279, 13.346752),
        new Coordinates(52.540356, 13.346856),
        new Coordinates(52.540675, 13.347299),
        new Coordinates(52.540994, 13.347743),
        new Coordinates(52.541193, 13.348018),
        new Coordinates(52.541264, 13.348114),
        new Coordinates(52.54155, 13.3485),
        new Coordinates(52.541789, 13.348913),
        new Coordinates(52.541919, 13.349172),
        new Coordinates(52.541987, 13.349327),
        new Coordinates(52.542013, 13.349384),
        new Coordinates(52.542105, 13.34959),
        new Coordinates(52.542036, 13.349679),
        new Coordinates(52.542003, 13.349724),
        new Coordinates(52.541914, 13.349844),
        new Coordinates(52.541937, 13.349968),
        new Coordinates(52.541942, 13.349997),
        new Coordinates(52.541982, 13.350209),
        new Coordinates(52.542013, 13.350366),
        new Coordinates(52.54208, 13.350728),
        new Coordinates(52.542147, 13.351089),
        new Coordinates(52.54224, 13.351241),
        new Coordinates(52.54225, 13.351304),
        new Coordinates(52.5423, 13.351539),
        new Coordinates(52.542355, 13.351806),
        new Coordinates(52.542361, 13.351832),
        new Coordinates(52.542413, 13.352084),
        new Coordinates(52.542472, 13.352367),
        new Coordinates(52.542531, 13.352649),
        new Coordinates(52.5426, 13.352979),
        new Coordinates(52.542678, 13.353347),
        new Coordinates(52.542694, 13.353422),
        new Coordinates(52.54279, 13.353892),
        new Coordinates(52.542873, 13.3543),
        new Coordinates(52.542917, 13.354514),
        new Coordinates(52.542944, 13.354646),
        new Coordinates(52.542969, 13.354769),
        new Coordinates(52.542989, 13.354866),
        new Coordinates(52.543012, 13.354982),
        new Coordinates(52.543044, 13.355135),
        new Coordinates(52.543082, 13.355324),
        new Coordinates(52.543133, 13.355571),
        new Coordinates(52.54318, 13.355798),
        new Coordinates(52.543261, 13.356194),
        new Coordinates(52.543379, 13.356764),
        new Coordinates(52.543387, 13.356801),
        new Coordinates(52.543412, 13.35692),
        new Coordinates(52.543455, 13.357131),
        new Coordinates(52.543467, 13.357187),
        new Coordinates(52.543502, 13.357355),
        new Coordinates(52.543517, 13.357425),
        new Coordinates(52.543581, 13.357732),
        new Coordinates(52.54366, 13.358111),
        new Coordinates(52.543676, 13.358189),
        new Coordinates(52.543683, 13.358224),
        new Coordinates(52.543718, 13.358342),
        new Coordinates(52.543815, 13.358654),
        new Coordinates(52.543853, 13.358779),
        new Coordinates(52.543926, 13.359013),
        new Coordinates(52.544056, 13.359421),
        new Coordinates(52.544179, 13.359806),
        new Coordinates(52.544286, 13.360141),
        new Coordinates(52.544325, 13.360263),
        new Coordinates(52.544477, 13.360725),
        new Coordinates(52.544628, 13.361186),
        new Coordinates(52.544691, 13.361541),
        new Coordinates(52.544716, 13.361646),
        new Coordinates(52.544732, 13.361711),
        new Coordinates(52.544765, 13.36186),
        new Coordinates(52.544816, 13.362105),
        new Coordinates(52.544847, 13.362249),
        new Coordinates(52.544857, 13.362364),
        new Coordinates(52.544861, 13.36259),
        new Coordinates(52.5449, 13.362854),
        new Coordinates(52.544896, 13.363131),
        new Coordinates(52.544891, 13.363468),
        new Coordinates(52.544885, 13.363888),
        new Coordinates(52.544879, 13.364307),
        new Coordinates(52.544877, 13.3644),
        new Coordinates(52.544875, 13.364529),
        new Coordinates(52.544873, 13.364703),
        new Coordinates(52.54487, 13.364859),
        new Coordinates(52.544868, 13.365019),
        new Coordinates(52.544865, 13.36525),
        new Coordinates(52.544862, 13.365415),
        new Coordinates(52.544857, 13.365664),
        new Coordinates(52.544856, 13.365858),
        new Coordinates(52.544852, 13.366096),
        new Coordinates(52.544853, 13.366193),
        new Coordinates(52.544854, 13.36628),
        new Coordinates(52.544855, 13.366521),
        new Coordinates(52.544858, 13.366917),
        new Coordinates(52.544859, 13.367001),
        new Coordinates(52.544842, 13.367045),
        new Coordinates(52.544832, 13.367104),
        new Coordinates(52.544829, 13.367534),
        new Coordinates(52.544826, 13.367963),
        new Coordinates(52.544846, 13.368032),
        new Coordinates(52.544843, 13.368126),
        new Coordinates(52.54484, 13.36819),
        new Coordinates(52.544846, 13.368689),
        new Coordinates(52.544852, 13.369189),
        new Coordinates(52.544864, 13.369421),
        new Coordinates(52.544861, 13.369538),
        new Coordinates(52.544874, 13.370091),
        new Coordinates(52.544722, 13.369978),
        new Coordinates(52.544692, 13.370087),
        new Coordinates(52.544643, 13.370291),
        new Coordinates(52.544595, 13.37048),
        new Coordinates(52.544543, 13.370688),
        new Coordinates(52.544521, 13.370774),
        new Coordinates(52.544509, 13.370821),
        new Coordinates(52.544429, 13.37114),
        new Coordinates(52.544381, 13.371329),
        new Coordinates(52.544366, 13.371389),
        new Coordinates(52.544257, 13.37182),
        new Coordinates(52.544148, 13.372252),
        new Coordinates(52.544028, 13.372731),
        new Coordinates(52.543894, 13.373265),
        new Coordinates(52.543856, 13.373415),
        new Coordinates(52.543777, 13.373739),
        new Coordinates(52.543748, 13.373852),
        new Coordinates(52.543708, 13.374015),
        new Coordinates(52.543657, 13.374218),
        new Coordinates(52.54362, 13.374328),
        new Coordinates(52.54348, 13.374853),
        new Coordinates(52.54343, 13.37504),
        new Coordinates(52.543404, 13.375136),
        new Coordinates(52.543337, 13.375389),
        new Coordinates(52.543321, 13.375447),
        new Coordinates(52.543222, 13.375819),
        new Coordinates(52.543122, 13.376191),
        new Coordinates(52.54311, 13.376238),
        new Coordinates(52.543078, 13.376408),
        new Coordinates(52.543189, 13.376553),
        new Coordinates(52.543219, 13.376593),
        new Coordinates(52.543471, 13.37693),
        new Coordinates(52.543723, 13.377266),
        new Coordinates(52.543975, 13.377603),
        new Coordinates(52.544156, 13.377831),
        new Coordinates(52.544336, 13.378058),
        new Coordinates(52.544353, 13.37808),
        new Coordinates(52.54457, 13.378352),
        new Coordinates(52.544656, 13.378462),
        new Coordinates(52.54468, 13.378492),
        new Coordinates(52.544763, 13.378597),
        new Coordinates(52.544695, 13.378707),
        new Coordinates(52.544676, 13.378738),
        new Coordinates(52.544635, 13.378806),
        new Coordinates(52.544495, 13.379036),
        new Coordinates(52.54447, 13.37908),
        new Coordinates(52.544244, 13.379453),
        new Coordinates(52.543979, 13.379892),
        new Coordinates(52.543751, 13.38026),
        new Coordinates(52.543523, 13.380627),
        new Coordinates(52.543222, 13.381147),
        new Coordinates(52.542921, 13.381667),
        new Coordinates(52.542619, 13.382187),
        new Coordinates(52.542318, 13.382707),
        new Coordinates(52.542298, 13.382742),
        new Coordinates(52.542206, 13.38284),
        new Coordinates(52.542275, 13.382953),
        new Coordinates(52.542295, 13.382992),
        new Coordinates(52.5424, 13.383192),
        new Coordinates(52.542449, 13.383285),
        new Coordinates(52.542531, 13.383521),
        new Coordinates(52.542694, 13.384112),
        new Coordinates(52.542856, 13.384703),
        new Coordinates(52.543019, 13.385294),
        new Coordinates(52.543181, 13.385885),
        new Coordinates(52.543343, 13.386476),
        new Coordinates(52.54353, 13.387121),
        new Coordinates(52.543716, 13.387767),
        new Coordinates(52.543902, 13.388413),
        new Coordinates(52.544089, 13.389059),
        new Coordinates(52.544185, 13.389404),
        new Coordinates(52.544281, 13.38975),
        new Coordinates(52.544343, 13.389966),
        new Coordinates(52.544389, 13.390136),
        new Coordinates(52.544426, 13.39027),
        new Coordinates(52.544584, 13.390838),
        new Coordinates(52.544594, 13.390874),
        new Coordinates(52.544623, 13.390978),
        new Coordinates(52.544676, 13.39104),
        new Coordinates(52.544719, 13.391091),
        new Coordinates(52.544763, 13.391212),
        new Coordinates(52.544779, 13.391258),
        new Coordinates(52.544992, 13.391834),
        new Coordinates(52.545039, 13.39196),
        new Coordinates(52.545198, 13.392478),
        new Coordinates(52.545358, 13.392996),
        new Coordinates(52.545541, 13.39361),
        new Coordinates(52.545725, 13.394224),
        new Coordinates(52.545908, 13.394839),
        new Coordinates(52.545928, 13.394906),
        new Coordinates(52.545992, 13.39512),
        new Coordinates(52.546037, 13.39529),
        new Coordinates(52.546064, 13.395469),
        new Coordinates(52.546106, 13.395941),
        new Coordinates(52.546149, 13.396414),
        new Coordinates(52.546163, 13.396569),
        new Coordinates(52.546221, 13.397229),
        new Coordinates(52.546264, 13.39768),
        new Coordinates(52.546269, 13.397734),
        new Coordinates(52.546284, 13.397895),
        new Coordinates(52.546263, 13.398045),
        new Coordinates(52.546254, 13.398105),
        new Coordinates(52.546261, 13.398182),
        new Coordinates(52.546295, 13.398562),
        new Coordinates(52.546329, 13.398943),
        new Coordinates(52.546347, 13.399149),
        new Coordinates(52.546363, 13.39919),
        new Coordinates(52.546413, 13.399258),
        new Coordinates(52.546429, 13.399382),
        new Coordinates(52.546484, 13.399958),
        new Coordinates(52.546538, 13.400535),
        new Coordinates(52.546592, 13.401112),
        new Coordinates(52.546604, 13.401262),
        new Coordinates(52.54662, 13.401435),
        new Coordinates(52.546655, 13.401827),
        new Coordinates(52.546688, 13.402186),
        new Coordinates(52.54675, 13.402862),
        new Coordinates(52.546812, 13.403537),
        new Coordinates(52.546874, 13.404211),
        new Coordinates(52.546936, 13.404886),
        new Coordinates(52.546984, 13.405445),
        new Coordinates(52.546998, 13.405602),
        new Coordinates(52.547054, 13.406223),
        new Coordinates(52.54711, 13.406844),
        new Coordinates(52.547166, 13.407466),
        new Coordinates(52.547227, 13.408135),
        new Coordinates(52.547288, 13.408805),
        new Coordinates(52.547349, 13.409474),
        new Coordinates(52.547363, 13.409619),
        new Coordinates(52.547373, 13.409708),
        new Coordinates(52.547428, 13.410364),
        new Coordinates(52.547484, 13.41102),
        new Coordinates(52.547539, 13.411676),
        new Coordinates(52.547595, 13.412332),
        new Coordinates(52.54765, 13.412988),
        new Coordinates(52.547657, 13.413066),
        new Coordinates(52.54767, 13.413231),
        new Coordinates(52.547666, 13.413287),
        new Coordinates(52.547663, 13.413322),
        new Coordinates(52.547652, 13.413454),
        new Coordinates(52.547647, 13.413519),
        new Coordinates(52.547643, 13.413564),
        new Coordinates(52.547632, 13.413723),
        new Coordinates(52.547628, 13.413772),
        new Coordinates(52.547576, 13.414433),
        new Coordinates(52.54752, 13.415159),
        new Coordinates(52.547511, 13.415289),
        new Coordinates(52.547451, 13.415586),
        new Coordinates(52.547366, 13.415841),
        new Coordinates(52.547181, 13.416374),
        new Coordinates(52.546987, 13.416928),
        new Coordinates(52.546792, 13.417482),
        new Coordinates(52.546597, 13.418036),
        new Coordinates(52.546579, 13.418088),
        new Coordinates(52.546519, 13.418257),
        new Coordinates(52.546462, 13.418384),
        new Coordinates(52.546439, 13.418434),
        new Coordinates(52.546224, 13.418915),
        new Coordinates(52.546008, 13.419395),
        new Coordinates(52.545792, 13.419875),
        new Coordinates(52.545594, 13.420334),
        new Coordinates(52.545396, 13.420793),
        new Coordinates(52.545198, 13.421253),
        new Coordinates(52.545017, 13.421672),
        new Coordinates(52.544836, 13.422092),
        new Coordinates(52.544655, 13.422512),
        new Coordinates(52.544596, 13.422645),
        new Coordinates(52.544928, 13.422956),
        new Coordinates(52.54526, 13.423267),
        new Coordinates(52.545488, 13.423481),
        new Coordinates(52.54564, 13.423611),
        new Coordinates(52.545992, 13.423905),
        new Coordinates(52.546131, 13.424042),
        new Coordinates(52.546227, 13.424135),
        new Coordinates(52.546412, 13.424294),
        new Coordinates(52.546655, 13.424513),
        new Coordinates(52.546899, 13.424732),
        new Coordinates(52.546946, 13.424774),
        new Coordinates(52.547018, 13.42484),
        new Coordinates(52.547395, 13.425171),
        new Coordinates(52.547453, 13.425224),
        new Coordinates(52.547721, 13.425472),
        new Coordinates(52.547996, 13.42572),
        new Coordinates(52.548276, 13.425964),
        new Coordinates(52.548555, 13.426208),
        new Coordinates(52.54863, 13.426266),
        new Coordinates(52.548587, 13.426393),
        new Coordinates(52.548421, 13.426869),
        new Coordinates(52.548256, 13.427345),
        new Coordinates(52.548053, 13.427977),
        new Coordinates(52.547927, 13.428321),
        new Coordinates(52.547907, 13.428378),
        new Coordinates(52.547849, 13.428536),
        new Coordinates(52.547811, 13.42864),
        new Coordinates(52.5478, 13.428678),
        new Coordinates(52.547767, 13.428776),
        new Coordinates(52.547719, 13.428913),
        new Coordinates(52.547696, 13.42898),
        new Coordinates(52.547562, 13.429365),
        new Coordinates(52.547487, 13.429582),
        new Coordinates(52.54733, 13.430039),
        new Coordinates(52.547173, 13.430496),
        new Coordinates(52.547154, 13.430548),
        new Coordinates(52.547034, 13.43087),
        new Coordinates(52.546914, 13.431191),
        new Coordinates(52.546753, 13.43168),
        new Coordinates(52.546591, 13.432169),
        new Coordinates(52.546429, 13.432646),
        new Coordinates(52.546266, 13.433123),
        new Coordinates(52.546079, 13.433662),
        new Coordinates(52.545891, 13.434201),
        new Coordinates(52.545667, 13.434834),
        new Coordinates(52.545442, 13.435467),
        new Coordinates(52.545282, 13.435928),
        new Coordinates(52.545122, 13.436389),
        new Coordinates(52.544961, 13.43685),
        new Coordinates(52.544828, 13.437227),
        new Coordinates(52.544695, 13.437603),
        new Coordinates(52.544613, 13.437846),
        new Coordinates(52.544455, 13.438306),
        new Coordinates(52.544261, 13.438871),
        new Coordinates(52.544067, 13.439436),
        new Coordinates(52.543873, 13.440001),
        new Coordinates(52.543679, 13.440566),
        new Coordinates(52.543485, 13.44113),
        new Coordinates(52.543434, 13.441279),
        new Coordinates(52.543382, 13.441413),
        new Coordinates(52.543362, 13.441463),
        new Coordinates(52.543321, 13.441568),
        new Coordinates(52.543397, 13.441657),
        new Coordinates(52.54372, 13.442031),
        new Coordinates(52.543965, 13.442315),
        new Coordinates(52.54421, 13.4426),
        new Coordinates(52.544468, 13.44293),
        new Coordinates(52.544677, 13.443277),
        new Coordinates(52.544775, 13.443447),
        new Coordinates(52.545007, 13.443861),
        new Coordinates(52.545136, 13.444136),
        new Coordinates(52.545226, 13.44432),
        new Coordinates(52.545252, 13.44438),
        new Coordinates(52.545333, 13.444537),
        new Coordinates(52.545505, 13.444949),
        new Coordinates(52.545585, 13.445122),
        new Coordinates(52.545705, 13.445383),
        new Coordinates(52.545959, 13.445941),
        new Coordinates(52.546212, 13.4465),
        new Coordinates(52.546435, 13.44699),
        new Coordinates(52.546658, 13.447479),
        new Coordinates(52.54681, 13.447811),
        new Coordinates(52.546828, 13.447852),
        new Coordinates(52.546915, 13.448048),
        new Coordinates(52.546851, 13.448171),
        new Coordinates(52.546808, 13.448276),
        new Coordinates(52.546674, 13.448603),
        new Coordinates(52.54654, 13.44893),
        new Coordinates(52.546515, 13.448996),
        new Coordinates(52.546419, 13.449222),
        new Coordinates(52.546359, 13.44936),
        new Coordinates(52.54633, 13.449429),
        new Coordinates(52.546084, 13.450025),
        new Coordinates(52.546021, 13.45017),
        new Coordinates(52.54608, 13.450433),
        new Coordinates(52.546206, 13.451003),
        new Coordinates(52.546332, 13.451573),
        new Coordinates(52.546345, 13.451631),
        new Coordinates(52.546425, 13.451992),
        new Coordinates(52.546505, 13.452354),
        new Coordinates(52.54661, 13.452826),
        new Coordinates(52.546716, 13.453298),
        new Coordinates(52.546821, 13.45377),
        new Coordinates(52.546949, 13.454365),
        new Coordinates(52.547078, 13.454961),
        new Coordinates(52.547207, 13.455556),
        new Coordinates(52.547336, 13.456151),
        new Coordinates(52.547464, 13.456746),
        new Coordinates(52.547581, 13.457277),
        new Coordinates(52.547698, 13.457808),
        new Coordinates(52.547741, 13.458001),
        new Coordinates(52.547847, 13.458484),
        new Coordinates(52.547954, 13.458967),
        new Coordinates(52.54806, 13.45945),
        new Coordinates(52.547768, 13.459625),
        new Coordinates(52.547477, 13.4598),
        new Coordinates(52.547185, 13.459975),
        new Coordinates(52.547087, 13.460029),
        new Coordinates(52.546953, 13.460107),
        new Coordinates(52.546574, 13.460329),
        new Coordinates(52.546195, 13.46055),
        new Coordinates(52.546041, 13.460644),
        new Coordinates(52.545748, 13.460811),
        new Coordinates(52.545455, 13.460978),
        new Coordinates(52.5455, 13.461389),
        new Coordinates(52.545541, 13.461761),
        new Coordinates(52.545582, 13.462133),
        new Coordinates(52.545642, 13.462505),
        new Coordinates(52.545701, 13.462876),
        new Coordinates(52.545723, 13.462979),
        new Coordinates(52.545817, 13.463413),
        new Coordinates(52.545908, 13.463833),
        new Coordinates(52.545994, 13.464231),
        new Coordinates(52.546081, 13.464628),
        new Coordinates(52.546196, 13.465162),
        new Coordinates(52.546321, 13.465737),
        new Coordinates(52.546286, 13.465889),
        new Coordinates(52.546286, 13.466617),
        new Coordinates(52.546329, 13.467014),
        new Coordinates(52.546372, 13.467412),
        new Coordinates(52.546384, 13.467477),
        new Coordinates(52.546347, 13.467492),
        new Coordinates(52.546345, 13.467571),
        new Coordinates(52.546329, 13.46764),
        new Coordinates(52.546279, 13.467696),
        new Coordinates(52.546292, 13.467784),
        new Coordinates(52.546337, 13.468096),
        new Coordinates(52.546355, 13.468227),
        new Coordinates(52.546415, 13.468239),
        new Coordinates(52.546454, 13.468275),
        new Coordinates(52.546495, 13.468357),
        new Coordinates(52.546544, 13.468335),
        new Coordinates(52.546633, 13.468771),
        new Coordinates(52.546723, 13.469258),
        new Coordinates(52.54678, 13.46955),
        new Coordinates(52.546859, 13.469954),
        new Coordinates(52.54694, 13.470359),
        new Coordinates(52.54706, 13.470809),
        new Coordinates(52.547236, 13.471318),
        new Coordinates(52.547397, 13.471718),
        new Coordinates(52.547572, 13.472075),
        new Coordinates(52.547695, 13.472309),
        new Coordinates(52.547777, 13.472447),
        new Coordinates(52.547709, 13.47255),
        new Coordinates(52.547645, 13.472683),
        new Coordinates(52.547635, 13.473237),
        new Coordinates(52.547629, 13.473522),
        new Coordinates(52.547625, 13.473735),
        new Coordinates(52.547614, 13.474397),
        new Coordinates(52.547607, 13.474702),
        new Coordinates(52.547606, 13.474784),
        new Coordinates(52.547598, 13.475168),
        new Coordinates(52.547588, 13.475699),
        new Coordinates(52.547578, 13.476229),
        new Coordinates(52.547568, 13.476759),
        new Coordinates(52.547559, 13.477172),
        new Coordinates(52.547551, 13.477585),
        new Coordinates(52.547545, 13.477985),
        new Coordinates(52.547539, 13.478385),
        new Coordinates(52.547536, 13.4787),
        new Coordinates(52.547508, 13.478808),
        new Coordinates(52.547505, 13.479272),
        new Coordinates(52.547426, 13.47961),
        new Coordinates(52.547312, 13.479998),
        new Coordinates(52.547179, 13.480535),
        new Coordinates(52.547038, 13.481062),
        new Coordinates(52.546904, 13.481533),
        new Coordinates(52.546862, 13.481686),
        new Coordinates(52.546851, 13.481724),
        new Coordinates(52.546781, 13.481983),
        new Coordinates(52.546748, 13.482102),
        new Coordinates(52.546628, 13.482549),
        new Coordinates(52.546871, 13.483042),
        new Coordinates(52.547115, 13.483536),
        new Coordinates(52.547358, 13.484029),
        new Coordinates(52.547184, 13.484661),
        new Coordinates(52.54701, 13.485292),
        new Coordinates(52.546858, 13.485853),
        new Coordinates(52.546706, 13.486415),
        new Coordinates(52.546555, 13.486976),
        new Coordinates(52.546403, 13.487538),
        new Coordinates(52.546251, 13.4881),
        new Coordinates(52.546097, 13.488671),
        new Coordinates(52.545942, 13.489243),
        new Coordinates(52.545787, 13.489814),
        new Coordinates(52.545643, 13.490364),
        new Coordinates(52.545499, 13.490914),
        new Coordinates(52.545403, 13.491278),
        new Coordinates(52.545247, 13.491856),
        new Coordinates(52.545091, 13.492434),
        new Coordinates(52.544935, 13.493012),
        new Coordinates(52.544831, 13.493395),
        new Coordinates(52.544735, 13.493701),
        new Coordinates(52.544882, 13.493981),
        new Coordinates(52.545029, 13.494262),
        new Coordinates(52.545205, 13.494605),
        new Coordinates(52.545381, 13.494947),
        new Coordinates(52.545599, 13.495381),
        new Coordinates(52.545817, 13.495816),
        new Coordinates(52.546034, 13.496232),
        new Coordinates(52.54625, 13.496648),
        new Coordinates(52.546384, 13.496898),
        new Coordinates(52.546459, 13.497042),
        new Coordinates(52.546507, 13.497136),
        new Coordinates(52.5466, 13.497309),
        new Coordinates(52.546888, 13.497848),
        new Coordinates(52.546956, 13.497983),
        new Coordinates(52.546962, 13.497995),
        new Coordinates(52.547052, 13.498172),
        new Coordinates(52.547254, 13.49857),
        new Coordinates(52.547285, 13.498631),
        new Coordinates(52.547363, 13.498775),
        new Coordinates(52.547486, 13.499003),
        new Coordinates(52.547615, 13.499244),
        new Coordinates(52.547741, 13.499478),
        new Coordinates(52.547758, 13.499509),
        new Coordinates(52.54782, 13.499625),
        new Coordinates(52.547743, 13.499737),
        new Coordinates(52.547714, 13.499779),
        new Coordinates(52.547685, 13.49982),
        new Coordinates(52.547542, 13.500028),
        new Coordinates(52.547373, 13.500272),
        new Coordinates(52.547205, 13.500517),
        new Coordinates(52.547117, 13.50064),
        new Coordinates(52.547046, 13.500754),
        new Coordinates(52.54699, 13.500868),
        new Coordinates(52.546927, 13.501028),
        new Coordinates(52.546859, 13.501269),
        new Coordinates(52.546765, 13.501775),
        new Coordinates(52.546701, 13.502196),
        new Coordinates(52.546637, 13.502616),
        new Coordinates(52.546551, 13.503178),
        new Coordinates(52.546536, 13.503329),
        new Coordinates(52.546522, 13.503573),
        new Coordinates(52.546509, 13.50382),
        new Coordinates(52.546501, 13.503962),
        new Coordinates(52.546491, 13.504184),
        new Coordinates(52.546474, 13.504669),
        new Coordinates(52.546468, 13.504842),
        new Coordinates(52.546458, 13.505106),
        new Coordinates(52.546433, 13.50582),
        new Coordinates(52.546427, 13.505982),
        new Coordinates(52.54641, 13.506474),
        new Coordinates(52.546393, 13.506967),
        new Coordinates(52.546376, 13.50746),
        new Coordinates(52.546373, 13.507605),
        new Coordinates(52.546369, 13.50787),
        new Coordinates(52.546612, 13.50794),
        new Coordinates(52.546662, 13.507951),
        new Coordinates(52.546944, 13.507997),
        new Coordinates(52.547226, 13.508044),
        new Coordinates(52.547273, 13.508066),
        new Coordinates(52.547327, 13.508127),
        new Coordinates(52.547396, 13.508039),
        new Coordinates(52.547407, 13.507738),
        new Coordinates(52.547409, 13.507702),
    };

    public override string ToString()
    {
        return "Map";
    }
}