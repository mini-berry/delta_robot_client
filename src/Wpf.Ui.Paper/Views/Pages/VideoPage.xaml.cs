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
using System.Windows;

namespace Wpf.Ui.Paper.Views.Pages;

/// <summary>
/// Interaction logic for DataView.xaml
/// </summary>
public partial class VideoPage : IDisposable
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
        Loaded += (_, _) => NotConnectDialog();
    }

    public void NotConnectDialog()
    {
        var currentWindow = (MainWindow)Window.GetWindow(this);
        if (currentWindow.IsConnected == false)
        {
            var uiMessageBox = new Wpf.Ui.Controls.MessageBox
            {
                Title = "请先连接",
                Content = "需要连接到客户端。",
                CloseButtonText = "确认",
            };
            _ = uiMessageBox.ShowDialogAsync();
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            // 释放托管资源
        }

        _mediaPlayer.Dispose();
        _libVLC.Dispose();
    }
}