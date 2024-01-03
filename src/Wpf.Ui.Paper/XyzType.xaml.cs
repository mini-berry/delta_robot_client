// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using SharpDX.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Threading;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using Windows.Media.Protection.PlayReady;
using Wpf.Ui.Controls;
using Wpf.Ui.Paper.Views.Pages;
using System.Threading;
using System.Windows.Markup;
using System.Xml.Linq;

namespace Wpf.Ui.Paper;

public class XyzType
{
    public double xAngel;
    public double yAngel;
    public double zAngel;
    public double xPos;
    public double yPos;
    public double zPos;

    public void SetData(string[] values)
    {
        _ = Double.TryParse(values[3], out xAngel);
        _ = Double.TryParse(values[4], out yAngel);
        _ = Double.TryParse(values[5], out zAngel);
        _ = Double.TryParse(values[0], out xPos);
        _ = Double.TryParse(values[1], out yPos);
        _ = Double.TryParse(values[2], out zPos);
    }
}

