// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using System;
using System.Windows;
using System.Windows.Threading;

namespace Wpf.Ui.Paper.Views.Pages;

/// <summary>
/// Interaction logic for DashboardPage.xaml
/// </summary>
public partial class ManualPage
{
    public ManualPage()
    {
        DataContext = this;
        InitializeComponent();
        Loaded += (_, _) => NotConnectDialog();

        var timer = new DispatcherTimer();
        timer.Interval = TimeSpan.FromSeconds(0.3);
        timer.Tick += new EventHandler(Timer_Tick);
        timer.Start();
    }

    private void Timer_Tick(object sender, EventArgs e)
    {
        var currentWindow = (MainWindow)Window.GetWindow(this);
        if (currentWindow != null && currentWindow.XyzData != null)
        {
            ChangeText(currentWindow.XyzData);
        }
    }

    public void NotConnectDialog()
    {
        var currentWindow = (MainWindow)Window.GetWindow(this);
        if (currentWindow._isConnected == false)
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

    public void ChangeText(XyzType xyzData)
    {
        xTextBox.SetCurrentValue(System.Windows.Controls.TextBox.TextProperty, xyzData.xPos.ToString());
        yTextBox.SetCurrentValue(System.Windows.Controls.TextBox.TextProperty, xyzData.yPos.ToString());
        zTextBox.SetCurrentValue(System.Windows.Controls.TextBox.TextProperty, xyzData.zPos.ToString());
        aTextBox.SetCurrentValue(System.Windows.Controls.TextBox.TextProperty, xyzData.xAngel.ToString());
        bTextBox.SetCurrentValue(System.Windows.Controls.TextBox.TextProperty, xyzData.yAngel.ToString());
        cTextBox.SetCurrentValue(System.Windows.Controls.TextBox.TextProperty, xyzData.zAngel.ToString());
    }
}
