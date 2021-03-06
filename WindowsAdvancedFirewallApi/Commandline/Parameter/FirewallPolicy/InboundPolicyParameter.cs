﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAdvancedFirewallApi.Commandline.Parameter.FirewallPolicy
{
	public class InboundPolicyParameter : PolicyParameter<InboundPolicyParameter>
	{
		public static readonly InboundPolicyParameter BlockInbound = new InboundPolicyParameter { Value = "blockinbound" };
		public static readonly InboundPolicyParameter BlockInboundAlways = new InboundPolicyParameter { Value = "blockinboundalways" };
		public static readonly InboundPolicyParameter AllowInbound = new InboundPolicyParameter { Value = "allowinbound" };

		public static readonly InboundPolicyParameter Default = BlockInbound;
	}
}
