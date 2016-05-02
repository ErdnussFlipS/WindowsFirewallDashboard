﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Data;
using WindowsAdvancedFirewallApi.Events.Objects;
using WindowsAdvancedFirewallApi.Library;
using WindowsAdvancedFirewallApi.Utils;

namespace WindowsAdvancedFirewallApi.Events.Arguments
{
	public class FirewallRuleAddedEventArgs : FirewallRuleBaseEventArgs
	{
		public FirewallRuleAddedEventArgs(EventLogEntry @event) : base(@event)
		{
			SetAttributes();
		}
		protected void SetAttributes()
		{
			SetAttributes(2, 21, 22);
			SetRuleAttributes(0, 1);

			Rule.ApplicationPath = FirewallLogEvent.ReplacementStrings[3];
			Rule.ServiceName = FirewallLogEvent.ReplacementStrings[4];
			Rule.Direction = FirewallLogEvent.ReplacementStrings[5].ParseInteger(int.MinValue).ToFirewallDirection();
			Rule.Protocol = FirewallLogEvent.ReplacementStrings[6].ParseInteger().ToFirewallProtocol();
			Rule.LocalPorts = FirewallLogEvent.ReplacementStrings[7].ToFirewallPorts();
			Rule.RemotePorts = FirewallLogEvent.ReplacementStrings[8].ToFirewallPorts();
			Rule.Action = FirewallLogEvent.ReplacementStrings[9].ParseInteger(int.MinValue).ToFirewallAction();
			Rule.Profiles = new ComparableList<FirewallProfileType>(FirewallLogEvent.ReplacementStrings[10].ParseInteger(int.MinValue).ToFirewallProfileTypes());
			Rule.LocalAddresses = FirewallLogEvent.ReplacementStrings[11].ToFirewallAddresses();
			Rule.RemoteAddresses = FirewallLogEvent.ReplacementStrings[12];
			Rule.RemoteMachineAuthorizationList = FirewallLogEvent.ReplacementStrings[13];
			Rule.RemoteUserAuthorizationList = FirewallLogEvent.ReplacementStrings[14];
			Rule.EmbeddedContext = FirewallLogEvent.ReplacementStrings[15];
			Rule.Flags = FirewallLogEvent.ReplacementStrings[16].ParseInteger();
			Rule.Active = Convert.ToBoolean(FirewallLogEvent.ReplacementStrings[17].ParseInteger(0));
			Rule.EdgeTraversal = FirewallLogEvent.ReplacementStrings[18].ParseInteger(0).ToFirewallEdgeTraversal();
			Rule.LooseSourceMapped = FirewallLogEvent.ReplacementStrings[19].ParseInteger(0);
			Rule.SecurityOptions = FirewallLogEvent.ReplacementStrings[20].ParseInteger(0);
			Rule.SchemaVersion = FirewallLogEvent.ReplacementStrings[23].ParseInteger(0);
			Rule.Status = FirewallLogEvent.ReplacementStrings[24].ParseLong(0);
			Rule.LocalOnlyMapped = FirewallLogEvent.ReplacementStrings[25].ParseInteger(0);
		}
	}
}
