//---------------------------------------------------------------------------
//
// <copyright file="MisFotosEnFlickrDetailPage.xaml.cs" company="Microsoft">
//    Copyright (C) 2015 by Microsoft Corporation.  All rights reserved.
// </copyright>
//
// <createdOn>10/27/2016 5:41:27 PM</createdOn>
//
//---------------------------------------------------------------------------

using System;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using AppStudio.DataProviders.Flickr;
using Unidad5Actividad7.Sections;
using Unidad5Actividad7.Navigation;
using Unidad5Actividad7.ViewModels;
using AppStudio.Uwp;

namespace Unidad5Actividad7.Pages
{
    public sealed partial class MisFotosEnFlickrDetailPage : Page
    {
        private DataTransferManager _dataTransferManager;

        public MisFotosEnFlickrDetailPage()
        {
            ViewModel = ViewModelFactory.NewDetail(new MisFotosEnFlickrSection());
            this.InitializeComponent();
			commandBar.DataContext = ViewModel;
        }

        public DetailViewModel ViewModel { get; set; }        

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            await ViewModel.LoadStateAsync(e.Parameter as NavDetailParameter);

            _dataTransferManager = DataTransferManager.GetForCurrentView();
            _dataTransferManager.DataRequested += OnDataRequested;
            ShellPage.Current.SupportFullScreen = true;

            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _dataTransferManager.DataRequested -= OnDataRequested;
            ShellPage.Current.SupportFullScreen = false;

            base.OnNavigatedFrom(e);
        }

        private void OnDataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            ViewModel.ShareContent(args.Request);
        }
    }
}
