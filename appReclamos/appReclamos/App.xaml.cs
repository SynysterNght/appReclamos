﻿using appReclamos.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace appReclamos
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage (new InicioPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
