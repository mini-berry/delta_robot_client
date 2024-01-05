// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using System;
using System.Security.Cryptography.X509Certificates;
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

    private void Timer_Tick(object? sender, EventArgs e)
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

    public void ChangeText(XyzType xyzData)
    {
        xTextBox.SetCurrentValue(System.Windows.Controls.TextBox.TextProperty, xyzData.XPos.ToString());
        yTextBox.SetCurrentValue(System.Windows.Controls.TextBox.TextProperty, xyzData.YPos.ToString());
        zTextBox.SetCurrentValue(System.Windows.Controls.TextBox.TextProperty, xyzData.ZPos.ToString());
        aTextBox.SetCurrentValue(System.Windows.Controls.TextBox.TextProperty, xyzData.XAngel.ToString());
        bTextBox.SetCurrentValue(System.Windows.Controls.TextBox.TextProperty, xyzData.YAngel.ToString());
        cTextBox.SetCurrentValue(System.Windows.Controls.TextBox.TextProperty, xyzData.ZAngel.ToString());
    }

    public void AngelButton_Click(object sender, RoutedEventArgs e)
    {
        var currentWindow = (MainWindow)Window.GetWindow(this);
        if (!String.IsNullOrEmpty(aMTextBox.Text) &&
            !String.IsNullOrEmpty(bMTextBox.Text) &&
            !String.IsNullOrEmpty(cMTextBox.Text) &&
            Double.TryParse(aMTextBox.Text, out currentWindow.XyzData.XAngel) &&
            Double.TryParse(bMTextBox.Text, out currentWindow.XyzData.YAngel) &&
            Double.TryParse(cMTextBox.Text, out currentWindow.XyzData.ZAngel) &&
            currentWindow != null
            )
        {
            currentWindow.SendString("A" + currentWindow.XyzData.XAngel.ToString() + ","
                + currentWindow.XyzData.YAngel.ToString() + "," +
                currentWindow.XyzData.ZAngel.ToString() + ";");
        }
        else
        {
            var uiMessageBox = new Wpf.Ui.Controls.MessageBox
            {
                Title = "数据无效",
                Content = "请检查数据有效性。",
                CloseButtonText = "确认",
            };
            _ = uiMessageBox.ShowDialogAsync();
        }
    }

    public void PosButton_Click(object sender, RoutedEventArgs e)
    {
        var currentWindow = (MainWindow)Window.GetWindow(this);
        if (!String.IsNullOrEmpty(xMTextBox.Text) &&
            !String.IsNullOrEmpty(yMTextBox.Text) &&
            !String.IsNullOrEmpty(zMTextBox.Text) &&
            Double.TryParse(xMTextBox.Text, out currentWindow.XyzData.XPos) &&
            Double.TryParse(yMTextBox.Text, out currentWindow.XyzData.YPos) &&
            Double.TryParse(zMTextBox.Text, out currentWindow.XyzData.ZPos) &&
            currentWindow != null
            )
        {
            currentWindow.SendString("P" + currentWindow.XyzData.XPos.ToString() + ","
                + currentWindow.XyzData.YPos.ToString() + "," +
                currentWindow.XyzData.ZPos.ToString() + ";");
        }
        else
        {
            var uiMessageBox = new Wpf.Ui.Controls.MessageBox
            {
                Title = "数据无效",
                Content = "请检查数据有效性。",
                CloseButtonText = "确认",
            };
            _ = uiMessageBox.ShowDialogAsync();
        }
    }
}
