﻿// <copyright file="TestingFrameworkDriver.cs" company="Automate The Planet Ltd.">
// Copyright 2016 Automate The Planet Ltd.
// Licensed under the Apache License, Version 2.0 (the "License");
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// <author>Anton Angelov</author>
// <site>http://automatetheplanet.com/</site>

using HybridTestFramework.UITests.Core;
using HybridTestFramework.UITests.Core.Utilities.ExceptionsAnalysis.ChainOfResponsibility;
using Microsoft.Practices.Unity;
using ArtOfTest.WebAii.Core;

namespace HybridTestFramework.UITests.TestingFramework.Engine
{
    public partial class TestingFrameworkDriver : IDriver
    {
        private readonly Browser originalBrowser;
        private readonly ElementFinderService elementFinderService;
        ////private readonly ExceptionAnalizedElementFinderService elementFinderService;
        private Manager driver;
        private IUnityContainer container;
        private BrowserSettings browserSettings;
        private Browser currentActiveBrowser;

        public TestingFrameworkDriver(
            IUnityContainer container,
            BrowserSettings browserSettings)
        {
            this.container = container;
            this.browserSettings = browserSettings;
            this.InitializeManager(browserSettings);
            this.LaunchNewBrowser();
            this.originalBrowser = this.driver.ActiveBrowser;
            this.currentActiveBrowser = this.driver.ActiveBrowser;
            this.elementFinderService = new ElementFinderService(container);
        }

        # region 9. Failed Tests Аnalysis- Chain of Responsibility Design Pattern
        ////public TestingFrameworkDriver(
        ////    IUnityContainer container,
        ////    BrowserSettings browserSettings,
        ////    IExceptionAnalyzer excepionAnalyzer)
        ////{
        ////    this.container = container;
        ////    this.browserSettings = browserSettings;
        ////    this.InitializeManager(browserSettings);
        ////    this.LaunchNewBrowser();
        ////    this.originalBrowser = this.driver.ActiveBrowser;
        ////    this.currentActiveBrowser = this.driver.ActiveBrowser;
        ////    this.elementFinderService = new ExceptionAnalizedElementFinderService(container, excepionAnalyzer);
        ////}
        #endregion
        private void InitializeManager(BrowserSettings browserSettings)
        {
            if (Manager.Current == null)
            {
                Settings localSettings = new Settings();
                localSettings.Web.KillBrowserProcessOnClose = true;
                localSettings.Web.RecycleBrowser = true;
                localSettings.DisableDialogMonitoring = true;
                localSettings.UnexpectedDialogAction =
                    UnexpectedDialogAction.DoNotHandle;
                localSettings.ElementWaitTimeout =
                    browserSettings.ElementsWaitTimeout;
                this.ResolveBrowser(
                    localSettings,
                    browserSettings.Type);
                if (localSettings.Web.DefaultBrowser != BrowserType.NotSet)
                {
                    this.driver = new Manager(localSettings);
                    this.driver.Start();
                }
            }
        }

        private void ResolveBrowser(
            Settings localSettings,
            Browsers executionBrowser)
        {
            switch (executionBrowser)
            {
                case Browsers.NotSet:
                case Browsers.InternetExplorer:
                    localSettings.Web.ExecutingBrowsers.Add(
                        BrowserExecutionType.InternetExplorer);
                    localSettings.Web.Browser =
                        BrowserExecutionType.InternetExplorer;
                    localSettings.Web.DefaultBrowser =
                        BrowserType.InternetExplorer;
                    break;
                case Browsers.Safari:
                    localSettings.Web.ExecutingBrowsers.Add(
                        BrowserExecutionType.Safari);
                    localSettings.Web.Browser =
                        BrowserExecutionType.Safari;
                    localSettings.Web.DefaultBrowser =
                        BrowserType.Safari;
                    break;
                case Browsers.Chrome:
                    localSettings.Web.ExecutingBrowsers.Add(
                        BrowserExecutionType.Chrome);
                    localSettings.Web.Browser =
                        BrowserExecutionType.Chrome;
                    localSettings.Web.DefaultBrowser =
                        BrowserType.Chrome;
                    break;
                case Browsers.Firefox:
                    localSettings.Web.ExecutingBrowsers.Add(
                        BrowserExecutionType.FireFox);
                    localSettings.Web.Browser =
                        BrowserExecutionType.FireFox;
                    localSettings.Web.DefaultBrowser =
                        BrowserType.FireFox;
                    break;
                case Browsers.NoBrowser:
                    localSettings.Web.ExecutingBrowsers.Clear();
                    localSettings.Web.Browser =
                        BrowserExecutionType.NotSet;
                    localSettings.Web.DefaultBrowser =
                        BrowserType.NotSet;
                    break;
                default:
                    localSettings.Web.ExecutingBrowsers.Add(
                        BrowserExecutionType.InternetExplorer);
                    localSettings.Web.Browser =
                        BrowserExecutionType.InternetExplorer;
                    localSettings.Web.DefaultBrowser =
                        BrowserType.InternetExplorer;
                    break;
            }
        }
    }
}