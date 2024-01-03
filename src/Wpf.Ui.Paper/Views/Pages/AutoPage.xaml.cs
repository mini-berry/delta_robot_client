// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using System.Windows;

namespace Wpf.Ui.Paper.Views.Pages;

/// <summary>
/// Interaction logic for AutoPage..xaml
/// </summary>
public partial class AutoPage
{
    public AutoPage()
    {
        DataContext = this;
        InitializeComponent();
        Loaded += (_, _) => NotConnectDialog();
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

    private void AutoButton_Click(object sender, RoutedEventArgs e)
    {

    }
}
