﻿using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Data;
using WindowsAdvancedFirewallApi.Events.Objects;
using WindowsAdvancedFirewallApi.Utils;

namespace WindowsAdvancedFirewallApi.Events.Arguments
{
	public class FirewallProfileSettingEventArgs : FirewallSettingEventArgs<FirewallProfileSetting>
	{
		internal FirewallProfileSettingEventArgs(EventLogEntry @event) : base(@event)
		{
			SetAttributes();
		}

		protected void SetAttributes()
		{
			SetAttributes(5, 6, 7);
			SetSettingAttributes(1, 3, 2, 4);

			Setting.Profiles = FirewallLogEvent.ReplacementStrings[0].ParseInteger(int.MinValue).ToFirewallProfileTypes();
		}
	}
}