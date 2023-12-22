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
    private int _counter = 0;

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

        CounterTextBlock.SetCurrentValue(System.Windows.Controls.TextBlock.TextProperty, _counter.ToString());
    }

    private void OnBaseButtonClick(object sender, RoutedEventArgs e)
    {
        CounterTextBlock.SetCurrentValue(
            System.Windows.Controls.TextBlock.TextProperty,
            (++_counter).ToString()
        );
    }
}
