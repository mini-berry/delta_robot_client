// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using System;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using System.Windows.Threading;
using Wpf.Ui.Controls;
using Wpf.Ui.Paper.Views.Pages;

namespace Wpf.Ui.Paper;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    private bool _isPaneOpenedOrClosedFromCode;
    private bool _isUserClosedPane;

    public bool IsConnected { get; set; }

    public TcpClient? TcpClient { get; set; }

    public XyzType XyzData { get; set; }

    public double? TcpPort { get; set; }

    public string? TcpIP { get; set; }

    public MainWindow()
    {
        DataContext = this;

        InitializeComponent();

        Appearance.SystemThemeWatcher.Watch(this);
        Loaded += (_, _) => RootNavigation.Navigate(typeof(HomePage));
        Closing += (_, _) => TcpDispose();
        IsConnected = false;

        XyzData = new XyzType();

        var timer = new DispatcherTimer();
        timer.Interval = TimeSpan.FromSeconds(1.0);
        timer.Tick += new EventHandler(Timer_Tick);
        timer.Start();
    }

    private void Timer_Tick(object? sender, EventArgs e)
    {
        if (TcpClient != null && IsConnected == true)
        {
            NetworkStream stream = TcpClient.GetStream();
            if (stream.DataAvailable)
            {
                var buffer = new byte[256];
                var bytesRead = stream.Read(buffer, 0, buffer.Length);
                var receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                var values = receivedData.TrimEnd(';').Split(',');
                if (values.Length == 6)
                {
                    XyzData.SetData(values);
                }
            }
        }
    }

    private void MainWindow_OnSizeChanged(object sender, SizeChangedEventArgs e)
    {
        if (_isUserClosedPane)
        {
            return;
        }

        _isPaneOpenedOrClosedFromCode = true;
        RootNavigation.SetCurrentValue(Controls.NavigationView.IsPaneOpenProperty, !(e.NewSize.Width <= 1200));
        _isPaneOpenedOrClosedFromCode = false;
    }

    private void NavigationView_OnPaneOpened(NavigationView sender, RoutedEventArgs args)
    {
        if (_isPaneOpenedOrClosedFromCode)
        {
            return;
        }

        _isUserClosedPane = false;
    }

    private void NavigationView_OnPaneClosed(NavigationView sender, RoutedEventArgs args)
    {
        if (_isPaneOpenedOrClosedFromCode)
        {
            return;
        }

        _isUserClosedPane = true;
    }

    public bool ConnectTo(string serverIp, double serverPort)
    {
        if(TcpClient != null && IsConnected)
        {
            TcpClient.Dispose();
            IsConnected = false;
        }

        try
        {
            TcpClient = new TcpClient();

            // 连接到服务器
            if (!TcpClient.ConnectAsync(serverIp, (int)serverPort).Wait(1000))
            {
                return false;
            }

            if (TcpClient.Connected && TcpClient.GetStream().CanRead)
            {
                IsConnected = true;
            }
            else
            {
                return false;
            }

            var messageToSend = "C;";
            var dataToSend = Encoding.UTF8.GetBytes(messageToSend);
            TcpClient.GetStream().Write(dataToSend, 0, dataToSend.Length);
            return true;
        }
        catch (Exception)
        {
            IsConnected = false;
            return false;
        }
    }

    public void SendString(string message)
    {
        if (TcpClient != null && TcpClient.Connected && TcpClient.GetStream().CanWrite)
        {
            var dataToSend = Encoding.UTF8.GetBytes(message);
            TcpClient.GetStream().Write(dataToSend, 0, dataToSend.Length);
        }
        else
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

    public void TcpDispose()
    {
        TcpClient?.Dispose();
    }
}

