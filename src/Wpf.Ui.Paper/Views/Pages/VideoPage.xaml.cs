// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using LibVLCSharp.Platforms.Windows;
using LibVLCSharp.Shared;
using LibVLCSharp.WPF;
using System;
using System.Collections.ObjectModel;
using Wpf.Ui.Paper.Models;

namespace Wpf.Ui.Paper.Views.Pages;

/// <summary>
/// Interaction logic for DataView.xaml
/// </summary>
public partial class VideoPage
{
    private readonly LibVLC _libVLC;
    private readonly LibVLCSharp.Shared.MediaPlayer _mediaPlayer;

    public VideoPage()
    {
        InitializeComponent();
        Core.Initialize();
        _libVLC = new LibVLC();
        _mediaPlayer = new MediaPlayer(_libVLC);
        videoStream.MediaPlayer = _mediaPlayer;

        // 设置视频文件路径
        var media = new Media(_libVLC, new Uri("D:/v.mp4"));
        _ = _mediaPlayer.Play(media);
    }
}