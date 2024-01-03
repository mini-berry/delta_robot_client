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

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    private bool _isPaneOpenedOrClosedFromCode;
    private bool _isUserClosedPane;
    public bool _isConnected=false;
    public TcpClient tcpClient;
    public XyzType XyzData;

    public double? TcpPort { get; set; }

    public string? TcpIP { get; set; }

    public MainWindow()
    {
        DataContext = this;

        InitializeComponent();

        Appearance.SystemThemeWatcher.Watch(this);
        Loaded += (_, _) => RootNavigation.Navigate(typeof(HomePage));
        _isConnected = false;

        XyzData = new XyzType();

        var timer = new DispatcherTimer();
        timer.Interval = TimeSpan.FromSeconds(1.0);
        timer.Tick += new EventHandler(Timer_Tick);
        timer.Start();
    }

    private void Timer_Tick(object sender, EventArgs e)
    {
        if (_isConnected == true)
        {
            NetworkStream stream = tcpClient.GetStream();
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

    public bool ConnectTo(string serverIp, double? serverPort)
    {
        if(_isConnected)
        {
            tcpClient.Dispose();
            _isConnected = false;
        }

        try
        {
            tcpClient = new TcpClient();

            // 连接到服务器
            if (!tcpClient.ConnectAsync(serverIp, (int)serverPort).Wait(1000))
            {
                return false;
            }

            if (tcpClient.Connected && tcpClient.GetStream().CanRead)
            {
                _isConnected = true;
            }
            else
            {
                return false;
            }

            var messageToSend = "Hello";
            var dataToSend = Encoding.UTF8.GetBytes(messageToSend);
            tcpClient.GetStream().Write(dataToSend, 0, dataToSend.Length);
            return true;
        }
        catch (Exception)
        {
            _isConnected = false;
            return false;
        }
    }
}

