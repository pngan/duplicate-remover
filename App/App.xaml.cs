﻿using System.Windows;

using Autofac;
using Model;
using ViewModel;
using View;

namespace DuplicateRemover
{
    public partial class App : Application
    {
        public void ApplicationStartup(object sender, StartupEventArgs e)
        {
            var builder = new ContainerBuilder();
            RegisterDependencies(builder);
            var container = builder.Build();

            StartApplication(container);
        }

        private void RegisterDependencies(ContainerBuilder builder)
        {

            builder.RegisterType<MessageViewModel>().As<IMessageViewModel>().SingleInstance();
            builder.RegisterType<MessageDialog>();

            builder.RegisterType<MainModel>().As<IMainModel>().SingleInstance();
            builder.RegisterType<MainViewModel>().As<IMainViewModel>().SingleInstance();
            builder.RegisterType<MainWindow>();
        }

        private static void StartApplication(IContainer container)
        {
            var window = container.Resolve<MainWindow>();
            window.Show();
        }
    }
}