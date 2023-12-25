// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using System.Windows;

namespace Wpf.Ui.Demo.Simple.Views.Pages;

/// <summary>
/// Interaction logic for AutoPage..xaml
/// </summary>
public partial class HomePage
{

    public HomePage()
    {
        DataContext = this;

        InitializeComponent();

        if (Appearance.ApplicationThemeManager.GetAppTheme() == Wpf.Ui.Appearance.ApplicationTheme.Dark)
        {
            Appearance.ApplicationThemeManager.Apply(Wpf.Ui.Appearance.ApplicationTheme.Dark);
        }
        else
        {
            Appearance.ApplicationThemeManager.Apply(Wpf.Ui.Appearance. ApplicationTheme.Light);
        }

    }

    private void ConnectButoon_Click(object sender, RoutedEventArgs e)
    {
        connectButoon.SetCurrentValue(System.Windows.Controls.ContentControl.ContentProperty, "连接中");
    }
    private void ConnectTo()
    { 
    string ipAddr=ipAddr.ToCharArray;
    }
}
