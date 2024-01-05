// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using System;

namespace Wpf.Ui.Paper;

public class XyzType
{
    public double XAngel;
    public double YAngel;
    public double ZAngel;
    public double XPos;
    public double YPos;
    public double ZPos;

    public void SetData(string[] values)
    {
        _ = Double.TryParse(values[3], out XAngel);
        _ = Double.TryParse(values[4], out YAngel);
        _ = Double.TryParse(values[5], out ZAngel);
        _ = Double.TryParse(values[0], out XPos);
        _ = Double.TryParse(values[1], out YPos);
        _ = Double.TryParse(values[2], out ZPos);
    }
}

