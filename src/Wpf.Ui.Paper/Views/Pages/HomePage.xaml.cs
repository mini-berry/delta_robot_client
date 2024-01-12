// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Wpf.Ui.Paper.Views.Pages;

/// <summary>
/// Interaction logic for AutoPage..xaml
/// </summary>
public partial class HomePage
{
    private readonly string ipAddressFormartRegex = @"^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$";
    private readonly ImageSource cameraMode_ImageSource =
new BitmapImage(new Uri("pack://application:,,,/Assets/Camera.png"));

    private readonly ImageSource autoMode_ImageSource =
new BitmapImage(new Uri("pack://application:,,,/Assets/Auto.png"));

    private readonly ImageSource manualMode_ImageSource =
new BitmapImage(new Uri("pack://application:,,,/Assets/Hand.png"));

    private readonly ImageSource cameraMode_ImageSource2 =
new BitmapImage(new Uri("pack://application:,,,/Assets/Camera2.png"));

    private readonly ImageSource autoMode_ImageSource2 =
new BitmapImage(new Uri("pack://application:,,,/Assets/Auto2.png"));

    private readonly ImageSource manualMode_ImageSource2 =
new BitmapImage(new Uri("pack://application:,,,/Assets/Hand2.png"));

    private readonly List<string> _suggestionList;
    private MainWindow currentWindow;

    public HomePage()
    {
        _suggestionList =
            [
                "127.0.0.1"
            ];
        _suggestionList.Insert(0, Properties.Settings.Default.IP);
        DataContext = this;
        InitializeComponent();
        currentWindow = (MainWindow)Window.GetWindow(this);
        ipAddr.OriginalItemsSource = _suggestionList;
        ipAddr.Text = Properties.Settings.Default.IP;
        portNum.Value = Properties.Settings.Default.portNum;

        if (Appearance.ApplicationThemeManager.GetAppTheme() == Wpf.Ui.Appearance.ApplicationTheme.Dark)
        {
            Appearance.ApplicationThemeManager.Apply(Wpf.Ui.Appearance.ApplicationTheme.Dark);
        }
        else
        {
            Appearance.ApplicationThemeManager.Apply(Wpf.Ui.Appearance.ApplicationTheme.Light);
        }
    }

    private void ConnectButoon_Click(object sender, RoutedEventArgs e)
    {
        if (!String.IsNullOrEmpty(ipAddr.Text))
        {
            if (portNum.Value != null)
            {
                Properties.Settings.Default.portNum = (double)portNum.Value;
            }

            Properties.Settings.Default.IP = ipAddr.Text;
            Properties.Settings.Default.Save();
        }

        currentWindow = (MainWindow)Window.GetWindow(this);
        if (currentWindow.TcpClient != null && currentWindow.IsConnected)
        {
            currentWindow.TcpClient.Dispose();
            currentWindow.IsConnected = false;
            connectButoon.SetCurrentValue(System.Windows.Controls.ContentControl.ContentProperty, "连接");
            connectButoon.SetCurrentValue(System.Windows.Controls.Control.BackgroundProperty, FindResource("AccentButtonBackground"));
            CameraMode_IMG.SetCurrentValue(System.Windows.Controls.Image.SourceProperty, cameraMode_ImageSource2);
            AutoMode_IMG.SetCurrentValue(System.Windows.Controls.Image.SourceProperty, autoMode_ImageSource2);
            ManualMode_IMG.SetCurrentValue(System.Windows.Controls.Image.SourceProperty, manualMode_ImageSource2);
        }
        else
        {
            if (!String.IsNullOrEmpty(ipAddr.Text) && IpValidate(ipAddr.Text))
            {
                if (!_suggestionList.Contains(ipAddr.Text))
                {
                    _suggestionList.Insert(0, ipAddr.Text);
                }

                connectButoon.SetCurrentValue(System.Windows.Controls.ContentControl.ContentProperty, "连接中");

                if (portNum.Value != null && currentWindow.ConnectTo(ipAddr.Text, (double)portNum.Value))
                {
                    connectButoon.SetCurrentValue(System.Windows.Controls.ContentControl.ContentProperty, "断开");
                    connectButoon.SetCurrentValue(System.Windows.Controls.Control.BackgroundProperty, new SolidColorBrush(Colors.Green));
                    CameraMode_IMG.SetCurrentValue(System.Windows.Controls.Image.SourceProperty, cameraMode_ImageSource);
                    AutoMode_IMG.SetCurrentValue(System.Windows.Controls.Image.SourceProperty, autoMode_ImageSource);
                    ManualMode_IMG.SetCurrentValue(System.Windows.Controls.Image.SourceProperty, manualMode_ImageSource);
                }
                else
                {
                    connectButoon.SetCurrentValue(System.Windows.Controls.ContentControl.ContentProperty, "连接");
                    var uiMessageBox = new Wpf.Ui.Controls.MessageBox
                    {
                        Title = "连接失败",
                        Content = "请检查IP地址和端口信息。",
                        CloseButtonText = "确认",
                    };
                    _ = uiMessageBox.ShowDialogAsync();
                }
            }
            else
            {
                connectButoon.SetCurrentValue(System.Windows.Controls.ContentControl.ContentProperty, "无效");
                connectButoon.SetCurrentValue(System.Windows.Controls.Control.BackgroundProperty, new SolidColorBrush(Colors.Red));
            }
        }
    }

    private bool IpValidate(string ipAddr)
    {
        // 检查输入的字符串是否符合IP地址格式
        if (!Regex.IsMatch(ipAddr, ipAddressFormartRegex))
        {
            return false;
        }

        return true;
    }

    private void AutoMode_Click(object sender, RoutedEventArgs e)
    {
        currentWindow = (MainWindow)Window.GetWindow(this);
        if (currentWindow.IsConnected)
        {
            _ = currentWindow.RootNavigation.Navigate(typeof(AutoPage));
        }
    }

    private void ManualMode_Click(object sender, RoutedEventArgs e)
    {
        currentWindow = (MainWindow)Window.GetWindow(this);
        if (currentWindow.IsConnected)
        {
            _ = currentWindow.RootNavigation.Navigate(typeof(ManualPage));
        }
    }

    private void CameraMode_Click(object sender, RoutedEventArgs e)
    {
        currentWindow = (MainWindow)Window.GetWindow(this);
        if (currentWindow.IsConnected)
        {
            _ = currentWindow.RootNavigation.Navigate(typeof(VideoPage));
        }
    }
}
