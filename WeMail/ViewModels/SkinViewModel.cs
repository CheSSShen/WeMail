﻿using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using MaterialDesignColors.ColorManipulation;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WeMail.ViewModels
{
    public class SkinViewModel:BindableBase
    {
		private bool isDarkTheme;

		public bool IsDarkTheme
		{
			get { return isDarkTheme; }
			set {
				if(SetProperty(ref isDarkTheme, value))
				{
					ModifyTheme(theme => theme.SetBaseTheme(value?Theme.Dark : Theme.Light));
				}
				
			}
		}

		public IEnumerable<ISwatch> Swatches { get; } = SwatchHelper.Swatches;
		public DelegateCommand<object> ChangeHueCommand {  get;private set; }
		private readonly PaletteHelper paletteHelper = new PaletteHelper();	
		public SkinViewModel()
		{
			ChangeHueCommand = new DelegateCommand<object>(ChangeHue);
		}
		private static void ModifyTheme(Action<ITheme> modificationAction)
		{
			var paletteHelper = new PaletteHelper();
			ITheme theme = paletteHelper.GetTheme();
			modificationAction?.Invoke(theme);
			paletteHelper.SetTheme(theme);
		}
		private void ChangeHue(object obj)
		{
			var hue = (Color)obj;
			ITheme theme = paletteHelper.GetTheme();
			theme.PrimaryDark = new ColorPair(hue.Darken());
			theme.PrimaryLight = new ColorPair(hue.Lighten()) ;
            theme.PrimaryMid = new ColorPair(hue);

			paletteHelper.SetTheme(theme);
        }
	}
}
