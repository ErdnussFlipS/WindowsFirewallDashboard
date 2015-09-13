﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFirewallDashboard.ViewModel
{
	public interface IViewModel : INotifyPropertyChanged { }
	public interface IViewModel<TModel> : IViewModel
	{
		[Browsable(false)]
		[Bindable(false)]
		TModel Model { get; set; }
	}
}
