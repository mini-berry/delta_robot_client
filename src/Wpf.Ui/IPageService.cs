// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using Wpf.Ui.Controls;

namespace Wpf.Ui;

/// <summary>
/// Represents a contract with the service that provides the pages for <see cref="INavigationView"/>.
/// </summary>
public interface IPageService
{
    /// <summary>
    /// Takes a page of the given type.
    /// </summary>
    /// <typeparam name="T">Page type.</typeparam>
    /// <returns>Instance of the registered page service.</returns>
    public T? GetPage<T>()
        where T : class;

    /// <summary>
    /// Takes a page of the given type.
    /// </summary>
    /// <param name="pageType">Page type.</param>
    /// <returns>Instance of the registered page service.</returns>
    public FrameworkElement? GetPage(Type pageType);
}
